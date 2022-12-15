using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class LockerSelection : Form
	{
		private MemberRoster member;
		private string badge;
		private SortableBindingList<JoinedListItem> sblLockersCurrent;
		private SortableBindingList<JoinedListItem> sblLockersAll;
		private WoodclubEntities context;
		public LockerSelection(string badge)
		{
			this.badge = badge;
			InitializeComponent();
			this.dataGridViewSelectedLockers.DefaultCellStyle.SelectionBackColor = Color.LightCyan;
			this.dataGridViewSelectedLockers.DefaultCellStyle.SelectionForeColor = Color.Black;
			this.dataGridViewSelectedLockers.CellContentClick += this.dataGridViewSelectedLockers_CellContentClick;
			this.dataGridViewAllLockers.DefaultCellStyle.SelectionBackColor = Color.LightCyan;
			this.dataGridViewAllLockers.DefaultCellStyle.SelectionForeColor = Color.Black;
			this.dataGridViewAllLockers.CellContentClick += this.dataGridViewSelectedLockers_CellContentClick;

			context = new WoodclubEntities();
			member = (from m in context.MemberRosters
						where m.Badge == badge
						select m).FirstOrDefault();
		}

		private void dataGridViewSelectedLockers_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = (DataGridView)sender;
			string senderName = dataGridView.Name;
			string buttonText = string.Empty;
			bool isCheckBoxCell = false;

			if (e.ColumnIndex == 0 && e.RowIndex >= 0)
			{
				dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
				buttonText = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
				isCheckBoxCell = true;
			}

			if (isCheckBoxCell)
			{
				if (buttonText == "Add")
				{
					if (senderName == "dataGridViewAllLockers")
					{
						DataGridViewRow row = this.dataGridViewAllLockers.Rows[e.RowIndex];
						JoinedListItem jli = new JoinedListItem
						{
							Selected = "Remove",
							Badge = this.member.Badge,
							FirstName = this.member.FirstName,
							LastName = this.member.LastName,
							Locker = row.Cells["LockerAll"].Value.ToString(),
							Location = row.Cells["WhereAll"].Value.ToString()
						};

						sblLockersCurrent.Add(jli);
						sblLockersAll.Remove(sblLockersAll[e.RowIndex]);
						this.dataGridViewAllLockers.EndEdit();
						this.dataGridViewAllLockers.Refresh();

					}
					else
					{
						//Shouldn't happen
					}
				}
				else
				{
					if (senderName == "dataGridViewSelectedLockers")
					{
						DataGridViewRow row = this.dataGridViewSelectedLockers.Rows[e.RowIndex];
					}
					else
					{
						//Shouldn't happen
					}
				} 
			}
		}

		private void LockerSelection_Load(object sender, EventArgs e)
		{
			var currentLockers = context.Lockers
				.Join(
					context.MemberRosters,
					locker => locker.Badge,
					member => member.Badge,
					(locker, member) => new { locker, member })
				.Join(
					context.LockerLocations,
					lockerMember => lockerMember.locker.LocationCode,
					lockerLocation => lockerLocation.Location,
					(lockerMember, lockerLocation) => new
					{
						Badge = lockerMember.member.Badge,
						FirstName = lockerMember.member.FirstName,
						LastName = lockerMember.member.LastName,
						Locker = lockerMember.locker.LockerTitle,
						Location = lockerLocation.Description
					})
				.Where(m => m.Badge == badge).ToList();

			List<JoinedListItem> joinedListCurrent = new List<JoinedListItem>();
			foreach (var locker in currentLockers)
			{
				joinedListCurrent.Add(new JoinedListItem
				{
					Selected = "Remove",
					Badge = locker.Badge,
					FirstName = locker.FirstName,
					LastName = locker.LastName,
					Locker = locker.Locker,
					Location = locker.Location
				});
			}
			sblLockersCurrent = new SortableBindingList<JoinedListItem>(joinedListCurrent);
			bs_SelectedLockers.DataSource = sblLockersCurrent;

			var allLockers = context.Lockers
				.Join(
					context.MemberRosters,
					locker => locker.Badge,
					member => member.Badge,
					(locker, member) => new { locker, member })
				.Join(
					context.LockerLocations,
					lockerMember => lockerMember.locker.LocationCode,
					lockerLocation => lockerLocation.Location,
					(lockerMember, lockerLocation) => new
					{
						Badge = lockerMember.member.Badge,
						FirstName = lockerMember.member.FirstName,
						LastName = lockerMember.member.LastName,
						Locker = lockerMember.locker.LockerTitle,
						Location = lockerLocation.Description
					})
				.Where(m => m.Badge != badge).ToList();

			List<JoinedListItem> joinedListAll = new List<JoinedListItem>();
			foreach (var locker in allLockers)
			{
				joinedListAll.Add(new JoinedListItem
				{
					Selected = "Add",
					Badge = locker.Badge,
					FirstName = locker.FirstName,
					LastName = locker.LastName,
					Locker = locker.Locker,
					Location = locker.Location
				}) ;
			}

			var vacantLockers = (from ll in context.Lockers
								 where ll.Badge == String.Empty
								 select ll).ToList();

			foreach (var lockerVacant in vacantLockers)
			{
				var location = (from loc in context.LockerLocations
								where loc.Location == lockerVacant.LocationCode
								select loc).FirstOrDefault();
				joinedListAll.Add(new JoinedListItem
				{
					Selected = "Add",
					Badge = String.Empty,
					FirstName = String.Empty,
					LastName = String.Empty,
					Locker = lockerVacant.LockerTitle,
					Location = location.Description
				});
			}


			sblLockersAll = new SortableBindingList<JoinedListItem>(joinedListAll);
			bs_AllLockers.DataSource = new SortableBindingList<JoinedListItem>(joinedListAll);
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			foreach (JoinedListItem item in sblLockersCurrent)
			{
				Locker l = (from loc in context.Lockers
							where loc.LockerTitle == item.Locker
							select loc).FirstOrDefault();
				l.Badge = item.Badge;
			}

			context.SaveChanges();
			DialogResult = DialogResult.OK;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void buttonApply_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}
	}

	public partial class JoinedListItem
	{
		public string Selected { get; set; }
		public string Badge { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Locker { get; set; }
		public string Location { get; set; }
	}
}

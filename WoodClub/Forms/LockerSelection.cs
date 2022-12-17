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
						JoinedListItem jli = this.sblLockersAll[e.RowIndex];
                        //                  JoinedListItem jli = new JoinedListItem
                        //                  {
                        //                      Selected = "Remove",
                        //                      BadgeOriginal = jli.
                        //	Badge = this.member.Badge,
                        //	FirstName = this.member.FirstName,
                        //	LastName = this.member.LastName,
                        //	Locker = row.Cells["LockerAll"].Value.ToString(),
                        //	Location = row.Cells["WhereAll"].Value.ToString()
                        //};

                        jli.Badge = this.member.Badge;
                        jli.LastName = this.member.LastName;
                        jli.FirstName = this.member.FirstName;
                        jli.Selected = "Remove";

						sblLockersCurrent.Add(jli);
						sblLockersAll.Remove(sblLockersAll[e.RowIndex]);
						this.dataGridViewAllLockers.EndEdit();
						this.dataGridViewAllLockers.Refresh();

					}
					else
					{
                        JoinedListItem jli = sblLockersCurrent[e.RowIndex];
                        jli.Badge = member.Badge;
                        jli.FirstName = member.FirstName;
                        jli.LastName = member.LastName;
                        jli.Selected = "Remove";
                        this.dataGridViewSelectedLockers.EndEdit();
                        this.dataGridViewSelectedLockers.Refresh();
                    }
				}
				else
				{
					if (senderName == "dataGridViewSelectedLockers")
					{
                        JoinedListItem jli = sblLockersCurrent[e.RowIndex];
                        if (jli.Badge == jli.BadgeOriginal)
                        {
                            jli.Badge = string.Empty;
                            jli.FirstName = string.Empty;
                            jli.LastName = string.Empty;
                            jli.Selected = "Add";
                            this.dataGridViewSelectedLockers.EndEdit();
                            this.dataGridViewSelectedLockers.Refresh();
                        }
                        else
                        {
                            jli.Badge = jli.BadgeOriginal;
                            jli.FirstName = jli.FirstNameOriginal;
                            jli.LastName = jli.LastNameOriginal;
                            jli.Selected = "Add";
                            sblLockersAll.Add(jli);
                            sblLockersCurrent.Remove(sblLockersCurrent[e.RowIndex]);
                            this.dataGridViewAllLockers.EndEdit();
                            this.dataGridViewAllLockers.Refresh();
                        }
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
            LoadLockers();
        }

        private void LoadLockers()
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
                    BadgeOriginal = locker.Badge,
                    FirstName = locker.FirstName,
                    FirstNameOriginal = locker.FirstName,
                    LastName = locker.LastName,
                    LastNameOriginal = locker.LastName,
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
                    BadgeOriginal = locker.Badge,
                    FirstName = locker.FirstName,
                    FirstNameOriginal = locker.FirstName,
                    LastName = locker.LastName,
                    LastNameOriginal = locker.LastName,
                    Locker = locker.Locker,
                    Location = locker.Location
                });
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
                    BadgeOriginal = String.Empty,
                    FirstName = String.Empty,
                    FirstNameOriginal = String.Empty,
                    LastName = String.Empty,
                    LastNameOriginal = String.Empty,
                    Locker = lockerVacant.LockerTitle,
                    Location = location.Description
                });
            }


            sblLockersAll = new SortableBindingList<JoinedListItem>(joinedListAll);
            bs_AllLockers.DataSource = new SortableBindingList<JoinedListItem>(joinedListAll);
        }

        private void SaveChanges()
        {
            foreach (JoinedListItem item in sblLockersCurrent)
            {
                Locker l = (from loc in context.Lockers
                            where loc.LockerTitle == item.Locker
                            select loc).FirstOrDefault();
                l.Badge = item.Badge;
            }

            context.SaveChanges();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void buttonApply_Click(object sender, EventArgs e)
		{
            SaveChanges();
            LoadLockers();
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

        public string BadgeOriginal { get; set; }
        public string FirstNameOriginal { get; set; }
        public string LastNameOriginal { get; set; }
    }
}

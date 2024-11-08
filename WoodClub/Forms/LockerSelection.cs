using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Form for editing a member's lockers.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class LockerSelection : Form
	{
		private MemberRoster member;
		private string badge;
		private SortableBindingList<JoinedListItem> sblLockersCurrent;
		private SortableBindingList<JoinedListItem> sblLockersAll;
		private WoodClubEntities context;

		/// <summary>
		/// Initializes a new instance of the <see cref="LockerSelection"/> class.
		/// </summary>
		/// <param name="badge">The member's badge.</param>
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

			context = new WoodClubEntities();
			member = (from m in context.MemberRosters
					  where m.Badge == badge
					  select m).FirstOrDefault();
		}

		/// <summary>
		/// Handles the CellContentClick event of the dataGridViewSelectedLockers control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
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
				if (buttonText == "Taken")
				{
					return;
				}

				if (buttonText == "Add")
				{
					if (senderName == "dataGridViewAllLockers")
					{
						JoinedListItem jli = new JoinedListItem
						{
							Selected = "Remove",
							BadgeOriginal = this.sblLockersAll[e.RowIndex].BadgeOriginal,
							Badge = this.member.Badge,
							FirstName = this.member.FirstName,
							LastName = this.member.LastName,
							Locker = this.sblLockersAll[e.RowIndex].Locker,
							Location = this.sblLockersAll[e.RowIndex].Location,
							FirstNameOriginal = this.sblLockersAll[e.RowIndex].FirstNameOriginal,
							LastNameOriginal = this.sblLockersAll[e.RowIndex].LastNameOriginal
						};

						sblLockersCurrent.Add(jli);
						sblLockersAll.Remove(sblLockersAll[e.RowIndex]);
						bs_AllLockers.DataSource = sblLockersAll;
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
							bs_SelectedLockers.DataSource = sblLockersCurrent;
						}
					}
					else
					{
						//Shouldn't happen
					}
				}
			}
		}

		/// <summary>
		/// Handles the Load event of the LockerSelection control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void LockerSelection_Load(object sender, EventArgs e)
		{
			LoadLockers();
		}

		/// <summary>
		/// Loads the lockers for a members badge.
		/// </summary>
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
									Location = lockerLocation.Description,
								})
							.Where(m => m.Badge == badge)
							.OrderBy(l => l.Locker).ToList();

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
						Location = lockerLocation.Description,
						Project = lockerMember.locker.Project
					})
				.Where(m => m.Badge != badge)
				.OrderBy(l => l.Locker).ToList();

			List<JoinedListItem> joinedListAll = new List<JoinedListItem>();
			foreach (var locker in allLockers)
			{
				joinedListAll.Add(new JoinedListItem
				{
					Selected = string.IsNullOrEmpty(locker.Project) ? "Add" : "Taken",
					Badge = locker.Badge,
					BadgeOriginal = locker.Badge,
					FirstName = locker.FirstName,
					FirstNameOriginal = locker.FirstName,
					LastName = locker.LastName,
					LastNameOriginal = locker.LastName,
					Locker = locker.Locker,
					Location = locker.Location,
					Project = locker.Project
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
					BadgeOriginal = String.Empty,
					FirstName = String.Empty,
					FirstNameOriginal = String.Empty,
					LastName = String.Empty,
					LastNameOriginal = String.Empty,
					Locker = lockerVacant.LockerTitle,
					Location = location.Description,
					Project = String.Empty
				});
			}


			sblLockersAll = new SortableBindingList<JoinedListItem>(joinedListAll);
			bs_AllLockers.DataSource = new SortableBindingList<JoinedListItem>(joinedListAll);
		}

		/// <summary>
		/// Saves the changes.
		/// </summary>
		private void SaveChanges()
		{
			foreach (JoinedListItem item in sblLockersCurrent)
			{
				Locker locker = (from loc in context.Lockers
								 where loc.LockerTitle == item.Locker
								 select loc).FirstOrDefault();
				locker.Badge = item.Badge;
				context.SaveChanges();

				if (!string.IsNullOrEmpty(item.BadgeOriginal) && item.Badge != item.BadgeOriginal)
				{
					List<Locker> lockers = (from l in this.context.Lockers
											where l.Badge == item.BadgeOriginal
											select l).ToList();
					MemberRoster oldMember = (from m in this.context.MemberRosters
											  where m.Badge == item.BadgeOriginal
											  select m).FirstOrDefault();
					if (lockers.Count == 0)
					{
						oldMember.Locker = "";
					}
					else
					{
						bool first = true;
						foreach (Locker lkr in lockers)
						{
							if (first)
							{
								oldMember.Locker = lkr.LockerTitle.Trim();
								first = false;
							}
							else
							{
								oldMember.Locker += "," + lkr.LockerTitle;
							}
						}
					}
				}
			}

			context.SaveChanges();
		}

		/// <summary>
		/// Handles the Click event of the Save button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveChanges();
			DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// Handles the Click event of the Cancel button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		/// <summary>
		/// Handles the Click event of the Apply button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void buttonApply_Click(object sender, EventArgs e)
		{
			SaveChanges();
			LoadLockers();
		}

		/// <summary>
		/// Private class used to hold the data returned by a
		/// LEFT JOIN on the database.
		/// </summary>
		private partial class JoinedListItem
		{
			/// <summary>
			/// Gets or sets the selected.
			/// </summary>
			/// <value>
			/// The selected.
			/// </value>
			public string Selected { get; set; }

			/// <summary>
			/// Gets or sets the badge.
			/// </summary>
			/// <value>
			/// The badge.
			/// </value>
			public string Badge { get; set; }

			/// <summary>
			/// Gets or sets the first name.
			/// </summary>
			/// <value>
			/// The first name.
			/// </value>
			public string FirstName { get; set; }

			/// <summary>
			/// Gets or sets the last name.
			/// </summary>
			/// <value>
			/// The last name.
			/// </value>
			public string LastName { get; set; }

			/// <summary>
			/// Gets or sets the locker.
			/// </summary>
			/// <value>
			/// The locker.
			/// </value>
			public string Locker { get; set; }

			/// <summary>
			/// Gets or sets the location.
			/// </summary>
			/// <value>
			/// The location.
			/// </value>
			public string Location { get; set; }

			/// <summary>
			/// Gets or sets the locker project.
			/// </summary>
			/// <value>
			/// The project or none.
			/// </value>
			public string Project { get; set; }

			/// <summary>
			/// Gets or sets the badge original.
			/// </summary>
			/// <value>
			/// The badge original.
			/// </value>
			public string BadgeOriginal { get; set; }

			/// <summary>
			/// Gets or sets the first name original.
			/// </summary>
			/// <value>
			/// The first name original.
			/// </value>
			public string FirstNameOriginal { get; set; }

			/// <summary>
			/// Gets or sets the last name original.
			/// </summary>
			/// <value>
			/// The last name original.
			/// </value>
			public string LastNameOriginal { get; set; }
		}
	}
}

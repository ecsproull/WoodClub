using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoodClub.Forms;

namespace WoodClub
{
	/// <summary>
	/// The home screen view. Displays a list of all member and allows
	/// the user to pick a member to view and edit their data.
	/// Also includes the main ToolBar to facilitate many other functions
	/// including reports, permissions, lockers, adding credits to multiple
	/// members, filtering the view and sending a member an email.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class MainMembers : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public static bool lockersUpdated = false;

		private readonly int listenPort = 5725;
		private readonly UdpClient udpClient = new UdpClient();

		private SortableBindingList<MemberRosterExt> blMembers;
		private SortableBindingList<MemberRosterExt> filteredBindingList = new SortableBindingList<MemberRosterExt>();
		private readonly BindingSource bsMembers;
		private MemberRoster currentMember;
		public bool update;
		public bool added;
		private bool filteredBoxEdit = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="MainMembers"/> class.
		/// </summary>
		public MainMembers()
		{
			this.blMembers = new SortableBindingList<MemberRosterExt>();
			this.bsMembers = new BindingSource();
			this.update = false;
			this.added = false;
			InitializeComponent();
			MessageIn();
		}

		/// <summary>
		/// Receives messages via UDP. When an update in made in the WoodClub application
		/// that requires an update to the card controllers a message is sent to the 
		/// ZkAccessIO application to update the controllers. After the update is done
		/// ZkAccessIO sends a message back saying it is done. That message is received here.
		/// </summary>
		private void MessageIn()
		{
			udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, listenPort));
			var from = new IPEndPoint(0, 0);

			Task.Run(() =>
			{
				while (true)
				{
					var recvBuffer = udpClient.Receive(ref from);
					string MsgIn = Encoding.ASCII.GetString(recvBuffer);
					//need to use Invoke because the new thread can't access the UI elements directly
					MethodInvoker mi = delegate () { MessageBox.Show(MsgIn); };
					Invoke(mi);
				}
			});
		}

		/// <summary>
		/// Handles the Load event of the MainMembers control.
		/// Several event handlers are assigned here.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void MainMembers_Load(object sender, EventArgs e)
		{
			log.Info("Starting application");
			try
			{
				LoadMembers();
			}
			catch (Exception ex)
			{
				log.Error("Members failed ", ex);
			}

			log.Info("members loaded");


			bsMembers.Position = 0;
			bindingNavigator1.BindingSource = bsMembers;
			toolStripTextBoxFilter.KeyUp += TextBoxName_KeyUp;
			badgeEntryTextBox.KeyUp += BadgeEntryTextbox_KeyUp; ;
			toolStripImportNewMembers.Click += ToolStripImportNewMembers_Click;
			MultipleMembersButton.Click += MultipleMembersButton_Click;
            menuItemFreeDayOnly.Click += MenuItemFreeDayOnly_Click;
		}


		/// <summary>
		/// Handles the KeyUp event of the BadgeEntryTextbox control. When a user
		/// starts to enter a badge number the filtered TextBox is ignored.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
		private void BadgeEntryTextbox_KeyUp(object sender, KeyEventArgs e)
		{
			filteredBoxEdit = false;
		}

		/// <summary>
		/// Handles the Click event of the MenuItemFreeDayOnly control. This button is located
		/// under the Reports ToolBar item and filters member that have a Free Day available.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void MenuItemFreeDayOnly_Click(object sender, EventArgs e)
        {
			FreeDayOnlyUsers fdo = new FreeDayOnlyUsers();
			fdo.Show();
        }

		/// <summary>
		/// Handles the Click event of the MultipleMembersButton located on the ToolBar.
		/// This form allow the user to enter the same credit to several members at one time.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void MultipleMembersButton_Click(object sender, EventArgs e)
		{
			MultipleEditor me = new MultipleEditor();
			me.ShowDialog();
			bsMembers.ResetCurrentItem();
			LoadMembers();
		}

		/// <summary>
		/// Loads the members. Also useful to reload the list after making changes.
		/// </summary>
		private void LoadMembers()
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				var members = (from m in context.MemberRosters
							   select m).ToList();

				int year = DateTime.Now.Year;
				List<MemberRosterExt> mre = new List<MemberRosterExt>();
				foreach (MemberRoster member in members)
				{
					var yearvisit = from t in context.Transactions              // List of Usage by member
									where t.TransDate.Value.Year == year
										 && t.Code == "U" | t.Code == "FD"
										 && t.Badge == member.Badge
									select t.TransDate.Value;
					int visitsCnt = yearvisit.DistinctBy(x => x.DayOfYear).Count();
					mre.Add(new MemberRosterExt(member, visitsCnt));
				}

				blMembers = new SortableBindingList<MemberRosterExt>(mre);  // blMembers list of members
				setBsMembersDataSource();
				dataGridView1.DataSource = bsMembers;
			}
		}

		/// <summary>
		/// Handles the Click event of the ToolStripImportNewMembers control. Under the Members ToolBar item
		/// the Import Orientation item invokes this event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ToolStripImportNewMembers_Click(object sender, EventArgs e)
		{
			NewMembers fnm = new NewMembers();
			fnm.ShowDialog();
			LoadMembers();
		}

		/// <summary>
		/// Handles the KeyUp event of the TextBoxName control. This is the filter control handler.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
		private void TextBoxName_KeyUp(object sender, KeyEventArgs e)
		{
			filteredBoxEdit = true;
			setBsMembersDataSource();

			if (e.KeyData == Keys.Enter && filteredBindingList.Count == 1)
			{
				MemberEditor editor = new MemberEditor(filteredBindingList[0].Badge);
				editor.ShowDialog();

				setTextBoxFocus();
			}
		}

		/// <summary>
		/// Sets the filtered text box focus.
		/// </summary>
		private void setTextBoxFocus()
		{
			if (filteredBoxEdit)
			{
				if (toolStripTextBoxFilter.Control.Text.Length > 0)
				{
					ActiveControl = toolStripTextBoxFilter.Control;
					toolStripTextBoxFilter.SelectionStart = 0;
					toolStripTextBoxFilter.SelectionLength = toolStripTextBoxFilter.Control.Text.Length;
				}
			}
			else
			{
				if (badgeEntryTextBox.Control.Text.Length > 0)
				{
					ActiveControl = badgeEntryTextBox.Control;
					badgeEntryTextBox.SelectionStart = 0;
					badgeEntryTextBox.SelectionLength = badgeEntryTextBox.Control.Text.Length;
				}
			}
		}

		/// <summary>
		/// Sets or clears the binding source members data source. When entering text
		/// into the filter box, this is where the updating of the member list takes place.
		/// </summary>
		private void setBsMembersDataSource()
		{
			string filter = toolStripTextBoxFilter.Text;
			if (filter == string.Empty)
			{
				bsMembers.DataSource = blMembers;
			}
			else
			{
				filteredBindingList = new SortableBindingList<MemberRosterExt>(blMembers.Where(
					x => x.FirstName.ToUpper().Contains(filter.ToUpper()) ||
					x.LastName.ToUpper().Contains(filter.ToUpper()) ||
					x.Badge.Contains(filter) ||
					x.Email.ToLower().Contains(filter.ToLower())).ToList());
				bsMembers.DataSource = filteredBindingList;
				dataGridView1.Refresh();
			}
		}

		/// <summary>
		/// Use the form level variable currentCustomer set in the
		/// BindingSource PositionChanged event to keep the item current
		/// when sorting via the DataGridView column headers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridView1_Sorted(object sender, EventArgs e)
		{
			if (bsMembers.CurrentRowIsValid())
			{
				bsMembers.Position = bsMembers.IndexOf(currentMember);
			}
		}

		/// <summary>
		/// Get the current member displayed in the DataGridView and
		/// assign it to a private variable for use in the Sorted event of
		/// the DataGridView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bsMembers_PositionChanged(object sender, EventArgs e)
		{
			if (bsMembers.CurrentRowIsValid())
			{
				currentMember = ((MemberRoster)bsMembers.Current);
			}
			else
			{
				currentMember = null;
			}
		}

		/// <summary>
		/// Handles the CellMouseDoubleClick event of the dataGridView1 control. This opens
		/// the member that has been double clicked.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
		private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			EditCurrentRow();
		}

		/// <summary>
		/// Deletes the selected item. The click is from the garbage can in the tool bar.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void DeleteMemberItemClick(object sender, EventArgs e)
		{
			int id;
			var current = bsMembers.Current as MemberRoster;
			if (current == null)
			{
				MessageBox.Show("No member selected.");
				return;
			}

			id = current.id;
			bsMembers.EndEdit();

			using (WoodClubEntities context = new WoodClubEntities())
			{
				try
				{
					// re-load the member from the database to get the latest Locker value
					var memberInDb = context.MemberRosters.SingleOrDefault(m => m.id == id);
					if (memberInDb == null)
					{
						MessageBox.Show("Member not found in database.");
						return;
					}

					// Prevent deletion if Locker is not empty
					if (!string.IsNullOrWhiteSpace(memberInDb.Locker))
					{
						MessageBox.Show($"Cannot delete member. Locker '{memberInDb.Locker}' is assigned. Release the locker before deleting.");
						return;
					}

					DialogResult dialogResult = MessageBox.Show("Confirm Delete?", "Item", MessageBoxButtons.YesNo);
					if (dialogResult != DialogResult.Yes)
					{
						return;
					}

					// safe to delete
					context.MemberRosters.Remove(memberInDb);
					context.SaveChanges();

					// update UI after successful DB delete
					bsMembers.RemoveCurrent();
					bsMembers.ResetCurrentItem();
					LoadMembers();

						// Delete succeeded - send notification email asynchronously
					Task.Run(async () =>
					{
						try
						{
							SendMail sm = new SendMail();
							string message = $"Member {memberInDb.FirstName} {memberInDb.LastName} (Badge: {memberInDb.Badge}) has been deleted from the Shop database.";
							List<EmailAddress> recpts = new List<EmailAddress>
							{
								new EmailAddress("treasurer@scwwoodshop.com", "Woodclub Treasurer"),
								new EmailAddress("ecsproull765@gmail.com", "Woodclub Admin")
							};
							await sm.SendMailAsync("Member Deleted from Shop DB", message, recpts);
							log.Info($"Deletion notification email sent for badge {memberInDb.Badge}");
						}
						catch (Exception ex)
						{
							log.Error($"Failed to send deletion notification email for badge {memberInDb.Badge}", ex);
							// Don't show MessageBox here - we're on a background thread
						}
					});
				}
				catch (Exception ex)
				{
					log.Error("Delete failed..", ex);
					MessageBox.Show("Delete failed: " + ex.Message);
				}
			}
		}

		/// <summary>
		/// Edits the current row which represents a member. Opens the member editor.
		/// </summary>
		private void EditCurrentRow()
		{
			MemberRoster roster = blMembers.FirstOrDefault(mem => mem.id == bsMembers.MemberIdentifier());
			MemberEditor frm = new MemberEditor(roster.Badge);
			frm.StartPosition = FormStartPosition.CenterScreen;
			try
			{
				if (frm.ShowDialog() == DialogResult.OK)        // Changes made - need to refresh from SQL
				{
					bsMembers.ResetCurrentItem();
					LoadMembers();
				}
			}
			finally
			{
				frm.Dispose();
			}

			setTextBoxFocus();
		}

		/// <summary>
		/// Handles the FormClosed event of the MainMembers form.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
		private void MainMembers_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		/// <summary>
		/// Handles the Click event of the monitorsToolStripMenuItem control. This the Monitors item
		/// under the Reports ToolBar item.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void monitorsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MonitorForm mf = new MonitorForm();
			mf.ShowDialog();
		}

		/// <summary>
		/// Handles the Click event of the monthlyClubUsageToolStripMenuItem control.
		/// Monthly club usage is under the Reports ToolBar item.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void monthlyClubUsageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShopUsage uf = new ShopUsage();
			uf.ShowDialog();
		}

		/// <summary>
		/// Handles the Click event of the summaryReportToolStripMenuItem control.
		/// This is the Summary item under the Lockers ToolBar item.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void summaryReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lockerSummaryToolStripMenuItem_Click(sender, e);
		}

		/// <summary>
		/// Handles the Click event of the lockerSummaryToolStripMenuItem control.
		/// Opens the locker summary from the Reports ToolBar item.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void lockerSummaryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LockerRpt lr = new LockerRpt();
			lr.ShowDialog();
			if (lockersUpdated)
			{
				lockersUpdated = false;
				LoadMembers();
			}
		}

		/// <summary>
		/// Handles the Click event of the dailySummaryToolStripMenuItem control.
		/// This shows the daily summary of shop usage.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void dailySummaryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormDaily fd = new FormDaily();
			fd.ShowDialog();
		}

		/// <summary>
		/// Handles the KeyDown event of the badge entry TextBox control. 
		/// TODO: look at removing the keyup handler.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
		private void badgeEntryTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			filteredBoxEdit = false;
			if (e.KeyCode == Keys.Enter)
			{
				Find_Badge();
			}
		}

		/// <summary>
		/// Finds the badge in the member list and opens the MemberEditor form for this badge.
		/// </summary>
		private void Find_Badge()
		{
			string badge = badgeEntryTextBox.Text;
			var result = blMembers.SingleOrDefault(b => b.Badge == badge);
			if (result != null)
			{
				int pos = result.id;
				int nx = 0;
				foreach (MemberRoster row in bsMembers)
				{
					if (row.id == pos)
					{
						if (bsMembers.Position == nx)
						{
							EditCurrentRow();
						}
						else
						{
							bsMembers.Position = nx;
						}
						break;
					}
					nx++;
				}
			}
			else
			{
				MessageBox.Show("Badge not found...");
			}

		}

		/// <summary>
		/// Handles the Click event of the lockerLocationsToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void lockerLocationsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LockerLocations lockerLocations = new LockerLocations();
			lockerLocations.ShowDialog();
		}

		/// <summary>
		/// Handles the Click event of the editLockersToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void editLockersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LockerData ld = new LockerData();
			ld.ShowDialog();
		}

		/// <summary>
		/// Handles the Click event of the lockerCostsToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void lockerCostsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LockerPrices lockerPrices = new LockerPrices();
			lockerPrices.ShowDialog();
		}

		/// <summary>
		/// Handles the Click event of the addMemberToolStripButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void addMemberToolStripButton_Click(object sender, EventArgs e)
		{
			MemberEditor frm = new MemberEditor(string.Empty);
			try
			{
				if (frm.ShowDialog() == DialogResult.OK)        // Changes made - need to refresh from SQL
				{
					bsMembers.ResetCurrentItem();
					LoadMembers();
				}
			}
			finally
			{
				frm.Dispose();
			}
		}

		/// <summary>
		/// Handles the event of the clubTracksToolStripMenuItem_Click control.
		/// Creates a HeadTracks report in CSV form that is compatible with the 
		/// recreation center's upload tool.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void clubTracksToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				List<MemberRoster> members = (from m in context.MemberRosters.OrderBy(x => x.LastName).ThenBy(x => x.FirstName)
											  where m.ClubDuesPaid == true
											  select m).ToList();

				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(string.Format("{0},{1},{2}", "Member Id", "Last Name", "First Name"));
				foreach (MemberRoster m in members)
				{
					int badgeInt = int.Parse(m.Badge);
					if (badgeInt < 10000)
					{
						stringBuilder.AppendLine(string.Format("{0},{1},{2}", m.RecCard, m.LastName, m.FirstName));
					}
				}

				string subPath = @"c:\ClubTracks";
				bool exists = System.IO.Directory.Exists(subPath);
				if (!exists)
				{
					System.IO.Directory.CreateDirectory(subPath);
				}

				string filename = @"c:\ClubTracks\members_" + DateTime.Now.ToString("MM-dd-yyy_HH_mm_ss") + ".csv";
				System.IO.File.WriteAllText(filename, stringBuilder.ToString());

				if (File.Exists(filename))
				{
					Process.Start("explorer.exe", Path.GetDirectoryName(filename));
				}
				else
				{
					MessageBox.Show("Opps, make sure this path is accessible: " + Path.GetDirectoryName(filename));
				}
			}

		}

		/// <summary>
		/// Handles the Click event of the restoreOldToolStripMenuItem control.
		/// Restores a member from the backup database table.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void restoreOldToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RestoreOldMember rso = new RestoreOldMember();
			var x = rso.ShowDialog();
			if (x == DialogResult.OK)
			{
				MainMembers_Load(null, null);
			}
		}

		/// <summary>
		/// Handles the Click event of the refreshToolStripButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void refreshToolStripButton_Click(object sender, EventArgs e)
		{
			MainMembers_Load(null, null);
		}

		/// <summary>
		/// Handles the Click event of the exportAllToolStripMenuItem control.
		/// Export All is under the Badges ToolBar menu item.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void exportAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				List<MemberRoster> allmembers = (from m in context.MemberRosters
												 where m.ClubDuesPaid == true
												 select m).ToList();
				List<MemberRFcard> members = new List<MemberRFcard>();
				foreach (MemberRoster m in allmembers)
				{
					members.Add(new MemberRFcard
					{
						FirstName = m.FirstName,
						LastName = m.LastName,
						Badge = m.Badge,
						Title = m.Title,
						Photo = m.Photo,
						RecCard = m.RecCard
					});
				}

				RFBadge.Export(members);
			}
		}

		/// <summary>
		/// Handles the Click event of the selectedToolStripMenuItem control.
		/// Selected is under the Badges ToolBar menu item.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void selectedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RFBadge frfb = new RFBadge();
			frfb.ShowDialog();
			LoadMembers();
		}

		/// <summary>
		/// Handles the Click event of the editMachinePermissions control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void editMachinePermissions_Click(object sender, EventArgs e)
		{
			var perms = new EditMachinePermissions();
			perms.ShowDialog();
		}

		/// <summary>
		/// Handles the Click event of the importPhotosToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void importPhotosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PhotoImport photoImport = new PhotoImport();
			photoImport.ShowDialog();
		}

		/// <summary>
		/// Handles the Click event of the updateDuesToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void updateDuesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateDuesPaid scwf = new UpdateDuesPaid();
			scwf.ShowDialog();
			DialogResult result = scwf.DialogResult;
			if (result == DialogResult.OK)
			{
				LoadMembers();
			}
		}

		/// <summary>
		/// Handles the Click event of the mailToToollStripButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void mailToToollStripButton_Click(object sender, EventArgs e)
		{
			string recipientEmail = string.Empty;
			foreach (DataGridViewRow r in dataGridView1.SelectedRows)
			{
				recipientEmail += r.Cells[5].Value.ToString() + ";";
			}

			string subject = " ";
			string body = " ";

			//// Construct the mailto URI
			string mailtoUri = $"mailto:{recipientEmail}?subject={subject}&body={body}";

			//// Launch the default mail app
			Process.Start(mailtoUri);
		}

		/// <summary>
		/// Handles the Click event of the updateWebsiteToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void updateWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PostToGoDaddy postToGoDaddy = new PostToGoDaddy();
			postToGoDaddy.PostMembersToGoDaddy();
		}

		/// <summary>
		/// Handles the Click event of the printRosterToolStripMenuItem control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void printRosterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PrintContacts pc = new PrintContacts();
			pc.ShowDialog();
		}

		private void syncToQBToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SyncToQB syncToQB = new SyncToQB();
			syncToQB.ShowDialog();
		}

		private void compareToQBToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CompareDbs compareDbs = new CompareDbs();
			compareDbs.ShowDialog();
		}

		private void reportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PermissionsReport pr = new PermissionsReport();
			pr.ShowDialog();
		}

		private void machinePermissionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PermissionsReport pr = new PermissionsReport();
			pr.ShowDialog();
		}

        private void updatePhotosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PostToGoDaddy postToGoDaddy = new PostToGoDaddy();
            postToGoDaddy.PostMemberPhotosMultipart();
        }

		private void setEmailPrefInQBToolStripMenuItem_Click(object sender, EventArgs e)
		{
			QBFunctions qbf = new QBFunctions();
			qbf.connectToQB();

			using (WoodClubEntities context = new WoodClubEntities())
			{
				List<MemberRoster> allmembers = (from m in context.MemberRosters
												 where m.ClubDuesPaid == true && m.Badge != "20001"
												 select m).ToList();
				List<MemberRFcard> members = new List<MemberRFcard>();
				foreach (MemberRoster m in allmembers)
				{
					if (qbf.NeedsPreferredDeliveryMethodUpdate(m.Badge, false))
					{
						qbf.SetCustomerPreferredDeliveryMethod(m.Badge, "Email", false);
					}
				}
			}

			qbf.disconnectFromQB();
		}
	}
}

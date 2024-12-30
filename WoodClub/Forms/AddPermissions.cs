using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Form used for adding member permissions.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class AddPermissions : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private Dictionary<string, string> permissionNames = new Dictionary<string, string>();
		private Dictionary<string, string> approversNames = new Dictionary<string, string>();
		private string singleBadge = string.Empty;

		/// <summary>
		/// Initializes a new instance of the <see cref="AddPermissions"/> class.
		/// </summary>
		/// <param name="badge">The badge.</param>
		public AddPermissions(string badge = null)
		{
			InitializeComponent();
			dataGridMultiMember.AutoGenerateColumns = false;

			if (badge != null)
			{
				singleBadge = badge;
				memberBadgesTextBox.Text = badge;
				memberBadgesTextBox.ReadOnly = true;
			}

			permissionNames.Add("none", "Select");
			permissionNames.Add("cnc", "Cnc");
			permissionNames.Add("laser", "Laser");
			permissionNames.Add("logsaw", "Log Saw");
			permissionNames.Add("lbc", "Lathe Boot Camp");
			permissionNames.Add("p2s_laser", "P2S Laser");
			permissionNames.Add("shp_origin", "Shaper Origin");
			permissionsBindingSource = new BindingSource();
			permissionsBindingSource.DataSource = permissionNames;
			permissionComboBox.DataSource = permissionsBindingSource;
			permissionComboBox.DisplayMember = "Value";
			permissionComboBox.ValueMember = "Key";

			approversNames.Add("none", "Select");
			approversNames.Add("3103", "Gary Roberts");
			approversNames.Add("4982", "Brian Potts");
			approversNames.Add("5329", "Art Lincoln");
			approversNames.Add("4286", "Daryl Coulthart");
			approversNames.Add("4775", "Bill Gentry");
			approversNames.Add("4413", "Jim Casey");
			approverBindingSource = new BindingSource();
			approverBindingSource.DataSource = approversNames;
			approverComboBox.DataSource = approverBindingSource;
			approverComboBox.DisplayMember = "Value";
			approverComboBox.ValueMember = "Key";

			LoadMembers();
		}

		/// <summary>
		/// Handles the Load event of the MultipleEditor control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void MultipleEditor_Load(object sender, EventArgs e)
		{
			memberBadgesTextBox.Leave += MemberBadgesTextBox_Leave;
			memberBadgesTextBox.KeyUp += MemberBadgesTextBox_KeyDown;
		}

		/// <summary>
		/// Handles the KeyDown event of the MemberBadgesTextBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
		private void MemberBadgesTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				LoadMembers();
			}
		}

		/// <summary>
		/// Handles the Leave event of the MemberBadgesTextBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void MemberBadgesTextBox_Leave(object sender, EventArgs e)
		{
			System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
			LoadMembers();
		}

		/// <summary>
		/// Loads the member.
		/// </summary>
		/// <param name="badge">The members badge.</param>
		/// <returns></returns>
		private List<MemberPermissionsItem> LoadMember(string badge)
		{
			List<string> badges = new List<string>(this.memberBadgesTextBox.Text.Split('.'));
			using (WoodClubEntities context = new WoodClubEntities())
			{
				var data = context.MachinePerms
					.Join(
						context.MemberRosters,
						machper => machper.Badge,
						member => member.Badge,
						(machper, member) => new MemberPermissionsItem
						{
							PermissionName = machper.MachineName,
							Badge = member.Badge,
							FirstName = member.FirstName,
							LastName = member.LastName,
							ApprovedBy = machper.ApprovedBy,
							ApprovedDate = machper.ApprovedDate,
							Blocked = machper.Blocked
						})
					.Where(b => b.Badge == badge)
					.OrderBy(x => x.Badge).ToList();

				if (data == null || data.Count == 0)
				{
					List<MemberPermissionsItem> memberPermissionsItems = new List<MemberPermissionsItem>();
					var member = (from mr in context.MemberRosters
								  where mr.Badge == badge
								  select mr).FirstOrDefault();

					if (member != null)
					{
						memberPermissionsItems.Add(new MemberPermissionsItem
						{
							Badge = member.Badge,
							FirstName = member.FirstName,
							LastName = member.LastName,
							PermissionName = "na",
						});
					}

					return memberPermissionsItems;
				}
				else
				{
					return data;
				}
				
			}
		}

		/// <summary>
		/// Loads a list of members. Uses the badge numbers entered into
		/// the MemberBadgesTextBox.
		/// </summary>
		private void LoadMembers()
		{
			List<string> badges = new List<string>(this.memberBadgesTextBox.Text.Split('.'));
			List<MemberPermissionsItem> memberPermissionsItems = new List<MemberPermissionsItem>();

			foreach (string badge in badges)
			{
				var list = LoadMember(badge);
				foreach (var item in list)
				{
					memberPermissionsItems.Add(item);
				}
			}

			dataGridMultiMember.DataSource = new SortableBindingList<MemberPermissionsItem>(memberPermissionsItems);
        }

		/// <summary>
		/// Handles the Click event of the cancelButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		/// <summary>
		/// Handles the Click event of the applyButton control.
		/// Saves the entered data.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void applyButton_Click(object sender, EventArgs e)
		{
			List<string> badges = new List<string>();
			using (WoodClubEntities context = new WoodClubEntities())
			{
				foreach (MemberPermissionsItem pi in
					(SortableBindingList<MemberPermissionsItem>)dataGridMultiMember.DataSource)
				{
					if (pi.Delete)
					{
						var perm = (from mp in context.MachinePerms
									where mp.Badge == pi.Badge && mp.MachineName == pi.PermissionName
									select mp).FirstOrDefault();
						if (perm != null)
						{
							context.MachinePerms.Remove(perm);
							context.SaveChanges();
						}
						continue;
					}
					else
					{
						if (!badges.Contains(pi.Badge))
						{
							badges.Add(pi.Badge);
						}
					}
				}

				if (!string.IsNullOrEmpty(singleBadge))
				{
					AddBadgePermission(context, singleBadge);
				}
				else
				{
					foreach (string badge in badges)
					{
						AddBadgePermission(context, badge);
					}
				}
			}

			LoadMembers();
		}

		private void AddBadgePermission(WoodClubEntities context, string badge)
		{
			string machine = ((KeyValuePair<string, string>)permissionComboBox.SelectedItem).Key;
			if (machine != "none")
			{
				string approver = ((KeyValuePair<string, string>)approverComboBox.SelectedItem).Key;
				if (approver == "none")
				{
					MessageBox.Show("Please select Approver");
				}
				else
				{
					MachinePerm mpi = (from pmi in context.MachinePerms
									   where pmi.Badge == badge && pmi.MachineName == machine
									   select pmi).FirstOrDefault();
					if (mpi == null)
					{
						context.MachinePerms.Add(new MachinePerm
						{
							Badge = badge,
							ApprovedDate = DateTime.Now.Date,
							ApprovedBy = approver,
							MachineName = machine,
						});
					}
				}

				context.SaveChanges();
			}
		}

		/// <summary>
		/// Handles the Click event of the btnSave control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void btnSave_Click(object sender, EventArgs e)
		{
			applyButton_Click(null, null);
			DialogResult = DialogResult.OK;
		}
	}
}

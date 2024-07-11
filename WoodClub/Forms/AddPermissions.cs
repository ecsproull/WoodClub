using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WoodClub
{
	public partial class AddPermissions : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private Dictionary<string, string> permissionNames = new Dictionary<string, string>();
		private Dictionary<string, string> approversNames = new Dictionary<string, string>();
		private string singleBadge = string.Empty;

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

		private void MultipleEditor_Load(object sender, EventArgs e)
		{
			memberBadgesTextBox.Leave += MemberBadgesTextBox_Leave;
			memberBadgesTextBox.KeyUp += MemberBadgesTextBox_KeyDown;
		}

		private void MemberBadgesTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				LoadMembers();
			}
		}

		private void MemberBadgesTextBox_Leave(object sender, EventArgs e)
		{
			System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
			LoadMembers();
		}

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

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void applyButton_Click(object sender, EventArgs e)
		{
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
				}

				if (!string.IsNullOrEmpty(singleBadge))
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
											   where pmi.Badge == singleBadge && pmi.MachineName == machine
											   select pmi).FirstOrDefault();
							if (mpi == null)
							{
								context.MachinePerms.Add(new MachinePerm
								{
									Badge = singleBadge,
									ApprovedDate = DateTime.Now.Date,
									ApprovedBy = approver,
									MachineName = machine,
								});
							}
						}

						context.SaveChanges();
					}
				}
				else
				{
					//check for multiple badges.
				}
			}

			LoadMembers();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			applyButton_Click(null, null);
			DialogResult = DialogResult.OK;
		}
	}
}

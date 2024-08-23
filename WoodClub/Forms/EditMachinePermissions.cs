using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	public partial class EditMachinePermissions : Form
	{
		private List<MemberPermissionsItem> data;
		private string currentBadge = null;
		public EditMachinePermissions(string badge = null)
		{
			InitializeComponent();
			currentBadge = badge;
		}

		private void EditMachinePermissions_Load(object sender, EventArgs e)
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				if (string.IsNullOrEmpty(currentBadge))
				{
					data = context.MachinePerms
					.Join(
						context.MemberRosters,
						machper => machper.Badge,
						member => member.Badge,
						(machper, member) =>  new MemberPermissionsItem
						{
							PermissionName =machper.MachineName,
							Badge = member.Badge,
							FirstName = member.FirstName,
							LastName = member.LastName,
							ApprovedBy = machper.ApprovedBy,
							ApprovedDate = machper.ApprovedDate,
							Blocked = machper.Blocked
						})
					.OrderBy(x => x.Badge).ToList();
				}
				else
				{
					data = context.MachinePerms
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
					.Where(b => b.Badge == currentBadge)
					.OrderBy(x => x.Badge).ToList();
				}
			}

			bindingSource1.DataSource = new SortableBindingList<MemberPermissionsItem>(data);
		}

		private void addPermButton_Click(object sender, EventArgs e)
		{
			AddPermissions ap = new AddPermissions();
			ap.ShowDialog();
		}
	}
}

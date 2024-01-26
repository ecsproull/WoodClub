using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoodClub.Forms
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
						(machper, member) => new { machper, member })
					.Join(
						context.MachineIds,
						idname => idname.machper.MachineId,
						machid => machid.id,
						(idname, machid) => new MemberPermissionsItem
						{
							PermissionName = machid.MachineTypeName,
							Badge = idname.member.Badge,
							FirstName = idname.member.FirstName,
							LastName = idname.member.LastName,
							ApprovedBy = idname.machper.ApprovedBy,
							ApprovedDate = idname.machper.ApprovedDate,
							Blocked = idname.machper.Blocked
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
						(machper, member) => new { machper, member })
					.Join(
						context.MachineIds,
						idname => idname.machper.MachineId,
						machid => machid.id,
						(idname, machid) => new MemberPermissionsItem
						{
							PermissionName = machid.MachineTypeName,
							Badge = idname.member.Badge,
							FirstName = idname.member.FirstName,
							LastName = idname.member.LastName,
							ApprovedBy = idname.machper.ApprovedBy,
							ApprovedDate = idname.machper.ApprovedDate,
							Blocked = idname.machper.Blocked
						})
					.Where(b => b.Badge == currentBadge)
					.OrderBy(x => x.Badge).ToList();
				}
			}

			bindingSource1.DataSource = new SortableBindingList<MemberPermissionsItem>(data);
		}
	}
}

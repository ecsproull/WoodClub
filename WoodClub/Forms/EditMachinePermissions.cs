using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Editor for a members machine permissions.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class EditMachinePermissions : Form
	{
		private List<MemberPermissionsItem> data;
		private string currentBadge = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="EditMachinePermissions"/> class.
		/// </summary>
		/// <param name="badge">The badge.</param>
		public EditMachinePermissions(string badge = null)
		{
			InitializeComponent();
			currentBadge = badge;
		}

		/// <summary>
		/// Handles the Load event of the EditMachinePermissions control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

		/// <summary>
		/// Handles the Click event of the addPermButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void addPermButton_Click(object sender, EventArgs e)
		{
			AddPermissions ap = new AddPermissions();
			ap.ShowDialog();
		}
	}
}

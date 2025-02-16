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
	public partial class PermissionsReport : Form
	{
		public PermissionsReport()
		{
			InitializeComponent();
		}

		private void PermissionsReport_Load(object sender, EventArgs e)
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				var list = (from mp in context.MachinePerms
									select mp.MachineName).Distinct().ToList();
				int i = 0;
				foreach (var p in list)
				{
					if (p.TrimEnd() == "lbc")
					{
						continue;
					}

					CheckBox box = new CheckBox();
					box.Tag = p.ToString();
					box.Text = p.ToString().ToUpper();
					box.Height = 20;
					box.Width = 95;
					box.Location = new Point(i++ * 100 + 15, 10); //vertical
															   //box.Location = new Point(i * 50, 10); //horizontal
					this.Controls.Add(box);
				}

				var permList = context.MachinePerms
				.Join(
					context.MemberRosters,
					perm => perm.Badge,
					member => member.Badge,
					(perm, member) => new PermissionReportItem
					{
						Badge = member.Badge,
						Permission = perm.MachineName.TrimEnd(),
						FirstName = member.FirstName,
						LastName = member.LastName,
						Email = member.Email,
						Phone = member.Phone
					})
				.Where(p => p.Permission != "lbc")
				.OrderBy(x => x.Badge).ToList();

				dataGridView1.DataSource = new SortableBindingList<PermissionReportItem>(permList);
			}
		}
	}
}

using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class AddPermissions : Form
	{
		private string badge;
		private AddPermissions() { }
		public AddPermissions(string badge)
		{
			this.badge = badge;
			InitializeComponent();
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
		}

		private void AddPermissions_Load(object sender, EventArgs e)
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				var permissions = (from p in context.MachineIds
								  select p).ToArray();

				foreach (var permission in permissions)
				{
					comboBox1.Items.Add(new ComboboxItem { Value = permission.id, Text = permission.MachineTypeName }) ;
				}
			}
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			MachinePerm machinePerm = new MachinePerm
			{
				Badge = badge,
				Blocked = false,
				MachineId = ((ComboboxItem)comboBox1.SelectedItem).Value,
				ApprovedDate = DateTime.Now.Date
			};

			DialogResult =  DialogResult.OK;
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}

	public class ComboboxItem
	{
		public string Text { get; set; }
		public int Value { get; set; }

		public override string ToString()
		{
			return Text;
		}
	}
}

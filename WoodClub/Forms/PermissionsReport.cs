using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class PermissionsReport : Form
	{
		private List<string> filters = new List<string>();
		private SortableBindingList<PermissionReportItem> bindingList;
		public PermissionsReport()
		{
			InitializeComponent();
			printDocument1.PrintPage += PrintDocument1_PrintPage;
		}

		private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
			this.dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
			e.Graphics.DrawImage(bm, 0, 0);
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
					box.Location = new Point(i++ * 100 + 15, 10);
					box.Click += new EventHandler(checkbox_Click);
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

				bindingList = new SortableBindingList<PermissionReportItem>(permList);
				dataGridView1.DataSource = bindingList;
				dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			}
		}

		void checkbox_Click(object sender, EventArgs e)
		{
			CheckBox box = ((CheckBox)sender);
			string text = box.Text.Trim().ToLower();
			if (box.Checked)
			{
				filters.Add(text);
			}
			else
			{
				filters.Remove(text);
			}



			//if list is empty clear filters.
			//else filter and refresh.
			string filter = string.Empty;
			foreach (var filterItem in filters)
			{
				if (string.IsNullOrEmpty(filter))
				{
					filter = filterItem;
				}
				else
				{
					filter += " || " + filterItem;
				}
			}

			if (filter == string.Empty)
			{
				dataGridView1.DataSource = bindingList;
			}
			else
			{
				SortableBindingList< PermissionReportItem> filteredBindingList = 
					new SortableBindingList<PermissionReportItem>(bindingList.Where(
					x => filters.Contains(x.Permission)).ToList());
				dataGridView1.DataSource = filteredBindingList;
				dataGridView1.Refresh();
			}
		}

		private void excel_Click(object sender, EventArgs e)
		{
			string pathDesktop = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
			string delimter = ",";
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Save CSV File";
			saveFileDialog.FileName = "permissions.csv";
			saveFileDialog.InitialDirectory = saveFileDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
			saveFileDialog.Filter = "CSV files|*.csv";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string filePath = saveFileDialog.FileName;
				try
				{
					SortableBindingList<PermissionReportItem> currentList = (SortableBindingList <PermissionReportItem>)dataGridView1.DataSource;
					int length = currentList.Count();
					string hdr = "Badge,Permission,First,Last,Email,Phone";

					using (System.IO.TextWriter writer = File.CreateText(filePath))
					{
						writer.WriteLine(hdr);
						for (int index = 0; index < length; index++)
						{
							PermissionReportItem item = currentList[index];
							string csv = item.Badge + "," +
										 item.Permission + "," +
										 item.FirstName + "," +
										 item.LastName + "," +
										 item.Email + "," +
										 item.Phone;
							writer.WriteLine(string.Join(delimter, csv));
						}
					}

					System.Diagnostics.Process.Start(filePath);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not write file to disk. Original error: " + ex.Message);
				}
			}
		}

		private void print_Click(object sender, EventArgs e)
		{
			printDocument1.DefaultPageSettings.Landscape = true;
			PrintPreviewDialog ppvd = new PrintPreviewDialog();
			ppvd.Document = printDocument1;
			ppvd.ShowDialog();
		}

		private void email_list_Click(object sender, EventArgs e)
		{
			string emailAddresses = string.Empty;
			SortableBindingList<PermissionReportItem> currentList = (SortableBindingList<PermissionReportItem>)dataGridView1.DataSource;
			foreach (PermissionReportItem item in currentList)
			{
				if (string.IsNullOrEmpty(emailAddresses))
				{
					emailAddresses = item.Email;
				}
				else
				{
					emailAddresses += ';' + item.Email;
				}
			}
			System.Windows.Forms.Clipboard.SetText(emailAddresses);
			MessageBox.Show("Email Address Copied to Clipboard");
		}
	}
}

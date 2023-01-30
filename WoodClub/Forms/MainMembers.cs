
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoodClub.Forms;

namespace WoodClub
{
	public partial class MainMembers : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public static bool lockersUpdated = false;

		private readonly int listenPort = 5725;
		private readonly UdpClient udpClient = new UdpClient();

		private SortableBindingList<MemberRoster> blMembers;
		private readonly BindingSource bsMembers;
		private MemberRoster currentMember;
		private Members members;
		public bool update;
		public bool added;

		public MainMembers()
		{
			this.blMembers = new SortableBindingList<MemberRoster>();
			this.bsMembers = new BindingSource();
			this.update = false;
			this.added = false;
			InitializeComponent();
			MessageIn();
		}

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

		private void Form1_Load(object sender, EventArgs e)
		{
			// TODO: This line of code loads data into the 'woodclubDataSet.MemberRoster' table. You can move, or remove it, as needed.
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
			toolStripImportNewMembers.Click += ToolStripImportNewMembers_Click;
			MultipleMembersButton.Click += MultipleMembersButton_Click;
		}

		private void MultipleMembersButton_Click(object sender, EventArgs e)
		{
			MultipleEditor me = new MultipleEditor();
			me.ShowDialog();
		}

		private void LoadMembers()
		{
			members = new Members(true);
			blMembers = new SortableBindingList<MemberRoster>(members.DataSource);  // blMembers list of members
			setBsMembersDataSource();
			dataGridView1.DataSource = bsMembers;
		}


		private void ToolStripImportNewMembers_Click(object sender, EventArgs e)
		{
			NewMembers fnm = new NewMembers();
			fnm.ShowDialog();
			LoadMembers();
		}

		private void TextBoxName_KeyUp(object sender, KeyEventArgs e)
		{
			setBsMembersDataSource();
		}

		private void setBsMembersDataSource()
		{
			string filter = toolStripTextBoxFilter.Text;
			if (filter == string.Empty)
			{
				bsMembers.DataSource = blMembers;
			}
			else
			{
				var filteredBindingList = new SortableBindingList<MemberRoster>(blMembers.Where(
					x => x.FirstName.ToUpper().Contains(filter.ToUpper()) ||
					x.LastName.ToUpper().Contains(filter.ToUpper()) ||
					x.Title.ToUpper().Contains(filter.ToUpper()) ||
					x.Badge.Contains(filter)).ToList());
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

		private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			EditCurrentRow();
		}

		private void DeleteItemClick(object sender, EventArgs e)
		{
			int id;
			DialogResult dialogResult = MessageBox.Show("Confirm Delete?", "Item", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.Yes)
			{
				MemberRoster mr = (MemberRoster)bsMembers.Current;
				id = mr.id;

				bsMembers.EndEdit();
				bsMembers.RemoveCurrent();
				using (WoodclubEntities context = new WoodclubEntities())
				{
					string cmd = "delete from MemberRoster where id=" + id.ToString();
					try
					{
						context.Database.ExecuteSqlCommand(cmd);

						bsMembers.ResetCurrentItem();
						LoadMembers();
					}
					catch (Exception ex)
					{
						log.Error("Update failed..", ex);

					}
				}
			}
		}

		private void EditCurrentRow()
		{
			MemberRoster roster = blMembers.FirstOrDefault(mem => mem.id == bsMembers.MemberIdentifier());
			Editor frm = new Editor(roster.Badge);
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

			ActiveControl = toolStripTextBox1.Control;
			toolStripTextBox1.SelectionStart = 0;
			toolStripTextBox1.SelectionLength = toolStripTextBox1.Control.Text.Length;
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		private void btnRFcard_Click(object sender, EventArgs e)
		{
			RFBadge frfb = new RFBadge();
			frfb.ShowDialog();
			LoadMembers();
		}

		private void monitorsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MonitorForm mf = new MonitorForm();
			mf.ShowDialog();
		}

		private void sCWPaidListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			scwForm scwf = new scwForm();
			scwf.ShowDialog();
			DialogResult result = scwf.DialogResult;
			if (result == DialogResult.OK)
			{
				LoadMembers();
			}
		}

		private void monthlyClubUsageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShopUsage uf = new ShopUsage();
			uf.ShowDialog();
		}

		private void summaryReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			lockerSummaryToolStripMenuItem_Click(sender, e);
		}

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

		private void dailySummaryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormDaily fd = new FormDaily();
			fd.ShowDialog();
		}

		private void tb_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				Find_Badge();
			}
		}

		private void Find_Badge()
		{
			string badge = toolStripTextBox1.Text;
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

		private void locationsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LockerLocations lockerLocations = new LockerLocations();
			lockerLocations.ShowDialog();
		}

		private void lockersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LockerData ld = new LockerData();
			ld.ShowDialog();
		}

		private void costsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LockerPrices lockerPrices = new LockerPrices();
			lockerPrices.ShowDialog();
		}
	}
}

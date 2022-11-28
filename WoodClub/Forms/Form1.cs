
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WoodClub.Forms;

namespace WoodClub
{
    public partial class Form1 : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private SortableBindingList<MemberRoster> blMembers = new SortableBindingList<MemberRoster>();
        // private static WoodclubEntities context = new WoodclubEntities();
        private BindingSource bsMembers = new BindingSource();
        private MemberRoster currentMember;
        private Members members;
        public bool update = false;
        public bool added = false;
        public static bool done = false;
        private static int listenPort = 5725;
        private static UdpClient udpClient = new UdpClient();

        public Form1()
        {
            InitializeComponent();
            MessageIn();
        }

        private void MessageIn()
        {
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, listenPort));
            var from = new IPEndPoint(0, 0);
            
            Task.Run(() =>
            {
                while (!done)
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
                members = new Members(true);            // Members datasource initialized from MemberRoster
            }
            catch (Exception ex)
            {
                log.Error("Members failed ", ex);
            }
            
            log.Info("members loaded");

            blMembers = new SortableBindingList<MemberRoster>(members.DataSource);  // blMembers list of members
            bsMembers.DataSource = blMembers;
            dataGridView1.DataSource = bsMembers;
            bsMembers.Position = 0;
            bindingNavigator1.BindingSource = bsMembers;
        }
        
        private void SetUpEventsForDataGridViewSorting()
        {
            bsMembers.PositionChanged += bsMembers_PositionChanged;
            dataGridView1.Sorted += dataGridView1_Sorted;
            bindingNavigator1.DeleteItem = null;
            bindingNavigator1.AddNewItem = null;
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
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            bsMembers.MoveLast();
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
                        members = new Members(true);
                        blMembers = new SortableBindingList<MemberRoster>(members.DataSource);  // blMembers list of members
                        bsMembers.DataSource = blMembers;
                        dataGridView1.DataSource = bsMembers;
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
            bool update = false;
            Editor frm = new Editor(roster);
            try
            {
                while (frm.ShowDialog() == DialogResult.Yes)        // Changes made - need to refresh from SQL
                {
                    woodclubDataSet.AcceptChanges();
                    bsMembers.ResetCurrentItem();
                    members = new Members(true);
                    blMembers = new SortableBindingList<MemberRoster>(members.DataSource);  // blMembers list of members
                    bsMembers.DataSource = blMembers;
                    dataGridView1.DataSource = bsMembers;
                    roster = blMembers.FirstOrDefault(mem => mem.id == bsMembers.MemberIdentifier());
                    frm = new Editor(roster);
                }

                woodclubDataSet.AcceptChanges();
                bsMembers.ResetCurrentItem();
                update = true;

            }
            finally
            {
                frm.Dispose();
            }
            if (update)     // need to reload sql
            { 
                members = new Members(true);
                blMembers = new SortableBindingList<MemberRoster>(members.DataSource);  // blMembers list of members
                bsMembers.DataSource = blMembers;
                dataGridView1.DataSource = bsMembers;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        
        private void btnRFcard_Click(object sender, EventArgs e)
        {
            formRFbadge frfb = new formRFbadge();
            frfb.ShowDialog();
            members = new Members(true);
            blMembers = new SortableBindingList<MemberRoster>(members.DataSource);  // blMembers list of members
            bsMembers.DataSource = blMembers;
            dataGridView1.DataSource = bsMembers;
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
                members = new Members(true);
                blMembers = new SortableBindingList<MemberRoster>(members.DataSource);  // blMembers list of members
                bsMembers.DataSource = blMembers;
                dataGridView1.DataSource = bsMembers;
            }
        }
        private void monthlyClubUsageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUsage uf = new FormUsage();
            uf.ShowDialog();
        }

        private void lockerSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LockerRpt lr = new LockerRpt();
            lr.ShowDialog();
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
                        bsMembers.Position = nx;
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
    }
}

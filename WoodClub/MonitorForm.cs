
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

namespace WoodClub
{
    public partial class MonitorForm : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                 (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static List<Monitors> DSmonitor = new List<Monitors>();
        private static List<Transaction> DStransaction = new List<Transaction>();
        //private static BindingList<Monitors> blMonitors = null;
        private static SortableBindingList<Monitors> blMonitors = new SortableBindingList<Monitors>(DSmonitor);
        private static BindingSource bsMonitors = new BindingSource();
        private static List<string> memberresult = new List<string>();
        private static Monitors currentMonitor = null;
        private int visitsCnt;
        private int monitorCnt;
        private int badgeLen;
        private MemberRoster member = null;
        private int year;
        public MonitorForm()
        {
            InitializeComponent();
            bsMonitors.DataSource = blMonitors;
            bsMonitors.PositionChanged += BsMonitors_PositionChanged;
        }

        private void BsMonitors_PositionChanged(object sender, EventArgs e)
        {
            if (bsMonitors.CurrentRowIsValid())
            {
                currentMonitor = ((Monitors)bsMonitors.Current);
            }
            else
            {
                currentMonitor = null;
            }
        }

        private void MonitorForm_Load(object sender, EventArgs e)
        {
           
            //bindingNavigator1.BindingSource = MonitorsBindingSource;
            year = DateTime.Now.Year;
            //
            // 	Find totals for year
            //  Total visists
            //  Total monitors
            //
            using (WoodclubEntities context = new WoodclubEntities())
            {
                var yearmember = from m in context.Transactions             // List of members using club
                                 where m.TransDate.Value.Year == year
                                 orderby m.Badge
                                 select m;
                memberresult = new List<string>(yearmember.ToList().Select(mb => mb.Badge).Distinct());

                badgeLen = memberresult.Count();
                this.UseWaitCursor = true;
                Task.Run(() => loadMonitor());
                log.Info("Monitor Load completed.");
            }
        }

        public void OnShowMonitor()
        {
            MonitorsBindingSource.DataSource = DSmonitor;
            dataGridViewMonitor.DataSource = bsMonitors;
            bsMonitors.Position = 0;
            //bindingNavigator1.BindingSource = bsMonitors;
            dataGridViewMonitor.Refresh();
            dataGridViewMonitor.Invalidate();
            this.UseWaitCursor = false;
        }
        //
        //  Load from Transactions & member data
        //
        private void loadMonitor()
        {
            using (WoodclubEntities context = new WoodclubEntities())
            {
                try
                {
                    for (int i = 1; i < badgeLen; i++)
                    {
                        string sBadge = memberresult.ElementAt(i);                  // Get Badge value
                        var yearvisit = from t in context.Transactions              // List of Usage by member
                                        where t.TransDate.Value.Year == year
                                             && t.Code == "U"
                                             && t.Badge == sBadge
                                        orderby t.TransDate.Value
                                        select t.TransDate.Value;
                        visitsCnt = yearvisit.Distinct().Count();
                        var yearmonitor = from m in context.Transactions            // List of Credits by member
                                          where m.TransDate.Value.Year == year
                                              && m.Code != "U"
                                              && m.Badge == sBadge
                                          orderby m.TransDate.Value
                                          select m;

                        var query = from rn in context.MemberRosters
                                    where rn.Badge == sBadge
                                    select rn.id;
                        int _id = query.SingleOrDefault();
                        if (_id != 0)        // Found
                        {
                            member = context.MemberRosters.Find(_id);
                            monitorCnt = yearmonitor.Count();
                            Monitors monitor = new Monitors();
                            monitor.Badge = sBadge;
                            monitor.FirstName = member.FirstName;
                            monitor.LastName = member.LastName;
                            monitor.Exempt = (bool)member.Exempt;
                            monitor.ClubDuesPaidDate = member.ClubDuesPaidDate == null ? "" : member.ClubDuesPaidDate.Value.ToShortDateString();
                            monitor.ClubDuesPaid = (bool)member.ClubDuesPaid;
                            monitor.CreditBank = member.CreditBank.ToString();
                            monitor.LastDayValid = member.LastDayValid == null ? "" : member.LastDayValid.Value.ToShortDateString();
                            
                            monitor.CreditAmt = yearmonitor.Sum(x => x.CreditAmt).ToString();
                            monitor.ShopVisits = visitsCnt.ToString();
                            try
                            {
                                int tlen = yearmonitor.Count() - 1;
                                if (tlen > 0)
                                {
                                    Transaction last = yearmonitor.ToList().ElementAt(tlen);
                                    if (last != null && last.TransDate != null)
                                    {
                                        monitor.LastMonitor = last.TransDate.Value.Date.ToShortDateString();
                                    }
                                }
                                else
                                {
                                    monitor.LastMonitor = "Never";
                                }

                            }
                            catch (Exception d)
                            {
                                log.Error("Transdate", d);
                            }

                            DSmonitor.Add(monitor);
                        }
                    }
                }
                catch (Exception e)
                {
                    log.Error("loadMonitor", e);
                }
            }
            log.Info("Scan complete");
            this.Invoke(new Action(() => blMonitors = new SortableBindingList<Monitors>(DSmonitor)));
            this.Invoke(new Action(() => bsMonitors.DataSource = blMonitors));
            this.Invoke(new Action(() => OnShowMonitor()));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string pathDesktop = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
            string filePath = pathDesktop + "\\monitor.csv";
            string delimter = ",";
            SaveFileDialog theDialog = new SaveFileDialog();
            theDialog.Title = "Save CSV File";
            theDialog.FileName = "monitor.csv";
            theDialog.InitialDirectory = theDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
            theDialog.Filter = "CSV files|*.csv";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int length = DSmonitor.Count();
                    string hdr = "Badge,First,Last,Credits,Credit Bank,ShopVisits,Last Monitor,Club Dues Paid Date";
                    
                    using (System.IO.TextWriter writer = File.CreateText(filePath))
                    {
                        writer.WriteLine(hdr);
                        for (int index = 0; index < length; index++)
                        {
                            Monitors mon = DSmonitor[index];
                            string csv = mon.Badge + "," + 
                                         mon.FirstName + "," + 
                                         mon.LastName + "," + 
                                         mon.CreditAmt + "," + 
                                         mon.CreditBank + "," + 
                                         mon.ShopVisits + "," + 
                                         mon.LastMonitor + "," +
                                         mon.ClubDuesPaidDate;
                            writer.WriteLine(string.Join(delimter, csv));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not write file to disk. Original error: " + ex.Message);
                }
            }
        }
    }
}

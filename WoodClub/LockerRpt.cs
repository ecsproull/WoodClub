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
    public partial class LockerRpt : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                 (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static List<Lockers> DSlocker = new List<Lockers>();
        private static SortableBindingList<Lockers> blLockers = new SortableBindingList<Lockers>(DSlocker);
        private static BindingSource bsLockers = new BindingSource();
        private static List<Transaction> DStransaction = new List<Transaction>();
        private static Lockers currentLocker = null;
        private MemberRoster member = null;
        private int year;
        private int visitsCnt;
        public LockerRpt()
        {
            InitializeComponent();
            bsLockers.DataSource = blLockers;
            dataGridViewLockers.DataSource = bsLockers;
            bsLockers.PositionChanged += BsLockers_PositionChanged;
            year = DateTime.Now.Year;
        }
        private void BsLockers_PositionChanged(object sender, EventArgs e)
        {
            if (bsLockers.CurrentRowIsValid())
            {
                currentLocker = ((Lockers)bsLockers.Current);
            }
            else
            {
                currentLocker = null;
            }
        }
        private void LockerForm_Load(object sender, EventArgs e)
        {
            using (WoodclubEntities context = new WoodclubEntities())
            {

                var query = from rn in context.MemberRosters
                            where rn.Locker != null && rn.Locker != ""
                            select rn;
                //
                //  Creat new list for display
                //
                foreach (MemberRoster member in query)
                {
                    //member = context.MemberRosters.Find(_id);
                    Lockers locker = new Lockers();
                    locker.Badge = member.Badge;
                    locker.FirstName = member.FirstName;
                    locker.LastName = member.LastName;
                    locker.ClubDuesPaid = (bool)member.ClubDuesPaid;
                    locker.CreditBank = member.CreditBank.ToString();
                    locker.LastDayValid = member.LastDayValid.Value.ToShortDateString();
                    locker.Modified = member.QBmodified == null ? "" : member.QBmodified.Value.ToShortDateString();
                    locker.HasLocker = member.Locker == null ? "" : member.Locker;
                    var yearvisit = from t in context.Transactions              // List of Usage by member
                                    where t.TransDate.Value.Year == year
                                         && t.Code == "U" | t.Code == "FD"
                                         && t.Badge == member.Badge
                                    select t.TransDate.Value;
                    visitsCnt = yearvisit.DistinctBy(x => x.DayOfYear).Count();
                    locker.ShopVisits = visitsCnt.ToString();
                    DSlocker.Add(locker);
                }
            }
            blLockers = new SortableBindingList<Lockers>(DSlocker);
            bsLockers.DataSource = blLockers;
            dataGridViewLockers.DataSource = bsLockers;
            bsLockers.Position = 0;
            dataGridViewLockers.Refresh();
            dataGridViewLockers.Invalidate();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string pathDesktop = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
            string filePath = pathDesktop + "\\monitor.csv";
            string delimter = ",";
            SaveFileDialog theDialog = new SaveFileDialog();
            theDialog.Title = "Save CSV File";
            theDialog.FileName = "lockers.csv";
            theDialog.InitialDirectory = theDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
            theDialog.Filter = "CSV files|*.csv";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = theDialog.FileName;
                try
                {
                    int length = DSlocker.Count();
                    string hdr = "Badge,First,Last,Club Dues Paid,Shop Visits,Credit Bank, Last Day Valid, Modified, HasLocker";

                    using (System.IO.TextWriter writer = File.CreateText(filePath))
                    {
                        writer.WriteLine(hdr);
                        for (int index = 0; index < length; index++)
                        {
                            Lockers locker = DSlocker[index];
                            string csv = locker.Badge + "," +
                                         locker.FirstName + "," +
                                         locker.LastName + "," +
                                         locker.ClubDuesPaid + "," +
                                         locker.ShopVisits + "," +
                                         locker.CreditBank + "," +
                                         locker.LastDayValid + "," +
                                         locker.Modified + "," +
                                         locker.HasLocker;
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

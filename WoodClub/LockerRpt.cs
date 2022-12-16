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
            int totalRevenue = 0;
            using (WoodclubEntities context = new WoodclubEntities())
            {

                var lmcl = context.Lockers
                .Join(
                    context.MemberRosters,
                    locker => locker.Badge,
                    member => member.Badge,
                    (locker, member) => new { locker, member })
                .Join(
                    context.LockerCosts,
                    lockerMember => lockerMember.locker.Code,
                    lockerCost => lockerCost.Code,
                    (lockerMember, lockerCost) => new { lockerMember, lockerCost })
                .Join(
                    context.LockerLocations,
                    lockerMemberCost => lockerMemberCost.lockerMember.locker.LocationCode,
                    location => location.Location,
                    (lockerMemberCost, location) => new
                    {
                        Badge = lockerMemberCost.lockerMember.member.Badge,
                        FirstName = lockerMemberCost.lockerMember.member.FirstName,
                        LastName = lockerMemberCost.lockerMember.member.LastName,
                        Email = lockerMemberCost.lockerMember.member.Email,
                        Phone = lockerMemberCost.lockerMember.member.Phone,
                        ClubDuesPaid = lockerMemberCost.lockerMember.member.ClubDuesPaid,
                        CreditBank = lockerMemberCost.lockerMember.member.CreditBank,
                        LastDayValid = lockerMemberCost.lockerMember.member.CreditBank,
                        Locker = lockerMemberCost.lockerMember.locker.LockerTitle,
                        Cost = lockerMemberCost.lockerCost.Cost,
                        Location = location.Description
                    })
                .OrderBy(x => x.Badge).ToList();
 
                DSlocker = new List<Lockers>();
                foreach (var member in lmcl)
                {
					//member = context.MemberRosters.Find(_id);
					Lockers locker = new Lockers();
					locker.Badge = member.Badge;
					locker.FirstName = member.FirstName;
					locker.LastName = member.LastName;
					locker.Email = member.Email;
					locker.Phone = member.Phone;
					locker.ClubDuesPaid = (bool)member.ClubDuesPaid;
					locker.CreditBank = member.CreditBank.ToString();
					locker.LastDayValid = member.LastDayValid;
					locker.HasLocker = member.Locker;
					var yearvisit = from t in context.Transactions              // List of Usage by member
									where t.TransDate.Value.Year == year
										 && t.Code == "U" | t.Code == "FD"
										 && t.Badge == member.Badge
									select t.TransDate.Value;
					visitsCnt = yearvisit.DistinctBy(x => x.DayOfYear).Count();
					locker.ShopVisits = visitsCnt.ToString();
                    locker.Cost = member.Cost.Value;
                    locker.Location = member.Location;
					DSlocker.Add(locker);
                    totalRevenue += member.Cost.Value;

                }
            }
            blLockers = new SortableBindingList<Lockers>(DSlocker);
            bsLockers.DataSource = blLockers;
            dataGridViewLockers.DataSource = bsLockers;
            bsLockers.Position = 0;
            dataGridViewLockers.Refresh();
            dataGridViewLockers.Invalidate();
            textBoxTotalRevenue.Text = String.Format("${0}", totalRevenue.ToString("N0"));
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
                    string hdr = "Badge,First,Last,Club Dues Paid,Shop Visits,Credit Bank, Last Day Valid, Locker, Cost, Location";

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
                                         locker.HasLocker + "," +
                                         locker.Cost + "," +
                                         locker.Location;
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

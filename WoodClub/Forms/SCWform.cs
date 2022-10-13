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
    public partial class scwForm: Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<string> paidList = new List<string>();
        private List<UnpaidMember> DSunpaid = new List<UnpaidMember>();
        private List<MemberRoster> DSroster = new List<MemberRoster>();
        

        public scwForm()
        {
            InitializeComponent();
        }

        private void scwForm_Load(object sender, EventArgs e)
        {
            Stream flStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open SCW Paid Members (.csv) File";
            theDialog.Filter = "CSV files|*.csv";
            theDialog.InitialDirectory = theDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((flStream = theDialog.OpenFile()) != null)
                    {
                        using (flStream)
                        {
                            using (StreamReader sr = new StreamReader(flStream))
                            {
                                while (!sr.EndOfStream)
                                {
                                    paidList.Add(sr.ReadLine());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

                //
                //  SCW member paid list loaded
                //  Scan Member roster and update MemberPaid
                //  If not found add to exception grid
                //
                BindingList<UnpaidMember> blUnpaid = new BindingList<UnpaidMember>(DSunpaid);

                using (WoodclubEntities context = new WoodclubEntities())
                {
                    try
                    {
                        DSroster = context.MemberRosters.Select(mem => mem)
                            .Distinct()
                            .OrderBy(mem => mem.Badge)
                            .ToList();
                    }
                    catch (Exception ex)
                    {
                        log.Fatal("Unable to get data...", ex);         // Capture exception
                    }
                    Task.Run(() => ScanSCW());
                    context.SaveChanges();
                    unpaidMemberBindingSource.DataSource = DSunpaid;
                    dataGridView1.DataSource = unpaidMemberBindingSource.DataSource;
                    dataGridView1.Invalidate();
                    log.Info("Scan complete");
                }
            }
            else
            {
                MessageBox.Show("Select Rec Paid File...");
            }
        }
        private void ScanSCW()
        {
            //
            //  Compare the member roster against scw paid
            //  If not found add to grid view
            //
            int id;
            using (WoodclubEntities context = new WoodclubEntities())
            {
                string card;
                string mrFound;
                foreach (MemberRoster MemberRoster in DSroster)
                {
                    string tmp = MemberRoster.RecCard;
                    if(tmp != null && tmp !="")
                    {
                        card = tmp.TrimStart('0');
                        mrFound = paidList.Find(item => item == card);
                    }
                    else
                    {
                        mrFound = null;
                    }
                    if (mrFound != null)        // found item
                    {
                        id = MemberRoster.id;
                        var member = context.MemberRosters.Find(id);
                        if (id != 0 && member != null)
                        {
                            member.RecDuesPaid = true;
                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        id = MemberRoster.id;
                        var member = context.MemberRosters.Find(id);
                        if (id != 0 && member != null && member.RecCard != null & member.RecCard != "")
                        {
                            member.RecDuesPaid = false;
                            UnpaidMember upm = new UnpaidMember();
                            upm.Badge = MemberRoster.Badge;
                            upm.FirstName = MemberRoster.FirstName;
                            upm.LastName = MemberRoster.LastName;
                            upm.MemberDate = MemberRoster.MemberDate;
                            upm.RecCard = MemberRoster.RecCard;
                            upm.Address = MemberRoster.Address;
                            upm.ClubDuesPaid = MemberRoster.ClubDuesPaid;
                            upm.ClubDuesPaidDate = MemberRoster.ClubDuesPaidDate;
                            upm.Phone = MemberRoster.Phone;
                            upm.Email = MemberRoster.Email;
                            upm.State = MemberRoster.State;
                            upm.Delete = false;
                            DSunpaid.Add(upm);
                            context.SaveChanges();
                        }
                    }
                }
               
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (WoodclubEntities context = new WoodclubEntities())
            {
                foreach (UnpaidMember unpaid in DSunpaid)
                {
                    if(unpaid.Delete == true)
                    {
                        var query = from rn in context.MemberRosters
                                    where rn.Badge == unpaid.Badge && rn.RecCard != null && rn.RecCard != ""
                                    select rn;
                        // query.Single().NewBadge = false;
                        if(query.Any())
                        {
                            int id = query.Single().id;

                            var entity = context.MemberRosters.Find(id);
                            if (id != 0 && entity != null)
                            {
                                context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                            }
                        } 
                    }
                }
                context.SaveChanges();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //bool check = (bool) dataGridView1.CurrentCell.Value;
            bool check = (bool)dataGridView1.CurrentRow.Cells[1].Value;         // Delete check box
            dataGridView1.CurrentCell.Value = check == true ? false : true;     // Flip state
        }
    }
}

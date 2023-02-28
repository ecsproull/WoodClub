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
    public partial class FreeDayOnlyUsers : Form
    {
        public FreeDayOnlyUsers()
        {
            InitializeComponent();
        }

        private void FreeDayOnlyUsers_Load(object sender, EventArgs e)
        {
            List<Transaction> fdo = new List<Transaction>();
            using(WoodclubEntities context = new WoodclubEntities())
            {
                DateTime startDate = DateTime.Parse("1/1/2023");
                DateTime endDate = DateTime.Parse("2/1/2023");
                var users = (from m in context.Transactions
                             where m.Code == "FD" && m.TransDate > startDate && m.TransDate < endDate
                             select m).ToList();
                foreach (Transaction tr in users)
                {
                    Transaction creditUsed = (from t in context.Transactions
                                                 where (t.Code == "CU" || 
                                                 (t.Code != "U" && t.CreditAmt > 0)) && t.Badge == tr.Badge && t.TransDate > startDate && t.TransDate < endDate
                                              select t).FirstOrDefault();

                    if (creditUsed == null)
                    {
                        fdo.Add(tr);
                    }    
                }

                List<MemberRoster> deadBeats = new List<MemberRoster>();
                foreach(Transaction trans in fdo)
                {
                    MemberRoster mr = (from m in context.MemberRosters
                                                where m.Badge == trans.Badge
                                                select m).FirstOrDefault();
                    if (mr != null && Convert.ToDouble(mr.CreditBank) < 1)
                    {
                        deadBeats.Add(mr);
                    }
                }

                this.bs_FreeDayOnly.DataSource = new SortableBindingList<MemberRoster>(deadBeats);
            }
        }
    }
}

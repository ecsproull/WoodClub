using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class FreeDayOnlyUsers : Form
    {
        private DateTime startDate;
        private DateTime endDate;
        private int creditAmount = 0;
        public FreeDayOnlyUsers()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/yyyy";
			dateTimePicker1.ValueChanged += DateTimePicker1_ValueChanged;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            this.startDate = DateTime.Parse("1/1/2023");
            this.endDate = startDate.AddMonths(1);
			nMaxCredits.ValueChanged += NMaxCredits_ValueChanged;
        }

		private void NMaxCredits_ValueChanged(object sender, EventArgs e)
		{
            this.creditAmount = (int)nMaxCredits.Value;
            FreeDayOnlyUsers_Load(null, null);
        }

		private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
            this.startDate = DateTime.Parse(dateTimePicker1.Value.Date.Month.ToString() + "/1/" + dateTimePicker1.Value.Date.Year.ToString());
            this.endDate = startDate.AddMonths(1);
            FreeDayOnlyUsers_Load(null, null);

        }

		private void FreeDayOnlyUsers_Load(object sender, EventArgs e)
        {
            List<Transaction> fdo = new List<Transaction>();
            using(WoodclubEntities context = new WoodclubEntities())
            {
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

                    Transaction entry = (from t in context.Transactions
                                         where t.Code == "u" && t.Badge == trans.Badge && t.TransDate > startDate && t.TransDate < endDate
                                         select t).FirstOrDefault();

                    MemberRoster mr = (from m in context.MemberRosters
                                                where m.Badge == trans.Badge
                                                select m).FirstOrDefault();
                    if (mr != null && Convert.ToDouble(mr.CreditBank) <= this.creditAmount)
                    {
                        deadBeats.Add(mr);
                    }
                }

                this.bs_FreeDayOnly.DataSource = new SortableBindingList<MemberRoster>(deadBeats);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                string badge = dataGridViewFreeDay.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (!string.IsNullOrEmpty(badge))
                {
                    Editor ed = new Editor(badge);
                    if (ed.ShowDialog() == DialogResult.OK)
                    {
                        MainMembers.lockersUpdated = true;
                        FreeDayOnlyUsers_Load(null, null);
                    }
                }
            }
        }
    }
}

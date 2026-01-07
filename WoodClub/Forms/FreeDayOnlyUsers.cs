using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
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
			dateTimePicker2.CustomFormat = "MM/yyyy";
			dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
			int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            this.startDate = DateTime.Parse("1/1/2025");
            this.endDate = DateTime.Parse("1/1/2026");
			nMaxCredits.ValueChanged += NMaxCredits_ValueChanged;
        }

		private void NMaxCredits_ValueChanged(object sender, EventArgs e)
		{
            this.creditAmount = (int)nMaxCredits.Value;
            //FreeDayOnlyUsers_Load(null, null);
        }

		private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
            this.startDate = DateTime.Parse(dateTimePicker1.Value.Date.Month.ToString() + "/1/" + dateTimePicker1.Value.Date.Year.ToString());
            this.endDate = startDate.AddMonths(12);
        }

		private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
		{
			this.endDate = DateTime.Parse(dateTimePicker1.Value.Date.Month.ToString() + "/1/" + dateTimePicker1.Value.Date.Year.ToString());;
		}

		private void FreeDayOnlyUsers_Load(object sender, EventArgs e)
        {

        }

        private void FreeDayOnlyUsers_2()
        {
            using (WoodClubEntities context = new WoodClubEntities())
            {
                var members = (from m in context.MemberRosters
                               where m.ClubDuesPaid == true
							   select m).ToList();
				List<MemberRoster> deadBeats = new List<MemberRoster>();
                foreach (MemberRoster member in members)
                {
					var CreditsGained = (from c in context.Transactions
                                         where c.Badge == member.Badge && c.CreditAmt > 0 && c.Code != "U" && c.TransDate >= startDate && c.TransDate < endDate
                                         select c.CreditAmt).DefaultIfEmpty(0).Sum();
                    var CreditsUsed = (from c in context.Transactions
                                       where c.Badge == member.Badge && c.CreditAmt < 0 && c.Code != "U" && c.TransDate >= startDate && c.TransDate < endDate
                                       select c.CreditAmt).DefaultIfEmpty(0).Sum();
                    var freeDaysUsed = (from c in context.Transactions where c.Badge == member.Badge && c.Code == "FD" && c.TransDate >= startDate && c.TransDate < endDate select c).Count();

                    if (CreditsGained == 0 && CreditsUsed == 0 && freeDaysUsed > 0)
                    {
                        deadBeats.Add(member);
                    }
                }
                this.bs_FreeDayOnly.DataSource = new SortableBindingList<MemberRoster>(deadBeats);
			}
        }
		private void runButton_Click(object sender, EventArgs e)
		{
            FreeDayOnlyUsers_2();
		}
	}
}

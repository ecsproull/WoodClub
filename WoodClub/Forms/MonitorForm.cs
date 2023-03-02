
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
    public partial class MonitorForm : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				 (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static List<Monitors> DSmonitor = new List<Monitors>();
		private static SortableBindingList<Monitors> blMonitors = new SortableBindingList<Monitors>(DSmonitor);
		private static BindingSource bsMonitors = new BindingSource();
		public MonitorForm()
		{
			InitializeComponent();
		}
		private void MonitorForm_Load(object sender, EventArgs e)
		{
			ShowMonitors();
		}

		private void LoadMonitors()
		{
			using (WoodclubEntities context = new WoodclubEntities())
			{
				DateTime ddd = DateTime.Parse("1999-01-01");
				DateTime dt = DateTime.Parse("2022/11/18");
				List<Monitors> lmcl = context.Transactions
					.Join(
						context.MemberRosters,
						tr => tr.Badge,
						mem => mem.Badge,
						(tr, mem) => new Monitors
						{
							Badge = tr.Badge,
							FirstName = mem.FirstName,
							LastName = mem.LastName,
							Exempt = mem.Exempt ?? false,
							ClubDuesPaid = mem.ClubDuesPaid ?? false,
							ClubDuesPaidDate = mem.ClubDuesPaidDate ?? ddd,
							CreditBank = mem.CreditBank,
							LastDayValid = mem.LastDayValid.ToString(),
							CreditAmt = tr.CreditAmt ?? 0,
							LastMonitor = tr.TransDate ?? ddd,
							Code = tr.Code,
							ShopVisits = 10,
							Lockers = mem.Locker
						})
					.Where(t => t.Code == "M4" && t.LastMonitor >= dt)
					.OrderByDescending(x => x.LastMonitor).ToList();

				foreach (Monitors x in lmcl)
				{
					DateTime startDate = DateTime.Parse("2022-11-18");
					var yearvisit = from t in context.Transactions              // List of Usage by member
									where t.TransDate > startDate
										 && t.Code == "U" | t.Code == "FD"
										 && t.Badge == x.Badge
									select t.TransDate.Value;
					x.ShopVisits = yearvisit.DistinctBy(u => u.DayOfYear).Count();

					DSmonitor.Add(x);
				}
			}

		}
		public void ShowMonitors()
		{
			LoadMonitors();
			blMonitors = new SortableBindingList<Monitors>(DSmonitor);
			bsMonitors.DataSource = blMonitors;
			dataGridViewMonitor.DataSource = bsMonitors;
		}
	}

}

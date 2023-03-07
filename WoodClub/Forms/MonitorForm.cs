
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

				badgeLen = memberresult.Count();
				this.UseWaitCursor = true;
				loadMonitor();
				log.Info("LoadYear: Monitor Load completed.");
			}

		}
		public void ShowMonitors()
		{
			LoadMonitors();
			blMonitors = new SortableBindingList<Monitors>(DSmonitor);
			bsMonitors.DataSource = blMonitors;
			dataGridViewMonitor.DataSource = bsMonitors;
			bsMonitors.Position = 0;
			dataGridViewMonitor.Refresh();
			dataGridViewMonitor.Invalidate();
			UseWaitCursor = false;
			this.btnRefresh.Enabled = true;
		}

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

											 && t.Badge == (string)sBadge
										select t.TransDate.Value;

						visitsCnt = yearvisit.DistinctBy(x => x.DayOfYear).Count();
						log.Info("debug");
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
							monitor.Exempt = member.Exempt == null ? false : (bool)member.Exempt;
							monitor.ClubDuesPaidDate = member.ClubDuesPaidDate == null ? DateTime.Now.AddYears(-5) : member.ClubDuesPaidDate.Value;
							monitor.ClubDuesPaid = (bool)member.ClubDuesPaid;
							monitor.CreditBank = member.CreditBank.ToString();
							monitor.LastDayValid = member.LastDayValid == null ? DateTime.Now.AddYears(-5) : member.LastDayValid.Value;
							monitor.Lockers = member.Locker == null ? "None" : member.Locker;
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
			OnShowMonitor();
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
				filePath = theDialog.FileName;
				try
				{
					int length = DSmonitor.Count();
					string hdr = "Badge,First,Last,Credits,Credit Bank,ShopVisits,Last Monitor,Club Dues Paid Date, Locker";

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
										 mon.ClubDuesPaidDate + "," +
										 mon.Lockers + ",";

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

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			int todayYear = DateTime.Now.Year;
			if (year > 2015 && year <= todayYear)    // Keep inbounds
			{
				LoadYear(year);
			}
			else
			{
				MessageBox.Show("Error: Please Enter a Valid Year!");
			}
		}

		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			year = dateTimePicker1.Value.Year;
		}
	}

}

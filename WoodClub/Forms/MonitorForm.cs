﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Form to display the monitor report.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
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

		/// <summary>
		/// Initializes a new instance of the <see cref="MonitorForm"/> class.
		/// </summary>
		public MonitorForm()
		{
			InitializeComponent();
			bsMonitors.DataSource = blMonitors;
			bsMonitors.PositionChanged += BsMonitors_PositionChanged;
		}

		/// <summary>
		/// Handles the PositionChanged event of the BindingSource control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

		/// <summary>
		/// Handles the Load event of the MonitorForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void MonitorForm_Load(object sender, EventArgs e)
		{
			year = DateTime.Now.Year;
			LoadYear(year);
		}

		/// <summary>
		/// Loads the year when the year changes.
		/// </summary>
		/// <param name="yr">The yr.</param>
		private void LoadYear(int yr)
		{
			//
			// 	Find totals for year
			//  Total visits
			//  Total monitors
			//
			btnRefresh.Enabled = false;
			using (WoodClubEntities context = new WoodClubEntities())
			{
				var yearmember = from m in context.Transactions             // List of members using club
								 where m.TransDate.Value.Year == year
								 orderby m.Badge
								 select m;
				memberresult = new List<string>(yearmember.ToList().Select(mb => mb.Badge).Distinct());

				badgeLen = memberresult.Count();
				this.UseWaitCursor = true;
				loadMonitor();
				log.Info("LoadYear: Monitor Load completed.");
			}
		}

		/// <summary>
		/// Called when [show monitor].
		/// </summary>
		public void OnShowMonitor()
		{
			blMonitors = new SortableBindingList<Monitors>(DSmonitor);
			bsMonitors.DataSource = blMonitors;
			dataGridViewMonitor.DataSource = bsMonitors;
			bsMonitors.Position = 0;
			dataGridViewMonitor.Refresh();
			dataGridViewMonitor.Invalidate();
			UseWaitCursor = false;
			this.btnRefresh.Enabled = true;
		}

		/// <summary>
		/// Loads the monitor.
		/// </summary>
		private void loadMonitor()
		{
			using (WoodClubEntities context = new WoodClubEntities())
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

		/// <summary>
		/// Handles the Click event of the Save button.
		/// Saves the data as a CSV file.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

		/// <summary>
		/// Handles the Click event of the Refresh button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

		/// <summary>
		/// Handles the ValueChanged event of the dateTimePicker1 control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			year = dateTimePicker1.Value.Year;
		}
	}
}

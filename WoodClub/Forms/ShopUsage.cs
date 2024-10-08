﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Form to display monthly shop usage.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class ShopUsage : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static List<Usage> DSusage = new List<Usage>();
		private static List<Transaction> DStransaction = new List<Transaction>();
		private static List<string> memberresult = new List<string>();
		private static SortableBindingList<Usage> blUsage = new SortableBindingList<Usage>(DSusage);
		private static BindingSource bsUsage = new BindingSource();     // Create binding source for datagrid
		private static Usage currentUsage = null;
		private int year;
		private int month;              // Month to search
		private int badgeLen;           // Number this month
		private int visitsCnt;          // Number of member visits
		private int monthCnt = 0;
		private MemberRoster member = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="ShopUsage"/> class.
		/// </summary>
		public ShopUsage()
		{
			InitializeComponent();
			bsUsage.DataSource = blUsage;
			bsUsage.PositionChanged += BindingSourceUsage_PositionChanged;
		}

		/// <summary>
		/// Handles the PositionChanged event of the BindingSourceUsage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void BindingSourceUsage_PositionChanged(object sender, EventArgs e)
		{
			if (bsUsage.CurrentRowIsValid())
			{
				currentUsage = ((Usage)bsUsage.Current);
			}
			else
			{
				currentUsage = null;
			}
		}

		/// <summary>
		/// Handles the Load event of the FormUsage control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void FormUsage_Load(object sender, EventArgs e)
		{
			year = DateTime.Now.Year;
			month = DateTime.Now.Month;
			LoadYear(year, month);
		}

		/// <summary>
		/// Loads the year.
		/// </summary>
		/// <param name="yr">The yr.</param>
		/// <param name="month">The month.</param>
		private void LoadYear(int yr, int month)
		{
			//
			// 	Find totals for year
			//  Total visits
			//  Total monitors
			//
			btnRefresh.Enabled = false;
			using (WoodClubEntities context = new WoodClubEntities())
			{
				var memberUsage = from m in context.Transactions             // List of members using club
								  where m.TransDate.Value.Year == yr && m.TransDate.Value.Month == month
								  orderby m.Badge
								  select m;
				memberresult = new List<string>(memberUsage.ToList().Select(mb => mb.Badge).Distinct());

				badgeLen = memberresult.Count();
				this.UseWaitCursor = true;
				monthCnt = 0;
				Task.Run(() => loadUsage());
				log.Info("LoadYear: Usage Load completed.");
			}
		}

		/// <summary>
		/// Loads the usage.
		/// </summary>
		private void loadUsage()
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				try
				{
					for (int i = 1; i < badgeLen; i++)
					{
						string sBadge = memberresult.ElementAt(i);                  // Get Badge value
						var monthvisit = from t in context.Transactions              // List of Usage by member
										 where t.TransDate.Value.Year == year && t.TransDate.Value.Month == month
											  && t.Code == "U"
											  && t.Badge == (string)sBadge
										 select t.TransDate.Value;

						visitsCnt = monthvisit.DistinctBy(x => x.DayOfYear).Count();
						log.Info("debug");

						var query = from rn in context.MemberRosters
									where rn.Badge == sBadge
									select rn.id;
						int _id = query.SingleOrDefault();
						if (_id != 0)        // Found
						{
							member = context.MemberRosters.Find(_id);
							//monitorCnt = yearmonitor.Count();
							Usage usage = new Usage();
							usage.Badge = sBadge;
							usage.FirstName = member.FirstName;
							usage.LastName = member.LastName;
							usage.CreditBank = member.CreditBank.ToString();
							usage.LastDayValid = member.LastDayValid == null ? "" : member.LastDayValid.Value.ToShortDateString();
							usage.ShopVisits = visitsCnt.ToString();
							if (visitsCnt > 0)
							{
								DSusage.Add(usage);
								monthCnt += visitsCnt;
							}
						}
					}
				}
				catch (Exception e)
				{
					log.Error("loadUsage", e);
				}
			}
			log.Info("Scan complete");
			this.Invoke(new Action(() => OnShowUsage()));
		}

		/// <summary>
		/// Called when [show usage].
		/// </summary>
		public void OnShowUsage()
		{
			txtTotal.Text = monthCnt.ToString();
			blUsage = new SortableBindingList<Usage>(DSusage);
			bsUsage.DataSource = blUsage;
			dataGridViewUsage.DataSource = bsUsage;
			bsUsage.Position = 0;
			dataGridViewUsage.Refresh();
			dataGridViewUsage.Invalidate();
			UseWaitCursor = false;
			this.btnRefresh.Enabled = true;
		}

		/// <summary>
		/// Handles the Click event of the Save button.
		/// Saves the data as CSV.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void btnSave_Click(object sender, EventArgs e)
		{
			string pathDesktop = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
			string filePath = pathDesktop + "\\usage.csv";
			string delimter = ",";
			SaveFileDialog theDialog = new SaveFileDialog();
			theDialog.Title = "Save CSV File";
			theDialog.FileName = "usage.csv";
			theDialog.InitialDirectory = theDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
			theDialog.Filter = "CSV files|*.csv";
			if (theDialog.ShowDialog() == DialogResult.OK)
			{
				filePath = theDialog.FileName;
				try
				{
					int length = DSusage.Count();
					string hdr = "Badge,First,Last,Last Day Valid, Credit Bank, Shop Visits";

					using (System.IO.TextWriter writer = File.CreateText(filePath))
					{
						writer.WriteLine(hdr);
						for (int index = 0; index < length; index++)
						{
							Usage visit = DSusage[index];
							string csv = visit.Badge + "," +
										 visit.FirstName + "," +
										 visit.LastName + "," +
										 visit.LastDayValid + "," +
										 visit.CreditBank + "," +
										 visit.ShopVisits + ",";
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
			txtTotal.Text = "";
			if (year > 2015 && year <= todayYear)    // Keep inbounds
			{
				LoadYear(year, month);

			}
			else
			{
				MessageBox.Show("Error: Please Enter a Valid Date!");
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
			month = dateTimePicker1.Value.Month;
		}
	}
}


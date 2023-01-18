using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class FormDaily : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static List<Daily> DSdaily = new List<Daily>();
		private static List<Transaction> DStransaction = new List<Transaction>();
		private static List<string> badgeList = new List<string>();
		private static SortableBindingList<Daily> blDaily = new SortableBindingList<Daily>(DSdaily);
		private static BindingSource bsDaily = new BindingSource();     // Create binding source for datagrid
		private static Daily currentDaily = null;
		private int badgeLen;           // Number this month
		private int monthCnt = 0;
		private int year;
		private int month;              // Month to search
		private int dayMonth;           // Day in month
		private int[] hourTotal;
		public FormDaily()
		{
			InitializeComponent();
			bsDaily.DataSource = blDaily;
			bsDaily.PositionChanged += BsDaily_PositionChanged;
		}

		private void BsDaily_PositionChanged(object sender, EventArgs e)
		{

			if (bsDaily.CurrentRowIsValid())
			{
				currentDaily = ((Daily)bsDaily.Current);
			}
			else
			{
				currentDaily = null;
			}
		}

		private void FormDaily_Load(object sender, EventArgs e)
		{
			year = DateTime.Now.Year;
			month = DateTime.Now.Month;
			hourTotal = new int[24];
			for (int i = 0; i < 24; i++)
			{
				hourTotal[i] = 0;      //Clear array on load
			}
			LoadYear(year, month);
		}
		private void LoadYear(int yr, int month)
		{
			//
			// 	Find totals for year
			//  Total visits
			//  Total monitors
			//
			btnDailyRefresh.Enabled = false;
			dayMonth = System.DateTime.DaysInMonth(yr, month);
			monthCnt = 0;

			for (int d = 1; d <= dayMonth; d++)
			{
				for (int i = 0; i < 24; i++)
				{
					hourTotal[i] = 0;      // Clear array
				}
				using (WoodclubEntities context = new WoodclubEntities())
				{
					var memberResult = (from m in context.Transactions          // List of members using club
										where m.TransDate.Value.Year == yr && m.TransDate.Value.Month == month && m.TransDate.Value.Day == d && m.Code == "U"
										select m).DistinctBy(m => m.Badge);               // Member Total
					badgeLen = memberResult.Count();

					if (badgeLen > 0)
					{
						monthCnt += badgeLen;

						this.UseWaitCursor = true;
						List<Transaction> hr_visits = memberResult.ToList();

						foreach (Transaction TransDate in hr_visits)
						{
							Transaction nx = TransDate;
							int hr = nx.TransDate.Value.Hour;
							hourTotal[hr]++;
						}
						this.Invoke(new Action(() => txtDailyTotal.Text = monthCnt.ToString())); // Need to use Invoke new action - avoids exception
					}
				}

				Daily daily = new Daily();
				DateTime dt = new DateTime(year, month, d);
				daily.date = dt.ToShortDateString();
				daily.TotalDay = badgeLen.ToString();
				daily.six_am = hourTotal[6].ToString();
				daily.seven_am = hourTotal[7].ToString();
				daily.eight_am = hourTotal[8].ToString();
				daily.nine_am = hourTotal[9].ToString();
				daily.ten_am = hourTotal[10].ToString();
				daily.eleven_am = hourTotal[11].ToString();
				daily.twelve_pm = hourTotal[12].ToString();
				daily.one_pm = hourTotal[13].ToString();
				daily.two_pm = hourTotal[14].ToString();
				daily.three_pm = hourTotal[15].ToString();
				daily.four_pm = hourTotal[16].ToString();
				daily.five_pm = hourTotal[17].ToString();
				// 6 PM to 10 PM
				int afterSix = hourTotal[18] + hourTotal[19] + hourTotal[20] + hourTotal[21] + hourTotal[22];
				daily.six_pm = afterSix.ToString();
				DSdaily.Add(daily);

			}
			this.Invoke(new Action(() => OnShowDaily()));
		}
		public void OnShowDaily()
		{
			blDaily = new SortableBindingList<Daily>(DSdaily);
			bsDaily.DataSource = blDaily;
			dataGridViewDaily.DataSource = bsDaily;
			bsDaily.Position = 0;
			dataGridViewDaily.Refresh();
			dataGridViewDaily.Invalidate();
			UseWaitCursor = false;
			this.btnDailyRefresh.Enabled = true;
		}
		private void DailydateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			year = DailydateTimePicker.Value.Year;
			month = DailydateTimePicker.Value.Month;
		}
		private void dailyBindingSource_CurrentChanged(object sender, EventArgs e)
		{

		}

		private void btnDailyRefresh_Click(object sender, EventArgs e)
		{
			int todayYear = DateTime.Now.Year;
			txtDailyTotal.Text = "";
			if (year > 2015 && year <= todayYear)    // Keep inbounds
			{
				dataGridViewDaily.Rows.Clear();
				dataGridViewDaily.Refresh();
				LoadYear(year, month);
			}
			else
			{
				MessageBox.Show("Error: Please Enter a Valid Date!");
			}
		}
		//
		//  Save data as CSV file
		//
		private void btnSave_Click(object sender, EventArgs e)
		{
			string pathDesktop = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
			string filePath = pathDesktop + "\\usage.csv";
			string delimter = ",";
			SaveFileDialog theDialog = new SaveFileDialog();
			theDialog.Title = "Save CSV File";
			theDialog.FileName = "daily.csv";
			theDialog.InitialDirectory = theDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
			theDialog.Filter = "CSV files|*.csv";
			if (theDialog.ShowDialog() == DialogResult.OK)
			{
				filePath = theDialog.FileName;
				try
				{
					int length = DSdaily.Count();
					string hdr = "Date,Total Day,6 AM,7 AM, 8 AM, 9 AM, 10 AM, 11 AM, 12 PM, 1 PM, 2 PM, 3 PM, 4 PM, 5 PM, 6 PM +";

					using (System.IO.TextWriter writer = File.CreateText(filePath))
					{
						writer.WriteLine(hdr);
						for (int index = 0; index < length; index++)
						{
							Daily visit = DSdaily[index];
							string csv = visit.date + "," +
										 visit.TotalDay + "," +
										 visit.six_am + "," +
										 visit.seven_am + "," +
										 visit.eight_am + "," +
										 visit.nine_am + "," +
										 visit.ten_am + "," +
										 visit.eleven_am + "," +
										 visit.twelve_pm + "," +
										 visit.one_pm + "," +
										 visit.two_pm + "," +
										 visit.three_pm + "," +
										 visit.four_pm + "," +
										 visit.five_pm + "," +
										 visit.six_pm + ",";
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

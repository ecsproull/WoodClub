using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WoodClub
{
	/// <summary>
	/// Class for creating a report of all of the lockers, in use and open.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class LockerRpt : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				 (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private static SortableBindingList<Lockers> blLockers = new SortableBindingList<Lockers>();
		private static BindingSource bsLockers = new BindingSource();
		private static List<Transaction> DStransaction = new List<Transaction>();
		private static Lockers currentLocker = null;
		private int year;
		private int visitsCnt;

		/// <summary>
		/// Initializes a new instance of the <see cref="LockerRpt"/> class.
		/// </summary>
		public LockerRpt()
		{
			InitializeComponent();
			bsLockers.DataSource = blLockers;
			dataGridViewLockers.DataSource = bsLockers;
			bsLockers.PositionChanged += BsLockers_PositionChanged;
			year = DateTime.Now.Year;
		}

		/// <summary>
		/// Handles the PositionChanged event of the BsLockers control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

		/// <summary>
		/// Handles the Load event of the LockerForm control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		private void LockerForm_Load(object sender, EventArgs e)
		{
			textBoxLockerFilter.KeyUp += TextBoxLockerFilter_KeyUp;
			int totalRevenue = 0;
			using (WoodClubEntities context = new WoodClubEntities())
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
						LastDayValid = lockerMemberCost.lockerMember.member.LastDayValid.ToString(),
						Locker = lockerMemberCost.lockerMember.locker.LockerTitle,
						Cost = lockerMemberCost.lockerCost.Cost,
						Location = location.Description,
						Project = lockerMemberCost.lockerMember.locker.Project
					})
				.OrderBy(x => x.Badge).ToList();

				blLockers = new SortableBindingList<Lockers>();
				foreach (var member in lmcl)
				{
					var yearvisit = from t in context.Transactions              // List of Usage by member
									where t.TransDate.Value.Year == year
										 && t.Code == "U" | t.Code == "FD"
										 && t.Badge == member.Badge
									select t.TransDate.Value;
					visitsCnt = yearvisit.DistinctBy(x => x.DayOfYear).Count();

					Lockers locker = new Lockers
					{
						Badge = member.Badge,
						FirstName = member.FirstName,
						LastName = member.LastName,
						Email = member.Email,
						Phone = member.Phone,
						ClubDuesPaid = (bool)member.ClubDuesPaid,
						CreditBank = member.CreditBank.ToString(),
						LastDayValid = member.LastDayValid,
						HasLocker = member.Locker,
						ShopVisits = visitsCnt.ToString(),
						Cost = member.Cost.Value,
						Location = member.Location,
						Project = member.Project
					};

					if (numericUpDownMinVisits.Value != 0 && 
						visitsCnt <= numericUpDownMinVisits.Value)
					{
						blLockers.Add(locker);
					}
					else if (numericUpDownMinVisits.Value == 0)
					{
						blLockers.Add(locker);
					}

					totalRevenue += member.Cost.Value;

				}

				if (numericUpDownMinVisits.Value == 0)
				{
					List<Locker> emptyLockers = (from l in context.Lockers
												 where l.Badge == string.Empty
												 select l).ToList();

					foreach (var el in emptyLockers)
					{
						//member = context.MemberRosters.Find(_id);
						Lockers locker = new Lockers
						{
							Badge = el.Badge,
							FirstName = string.Empty,
							LastName = string.Empty,
							Email = string.Empty,
							Phone = string.Empty,
							ClubDuesPaid = false,
							CreditBank = string.Empty,
							LastDayValid = string.Empty,
							HasLocker = el.LockerTitle,
							ShopVisits = string.Empty,
							Cost = 0,
							Location = el.LocationCode,
							Project = el.Project
						};

						blLockers.Add(locker);
					}
				}
			}

			bsLockers.DataSource = blLockers;
			dataGridViewLockers.DataSource = bsLockers;
			bsLockers.Position = 0;
			dataGridViewLockers.Refresh();
			dataGridViewLockers.Invalidate();
			textBoxTotalRevenue.Text = String.Format("${0}", totalRevenue.ToString("N0"));
			dataGridViewLockers.CellContentClick += DataGridViewLockers_CellContentClick;
		}
		/// <summary>
		/// Handles the CellContentClick event of the DataGridViewLockers control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		private void DataGridViewLockers_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0 && e.RowIndex >= 0)
			{
				string badge = dataGridViewLockers.Rows[e.RowIndex].Cells[0].Value.ToString();
				if (!string.IsNullOrEmpty(badge))
				{
					MemberEditor ed = new MemberEditor(badge);
					if (ed.ShowDialog() == DialogResult.OK)
					{
						MainMembers.lockersUpdated = true;
						LockerForm_Load(null, null);
					}
				}
			}
		}

		/// <summary>
		/// Handles the KeyUp event of the TextBoxLockerFilter control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		private void TextBoxLockerFilter_KeyUp(object sender, KeyEventArgs e)
		{
			string filter = textBoxLockerFilter.Text;
			if (filter == string.Empty)
			{
				bsLockers.DataSource = blLockers;
			}
			else
			{
				var filteredBindingList = new SortableBindingList<Lockers>(blLockers.Where(x => x.HasLocker.Contains(filter.ToUpper()) ||
																								x.Badge.Contains(filter) ||
																								x.FirstName.ToUpper().Contains(filter.ToUpper()) ||
																								x.LastName.ToUpper().Contains(filter.ToUpper())).ToList());
				bsLockers.DataSource = filteredBindingList;
				dataGridViewLockers.Refresh();
			}
		}

		/// <summary>
		/// Handles the Click event of the btnSave control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		private void btnSave_Click(object sender, EventArgs e)
		{
			string pathDesktop = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
			string delimter = ",";
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Save CSV File";
			saveFileDialog.FileName = "lockers.csv";
			saveFileDialog.InitialDirectory = saveFileDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
			saveFileDialog.Filter = "CSV files|*.csv";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string filePath = saveFileDialog.FileName;
				try
				{
					int length = blLockers.Count();
					string hdr = "Badge,First,Last,Club Dues Paid,Shop Visits,Credit Bank, Last Day Valid, Locker, Cost, Location";

					using (System.IO.TextWriter writer = File.CreateText(filePath))
					{
						writer.WriteLine(hdr);
						for (int index = 0; index < length; index++)
						{
							Lockers locker = blLockers[index];
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

					System.Diagnostics.Process.Start(filePath);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not write file to disk. Original error: " + ex.Message);
				}
			}
		}

		private StringFormat strFormat;
		private List<int> columnLefts = new List<int>();
		private List<int> columnWidths = new List<int>();
		private int cellHeight;
		private int rowsPrinted = 0;
		private bool firstPage;
		private bool newPage;
		private int totalWidth;
		private int headerHeight = 0;
		private List<int> columnSkip = new List<int>();

		/// <summary>
		/// Handles the Click event of the buttonPrint control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		private void buttonPrint_Click(object sender, EventArgs e)
		{
			dataGridViewLockers.Columns[1].Visible = false;
			dataGridViewLockers.Columns[6].Visible = false;
			dataGridViewLockers.Columns[7].Visible = false;
			dataGridViewLockers.Columns[8].Visible = false;
			dataGridViewLockers.Columns[11].Visible = false;
			
			printDialogLockers.AllowSomePages = true;
			PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
			printPrvDlg.ClientSize = new System.Drawing.Size(1200, 800);

			printLockerReport.BeginPrint += PrintLockerReport_BeginPrint;
			printLockerReport.PrintPage += PrintLockerReport_PrintPage;
			printLockerReport.DefaultPageSettings.Landscape = true;

			printPrvDlg.Document = printLockerReport;
			printPrvDlg.ShowDialog();
		}

		/// <summary>
		/// Handles the BeginPrint event of the PrintLockerReport control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Drawing.Printing.PrintEventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		private void PrintLockerReport_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			try
			{
				//columnSkip.Add(1);
				//columnSkip.Add(6);
				//columnSkip.Add(7);
				//columnSkip.Add(8);
				//columnSkip.Add(11);

				printLockerReport = new PrintDocument();
				strFormat = new StringFormat
				{
					Alignment = StringAlignment.Near,
					LineAlignment = StringAlignment.Center,
					Trimming = StringTrimming.EllipsisCharacter
				};

				columnLefts.Clear();
				columnWidths.Clear();
				cellHeight = 0;
				firstPage = true;
				newPage = true;
				rowsPrinted = 0;

				// Calculating Total Widths
				totalWidth = 0;
				int col = 0;
				foreach (DataGridViewColumn dgvGridCol in dataGridViewLockers.Columns)
				{
					if (!columnSkip.Contains(col))
					{
						totalWidth += dgvGridCol.Width;
					}
					col++;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Handles the PrintPage event of the PrintLockerReport control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		private void PrintLockerReport_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			try
			{
				//Set the top margin
				int iTopMargin = e.MarginBounds.Top;
				//Whether more pages have to print or not
				bool bMorePagesToPrint = false;

				//For the first page to print set the cell width and header height
				if (firstPage)
				{
					int iTmpWidth = 0;
					int iLeftMargin = e.MarginBounds.Left;
					int col = 0;
					foreach (DataGridViewColumn GridCol in dataGridViewLockers.Columns)
					{
						if (!columnSkip.Contains(col))
						{
							iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
								(double)totalWidth * (double)totalWidth *
								((double)e.MarginBounds.Width / (double)totalWidth))));

							headerHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
								GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

							// Save width and height of headers
							columnLefts.Add(iLeftMargin);
							columnWidths.Add(iTmpWidth);
							iLeftMargin += iTmpWidth;
						}
						col++;
					}
				}

				//Loop till all the grid rows get printed
				int iRow = rowsPrinted;
				while (iRow <= dataGridViewLockers.Rows.Count - 1)
				{
					DataGridViewRow GridRow = dataGridViewLockers.Rows[iRow];
					//Set the cell height
					cellHeight = GridRow.Height + 5;
					int iCount = 0;
					//Check whether the current page settings allows more rows to print
					if (iTopMargin + cellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
					{
						newPage = true;
						firstPage = false;
						bMorePagesToPrint = true;
						break;
					}
					else
					{
						if (newPage)
						{
							//Draw Header
							e.Graphics.DrawString("Locker Summary",
								new Font(dataGridViewLockers.Font, FontStyle.Bold),
								Brushes.Black, e.MarginBounds.Left,
								e.MarginBounds.Top - e.Graphics.MeasureString("Locker Summary",
								new Font(dataGridViewLockers.Font, FontStyle.Bold),
								e.MarginBounds.Width).Height - 13);

							String strDate = DateTime.Now.ToLongDateString() + " " +
								DateTime.Now.ToShortTimeString();
							//Draw Date
							e.Graphics.DrawString(strDate,
								new Font(dataGridViewLockers.Font, FontStyle.Bold), Brushes.Black,
								e.MarginBounds.Left +
								(e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
								new Font(dataGridViewLockers.Font, FontStyle.Bold),
								e.MarginBounds.Width).Width),
								e.MarginBounds.Top - e.Graphics.MeasureString("Customer Summary",
								new Font(new Font(dataGridViewLockers.Font, FontStyle.Bold),
								FontStyle.Bold), e.MarginBounds.Width).Height - 13);

							//Draw Columns                 
							iTopMargin = e.MarginBounds.Top;
							int coll = 0;
							foreach (DataGridViewColumn GridCol in dataGridViewLockers.Columns)
							{
								if (!columnSkip.Contains(coll))
								{
									e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
									new Rectangle((int)columnLefts[iCount], iTopMargin,
									(int)columnWidths[iCount], headerHeight));

									e.Graphics.DrawRectangle(Pens.Black,
										new Rectangle((int)columnLefts[iCount], iTopMargin,
										(int)columnWidths[iCount], headerHeight));

									e.Graphics.DrawString(GridCol.HeaderText,
										GridCol.InheritedStyle.Font,
										new SolidBrush(GridCol.InheritedStyle.ForeColor),
										new RectangleF((int)columnLefts[iCount], iTopMargin,
										(int)columnWidths[iCount], headerHeight), strFormat);
									iCount++;
								}
								coll++;
							}

							newPage = false;
							iTopMargin += headerHeight;
						}
						iCount = 0;
						//Draw Columns Contents
						int col = 0;
						foreach (DataGridViewCell Cel in GridRow.Cells)
						{
							if (!columnSkip.Contains(col))
							{
								if (Cel.Value != null)
								{
									e.Graphics.DrawString(Cel.Value.ToString(),
										Cel.InheritedStyle.Font,
										new SolidBrush(Cel.InheritedStyle.ForeColor),
										new RectangleF((int)columnLefts[iCount],
										(float)iTopMargin,
										(int)columnWidths[iCount], (float)cellHeight),
										strFormat);
								}
								//Drawing Cells Borders 
								e.Graphics.DrawRectangle(Pens.Black,
									new Rectangle((int)columnLefts[iCount], iTopMargin,
									(int)columnWidths[iCount], cellHeight));
								iCount++;
							}

							col++;
						}
					}

					rowsPrinted++;
					iRow++;
					iTopMargin += cellHeight;
				}

				//If more lines exist, print another page.
				if (bMorePagesToPrint)
				{
					e.HasMorePages = true;
				}
				else
				{
					e.HasMorePages = false;
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK,
				   MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the comboBox1 control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
			ComboBox cb = sender as ComboBox;
			SortableBindingList<Lockers> filteredBindingList = blLockers;
			switch(cb.Text)
            {
				case "Open":
					filteredBindingList = new SortableBindingList<Lockers>(blLockers.Where(
						x => x.Badge == string.Empty && (x.Project == null || x.Project == string.Empty)).ToList());
					break;
				case "Assigned":
					filteredBindingList = new SortableBindingList<Lockers>(blLockers.Where(
						x => x.Badge != string.Empty).ToList());
					break;
				case "Training":
					filteredBindingList = new SortableBindingList<Lockers>(blLockers.Where(
						x => x.Project != null && x.Project != string.Empty).ToList());
					break;
				case "Special Project":
					filteredBindingList = new SortableBindingList<Lockers>(blLockers.Where(
						x => x.Project != null && x.Project.ToLower().StartsWith("sp ")).ToList());
					break;
			}

			bsLockers.DataSource = filteredBindingList;
			dataGridViewLockers.Refresh();
		}

		/// <summary>
		/// Handles the Click event of the button2 control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		private void button2_Click(object sender, EventArgs e)
		{
			var tagsToPrint = blLockers.Filter(l => l.PrintTag == true);
			string message = string.Empty;
			
			PrintDocument printDocument = new PrintDocument();
			printDocument.PrintPage += printTags;
			PrintDialog printDialog = new PrintDialog();
			PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
			printPreviewDialog.Document = printDocument;
			DialogResult result = printPreviewDialog.ShowDialog();
			// printDocument.Print();
			
			
			
			foreach (var tag in tagsToPrint)
			{
				message += tag.FirstName + " " + tag.LastName + " - " + tag.HasLocker + Environment.NewLine;
			}
			//MessageBox.Show(message);
		}

		/// <summary>
		/// Prints the tags.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		private void printTags(object sender, PrintPageEventArgs e)
		{
			var tagsToPrint = blLockers.Filter(l => l.PrintTag == true);
			Graphics g = e.Graphics;

			// Set the page unit to millimeters
			g.PageUnit = GraphicsUnit.Inch;
			float x = 0.25f;
			float y = 0.25f;
			float width = 3.75f;
			float height = 2f;

			//Locker text rect
			float textRectLockerTop = 0.25f;
			float textRectNameTop = 1.15f;
			float textLockerHeight = 0.625f;
			float textNameHeight = 0.45f;

			int i = 0;
			foreach( var tag in tagsToPrint)
			{
				float x1;
				float y1;
				if (i < 4)
				{
					y1 = y + (i * y) + (i * height);
					x1 = x;
				}
				else
				{
					y1 = y + ((i - 4) * y) + ((i - 4)  * height);
					x1 = 2 * x + width;
				}

				// Create a rectangle object
				RectangleF rectOutter = new RectangleF(x, y, width, height);
				Pen pen = new Pen(Color.Black, 0.01f);
				g.DrawRectangle(pen, x1, y1, rectOutter.Width, rectOutter.Height);


				RectangleF rectLocker = new RectangleF(x1, y1 + textRectLockerTop, width - 0.1f, textLockerHeight);
				Font fontLocker = new Font("Arial", 48, FontStyle.Bold);
				Brush brush = Brushes.Black;
				var format = new StringFormat() { Alignment = StringAlignment.Far };
				g.DrawString(tag.HasLocker, fontLocker, brush, rectLocker, format);


				RectangleF rectName = new RectangleF(x1, y1 + textRectNameTop, width - 0.1f, textNameHeight);
				string name = tag.FirstName + " " + tag.LastName;
				Font fontName;
				if (name.Length > 16)
				{
					fontName = new Font("Arial", 24, FontStyle.Bold);
				}
				else
				{
					fontName = new Font("Arial", 28, FontStyle.Bold);
				}
					g.DrawString(name, fontName, brush, rectName, format);

				i++;
			}
		}

		/// <summary>
		/// Handles the ValueChanged event of the min visits control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <returns></returns>
		private void numericUpDownMinVisits_ValueChanged(object sender, EventArgs e)
		{
			if (numericUpDownMinVisits.Value == 0)
			{
				buttonEmailSlackers.Enabled = false;
			}
			else
			{
				buttonEmailSlackers.Enabled = true;
			}
			LockerForm_Load(null, null);
		}

		private async void buttonEmailSlackers_Click(object sender, EventArgs e)
		{
			SendMail sm = new SendMail();
			foreach (Lockers locker in bsLockers)
			{
				if (locker.PrintTag)
				{
					StringBuilder sb = new StringBuilder("<p>Greetings <firstname> <lastname>, I hope all is well with you. The WoodShop policy is that you must use the shop a minimum of 24 times a year to keep your locker. ");
					sb.AppendLine("Our records indicate that you have been in <visits> <times> since Jan 1st 2024. Please let us know if you are planning on visiting the shop regularly or are planning on giving up your locker.</p>");
					sb.AppendLine();
					sb.AppendLine("<p><b>If on Dec 15 2024 you have not notified us of your circumstances OR visited the shop an appropriate number of times to meet this commitment by the end of the year, you will be asked to vacate your locker.</b></p>");
					sb.Replace("<firstname>", locker.FirstName);
					sb.Replace("<lastname>", locker.LastName);
					sb.Replace("<visits>", locker.ShopVisits);
					if (locker.ShopVisits == "1")
					{
						sb.Replace("<times>", "time");
					}
					else
					{
						sb.Replace("<times>", "times");
					}

					List<EmailAddress> recpts = new List<EmailAddress>();
					recpts.Add(new EmailAddress(locker.Email, locker.FirstName + " " + locker.LastName));
					await sm.SendMailAsync("Woodclub Usage and Lockers", sb.ToString(), recpts);

					//MessageBox.Show(sb.ToString());
				}
			}
		}

		private void dataGridViewLockers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			bool setState = true;
			bool check = true;
			if (e.ColumnIndex == 1)
			{
				foreach (Lockers locker in bsLockers)
				{
					if (setState)
					{
						setState = false;
						check = !locker.PrintTag;
					}

					locker.PrintTag = check;
				}

				dataGridViewLockers.DataSource = bsLockers;
				dataGridViewLockers.Refresh();
			}
		}
	}
}

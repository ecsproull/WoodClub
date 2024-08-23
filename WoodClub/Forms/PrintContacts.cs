using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Form to print the contact list for the entire shop.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class PrintContacts : Form
	{
		private List<MemberRoster> contactList;

		/// <summary>
		/// Initializes a new instance of the <see cref="PrintContacts"/> class.
		/// </summary>
		public PrintContacts()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Handles the Click event for the Excel button.
		/// Export a CSV file and then opens it in Excel.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void openExcel_Click(object sender, EventArgs e)
		{
			string delimter = ",";
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Save CSV File";
			saveFileDialog.FileName = "contacts.csv";
			saveFileDialog.InitialDirectory = saveFileDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
			saveFileDialog.Filter = "CSV files|*.csv";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string filePath = saveFileDialog.FileName;
				try
				{
					int length = contactList.Count();
					string hdr = "First, Last, Phone, Emergency Contact, Emergency Phone";

					using (System.IO.TextWriter writer = File.CreateText(filePath))
					{
						writer.WriteLine(hdr);
						for (int index = 0; index < length; index++)
						{
							MemberRoster contact = contactList[index];
							string csv = contact.FirstName + "," +
										 contact.LastName + "," +
										 contact.Phone + "," +
										 contact.ERContactFirstName + "," +
										 contact.ERContactPhone;
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

		/// <summary>
		/// Handles the Load event of the PrintContacts control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void PrintContacts_Load(object sender, EventArgs e)
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				contactList = (from m in context.MemberRosters.OrderBy(x => x.LastName).ThenBy(x => x.FirstName)
											  where m.Private == false
											  select m).ToList();
				bindingSourceContacts.DataSource = new SortableBindingList<MemberRoster>(contactList);
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
		/// Handles the Click event of the Print button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void buttonPrint_Click(object sender, EventArgs e)
		{
			printContactsDialog.AllowSomePages = true;
			PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
			printPrvDlg.ClientSize = new System.Drawing.Size(800, 1200);

			printDocumentContacts.BeginPrint += PrintRoster_BeginPrint;
			printDocumentContacts.PrintPage += PrintRoster_PrintPage;
			printDocumentContacts.DefaultPageSettings.Landscape = false;

			printPrvDlg.Document = printDocumentContacts;
			printPrvDlg.ShowDialog();
		}

		/// <summary>
		/// Handles the BeginPrint event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Drawing.Printing.PrintEventArgs"/> instance containing the event data.</param>
		private void PrintRoster_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			try
			{
				printDocumentContacts = new PrintDocument
				{
					PrinterSettings =
					{
						Duplex = Duplex.Vertical,
						PrinterName = "Brother MFC-L3770CDW series"
					}
				};

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
				foreach (DataGridViewColumn dgvGridCol in dataGridView1.Columns)
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

		int pageCount = 0;

		/// <summary>
		/// Handles the PrintPage event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
		private void PrintRoster_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
					foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
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
				while (iRow <= dataGridView1.Rows.Count - 1)
				{
					DataGridViewRow GridRow = dataGridView1.Rows[iRow];
					//Set the cell height
					cellHeight = GridRow.Height + 5;
					int iCount = 0;
					//Check whether the current page settings allows more rows to print
					if (iTopMargin + cellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
					{
						newPage = true;
						firstPage = false;
						bMorePagesToPrint = true;
						pageCount++;
						break;
					}
					else
					{
						if (newPage)
						{
							//Draw Header
							e.Graphics.DrawString("Contact List",
								new Font(dataGridView1.Font, FontStyle.Bold),
								Brushes.Black, e.MarginBounds.Left,
								e.MarginBounds.Top - e.Graphics.MeasureString("Contact",
								new Font(dataGridView1.Font, FontStyle.Bold),
								e.MarginBounds.Width).Height - 13);

							String strDate = DateTime.Now.ToLongDateString() + " " +
								DateTime.Now.ToShortTimeString();
							//Draw Date
							e.Graphics.DrawString(strDate,
								new Font(dataGridView1.Font, FontStyle.Bold), Brushes.Black,
								e.MarginBounds.Left +
								(e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
								new Font(dataGridView1.Font, FontStyle.Bold),
								e.MarginBounds.Width).Width),
								e.MarginBounds.Top - e.Graphics.MeasureString(strDate,
								new Font(new Font(dataGridView1.Font, FontStyle.Bold),
								FontStyle.Bold), e.MarginBounds.Width).Height - 13);

							//Draw Columns                 
							iTopMargin = e.MarginBounds.Top;
							int coll = 0;
							foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
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
				if (bMorePagesToPrint && pageCount < 5)
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
		/// Handles the event of the Print button.
		/// TODO: figure out why we have a extra call here?
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void buttonPrint_Click_1(object sender, EventArgs e)
		{
			buttonPrint_Click(null, null);
		}
	}
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	public partial class LockerRpt : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                 (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static List<Lockers> DSlocker = new List<Lockers>();
        private static SortableBindingList<Lockers> blLockers = new SortableBindingList<Lockers>(DSlocker);
        private static BindingSource bsLockers = new BindingSource();
        private static List<Transaction> DStransaction = new List<Transaction>();
        private static Lockers currentLocker = null;
        private int year;
        private int visitsCnt;
        public LockerRpt()
        {
            InitializeComponent();
            bsLockers.DataSource = blLockers;
            dataGridViewLockers.DataSource = bsLockers;
            bsLockers.PositionChanged += BsLockers_PositionChanged;
            year = DateTime.Now.Year;
        }

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

        private void LockerForm_Load(object sender, EventArgs e)
        {
            textBoxLockerFilter.KeyUp += TextBoxLockerFilter_KeyUp;
            int totalRevenue = 0;
            using (WoodclubEntities context = new WoodclubEntities())
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

                DSlocker = new List<Lockers>();
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

                    DSlocker.Add(locker);
                    totalRevenue += member.Cost.Value;

                }

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

                    DSlocker.Add(locker);
                }
            }

            blLockers = new SortableBindingList<Lockers>(DSlocker);
            bsLockers.DataSource = blLockers;
            dataGridViewLockers.DataSource = bsLockers;
            bsLockers.Position = 0;
            dataGridViewLockers.Refresh();
            dataGridViewLockers.Invalidate();
            textBoxTotalRevenue.Text = String.Format("${0}", totalRevenue.ToString("N0"));
			dataGridViewLockers.CellContentClick += DataGridViewLockers_CellContentClick;
        }

		private void DataGridViewLockers_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                string badge = dataGridViewLockers.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (!string.IsNullOrEmpty(badge))
                {
                    Editor ed = new Editor(badge);
                    if (ed.ShowDialog() == DialogResult.OK)
                    {
                        MainMembers.lockersUpdated = true;
                    }
                }
            }
		}

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
                    int length = DSlocker.Count();
                    string hdr = "Badge,First,Last,Club Dues Paid,Shop Visits,Credit Bank, Last Day Valid, Locker, Cost, Location";

                    using (System.IO.TextWriter writer = File.CreateText(filePath))
                    {
                        writer.WriteLine(hdr);
                        for (int index = 0; index < length; index++)
                        {
                            Lockers locker = DSlocker[index];
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

        private void buttonPrint_Click(object sender, EventArgs e)
        {
			printDialogLockers.AllowSomePages = true;
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
            printPrvDlg.ClientSize = new System.Drawing.Size(1200, 800);
            
			printLockerReport.BeginPrint += PrintLockerReport_BeginPrint;
			printLockerReport.PrintPage += PrintLockerReport_PrintPage;
            printLockerReport.DefaultPageSettings.Landscape = true;

            printPrvDlg.Document = printLockerReport;
            printPrvDlg.ShowDialog();
        }

		private void PrintLockerReport_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            try
            {
				columnSkip.Add(3);
				columnSkip.Add(4);
				columnSkip.Add(5);
				columnSkip.Add(10);

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

                //Loop till all the grid rows not get printed
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
    }
}

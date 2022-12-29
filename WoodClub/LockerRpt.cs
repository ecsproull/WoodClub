using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
                        Location = location.Description
                    })
                .OrderBy(x => x.Badge).ToList();

                DSlocker = new List<Lockers>();
                foreach (var member in lmcl)
                {
                    //member = context.MemberRosters.Find(_id);
                    Lockers locker = new Lockers();
                    locker.Badge = member.Badge;
                    locker.FirstName = member.FirstName;
                    locker.LastName = member.LastName;
                    locker.Email = member.Email;
                    locker.Phone = member.Phone;
                    locker.ClubDuesPaid = (bool)member.ClubDuesPaid;
                    locker.CreditBank = member.CreditBank.ToString();
                    locker.LastDayValid = member.LastDayValid;
                    locker.HasLocker = member.Locker;
                    var yearvisit = from t in context.Transactions              // List of Usage by member
                                    where t.TransDate.Value.Year == year
                                         && t.Code == "U" | t.Code == "FD"
                                         && t.Badge == member.Badge
                                    select t.TransDate.Value;
                    visitsCnt = yearvisit.DistinctBy(x => x.DayOfYear).Count();
                    locker.ShopVisits = visitsCnt.ToString();
                    locker.Cost = member.Cost.Value;
                    locker.Location = member.Location;
                    DSlocker.Add(locker);
                    totalRevenue += member.Cost.Value;

                }

                List<Locker> emptyLockers = (from l in context.Lockers
                                             where l.Badge == string.Empty
                                             select l).ToList();

                foreach (var el in emptyLockers)
                {
                    //member = context.MemberRosters.Find(_id);
                    Lockers locker = new Lockers();
                    locker.Badge = el.Badge;
                    locker.FirstName = string.Empty;
                    locker.LastName = string.Empty;
                    locker.Email = string.Empty;
                    locker.Phone = string.Empty;
                    locker.ClubDuesPaid = false;
                    locker.CreditBank = string.Empty;
                    locker.LastDayValid = string.Empty;
                    locker.HasLocker = el.LockerTitle;
                    var yearvisit = string.Empty;
                    visitsCnt = 0;
                    locker.ShopVisits = string.Empty;
                    locker.Cost = 0;
                    locker.Location = el.LocationCode;
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
                Editor ed = new Editor(badge);
                if (ed.ShowDialog() == DialogResult.OK)
                {
                    Form1.lockersUpdated = true;
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
            string filePath = pathDesktop + "\\monitor.csv";
            string delimter = ",";
            SaveFileDialog theDialog = new SaveFileDialog();
            theDialog.Title = "Save CSV File";
            theDialog.FileName = "lockers.csv";
            theDialog.InitialDirectory = theDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
            theDialog.Filter = "CSV files|*.csv";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = theDialog.FileName;
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

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
			printDialog.AllowSomePages = true;
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();
            printPrvDlg.ClientSize = new System.Drawing.Size(1200, 800);
            printDialog.Document = printLockerReport;
			printLockerReport.BeginPrint += PrintLockerReport_BeginPrint;
			printLockerReport.PrintPage += PrintLockerReport_PrintPage;

            printPrvDlg.Document = printLockerReport;
            printLockerReport.DefaultPageSettings.Landscape = true;
            printPrvDlg.ShowDialog();

            if (DialogResult.OK == printDialog.ShowDialog())
            {
                printLockerReport.DocumentName = "Locker Report";
               // printLockerReport.Print();
            }
        }


        private StringFormat strFormat;
        private List<int> arrColumnLefts = new List<int>();
        private List<int> arrColumnWidths = new List<int>();
        private int iCellHeight;
        private int rowsPrinted = 0;
        private bool bFirstPage;
        private bool bNewPage;
        private int iTotalWidth;
        private List<int> columnSkip = new List<int>();

		private void PrintLockerReport_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
            try
            {
                this.columnSkip.Add(3);
                this.columnSkip.Add(4);
                this.columnSkip.Add(5);
                this.columnSkip.Add(10);

                this.printLockerReport = new System.Drawing.Printing.PrintDocument();
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                bFirstPage = true;
                bNewPage = true;
                rowsPrinted = 0;

                // Calculating Total Widths
                iTotalWidth = 0;
                int col = 0;
                foreach (DataGridViewColumn dgvGridCol in dataGridViewLockers.Columns)
                {
                    if (!columnSkip.Contains(col))
                    {
                        iTotalWidth += dgvGridCol.Width;
                    }
                    col++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int iHeaderHeight = 0;

        private void PrintLockerReport_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    int iTmpWidth = 0;
                    int iLeftMargin = e.MarginBounds.Left;
                    int col = 0;
                    foreach (DataGridViewColumn GridCol in dataGridViewLockers.Columns)
                    {
                        if (!columnSkip.Contains(col))
                        {
                            iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                (double)iTotalWidth * (double)iTotalWidth *
                                ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                            iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                            // Save width and height of headers
                            arrColumnLefts.Add(iLeftMargin);
                            arrColumnWidths.Add(iTmpWidth);
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
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
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
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawRectangle(Pens.Black,
                                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawString(GridCol.HeaderText,
                                        GridCol.InheritedStyle.Font,
                                        new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                    iCount++;
                                }
                                coll++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
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
                                        new RectangleF((int)arrColumnLefts[iCount],
                                        (float)iTopMargin,
                                        (int)arrColumnWidths[iCount], (float)iCellHeight),
                                        strFormat);
                                }
                                //Drawing Cells Borders 
                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iCellHeight));
                                iCount++;
                            }

                            col++;
                        }
                    }
                    rowsPrinted++;
                    iRow++;
                    iTopMargin += iCellHeight;
                }
                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }
        }
    }
}

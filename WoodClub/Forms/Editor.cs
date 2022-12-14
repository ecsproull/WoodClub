using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using WoodClub.Forms;


namespace WoodClub
{
    public partial class Editor : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private FormDirtyTracker formDirtyTracker = null;
        private MemberRoster member { get; set; }
        private bool Adding { get; set; }
        private int currentId;
        private BindingSource bsBadges = new BindingSource();
        private List<BadgeCode> DataSource;
        private List<acc_timeseg> TZdatasource;
        private List<Transaction> DStransactions = new List<Transaction>();
        private bool authorize = false;
        private bool oneTime = false;
        private bool newCredit = false;     // Credit values trigger transaction
        private double creditBankStart = 0.0;
        private Dictionary<string, TransactionAddition> creditTransactions = new Dictionary<string, TransactionAddition>();
        private bool newAccess = false;
        private bool newBadge = false;      // New Badge Request
        private Byte[] bArray = null;
        private string entCode = "FS";      // Default
        private string today = DateTime.Now.Date.ToShortDateString();
        private int TZaccess = 0;
        
        public Editor(MemberRoster member)
        {
            InitializeComponent();
            this.member = member;
            currentId = member.id;
            var CfgReader = new System.Configuration.AppSettingsReader();
           
            using (ZKTecoEntities zkcontext = new ZKTecoEntities())
            {
                string groupTime = member.GroupTime; 
                if(groupTime == null)
                {
                    groupTime = "Members";
                    member.GroupTime = groupTime;       // Set a default value
                    AccessTime.SelectedItem = "Members";
                }
                try
                {
                    TZdatasource = zkcontext.acc_timeseg.Select(c => c).ToList();
                    foreach (var element in TZdatasource)
                    {
                        if(element.timeseg_name == groupTime)
                        {
                            TZaccess = (int) element.id;
                        }
                    }
                }
                catch (Exception tz)
                {
                    log.Fatal("Unable to load ZKTeco data", tz);
                }
            }
            using (WoodclubEntities context = new WoodclubEntities())
            {
                try
                {
                    DataSource = context.BadgeCodes.Select(c => c).Where(x => x.ShowInUi == 1).ToList();              
                }
                catch (Exception ex)
                {
                    log.Fatal("Unable to get data...", ex);         // Capture exception
                }
            
           
                BindingList<BadgeCode> blBadges = new BindingList<BadgeCode>(DataSource);
                bsBadges.DataSource = blBadges;
                dataGridViewCodes.DataSource = bsBadges.DataSource;
                // Add names to list box
                if(TZdatasource != null)
                {
                    AccessTime.Items.Clear();
                    foreach (acc_timeseg tz in TZdatasource)
                    {
                        AccessTime.Items.Add(tz.timeseg_name);
                        if(member.GroupTime == tz.timeseg_name)
                        {
                            AccessTime.SelectedIndex = AccessTime.Items.Count-1;
                        }  
                    }
                    
                }

                if (string.IsNullOrWhiteSpace(member.Badge))
                {
                    Adding = true;
                    this.Text = "Adding New Member";
                }
                else
                {
                    Adding = false;
                    txtRecCard.Text = member.RecCard;
                    txtBadge.Text = member.Badge;
                    txtFirstNm.Text = member.FirstName;
                    txtLastNm.Text = member.LastName;
                    txtAddress.Text = member.Address;
                    txtState.Text = member.State;
                    txtZip.Text = member.Zip;
                    txtPhone.Text = member.Phone;
                    txtEmail.Text = member.Email;
                    txtTitle.Text = member.Title;
                    cbMain.Checked = EntryCodes(member.EntryCodes, "F");
                    cbSide.Checked = EntryCodes(member.EntryCodes, "S");
                    cbMaint.Checked = EntryCodes(member.EntryCodes, "M");
                    cbLumber.Checked = EntryCodes(member.EntryCodes, "L");
                    cbOffice.Checked = EntryCodes(member.EntryCodes, "O");
                    cbAssembly.Checked = EntryCodes(member.EntryCodes, "A");
                    cbMachine.Checked = EntryCodes(member.EntryCodes, "T");
                    cbExempt.Checked = member.Exempt == null ? false : (bool)member.Exempt;
                    cbClubDuesPaid.Checked = member.ClubDuesPaid == null ? false : (bool)member.ClubDuesPaid;
                    cbRecDuesPaid.Checked = member.RecDuesPaid == null ? false : (bool)member.RecDuesPaid;
                    cbNewBadge.Checked = member.NewBadge == null ? false : (bool)member.NewBadge;
                    cbExtendHr.Checked = member.ExtHour == null ? false : (bool)member.ExtHour;
                    txtJoinDate.Text = member.MemberDate == null ? "" : checkDate(member.MemberDate.Value);
                    txtClubDuesPaid.Text = member.ClubDuesPaidDate == null ? today : member.ClubDuesPaidDate.Value.ToShortDateString();
                    txtLastDay.Text = member.LastDayValid == null ? today : member.LastDayValid.Value.Date.ToShortDateString();
                    txtCredits.Text = member.CreditBank == null ? txtCredits.Text = "0" : member.CreditBank;
                    txtRFcard.Text = member.CardNo == null ? txtRFcard.Text = "" : member.CardNo;
                    authorize = member.Authorized == null ? false : (bool)member.Authorized;
                    oneTime = member.OneTime == null ? false : (bool)member.OneTime;
                    creditBankStart = Convert.ToDouble(txtCredits.Text);
                    PopulateLockers();
                    AuthorizedIndex();
                    //
                    // Show picture if it exist
                    if (member.Photo != null && member.Photo.Length > 0)
                    {
                        bArray = member.Photo;      // Save for later
                        MemoryStream ms = new MemoryStream(bArray);
                        Image img = Image.FromStream(ms);
                        Image newImage = ScaleImage(img, 200, 200);
                        pictureBox1.Image = newImage;
                    }
                }
            }
            populateTransactions();
            populateNewCredits();
            formDirtyTracker = new FormDirtyTracker(this);
            formDirtyTracker.MarkAsClean();
            AssignHandlersForControlCollection(this.Controls);

        }

        private void PopulateLockers()
        {
            using (WoodclubEntities context = new WoodclubEntities())
            {
                List<Locker> lockers = (from l in context.Lockers
                                        where l.Badge == member.Badge
                                        select l).ToList();
                if (lockers.Count == 0)
                {
                    txtLocker.Text = "";
                }
                else
                {
                    bool first = true;
                    foreach (Locker locker in lockers)
                    {
                        if (first)
                        {
                            txtLocker.Text = locker.LockerTitle.Trim();
                            first = false;
                        }
                        else
                        {
                            txtLocker.Text += "," + locker.LockerTitle;
                        }
                    }
                }
            }
        }
        //
        //      Get current Access Time index
        //
        private void AuthorizedIndex()
        {
            if (TZdatasource != null)
            {
                string group = member.GroupTime;
                int i = 0;
                foreach (acc_timeseg tz in TZdatasource)
                {
                    if (group == tz.timeseg_name)
                    {
                        AccessTime.SelectedIndex = i;
                        AccessTime.SetSelected(i, true);
                        member.AuthorizedTimeZone = tz.id;
                        TZaccess = tz.id;
                    }
                    i++;
                }
            }
        }
        //
        //      Date Check and conversion
        //
        private string checkDate(DateTime dt)
        {
            string result = DateTime.Now.ToShortDateString();
            if(dt != null)
            {
                result = dt.ToShortDateString();
            }
            return result;
        }
        //
        //  Entry codes represented in DB F=front, S=Side, M=Maintenance, L=Lumber
        //  Second controller O=office, A=assembly room rear, T=(tool / Machine Rear)
        //
        private bool EntryCodes(string fsml, string code)
        {
            bool result = false;
            if (fsml != null)
            { 
                result = fsml.Contains(code);
            }
            return result;
        }
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
        private static DateTime convStringDate(String d)
        {
            DateTime dt = DateTime.Now.Date;
            if (d != null)
            {
                try
                { 
                    dt = DateTime.Parse(d);
                }
                catch
                {
                    dt = DateTime.Now.Date;
                }
            }
            return dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            applyChanges();
            DialogResult = DialogResult.OK;
            return;
        }

        private void applyChanges()
        {
            Members newMember = new Members();
            MemberRoster member = fillRecord();
            member.id = currentId;

            if (TZaccess == 0)
            {
                MessageBox.Show("Please select members access time!");
                return;
            }

            if (Adding)
            {
                newMember.OneTime = true;
                newMember.AddNew(member);
                using (WoodclubEntities context = new WoodclubEntities())
                {
                    if (newBadge)        // New Badge Request
                    {
                        if (member.Photo != null)    // no photo - no badge
                        {
                            MemberRFcard mrfc = new MemberRFcard();
                            mrfc.Badge = member.Badge;
                            mrfc.FirstName = member.FirstName;
                            mrfc.LastName = member.LastName;
                            mrfc.Title = member.Title;
                            mrfc.Photo = member.Photo;
                            context.MemberRFcards.Add(mrfc);
                            context.SaveChanges();
                        }
                        else
                        {
                            cbNewBadge.Checked = false;
                            member.NewBadge = false;
                            MessageBox.Show("New Badge requires photo!");
                        }

                    }
                }
                DialogResult = DialogResult.OK;
            }
            else
            {
                using (WoodclubEntities context = new WoodclubEntities())
                {
                    if (newCredit)
                    {
                        if (newMember.UpdateMember(member))
                        {
                            context.SaveChanges();

                            foreach (KeyValuePair<string, TransactionAddition> trans in creditTransactions)
                            {
                                Transaction creditTransaction = new Transaction();
                                creditTransaction.Badge = txtBadge.Text;
                                creditTransaction.Code = trans.Value.Code;
                                creditTransaction.EventType = trans.Value.EventType;
                                creditTransaction.CreditAmt = trans.Value.TotalAmount;
                                creditTransaction.RecCard = txtRecCard.Text;
                                creditTransaction.TransDate = DateTime.Now;
                                context.Transactions.Add(creditTransaction);
                            }

                            context.SaveChanges();
                        }
                    }

                    if (newAccess)
                    {
                        if (AccessTime.SelectedItem != null)
                        {
                            member.GroupTime = AccessTime.SelectedItem.ToString();
                            AuthorizedIndex();
                        }
                        else
                        {
                            MessageBox.Show("Please select members access time.");
                            return;
                        }
                        member.AuthorizedTimeZone = TZaccess;
                        member.EntryCodes = getFSML();
                        DoorUpdate();
                        // Modifing record
                        if (!newMember.UpdateMember(member))
                        {
                            MessageBox.Show("Update failed!");
                        }
                    }
                    if (newBadge)        // New Badge Request
                    {
                        if (member.Photo != null)
                        {
                            MemberRFcard mrfc = (from mf in context.MemberRFcards
                                                 where mf.Badge == member.Badge
                                                 select mf).FirstOrDefault();
                            if (mrfc == null)
                            {
                                mrfc = new MemberRFcard();
                                mrfc.Badge = member.Badge;
                                mrfc.FirstName = member.FirstName;
                                mrfc.LastName = member.LastName;
                                mrfc.Title = member.Title;
                                mrfc.Photo = member.Photo;
                                mrfc.RecCard = member.RecCard;
                                context.MemberRFcards.Add(mrfc);
                            }
                            else
                            {
                                mrfc.Badge = member.Badge;
                                mrfc.FirstName = member.FirstName;
                                mrfc.LastName = member.LastName;
                                mrfc.Title = member.Title;
                                mrfc.Photo = member.Photo;
                                mrfc.RecCard = member.RecCard;
                            }

                            var mem = (from mm in context.MemberRosters
                                       where mm.Badge == member.Badge
                                       select mm).FirstOrDefault();
                            mem.NewBadge = false;
                            context.SaveChanges();
                        }
                        else
                        {
                            cbNewBadge.Checked = false;
                            member.NewBadge = false;
                            MessageBox.Show("New Badge requires photo!");
                        }
                    }
                }
            }
        }

        //
        //  Fills MemberRoster record with form data
        //
        private MemberRoster fillRecord()
        {
            DateTime today = new DateTime();
            today = DateTime.Now.Date;               // Use as default if date not available         
            MemberRoster member = new MemberRoster();
            member.Badge = txtBadge.Text == null | txtBadge.Text == "" ? "0000" : txtBadge.Text;
            member.RecCard = txtRecCard.Text;
            member.FirstName = txtFirstNm.Text;
            member.LastName = txtLastNm.Text;
            member.Address = txtAddress.Text;
            member.State = txtState.Text;
            member.Zip = txtZip.Text;
            member.Phone = txtPhone.Text;
            member.Email = txtEmail.Text;
            member.Title = txtTitle.Text;
            member.Locker = txtLocker.Text;
            member.RecCard = txtRecCard.Text;
            member.CreditBank = txtCredits.Text;
            member.CardNo = txtRFcard.Text == null ? "" : txtRFcard.Text;
            member.MemberDate = txtJoinDate.Text == null ? today : (txtJoinDate.Text == "" ? today :convStringDate(txtJoinDate.Text));
            member.ExemptModDate = txtExemptDate.Text == null ? today : (txtExemptDate.Text == "" ? today :convStringDate(txtExemptDate.Text));
            member.LastDayValid = txtLastDay.Text == null ? today : (txtLastDay.Text == "" ? today : convStringDate(txtLastDay.Text));
            member.ClubDuesPaidDate = txtClubDuesPaid.Text == null ? today : (txtClubDuesPaid.Text == "" ? today : convStringDate(txtClubDuesPaid.Text));
            member.Exempt = cbExempt.Checked;
            member.ClubDuesPaid = cbClubDuesPaid.Checked;
            member.RecDuesPaid = cbRecDuesPaid.Checked;
            member.NewBadge = cbNewBadge.Checked;
            member.ExtHour = cbExtendHr.Checked;
            member.Authorized = authorize;          // Flag current state
            member.AuthorizedTimeZone = TZaccess;
            member.OneTime = oneTime;
            member.EntryCodes = getFSML();
            member.Photo = bArray;
            return member;
        }
        //
        //  Converts values of check boxes Maintenance & Lumber return update string
        //
        private string getFSML()
        {
            string result = "";
            if(cbMain.Checked)
            {
                result = "F";
            }
            if (cbSide.Checked)
            {
                result += "S";
            }
            if(cbMaint.Checked)
            {
                result += "M";
            }
            if(cbLumber.Checked)
            {
                result += "L";
            }
            if(cbOffice.Checked)
            {
                result += "O";
            }
            if(cbAssembly.Checked)
            {
                result += "A";
            }
            if(cbMachine.Checked)
            {
                result += "T";
            }
            entCode = result;           // Save for transaction record
            return result;
        }

        private new void TextChanged(object sender, EventArgs e)
        {
            newAccess = true;
        }

        private void btnLoadPhoto_Click(object sender, EventArgs e)
        {
            Stream flStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Image (.jpg) File";
            theDialog.Filter = "JPG files|*.jpg";
            theDialog.InitialDirectory = theDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Images";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((flStream = theDialog.OpenFile()) != null)
                    {
                        using (flStream)
                        {
                            Image img = Image.FromStream(flStream);
                            Image newImage = ScaleImage(img, 200, 200);
                            pictureBox1.Image = newImage;
                            Image newImg = ScaleImage(img, 800, 420);
                            
                            using (var myStream = new MemoryStream())
                            {
                                newImg.Save(myStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                bArray = myStream.ToArray();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void dataGridViewCodes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            log.Info("Credits selected");
            float credit = float.Parse(txtCredits.Text);
            float value = 12;
            DataGridViewRow selectedRow = dgv.SelectedRows[0];
            string value1 = selectedRow.Cells[0].Value.ToString();
            string value2 = selectedRow.Cells[1].Value.ToString();
            string value3 = selectedRow.Cells[2].Value.ToString();

            credit += float.Parse(value3);
            value = credit > 12 ? 12 : credit;
            authorize = true;
            newCredit = true;

            using (WoodclubEntities context = new WoodclubEntities())
            {
                DateTime dtLimit = DateTime.Now.AddDays(-3);
                var previousEntry = (from m in context.Transactions             // List of members using club
                                    where member.Badge == m.Badge && m.TransDate >  dtLimit && m.Code == value1
                                select m).OrderByDescending(x => x.TransDate).ToList();
                if (previousEntry.Count > 0)
                {
                    string message = string.Format("This member has had {0} - {1} entries in the last 3 days.", previousEntry.Count.ToString(), value1);
                    message += Environment.NewLine + "Was this intentional?";
                    DialogResult dialogResult = MessageBox.Show(message, "Double Entry Check", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            if (creditTransactions.ContainsKey(value1))
            {
                DialogResult dialogResult = MessageBox.Show("Did you intend to add multiple " + value1 + " credits?", "Double Entry Check", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    creditTransactions[value1].TotalAmount += float.Parse(value3);
                    txtCredits.Text = value.ToString();
                }
            }
            else
            {
                creditTransactions.Add(value1, new TransactionAddition(value1, value2, float.Parse(value3)));
                txtCredits.Text = value.ToString();
            }

            populateNewCredits();
        }

        private void txtRFcard_TextChanged(object sender, EventArgs e)
        {
            newAccess = true;
        }

        private void AccessTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            log.Info("New Timezone");
            int select = AccessTime.SelectedIndex;
            string text = AccessTime.GetItemText(AccessTime.SelectedItem);
            if(member != null)
            {
                member.GroupTime = text;
                newAccess = true;
            }
        }

        private void cbNewBadge_CheckedChanged(object sender, EventArgs e)
        {
            newBadge = cbNewBadge.Checked;        
        }

        private void populateNewCredits()
        {
            GridViewNewCredits.Rows.Clear();
            GridViewNewCredits.ColumnCount = 3;
            GridViewNewCredits.Columns[0].Name = "Code";
            GridViewNewCredits.Columns[1].Name = "Description";
            GridViewNewCredits.Columns[2].Name = "Value";
            GridViewNewCredits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach(KeyValuePair<string, TransactionAddition> kvp in creditTransactions)
            {
                string[] row = new string[] { kvp.Value.Code, kvp.Value.EventType, kvp.Value.TotalAmount.ToString() };
                GridViewNewCredits.Rows.Add(row);
            }
        }

        private void populateTransactions()
        {
            using (WoodclubEntities context = new WoodclubEntities())
            {
                try
                {
                    TransDataGridView.Rows.Clear();
                    var activity = (from m in context.Transactions             // List of members using club
                                    where m.Badge == member.Badge
                                    select m).OrderByDescending(x => x.TransDate);

                    TransDataGridView.ColumnCount = 4;
                    TransDataGridView.Columns[0].Name = "Action";
                    TransDataGridView.Columns[1].Name = "Code";
                    TransDataGridView.Columns[2].Name = "Credits";
                    TransDataGridView.Columns[3].Name = "Date/Time";
                    TransDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    foreach (Transaction t in activity)
                    {
                        Activity ac = new Activity();
                        ac.Code = t.Code;
                        ac.Credits = t.CreditAmt.ToString();
                        if (t.EventType == "Door 1")
                        {
                            ac.Event = "Main Door";
                        }
                        else if (t.EventType == "Door 2")
                        {
                            ac.Event = "Assembly Door";
                        }
                        else if (t.EventType == "Door 3")
                        {
                            ac.Event = "Maintenace Door";
                        }
                        else if (t.EventType == "Door 4")
                        {
                            ac.Event = "Lumber Rm Door";
                        }
                        else
                        {
                            ac.Event = t.EventType;
                        }
                        ac.dateTime = (DateTime)t.TransDate;
                        string[] row = new string[] { ac.Event, ac.Code, ac.Credits, ac.dateTime.ToString() };
                        if (creditsOnlyChkbx.Checked)
                        {
                            if (ac.Code != "U")
                            {
                                TransDataGridView.Rows.Add(row);
                            }
                        }
                        else
                        {
                            TransDataGridView.Rows.Add(row);
                        }
                    }
                    log.Info("debug");
                }
                catch (Exception ex)
                {
                    log.Fatal("Unable to get data...", ex);         // Capture exception
                }
            }
        }
        /*
            Handle ALL doors on controllers - EntryMonitor not needed
            Connected to second controller on startup
        */
        private void DoorUpdate()
        {
            int PORT = 5724;
            string codes = getFSML();
            member.EntryCodes = codes;
            string enable = txtBadge.Text + "," + txtRFcard.Text + "," + codes + "," + TZaccess.ToString();
            String dataOut = "[," + enable + ",]";
            log.Info("Enabled: " + enable);
            byte[] bytesOut1 = Encoding.ASCII.GetBytes(dataOut);
            UdpClient udpClient = new UdpClient();
            udpClient.Send(bytesOut1, bytesOut1.Length, "255.255.255.255", PORT);
        }

        private void creditsOnlyChkbx_CheckedChanged(object sender, EventArgs e)
        {
            populateTransactions();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            creditTransactions = new Dictionary<string, TransactionAddition>();
            txtCredits.Text = creditBankStart.ToString();
            populateNewCredits();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; 
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            applyChanges();
            DialogResult = DialogResult.Yes;
        }

        // event handlers
        private void GenericChangedHandler(object sender, EventArgs e)
        {
            SetBackgroundColor(sender as Control);
        }

        private void SetBackgroundColor(Control c)
        {
            bool resetBackColor = false;
            if (formDirtyTracker.IsDirty)
            {
                if (formDirtyTracker.GetListOfDirtyControls().Contains(c))
                {
                    c.BackColor = Color.Yellow;
                }
                else
                {
                    resetBackColor = true;
                }
            }
            else
            {
                resetBackColor = true;
            }

            if(resetBackColor)
            {
                formDirtyTracker.resetControlBackColor(c);
            }
        }

        private void AssignHandlersForControlCollection(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is TextBox)
                    (c as TextBox).TextChanged
                      += new EventHandler(GenericChangedHandler);

                if (c is CheckBox)
                    (c as CheckBox).CheckedChanged
                      += new EventHandler(GenericChangedHandler);

                if (c is ListBox)
                    (c as ListBox).SelectedIndexChanged
                      += new EventHandler(GenericChangedHandler);

                if (c.HasChildren)
                    AssignHandlersForControlCollection(c.Controls);
            }
        }

		private void editLocker_Click(object sender, EventArgs e)
		{
            using (LockerSelection frm = new LockerSelection(member.Badge))
            {
                frm.ShowDialog();
                PopulateLockers();
            }
        }
	}
}

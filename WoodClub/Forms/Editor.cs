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
        private string badge = string.Empty;
        private bool Adding { get; set; }
        private BindingSource bsBadges = new BindingSource();
        private List<BadgeCode> DataSource;
        private List<acc_timeseg> TZdatasource;
        private List<string> controlsTriggerUpdate = new List<string>();
        private bool newCredit = false;     // Credit values trigger transaction
        private double creditBankStart = 0.0;
        private Dictionary<string, TransactionAddition> creditTransactions = new Dictionary<string, TransactionAddition>();
        private bool updateController = false;
        private int TZaccess = 0;
        private WoodclubEntities context;
        
        public Editor(string badge)
        {
            InitializeComponent();
            this.badge = badge;
            this.controlsTriggerUpdate.Add("cbUpdateControllers");
            this.controlsTriggerUpdate.Add("cbExempt");
            this.controlsTriggerUpdate.Add("txtRFcard");
            this.controlsTriggerUpdate.Add("cbClubDuesPaid");
            this.controlsTriggerUpdate.Add("txtCredits");
            this.controlsTriggerUpdate.Add("txtLastDay");
            this.controlsTriggerUpdate.Add("cbExtendHr");
            this.controlsTriggerUpdate.Add("cbMain");
            this.controlsTriggerUpdate.Add("cbSide");
            this.controlsTriggerUpdate.Add("cbLumber");
            this.controlsTriggerUpdate.Add("cbMaint");
            this.controlsTriggerUpdate.Add("cbOffice");
            this.controlsTriggerUpdate.Add("cbAssembly");
            this.controlsTriggerUpdate.Add("cbMachine");
            this.controlsTriggerUpdate.Add("AccessTime");
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            this.context = new WoodclubEntities();
            if (this.badge != null)
            {
                this.member = (from m in context.MemberRosters
                               where m.Badge == badge
                               select m).FirstOrDefault();
            }
            else
            {
                this.member = new MemberRoster();
            }

            this.memberBindingSource.DataSource = member;

            using (ZKTecoEntities zkcontext = new ZKTecoEntities())
            {
                string groupTime = member.GroupTime;
                if (groupTime == null)
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
                        if (element.timeseg_name == groupTime)
                        {
                            TZaccess = (int)element.id;
                        }
                    }
                }
                catch (Exception tz)
                {
                    log.Fatal("Unable to load ZKTeco data", tz);
                }
            }

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
            if (TZdatasource != null)
            {
                AccessTime.Items.Clear();
                foreach (acc_timeseg tz in TZdatasource)
                {
                    AccessTime.Items.Add(tz.timeseg_name);
                    if (member.GroupTime == tz.timeseg_name)
                    {
                        AccessTime.SelectedIndex = AccessTime.Items.Count - 1;
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
                cbMain.Checked = EntryCodes(member.EntryCodes, "F");
                cbSide.Checked = EntryCodes(member.EntryCodes, "S");
                cbMaint.Checked = EntryCodes(member.EntryCodes, "M");
                cbLumber.Checked = EntryCodes(member.EntryCodes, "L");
                cbOffice.Checked = EntryCodes(member.EntryCodes, "O");
                cbAssembly.Checked = EntryCodes(member.EntryCodes, "A");
                cbMachine.Checked = EntryCodes(member.EntryCodes, "T");

                creditBankStart = Convert.ToDouble(member.CreditBank == String.Empty ? "0" : member.CreditBank);
                AuthorizedIndex();
                //
                // Show picture if it exist
                if (member.Photo != null && member.Photo.Length > 0)
                {
                    var bArray = member.Photo;      // Save for later
                    MemoryStream ms = new MemoryStream(bArray);
                    Image img = Image.FromStream(ms);
                    Image newImage = ScaleImage(img, 200, 200);
                    pictureBox1.Image = newImage;
                }
            }


            populateTransactions();
            populateNewCredits();
            AssignHandlersForControlCollection(this.Controls);

            if (sender == null) // we are reloading the page after an edit.
            {
                List<Control> controls = this.formDirtyTracker.GetListOfDirtyControls();
                foreach (Control control in controls)
                {
                    this.formDirtyTracker.resetControlBackColor(control);
                }
            }

            if (formDirtyTracker == null)
            {
                this.formDirtyTracker = new FormDirtyTracker(this);
            }

            this.txtRFcard.TextChanged += TxtRFcard_TextChanged;
            this.formDirtyTracker.MarkAsClean();
            this.updateController = false;
        }

		private void TxtRFcard_TextChanged(object sender, EventArgs e)
		{
			this.txtRFcard.Text = txtRFcard.Text.TrimStart('0');
		}

		private void PopulateLockers()
        {
            List<Locker> lockers = (from l in this.context.Lockers
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
                        this.member.Locker = locker.LockerTitle.Trim();
                        first = false;
                    }
                    else
                    {
                        this.member.Locker += "," + locker.LockerTitle;
                    }
                }

                this.memberBindingSource.ResetCurrentItem();
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

        private void applyChanges()
        {
            List<Control> controls = this.formDirtyTracker.GetListOfDirtyControls();
            foreach (Control control in controls)
            {
                if (this.controlsTriggerUpdate.Contains(control.Name))
                {
                    if (control.Name == "txtCredits")
                    {
                        DateTime today = DateTime.Now.Date;
                        string lastDayValid = txtLastDay.Text;
                        string[] parts = lastDayValid.Split('/');
                        DateTime ldv = new DateTime(Convert.ToInt32(parts[2]), 
                            Convert.ToInt32(parts[0]), 
                            Convert.ToInt32(parts[1]));

                        if (today > ldv)
                        {
                            updateController = true;
                        }
                    }
                    else
                    {
                        updateController = true;
                    }
                }
            }

            string entryCodes = getDoorEntryCodes();
            
            if (TZaccess == 0)
            {
                MessageBox.Show("Please select members access time!");
                return;
            }

            if (Adding)
            {
                this.member.OneTime = true;
                if (this.member.NewBadge.Value)        // New Badge Request
                {
                    if (member.Photo != null)    // no photo - no badge
                    {
                        MemberRFcard mrfc = new MemberRFcard();
                        mrfc.Badge = member.Badge;
                        mrfc.FirstName = member.FirstName;
                        mrfc.LastName = member.LastName;
                        mrfc.Title = member.Title;
                        mrfc.Photo = member.Photo;
                        this.context.MemberRFcards.Add(mrfc);
                        this.member.NewBadge = false;
 
                    }
                    else
                    {
                        cbNewBadge.Checked = false;
                        member.NewBadge = false;
                        MessageBox.Show("New Badge requires photo!");
                    }
                }
            }
            else
            {
                if (newCredit)
                {
                    foreach (KeyValuePair<string, TransactionAddition> trans in creditTransactions)
                    {
                        Transaction creditTransaction = new Transaction();
                        creditTransaction.Badge = txtBadge.Text;
                        creditTransaction.Code = trans.Value.Code;
                        creditTransaction.EventType = trans.Value.EventType;
                        creditTransaction.CreditAmt = trans.Value.TotalAmount;
                        creditTransaction.RecCard = txtRecCard.Text;
                        creditTransaction.TransDate = DateTime.Now;
                        this.context.Transactions.Add(creditTransaction);
                    }
                }

                if (updateController)
                {
                    MessageBox.Show("Controller Updating");
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
                    member.EntryCodes = getDoorEntryCodes();
                    DoorUpdate();
                }

                if (this.member.NewBadge.Value)        // New Badge Request
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
                            this.context.MemberRFcards.Add(mrfc);
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

                        this.member.NewBadge = false;
                    }
                    else
                    {
                        cbNewBadge.Checked = false;
                        member.NewBadge = false;
                        MessageBox.Show("New Badge requires photo!");
                    }
                }
            }

            this.context.SaveChanges();
        }


        //
        //  Converts values of check boxes Maintenance & Lumber return update string
        //
        private string getDoorEntryCodes()
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

            return result;
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
                                this.member.Photo = myStream.ToArray();
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
            DataGridViewRow selectedRow = dgv.SelectedRows[0];
            string value1 = selectedRow.Cells[0].Value.ToString();
            string value2 = selectedRow.Cells[1].Value.ToString();
            string value3 = selectedRow.Cells[2].Value.ToString();

            credit += float.Parse(value3);
            float value = credit > 12 ? 12 : credit;
            newCredit = true;

            DateTime dtLimit = DateTime.Now.AddDays(-3);
            var previousEntry = (from m in this.context.Transactions             // List of members using club
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

            if (creditTransactions.ContainsKey(value1))
            {
                DialogResult dialogResult = MessageBox.Show("Did you intend to add multiple " + value1 + " credits?", "Double Entry Check", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    creditTransactions[value1].TotalAmount += float.Parse(value3);
                    //txtCredits.Text = value.ToString();
                    this.member.CreditBank = value.ToString();
                    this.memberBindingSource.ResetCurrentItem();
                }
            }
            else
            {
                creditTransactions.Add(value1, new TransactionAddition(value1, value2, float.Parse(value3)));
                //txtCredits.Text = value.ToString();
                this.member.CreditBank = value.ToString();
                this.memberBindingSource.ResetCurrentItem();
            }

            populateNewCredits();
        }

        private void AccessTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            log.Info("New Timezone");
            int select = AccessTime.SelectedIndex;
            string text = AccessTime.GetItemText(AccessTime.SelectedItem);
            if(member != null)
            {
                member.GroupTime = text;
            }
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
            try
            {
                TransDataGridView.Rows.Clear();
                var activity = (from m in this.context.Transactions             // List of members using club
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

        private void DoorUpdate()
        {
            int PORT = 5724;
            string codes = getDoorEntryCodes();
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


        // event handlers
        private void GenericChangedHandler(object sender, EventArgs e)
        {
            SetBackgroundColor(sender as Control);
        }

        private void SetBackgroundColor(Control c)
        {
            if (formDirtyTracker == null)
                return;

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            applyChanges();
            DialogResult = DialogResult.OK;
            return;
        }

        private bool changesApplied = false;
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (changesApplied)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            applyChanges();
            context.Dispose();

            Editor_Load(null, null);
            changesApplied = true;
        }
    }
}

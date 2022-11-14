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


namespace WoodClub
{
    public partial class Editor : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private MemberRoster member { get; set; }
        private bool Adding { get; set; }
        private int currentId;
        private BindingSource bsBadges = new BindingSource();
        private List<BadgeCode> DataSource;
        private List<acc_timeseg> TZdatasource;
        private List<Transaction> DStransactions = new List<Transaction>();
       // private static BindingSource activityBindingSource = new BindingSource();
        private bool dirty = false;
        private bool authorize = false;
        private bool newCredit = false;     // Credit values trigger transaction
        private Dictionary<string, float> creditTransactions = new Dictionary<string, float>();
        private bool newAccess = false;
        private bool newBadge = false;      // New Badge Request
        private Byte[] bArray = null;
        public string BadgeCode1 { get; set; }
        private string entCode = "FS";      // Default
        private string value1 = "";         // viewgrid Credit Code
        private string value2 = "";         // viewgrid Desciption
        private string value3 = "";         // viewgrid Value
        private string today = DateTime.Now.Date.ToShortDateString();
        private int TZaccess = 0;
        
        public int id { get; set; }
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
                    txtLocker.Text = member.Locker == null ? "" : member.Locker;
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
            Members newMember = new Members();
            MemberRoster member = fillRecord();
            member.id = currentId;
            
            
            
            if(TZaccess == 0)
            {
                MessageBox.Show("Please select members access time!");
                return;
            }
            
            if (Adding)
            {
                newMember.AddNew(member);
                /*
                Admin admin = new Admin();                  // Update controller records via database & entry app
                admin.Badge = txtBadge.Text;
                admin.CardNo = txtRFcard.Text;
                admin.EntryCodes = entCode;
                admin.AuthorizedTimeZone = TZaccess;
                admin.RequestDate = DateTime.Now;
                admin.Action = "A";
                admin.IsDirty = true;
                */
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
                DialogResult = dirty ? DialogResult.OK : DialogResult.Yes;
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

                            foreach(KeyValuePair<string, float> trans in creditTransactions)
                            {
                                Transaction creditTransaction = new Transaction();
                                creditTransaction.Badge = txtBadge.Text;
                                creditTransaction.Code = trans.Key;
                                creditTransaction.EventType = value2;
                                creditTransaction.CreditAmt = trans.Value;
                                creditTransaction.RecCard = txtRecCard.Text;
                                creditTransaction.TransDate = DateTime.Now;
                                context.Transactions.Add(creditTransaction);
                            }

                            context.SaveChanges();
                        }
                    }

                    if (newAccess)
                    {
                        if(AccessTime.SelectedItem != null)
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
                        if(!newMember.UpdateMember(member))
                        {
                            MessageBox.Show("Update failed!");
                        }
                        dirty = true;
                    }
                    if (newBadge)        // New Badge Request
                    {
                        if (member.Photo != null)
                        {
                            MemberRFcard mrfc = new MemberRFcard();
                            mrfc.Badge = member.Badge;
                            mrfc.FirstName = member.FirstName;
                            mrfc.LastName = member.LastName;
                            mrfc.Title = member.Title;
                            mrfc.Photo = member.Photo;
                            mrfc.RecCard = member.RecCard;
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
            }
            DialogResult = dirty ? DialogResult.OK : DialogResult.Yes;
            return;
        }
        private float convFloat(String fl)
        {
            float result = 0;
            float.TryParse(fl, out result);
            return result;
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
            member.OneTime = true;
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
            dirty = true;
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
                        dirty = true;
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
            log.Info("Credits selected");
            float credit = float.Parse(txtCredits.Text);
            float value = 12;
            
            foreach (DataGridViewRow row in dataGridViewCodes.SelectedRows)
            {
                 value1 = row.Cells[0].Value.ToString();
                 value2 = row.Cells[1].Value.ToString();
                 value3 = row.Cells[2].Value.ToString();
            }
            credit += float.Parse(value3);
            value = credit > 12 ? 12 : credit;
            txtCredits.Text = value.ToString();
            authorize = true;
            newCredit = true;

            if (creditTransactions.ContainsKey(value1))
            {
                creditTransactions[value1] += float.Parse(value3);
            }
            else
            {
                creditTransactions.Add(value1, float.Parse(value3));
            }
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
        //
        //  Follow state of checkbox
        // 
        private void cbNewBadge_CheckedChanged(object sender, EventArgs e)
        {
            newBadge = cbNewBadge.Checked;        
        }

        private void transaction_Click(object sender, EventArgs e)
        {
            using (WoodclubEntities context = new WoodclubEntities())
            {
                // By not using a databinding source (aka List) we can click the columns to sort.
                // Easier to find the data in a long list.
                //List<Activity> DStransactions = new List<Activity>();
                try
                {
                    var activity = from m in context.Transactions             // List of members using club
                                      where m.Badge == member.Badge
                                      select m;

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
                        TransDataGridView.Rows.Add(row);
                    }
                    log.Info("debug");
                }
                catch (Exception ex)
                {
                    log.Fatal("Unable to get data...", ex);         // Capture exception
                }
               
                //TransDataGridView.DataSource = DStransactions;
                TransDataGridView.Refresh();
                TransDataGridView.Invalidate();
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
    }
}

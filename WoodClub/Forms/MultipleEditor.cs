using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WoodClub
{
	public partial class MultipleEditor : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private Dictionary<string, TransactionAddition> creditTransactions;
		private List<BadgeCode> DataSource;
		private BindingSource bsBadges;
		private bool newCredit;
		private WoodclubEntities context;
		private SortableBindingList<MultipleEditMember> members;

		public MultipleEditor()
		{
			InitializeComponent();
			this.newCredit = false;
			this.bsBadges = new BindingSource();
			this.creditTransactions = new Dictionary<string, TransactionAddition>();
		}

		private void MultipleEditor_Load(object sender, EventArgs e)
		{
			this.dataGridMultiMember.AutoGenerateColumns = false;
			this.dataGridViewCodesMulti.AutoGenerateColumns = false;
			this.context = new WoodclubEntities();
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
			this.dataGridViewCodesMulti.DataSource = bsBadges.DataSource;
			this.populateNewCredits();
			memberBadgesTextBox.Leave += MemberBadgesTextBox_Leave;
			memberBadgesTextBox.KeyUp += MemberBadgesTextBox_KeyDown;
		}

		private void MemberBadgesTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				LoadMembers();
			}
		}

		private void MemberBadgesTextBox_Leave(object sender, EventArgs e)
		{
			TextBox textBox = sender as TextBox;
			LoadMembers();
		}

		private void LoadMembers()
		{
			List<string> badges = new List<string>(this.memberBadgesTextBox.Text.Split(','));
			var membersForCredits = (from m in context.MemberRosters
									 where badges.Contains(m.Badge)
									 select m).ToList();
			this.members = new SortableBindingList<MultipleEditMember>();
			foreach (var member in membersForCredits)
            {
                Transaction transaction = (from t in context.Transactions
                                           where t.Badge == member.Badge && t.Code != "U" && t.CreditAmt > 0
                                           select t).OrderByDescending(td => td.TransDate).FirstOrDefault();

                int TzAccess = GetAccessTime(member.GroupTime);

                this.members.Add(new MultipleEditMember
                {
                    Badge = member.Badge,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Credits = member.CreditBank,
                    LastCreditAwarded = transaction == null ? "None" : transaction.EventType,
                    TransactionDate = transaction == null ? "None" : transaction.TransDate.Value.Date.ToShortDateString(),
                });

                this.memberBindingSource.DataSource = this.members;
                this.dataGridMultiMember.DataSource = this.memberBindingSource;
            }
        }

        private static int GetAccessTime(string groupTime)
        {
            int TzAccess = 0;
            using (ZKTecoEntities zkcontext = new ZKTecoEntities())
            {
                List<acc_timeseg> TZdatasource = zkcontext.acc_timeseg.Select(c => c).ToList();
                foreach (var element in TZdatasource)
                {
                    if (element.timeseg_name == groupTime)
                    {
                        TzAccess = (int)element.id;
                    }
                }
            }

            return TzAccess;
        }

        private void ApplyChanges()
		{
			if (newCredit)
			{
				foreach (MultipleEditMember member in this.members)
				{
					foreach (KeyValuePair<string, TransactionAddition> trans in creditTransactions)
					{
						DateTime dtLimit = DateTime.Now.AddDays(-3);
						var previousEntry = (from m in this.context.Transactions
											 where member.Badge == m.Badge && m.TransDate > dtLimit && m.Code == trans.Key
											 select m).OrderByDescending(x => x.TransDate).ToList();
						
						if (previousEntry.Count > 0)
						{
							string message = string.Format("{2} {3} has had {0} - {1} entries in the last 3 days.", previousEntry.Count.ToString(), trans.Key, member.FirstName, member.LastName);
							message += Environment.NewLine + "Was this intentional?";
							DialogResult dialogResult = MessageBox.Show(message, "Double Entry Check", MessageBoxButtons.YesNo);
							if (dialogResult == DialogResult.No)
							{
								continue;
							}
						}

						MemberRoster memberRoster = (from m in context.MemberRosters
													 where m.Badge == member.Badge
													 select m).FirstOrDefault();

						bool updateController = memberRoster.LastDayValid < DateTime.Now && float.Parse(memberRoster.CreditBank) < 1 && !memberRoster.OneTime.Value;
						
						Transaction creditTransaction = new Transaction
						{
							Badge = member.Badge,
							Code = trans.Value.Code,
							EventType = trans.Value.EventType,
							CreditAmt = trans.Value.TotalAmount,
							RecCard = memberRoster.RecCard,
							TransDate = DateTime.Now
						};

						this.context.Transactions.Add(creditTransaction);
						
						float creditBank = float.Parse(memberRoster.CreditBank);
						creditBank += trans.Value.TotalAmount;
						creditBank = creditBank > 12 ? 12 : creditBank;
						memberRoster.CreditBank = creditBank.ToString();
						context.SaveChanges();

						if (updateController)
						{
							DoorUpdate(memberRoster);
						}
					}
				}

				LoadMembers();
				btnSave.Enabled = false;
				buttonApply.Enabled = false;
			}
		}

		private void DataGridViewCodes_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			DataGridView dgv = sender as DataGridView;
			DataGridViewRow selectedRow = dgv.SelectedRows[0];
			string value1 = selectedRow.Cells[0].Value.ToString();
			string value2 = selectedRow.Cells[1].Value.ToString();
			string value3 = selectedRow.Cells[2].Value == null ? "0" : selectedRow.Cells[2].Value.ToString();

			newCredit = true;
			if (this.creditTransactions.Count == 0)
			{
				creditTransactions.Add(value1, new TransactionAddition(value1, value2, float.Parse(value3)));
			}
			else
			{
				MessageBox.Show("Only one Credit Type allowed.");
				return;
			}

			populateNewCredits();
		}

		private void populateNewCredits()
		{
			GridViewNewCredits.Rows.Clear();
			GridViewNewCredits.ColumnCount = 3;
			GridViewNewCredits.Columns[0].Name = "Code";
			GridViewNewCredits.Columns[1].Name = "Description";
			GridViewNewCredits.Columns[2].Name = "Value";
			GridViewNewCredits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

			foreach (KeyValuePair<string, TransactionAddition> kvp in creditTransactions)
			{
				string[] row = new string[] { kvp.Value.Code, kvp.Value.EventType, kvp.Value.TotalAmount.ToString() };
				GridViewNewCredits.Rows.Add(row);
			}
		}

		private void DoorUpdate(MemberRoster member)
		{
			int zkAccessTime = GetAccessTime(member.GroupTime);
			int PORT = 5724;
			string enable = member.Badge + "," + member.CardNo + "," + member.EntryCodes.Trim() + "," + zkAccessTime.ToString();
			String dataOut = "[," + enable + ",]";
			log.Info("Enabled: " + enable);
			byte[] bytesOut1 = Encoding.ASCII.GetBytes(dataOut);
			UdpClient udpClient = new UdpClient();
			udpClient.Send(bytesOut1, bytesOut1.Length, "255.255.255.255", PORT);
		}

		private void ButtonClear_Click(object sender, EventArgs e)
		{
			creditTransactions = new Dictionary<string, TransactionAddition>();
			//txtCredits.Text = creditBankStart.ToString();
			populateNewCredits();
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			ApplyChanges();
			DialogResult = DialogResult.OK;
			return;
		}

		private bool changesApplied = false;
		private void ButtonCancel_Click(object sender, EventArgs e)
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

		private void ButtonApply_Click(object sender, EventArgs e)
		{
			ApplyChanges();
			context.Dispose();

			MultipleEditor_Load(null, null);
			changesApplied = true;
		}
	}
}

using Interop.QBXMLRP2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WoodClub
{
	/// <summary>
	/// Form to display new members when imported from the Orientation signup.
	/// This form can import the new members into the shop database and into QuickBooks.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class NewMembers : Form
	{
		private List<NewMember> members = new List<NewMember>();

		/// <summary>
		/// Initializes a new instance of the <see cref="NewMembers"/> class.
		/// </summary>
		public NewMembers()
		{
			InitializeComponent();
			dataGridView1.CellClick += DataGridView1_CellClick;
			dataGridView1.MouseClick += DataGridView1_MouseClick;
			dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			printDocument1.PrintPage += PrintDocument1_PrintPage;

			if (System.Environment.MachineName != "TREASURERS_PC")
			{
				quickBooksButton.Enabled = false;
			}
		}


		/// <summary>
		/// Handles the PrintPage event of the Print button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
		private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
			this.dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
			e.Graphics.DrawImage(bm, 0, 0);
		}

		/// <summary>
		/// Handles the MouseClick event of the DataGridView1 control.
		/// Adds a Copy item to the context menu.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
		{
			MenuItem copyItem = new MenuItem("Copy");
			copyItem.Click += Copy_Click;
			if (e.Button == MouseButtons.Right)
			{
				ContextMenu m = new ContextMenu();
				m.MenuItems.Add(copyItem);
				m.Show(dataGridView1, new Point(e.X, e.Y));
			}
		}

		/// <summary>
		/// Handles the Click event of the Copy menu item.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void Copy_Click(object sender, EventArgs e)
		{
			DataObject dataObj = dataGridView1.GetClipboardContent();
			Clipboard.SetDataObject(dataObj, true);
		}

		/// <summary>
		/// Handles the CellClick event of the DataGridView1 control. 
		/// Prevents selecting a row that is already in the database.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0 && dataGridView1.Rows[e.RowIndex].Cells[0].ReadOnly)
			{
				MessageBox.Show("Rec Card is already in the database");
			}
		}

		/// <summary>
		/// Gets the starting badge number.
		/// </summary>
		/// <returns></returns>
		private int GetStartingBadgeNumber()
		{
			FirstBadgeNumber fbn = new FirstBadgeNumber();
			fbn.ShowDialog();
			return fbn.BadgeNumber;	
		}


		/// <summary>
		/// Gets the orientation list from the club website.
		/// </summary>
		/// <returns></returns>
		private async Task<List<NewMemberRaw>> GetOrientation()
		{
			List<NewMemberRaw> newMembers = new List<NewMemberRaw>();
			const string key = "8c62a157-7ee8-5401-9f91-930eac39fe2f";
			var dtStart = DateTime.Now;
			var dtEnd = DateTime.Now;
			if (dtStart.Day > 13)
			{
				dtStart = new DateTime(dtStart.Year, dtStart.Month, 15);
				dtEnd = dtStart.AddMonths(1);
			}
			else
			{
				dtEnd = new DateTime(dtStart.Year, dtStart.Month, 15);
				dtStart = dtEnd.AddMonths(-1);
			}

			var og = new OrientationGet
			{
				key = key,
				start_date = (int)dtStart.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
				end_date = (int)dtEnd.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
			};

			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			var baseAddress = "https://scwwoodshop.com";
			//var baseAddress = "https://woodclubtest.site";
			var api = "/wp-json/scwmembers/v1/orientation";

			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseAddress);
				client.DefaultRequestHeaders.Accept.Add(contentType);
				var jsonData = JsonConvert.SerializeObject(og);
				var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var response = await client.PostAsync(api, contentData);
				if (response.IsSuccessStatusCode)
				{
					var stringData = await response.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<List<NewMemberRaw>>(stringData);
					newMembers = result;
				}
			}

			return newMembers;
		}

		/// <summary>
		/// Handles the Load event of the FormNewMembers control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private async void FormNewMembers_Load(object sender, EventArgs e)
		{
			CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			List<NewMemberRaw> newMembers = await GetOrientation();
			int firstBadgeNumber = GetStartingBadgeNumber();

			string startDate = string.Empty;
			DateTime startScanDate = DateTime.Now.AddDays(-10);
			DateTime endScanDate = DateTime.Now.AddDays(+23);
			using (WoodClubEntities context = new WoodClubEntities())
			{
				foreach (NewMemberRaw newMember in newMembers)
				{
					var member = (from m in context.MemberRosters
								  where m.RecCard == newMember.new_member_rec_card
								  select m).FirstOrDefault();
					DateTime memberStartDate = DateTime.Parse(newMember.session_start_formatted).Date;
					members.Add(new NewMember
					{
						Add = member == null,
						CreateInvoice = member == null,
						FirstName = this.FormatProperName(newMember.new_member_first),
						LastName = this.FormatProperName(newMember.new_member_last),
						Email = newMember.new_member_email,
						Phone = newMember.new_member_phone,
						Address = this.FormatAddress(newMember.new_member_street),
						City = "Sun City West",
						State = "AZ",
						ZipCode = "85375",
						RecNo = newMember.new_member_rec_card,
						MemberDate = memberStartDate,
						Badge = member != null ? member.Badge : string.Empty,
						CardNo = member != null ? member.CardNo : string.Empty,
					}); ;
				}
			}

			bs_newmember.DataSource = members;
			this.dataGridView1.DataSource = bs_newmember;

			for (int i = 0; i < members.Count; i++)
			{
				this.dataGridView1.Rows[i].Cells["BillTo"].Value = members[i].FirstName + " " + members[i].LastName;
				if (!members[i].Add)
				{
					this.dataGridView1.Rows[i].Cells[0].ReadOnly = true;
				}
				else
				{
					if (firstBadgeNumber > 0)
					{
						this.dataGridView1.Rows[i].Cells["Badge"].Value = firstBadgeNumber.ToString();
						firstBadgeNumber++;
					}
				}
			}

			dataGridView1.Columns["Add"].DisplayIndex = 0;
			dataGridView1.Columns["Invoice"].DisplayIndex = 1;
			dataGridView1.Columns["Badge"].DisplayIndex = 2;
			dataGridView1.Columns["RecNo"].DisplayIndex = 3;
			dataGridView1.Columns["FirstName"].DisplayIndex = 4;
			dataGridView1.Columns["LastName"].DisplayIndex = 5;
			dataGridView1.Columns["BillTo"].DisplayIndex = 6;
		}

		/// <summary>
		/// Formats the name of the proper.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		private string FormatProperName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}

			string ret = name.ToLower();
			string firstChar = ret.Substring(0, 1).ToUpper();
			string rest = ret.Substring(1, ret.Length - 1);
			ret = firstChar + rest;
			return ret;
		}

		/// <summary>
		/// Formats the address.
		/// </summary>
		/// <param name="addr"></param>
		/// <returns></returns>
		private string FormatAddress(string addr)
		{
			if (string.IsNullOrEmpty(addr))
			{
				return addr;
			}

			addr = addr.Replace(".", "").Replace(",", "");

			var parts = addr.Split(' ');
			string ret = parts[0];
			for (int i = 1; i < parts.Length; i++)
			{
				ret += " " + FormatProperName(parts[i]);
			}

			return ret;
		}

		/// <summary>
		/// Adds the new members to the shop database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAddToDb_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			bs_newmember.EndEdit();

			using (WoodClubEntities context = new WoodClubEntities())
			{
				foreach (NewMember r in members)
				{
					if (r.Add)
					{
						var member = (from mr in context.MemberRosters
									  where mr.Badge == r.Badge
									  select mr).FirstOrDefault();
						if (member != null)
						{
							MessageBox.Show($"Badge {r.Badge} is already in the database.");
							continue;
						}

						context.MemberRosters.Add(new MemberRoster
						{
							Title = string.Empty,
							Badge = r.Badge,
							CardNo = r.CardNo,
							FirstName = r.FirstName,
							LastName = r.LastName,
							Email = r.Email,
							Phone = r.Phone,
							Address = r.Address,
							State = r.State,
							Zip = r.ZipCode,
							RecCard = r.RecNo,
							MemberDate = r.MemberDate,
							ClubDuesPaidDate = r.MemberDate,
							OneTime = true,
							EntryCodes = "F",
							GroupTime = "Members",
							RecDuesPaid = true,
							ClubDuesPaid = true,
							CreditBank = r.Badge == String.Empty ? "0" : "1",
							AuthorizedTimeZone = 3,
							ExemptModDate = r.MemberDate,
							Authorized = false,
							ExtHour = false,
							Exempt = false,
							Locker = String.Empty,
							NewBadge = false,
							LastDayValid = r.MemberDate.AddDays(2),
						});

						if (Convert.ToInt32(r.Badge) < 9000)
						{
							context.Transactions.Add(new Transaction
							{
								Badge = r.Badge,
								Code = "Q4",
								TransDate = DateTime.Now,
								CreditAmt = 1,
								EventType = "Orientation 1 Credit",
								RecCard = r.RecNo
							});
						}
					}
				}

				context.SaveChanges();
				MessageBox.Show("Add Members to Database has completed.");
				btn.Enabled = false;
			}
		}

		
		/// <summary>
		/// Adds the new members to QuickBooks.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void quickBooksButton_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			QBFunctions qbf = new QBFunctions();
            qbf.connectToQB();
			bool disableButton = true;
            try
            {
				foreach (NewMember member in members)
				{
					if (member.Add)
					{
						string customerResponse = qbf.processRequestFromQB(qbf.buildAddCustomersQueryRqXML(member));
						string dataExtModResponse = qbf.processRequestFromQB(qbf.buildDataExtMod(member.Badge, "Customer Name", member.FirstName + " " + member.LastName));

						if (member.CreateInvoice)
						{
							string invoiceAddResponse = qbf.processRequestFromQB(qbf.buildInvoiceAddRqXML(member.Badge));
							InvoiceData invoiceData = parseDuesInvoices(invoiceAddResponse);
							CustomerData customerData = parseCustomerQueryRs(customerResponse);
							string paymentAddResponse = qbf.processRequestFromQB(qbf.buildReceivePaymentAddRqXML(customerData, invoiceData));
						}
					}
				}
            }
            catch (Exception ex) { 
				MessageBox.Show(ex.Message);
				disableButton = false;
			}
			finally { qbf.disconnectFromQB(); }
			MessageBox.Show("Add Members to QuickBooks has completed.");
			btn.Enabled = !disableButton;
		}

		/// <summary>
		/// Prints the new member list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void printNewMembersButton_Click(object sender, EventArgs e)
		{
			printDocument1.DefaultPageSettings.Landscape = true;
			PrintPreviewDialog ppvd = new PrintPreviewDialog();
			ppvd.Document = printDocument1;
			ppvd.ShowDialog();
			//printDocument1.Print();
		}

		private InvoiceData parseDuesInvoices(string xml)
		{
			List<InvoiceData> invoiceDatas = new List<InvoiceData>();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			XmlNode root = doc.DocumentElement;
			XmlNode ivn = root.SelectSingleNode("//InvoiceRet");
			InvoiceData ivd = new InvoiceData();
			ivd.TxnID = ivn.SelectSingleNode("TxnID").InnerText;
			ivd.EditSequence = ivn.SelectSingleNode("EditSequence").InnerText;
			ivd.Badge = ivn.SelectSingleNode("CustomerRef/FullName").InnerText;
			ivd.IsPaid = ivn.SelectSingleNode("IsPaid").InnerText == "true";
			ivd.DueDate = ivn.SelectSingleNode("DueDate").InnerText;
			ivd.Subtotal = ivn.SelectSingleNode("Subtotal").InnerText;
			ivd.BalanceRemaining = ivn.SelectSingleNode("BalanceRemaining").InnerText;

			return ivd;
		}

		private CustomerData parseCustomerQueryRs(string xml)
		{
			List<CustomerData> customerData = new List<CustomerData>();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			XmlNode root = doc.DocumentElement;
			XmlNode cn = root.SelectSingleNode("//CustomerRet");
			CustomerData cd = new CustomerData();
			cd.FullName = cn.SelectSingleNode("FullName").InnerText;
			cd.FirstName = getInnerText(cn.SelectSingleNode("FirstName"));
			cd.Balance = getInnerText(cn.SelectSingleNode("Balance"));
			cd.AccountNumber = getInnerText(cn.SelectSingleNode("AccountNumber"));
			cd.LastName = getInnerText(cn.SelectSingleNode("LastName"));
			cd.Phone = getInnerText(cn.SelectSingleNode("Phone"));
			cd.Email = getInnerText(cn.SelectSingleNode("Email"));
			return cd;
		}

		private string getInnerText(XmlNode node)
		{
			string retVal = "";
			if (node != null)
			{
				if (node.InnerText != null)
				{
					retVal = node.InnerText;
				}
			}

			return retVal;
		}
	}

	internal class InvoiceData
	{
		public string TxnID { get; set; }
		public bool IsPaid { get; set; } = false;
		public bool IsDues { get; set; } = false;
		public bool HasLateFees { get; set; } = false;
		public string EditSequence { get; set; }
		public string DueDate { get; set; }
		public string Subtotal { get; set; }
		public string BalanceRemaining { get; set; }
		public string Badge { get; set; }
		public string CustomerName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string AppliedAmount { get; set; }
		public List<InvoiceLineItemData> LineItems { get; set; } = new List<InvoiceLineItemData>();
	}

	internal class InvoiceLineItemData
	{
		public string TxnLineID { get; set; }
		public string Amount { get; set; }
		public string FullName { get; set; }
	}
}

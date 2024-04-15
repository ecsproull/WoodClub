using Interop.QBXMLRP2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WoodClub.Forms;

namespace WoodClub
{
    public partial class NewMembers : Form
	{
		private List<NewMember> members = new List<NewMember>();
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

		private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
			this.dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
			e.Graphics.DrawImage(bm, 0, 0);
		}

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

		private void Copy_Click(object sender, EventArgs e)
		{
			DataObject dataObj = dataGridView1.GetClipboardContent();
			Clipboard.SetDataObject(dataObj, true);
		}

		private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0 && dataGridView1.Rows[e.RowIndex].Cells[0].ReadOnly)
			{
				MessageBox.Show("Rec Card is already in the database");
			}
		}


		private int GetStartingBadgeNumber()
		{
			FirstBadgeNumber fbn = new FirstBadgeNumber();
			fbn.ShowDialog();
			return fbn.BadgeNumber;	
		}

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

		private string FormatCity(string city)
		{
			if (string.IsNullOrEmpty(city))
			{
				return city;
			}

			city = city.Replace(".", "");

			if (city.ToLower() == "scw")
			{
				return "Sun City West";
			}

			var parts = city.Split(' ');
			string ret = FormatProperName(parts[0]);
			for (int i = 1; i < parts.Length; i++)
			{
				ret += " " + FormatProperName(parts[i]);
			}

			return ret;
		}

		private void buttonAddToDb_Click(object sender, EventArgs e)
		{
			bs_newmember.EndEdit();

			using (WoodClubEntities context = new WoodClubEntities())
			{

				foreach (NewMember r in members)
				{
					if (r.Add)
					{
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
			}
		}

        private string sessionTicket;
        private RequestProcessor2 requestProcessor = null;
        private string maxVersion;
        private string companyFile = "";
        private QBFileMode qbFileMode = QBFileMode.qbFileOpenDoNotCare;
        private static string appID = "IDN12345";
        private static string appName = "WoodClub";
		private void quickBooksButton_Click(object sender, EventArgs e)
		{
            connectToQB();
            try
            {
				foreach (NewMember member in members)
				{
					if (member.Add)
					{
						string response = processRequestFromQB(buildAddCustomersQueryRqXML(member));

						if (member.CreateInvoice)
						{
							response = processRequestFromQB(buildInvoiceAddRqXML(member.Badge));
						}
					}
				}
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
			finally { disconnectFromQB(); }
        }

        private string buildAddCustomersQueryRqXML(NewMember newMember)
        {
            string xml = "";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
            XmlElement customerAddRq = xmlDoc.CreateElement("CustomerAddRq");
            qbXMLMsgsRq.AppendChild(customerAddRq);

            XmlElement customerAdd = xmlDoc.CreateElement("CustomerAdd");
			customerAdd.AppendChild(xmlDoc.CreateElement("Name")).InnerText = newMember.Badge;
            customerAdd.AppendChild(xmlDoc.CreateElement("IsActive")).InnerText = "true";
            customerAdd.AppendChild(xmlDoc.CreateElement("FirstName")).InnerText = newMember.FirstName;
            customerAdd.AppendChild(xmlDoc.CreateElement("LastName")).InnerText = newMember.LastName;

            XmlElement billAddress = xmlDoc.CreateElement("BillAddress");
            billAddress.AppendChild(xmlDoc.CreateElement("Addr1")).InnerText = newMember.FirstName + " " + newMember.LastName;
			billAddress.AppendChild(xmlDoc.CreateElement("Addr2")).InnerText = newMember.Address;
			billAddress.AppendChild(xmlDoc.CreateElement("City")).InnerText = "Sun City West";
			billAddress.AppendChild(xmlDoc.CreateElement("State")).InnerText = "AZ";
            billAddress.AppendChild(xmlDoc.CreateElement("PostalCode")).InnerText = "85375";
            customerAdd.AppendChild(billAddress);

            customerAdd.AppendChild(xmlDoc.CreateElement("Phone")).InnerText = newMember.Phone;
            customerAdd.AppendChild(xmlDoc.CreateElement("Fax")).InnerText = newMember.Badge;
            customerAdd.AppendChild(xmlDoc.CreateElement("Email")).InnerText = newMember.Email;

			XmlElement customerTypeRef = xmlDoc.CreateElement("CustomerTypeRef");
			customerTypeRef.AppendChild(xmlDoc.CreateElement("FullName")).InnerText = "Club Member:X06F";
			customerAdd.AppendChild(customerTypeRef);

            customerAdd.AppendChild(xmlDoc.CreateElement("AccountNumber")).InnerText = newMember.RecNo;

            customerAddRq.AppendChild(customerAdd);

            xml = xmlDoc.OuterXml;
            return xml;
        }

		private string buildInvoiceAddRqXML(string customer)
		{
			string requestXML = "";

			//if (!validateInput()) return null;

			//GET ALL INPUT INTO XML
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
			qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
			XmlElement InvoiceAddRq = xmlDoc.CreateElement("InvoiceAddRq");
			qbXMLMsgsRq.AppendChild(InvoiceAddRq);
			XmlElement InvoiceAdd = xmlDoc.CreateElement("InvoiceAdd");
			InvoiceAddRq.AppendChild(InvoiceAdd);

			XmlElement Element_CustomerRef = xmlDoc.CreateElement("CustomerRef");
			InvoiceAdd.AppendChild(Element_CustomerRef);
			XmlElement Element_CustomerRef_FullName = xmlDoc.CreateElement("FullName");
			Element_CustomerRef.AppendChild(Element_CustomerRef_FullName).InnerText = customer;


			//Line Items
			XmlElement invoiceLineAdd = xmlDoc.CreateElement("InvoiceLineAdd");
			InvoiceAdd.AppendChild(invoiceLineAdd);
			XmlElement itemRef = xmlDoc.CreateElement("ItemRef");
			invoiceLineAdd.AppendChild(itemRef);
			itemRef.AppendChild(xmlDoc.CreateElement("FullName")).InnerText = "X06";
			invoiceLineAdd.AppendChild(xmlDoc.CreateElement("Quantity")).InnerText = "1";
			invoiceLineAdd.AppendChild(xmlDoc.CreateElement("Amount")).InnerText = "50.00";

			XmlElement invoiceLineAdd2 = xmlDoc.CreateElement("InvoiceLineAdd");
			InvoiceAdd.AppendChild(invoiceLineAdd2);
			XmlElement itemRef2 = xmlDoc.CreateElement("ItemRef");
			invoiceLineAdd2.AppendChild(itemRef2);
			itemRef2.AppendChild(xmlDoc.CreateElement("FullName")).InnerText = "X08";
			invoiceLineAdd2.AppendChild(xmlDoc.CreateElement("Quantity")).InnerText = "1";
			invoiceLineAdd2.AppendChild(xmlDoc.CreateElement("Amount")).InnerText = "35.00";

			InvoiceAddRq.SetAttribute("requestID", "99");
			requestXML = xmlDoc.OuterXml;

			return requestXML;
		}

		private XmlElement buildRqEnvelope(XmlDocument doc, string maxVer)
        {
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));
            doc.AppendChild(doc.CreateProcessingInstruction("qbxml", "version=\"" + maxVer + "\""));
            XmlElement qbXML = doc.CreateElement("QBXML");
            doc.AppendChild(qbXML);
            XmlElement qbXMLMsgsRq = doc.CreateElement("QBXMLMsgsRq");
            qbXML.AppendChild(qbXMLMsgsRq);
            qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
            return qbXMLMsgsRq;
        }

        private void connectToQB()
        {
            if (string.IsNullOrEmpty(sessionTicket))
            {
                requestProcessor = new RequestProcessor2Class();
                requestProcessor.OpenConnection(appID, appName);
                sessionTicket = requestProcessor.BeginSession(companyFile, qbFileMode);
                string[] versions = requestProcessor.get_QBXMLVersionsForSession(sessionTicket);
                maxVersion = versions[versions.Length - 1];
            }
        }

        private void disconnectFromQB()
        {
            if (sessionTicket != null)
            {
                try
                {
                    requestProcessor.EndSession(sessionTicket);
                    sessionTicket = null;
                    requestProcessor.CloseConnection();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private string processRequestFromQB(string request)
        {
            try
            {
                return requestProcessor.ProcessRequest(sessionTicket, request);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

		private void printNewMembersButton_Click(object sender, EventArgs e)
		{
			printDocument1.DefaultPageSettings.Landscape = true;
			PrintPreviewDialog ppvd = new PrintPreviewDialog();
			ppvd.Document = printDocument1;
			ppvd.ShowDialog();
			//printDocument1.Print();
		}
	}
}

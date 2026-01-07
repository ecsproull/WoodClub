using Interop.QBXMLRP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml;

namespace WoodClub
{
	internal class QBFunctions
	{
		private string sessionTicket;
		private RequestProcessor2 requestProcessor = null;
		private string maxVersion;
		private string companyFile = "";
		private QBFileMode qbFileMode = QBFileMode.qbFileOpenDoNotCare;
		private static string appID = "IDN12345";
		private static string appName = "WoodClub";


		private int getCount(string request)
		{
			string response = processRequestFromQB(buildDataCountQuery(request));
			int count = parseRsForCount(response, request);
			return count;
		}

		/// <summary>
		/// Builds the XML request to delete an invoice from QuickBooks.
		/// </summary>
		public string buildInvoiceDeleteRqXML(string txnID, string editSequence)
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
			qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
			
			XmlElement invoiceDelRq = xmlDoc.CreateElement("TxnDelRq");
			qbXMLMsgsRq.AppendChild(invoiceDelRq);
			
			invoiceDelRq.AppendChild(xmlDoc.CreateElement("TxnDelType")).InnerText = "Invoice";
			invoiceDelRq.AppendChild(xmlDoc.CreateElement("TxnID")).InnerText = txnID;
			
			return xmlDoc.OuterXml;
		}

		/// <summary>
		/// Builds the XML request to set a customer as inactive in QuickBooks.
		/// </summary>
		public string buildCustomerSetInactiveRqXML(string badge)
		{
			if (string.IsNullOrWhiteSpace(badge)) return null;
			
			// First get the customer's ListID and EditSequence
			string queryXml = buildCustomerQueryRqXML(
				new string[] { "ListID", "EditSequence" }, 
				badge, 
				string.Empty, 
				string.Empty, 
				string.Empty, 
				false);
			
			string queryResp = processRequestFromQB(queryXml);
			CustomerIdData cid;
			try
			{
				cid = parseCustomerQueryRsShort(queryResp);
			}
			catch
			{
				return null;
			}
			
			if (string.IsNullOrEmpty(cid.ListId) || string.IsNullOrEmpty(cid.EditSequence))
				return null;
			
			// Build the CustomerMod request to set IsActive = false
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
			qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
			
			XmlElement customerModRq = xmlDoc.CreateElement("CustomerModRq");
			qbXMLMsgsRq.AppendChild(customerModRq);
			
			XmlElement customerMod = xmlDoc.CreateElement("CustomerMod");
			customerModRq.AppendChild(customerMod);
			
			customerMod.AppendChild(xmlDoc.CreateElement("ListID")).InnerText = cid.ListId;
			customerMod.AppendChild(xmlDoc.CreateElement("EditSequence")).InnerText = cid.EditSequence;
			customerMod.AppendChild(xmlDoc.CreateElement("IsActive")).InnerText = "false";
			
			return xmlDoc.OuterXml;
		}

		// Parse QBXML error details if present and return a concise message, otherwise empty string
		public string ParseQbError(string xml)
		{
			if (string.IsNullOrEmpty(xml)) return string.Empty;
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(xml);
				XmlNode errNode = doc.SelectSingleNode("//QBXML/QBXMLMsgsRs/*[contains(name(), 'Rs')]/Error");
				if (errNode == null)
				{
					// some responses put Error directly under QBXMLMsgsRs
					errNode = doc.SelectSingleNode("//QBXMLMsgsRs/Error");
				}
				if (errNode != null)
				{
					string code = getInnerText(errNode.SelectSingleNode("ErrorCode"));
					string msg = getInnerText(errNode.SelectSingleNode("Description"));
					return $"Code={code}; Msg={msg}";
				}
			}
			catch { }
			return string.Empty;
		}

		public virtual string buildDataCountQuery(string request)
		{
			string input = "";
			XmlDocument inputXMLDoc = new XmlDocument();
			XmlElement qbXMLMsgsRq = buildRqEnvelope(inputXMLDoc, maxVersion);
			XmlElement queryRq = inputXMLDoc.CreateElement(request);
			queryRq.SetAttribute("metaData", "MetaDataOnly");
			qbXMLMsgsRq.AppendChild(queryRq);
			input = inputXMLDoc.OuterXml;
			return input;
		}

		public virtual int parseRsForCount(string xml, string request)
		{
			int ret = -1;
			try
			{
				XmlNodeList RsNodeList = null;
				XmlDocument Doc = new XmlDocument();
				Doc.LoadXml(xml);
				string tagname = request.Replace("Rq", "Rs");
				RsNodeList = Doc.GetElementsByTagName(tagname);
				System.Text.StringBuilder popupMessage = new System.Text.StringBuilder();
				XmlAttributeCollection rsAttributes = RsNodeList.Item(0).Attributes;
				XmlNode retCount = rsAttributes.GetNamedItem("retCount");
				ret = Convert.ToInt32(retCount.Value);
			}
			catch (Exception e)
			{
				MessageBox.Show("Error encountered: " + e.Message);
				ret = -1;
			}
			return ret;
		}

		private List<CustomerData> parseCustomerQueryRs(string xml)
		{
			List<CustomerData> customerData = new List<CustomerData>();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			XmlNode root = doc.DocumentElement;
			XmlNodeList customerNodes = root.SelectNodes("//CustomerRet");

			foreach (XmlNode cn in customerNodes)
			{
				if (cn.SelectSingleNode("FullName").InnerText == "---") continue;
				CustomerData cd = new CustomerData();
				cd.FullName = cn.SelectSingleNode("FullName").InnerText;
				cd.FirstName = getInnerText(cn.SelectSingleNode("FirstName"));
				cd.Balance = getInnerText(cn.SelectSingleNode("Balance"));
				cd.AccountNumber = getInnerText(cn.SelectSingleNode("AccountNumber"));
				cd.LastName = getInnerText(cn.SelectSingleNode("LastName"));
				cd.Phone = getInnerText(cn.SelectSingleNode("Phone"));
				cd.Email = getInnerText(cn.SelectSingleNode("Email"));

				customerData.Add(cd);
			}

			return customerData;
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

		public List<CustomerData> loadCustomers(
		string fullName,
		string status,
		string balanceFilter,
		string balanceAmount,
		bool includeOpenInvoices = false,
		bool connectToQuickBooks = true)
		{
			List<CustomerData> customerData = new List<CustomerData>();
			string request = "CustomerQueryRq";
			try
			{
				if (connectToQuickBooks)
				{
					connectToQB();
				}
				int count = getCount(request);
				string xml = buildCustomerQueryRqXML(
					new string[] { "FullName", "FirstName", "LastName", "Balance", "AccountNumber", "CustomerTypeRef", "DataExtRet" },
					fullName,
					status,
					balanceFilter,
					balanceAmount);
				string response = processRequestFromQB(xml);
			customerData = parseCustomerQueryRs(response);

			if (includeOpenInvoices && customerData != null && customerData.Count > 0)
			{
				// For each customer, query for unpaid/open invoices
				foreach (var cd in customerData)
				{
					try
					{
						 string invQuery = buildInvoiceQueryRqXML(cd.FullName);
						string invResp = processRequestFromQB(invQuery);
						// check for QB errors
						string qberr = ParseQbError(invResp);
						if (!string.IsNullOrEmpty(qberr))
						{
							MessageBox.Show($"Invoice query failed for {cd.FullName}: {qberr}");
							cd.OpenInvoices = new List<InvoiceData>();
							continue;
						}
						var invoices = parseInvoiceQueryRs(invResp);
						cd.OpenInvoices = invoices ?? new List<InvoiceData>();
					}
					catch { /* continue on invoice parse errors */ }
				}
			}
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }

			if (connectToQuickBooks)
			{
				disconnectFromQB();
			}

			return customerData;
		}

		/// <summary>
		/// Builds the query to add a new member to QuickBooks.
		/// </summary>
		/// <param name="newMember"></param>
		/// <returns></returns>
		public string buildAddCustomersQueryRqXML(NewMember newMember)
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

			//customerAdd.AppendChild(xmlDoc.CreateElement("PreferredDeliveryMethod")).InnerText = "Email";

			XmlElement customerTypeRef = xmlDoc.CreateElement("CustomerTypeRef");
			customerTypeRef.AppendChild(xmlDoc.CreateElement("FullName")).InnerText = "Club Member:X06F";
			customerAdd.AppendChild(customerTypeRef);

			customerAdd.AppendChild(xmlDoc.CreateElement("AccountNumber")).InnerText = newMember.RecNo;

			customerAddRq.AppendChild(customerAdd);

			xml = xmlDoc.OuterXml;
			return xml;
		}

	/// <summary>
	/// Build an InvoiceQueryRq for unpaid/open invoices for a customer FullName
	/// </summary>
	public string buildInvoiceQueryRqXML(string customerFullName)
	{
		XmlDocument xmlDoc = new XmlDocument();
		XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
		qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
		XmlElement invQueryRq = xmlDoc.CreateElement("InvoiceQueryRq");
		qbXMLMsgsRq.AppendChild(invQueryRq);

		// Filter by customer full name using EntityFilter
		if (!string.IsNullOrEmpty(customerFullName))
		{
			XmlElement entityFilter = xmlDoc.CreateElement("EntityFilter");
			invQueryRq.AppendChild(entityFilter);
			XmlElement fullName = xmlDoc.CreateElement("FullName");
			entityFilter.AppendChild(fullName).InnerText = customerFullName;
		}

		// Only return unpaid invoices
		XmlElement paidStatus = xmlDoc.CreateElement("PaidStatus");
		invQueryRq.AppendChild(paidStatus).InnerText = "NotPaidOnly";

		invQueryRq.SetAttribute("requestID", "1");
		return xmlDoc.OuterXml;
	}

		private List<InvoiceData> parseInvoiceQueryRs(string xml)
		{
			var list = new List<InvoiceData>();
			if (string.IsNullOrEmpty(xml)) return list;
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(xml);
				// Check for QBXML errors
				var qbErr = ParseQbError(xml);
				if (!string.IsNullOrEmpty(qbErr))
				{
					// throw with detail so caller can handle/log it
					throw new Exception("QB Error: " + qbErr);
				}
				XmlNodeList invNodes = doc.SelectNodes("//InvoiceRet");
				foreach (XmlNode ivn in invNodes)
				{
					InvoiceData ivd = new InvoiceData();
					ivd.TxnID = getInnerText(ivn.SelectSingleNode("TxnID"));
					ivd.EditSequence = getInnerText(ivn.SelectSingleNode("EditSequence"));
					ivd.Badge = getInnerText(ivn.SelectSingleNode("CustomerRef/FullName"));
					ivd.Subtotal = getInnerText(ivn.SelectSingleNode("Subtotal"));
					ivd.BalanceRemaining = getInnerText(ivn.SelectSingleNode("BalanceRemaining"));
					ivd.DueDate = getInnerText(ivn.SelectSingleNode("DueDate"));
					list.Add(ivd);
				}
			}
			catch
			{
				// ignore parse errors, return what we have
			}
			return list;
		}

		/// <summary>
		/// Builds a Data Extension Modification Request. This is used to modify custom fields
		/// in the customer part of QB.
		/// </summary>
		/// <param name="fullCustomerName"></param>
		/// <param name="customFieldName"></param>
		/// <param name="customFieldValue"></param>
		/// <returns></returns>
		public string buildDataExtMod(string fullCustomerName, string customFieldName, string customFieldValue)
		{
			string requestXML = "";
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
			qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
			XmlElement dataExtModRq = xmlDoc.CreateElement("DataExtModRq");
			qbXMLMsgsRq.AppendChild(dataExtModRq);
			XmlElement dataExtMod = xmlDoc.CreateElement("DataExtMod");
			dataExtModRq.AppendChild(dataExtMod);

			XmlElement ownerId = xmlDoc.CreateElement("OwnerID");
			dataExtMod.AppendChild(ownerId).InnerText = "0";

			XmlElement dataExtName = xmlDoc.CreateElement("DataExtName");
			dataExtMod.AppendChild(dataExtName).InnerText = customFieldName;

			XmlElement listDataExtType = xmlDoc.CreateElement("ListDataExtType");
			dataExtMod.AppendChild(listDataExtType).InnerText = "Customer";

			XmlElement listObjRef = xmlDoc.CreateElement("ListObjRef");
			dataExtMod.AppendChild(listObjRef);

			XmlElement fullName = xmlDoc.CreateElement("FullName");
			listObjRef.AppendChild(fullName).InnerText = fullCustomerName;

			XmlElement dataExtValue = xmlDoc.CreateElement("DataExtValue");
			dataExtMod.AppendChild(dataExtValue).InnerText = customFieldValue;

			requestXML = xmlDoc.OuterXml;
			return requestXML;
		}

		/// <summary>
		/// Builds the query to add an invoice to QuickBooks.
		/// </summary>
		/// <param name="customer"></param>
		/// <returns></returns>
		public string buildInvoiceAddRqXML(string customer)
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
			invoiceLineAdd.AppendChild(xmlDoc.CreateElement("Amount")).InnerText = "40.00";

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

		/// <summary>
		/// Builds the query to add a payment to a QuickBooks invoice.
		/// </summary>
		/// <param name="customer"></param>
		/// <returns></returns>
		public string buildReceivePaymentAddRqXML(CustomerData customer, InvoiceData invoice)
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
			qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
			XmlElement ReceivePaymentAddRq = xmlDoc.CreateElement("ReceivePaymentAddRq");
			qbXMLMsgsRq.AppendChild(ReceivePaymentAddRq);
			XmlElement ReceivePaymentAdd = xmlDoc.CreateElement("ReceivePaymentAdd");
			ReceivePaymentAddRq.AppendChild(ReceivePaymentAdd);

			XmlElement Element_CustomerRef = xmlDoc.CreateElement("CustomerRef");
			ReceivePaymentAdd.AppendChild(Element_CustomerRef);
			XmlElement Element_CustomerRef_FullName = xmlDoc.CreateElement("FullName");
			Element_CustomerRef.AppendChild(Element_CustomerRef_FullName).InnerText = customer.FullName;

			XmlElement TotalAmount = xmlDoc.CreateElement("TotalAmount");
			ReceivePaymentAdd.AppendChild(TotalAmount).InnerText = invoice.Subtotal;

			XmlElement Element_PaymentMethodRef = xmlDoc.CreateElement("PaymentMethodRef");
			ReceivePaymentAdd.AppendChild(Element_PaymentMethodRef);
			XmlElement Element_PaymentMethod_FullName = xmlDoc.CreateElement("FullName");
			Element_PaymentMethodRef.AppendChild(Element_PaymentMethod_FullName).InnerText = "office";

			XmlElement Element_Memo = xmlDoc.CreateElement("Memo");
			ReceivePaymentAdd.AppendChild(Element_Memo).InnerText = customer.FullName + " Orientation";

			XmlElement IsAutoApply = xmlDoc.CreateElement("IsAutoApply");
			ReceivePaymentAdd.AppendChild(IsAutoApply).InnerText = "true";

			string xml = xmlDoc.OuterXml;
			return xml;
		}

		public string buildCustomerQueryRqXML(
		string[] includeRetElement,
		string fullName,
		string status,
		string balanceFilter,
		string balanceAmount,
		bool customFields = false
		)
		{
			string xml = "";
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
			qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
			XmlElement CustomerQueryRq = xmlDoc.CreateElement("CustomerQueryRq");
			qbXMLMsgsRq.AppendChild(CustomerQueryRq);

			if (!string.IsNullOrEmpty(fullName))
			{
				XmlElement fullNameElement = xmlDoc.CreateElement("FullName");
				CustomerQueryRq.AppendChild(fullNameElement).InnerText = fullName;
			}

			if (!string.IsNullOrEmpty(status))
			{
				XmlElement activeStatus = xmlDoc.CreateElement("ActiveStatus");
				activeStatus.InnerText = status;
				CustomerQueryRq.AppendChild(activeStatus);
			}

			if (!string.IsNullOrEmpty(balanceFilter))
			{
				XmlElement totalBalFilter = xmlDoc.CreateElement("TotalBalanceFilter");
				XmlElement balanceFilterOperator = xmlDoc.CreateElement("Operator");
				XmlElement balanceFilterAmount = xmlDoc.CreateElement("Amount");
				totalBalFilter.AppendChild(balanceFilterOperator).InnerText = balanceFilter;
				totalBalFilter.AppendChild(balanceFilterAmount).InnerText = balanceAmount;
				CustomerQueryRq.AppendChild(totalBalFilter);
			}

			for (int x = 0; x < includeRetElement.Length; x++)
			{
				XmlElement includeRet = xmlDoc.CreateElement("IncludeRetElement");
				CustomerQueryRq.AppendChild(includeRet).InnerText = includeRetElement[x];
			}

			if (customFields)
			{
				XmlElement ownerIdElement = xmlDoc.CreateElement("OwnerID");
				CustomerQueryRq.AppendChild(ownerIdElement).InnerText = "0";
			}

			CustomerQueryRq.SetAttribute("requestID", "1");
			xml = xmlDoc.OuterXml;
			return xml;
		}

		/// <summary>
		/// TODO: Add phone number and email update to this form.
		/// </summary>
		/// <param name="customerIdData"></param>
		/// <param name="customerType"></param>
		/// <returns></returns>
		public string BuildCustomerMod(CustomerIdData customerIdData, string customerType, MemberRoster member)
		{
			string requestXML = "";
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
			qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
			XmlElement customerModRq = xmlDoc.CreateElement("CustomerModRq");
			qbXMLMsgsRq.AppendChild(customerModRq);
			XmlElement customerMod = xmlDoc.CreateElement("CustomerMod");
			customerModRq.AppendChild(customerMod);

			XmlElement listId = xmlDoc.CreateElement("ListID");
			customerMod.AppendChild(listId).InnerText = customerIdData.ListId;

			XmlElement editSequence = xmlDoc.CreateElement("EditSequence");
			customerMod.AppendChild(editSequence).InnerText = customerIdData.EditSequence;

			XmlElement phone = xmlDoc.CreateElement("Phone");
			customerMod.AppendChild(phone).InnerText = member.Phone;

			XmlElement email = xmlDoc.CreateElement("Email");
			customerMod.AppendChild(email).InnerText = member.Email;

			XmlElement customerTypeRef = xmlDoc.CreateElement("CustomerTypeRef");
			customerMod.AppendChild(customerTypeRef);

			XmlElement fullName = xmlDoc.CreateElement("FullName");
			customerTypeRef.AppendChild(fullName).InnerText = customerType;

			requestXML = xmlDoc.OuterXml;
			return requestXML;
		}

		public CustomerIdData parseCustomerQueryRsShort(string xml)
		{
			CustomerIdData customerIdData = new CustomerIdData();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			XmlNode root = doc.DocumentElement;
			XmlNode customerNode = root.SelectSingleNode("//CustomerRet");
			customerIdData.ListId = customerNode.SelectSingleNode("ListID").InnerText;
			customerIdData.EditSequence = customerNode.SelectSingleNode("EditSequence").InnerText;
			return customerIdData;
		}

		/// <summary>
		/// Helper function to build the envelope around an QBXml query.
		/// </summary>
		/// <param name="doc"></param>
		/// <param name="maxVer"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Connects to QuickBooks.
		/// </summary>
		public void connectToQB()
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

		/// <summary>
		/// Disconnect from QuickBooks.
		/// </summary>
		public void disconnectFromQB()
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

		/// <summary>
		/// Helper function to process a request from QuickBooks.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public string processRequestFromQB(string request)
		{
			// Validate that the outgoing request is well-formed XML before sending.
			try
			{
				var doc = new XmlDocument();
				doc.LoadXml(request);
			}
			catch (Exception xmlEx)
			{
				MessageBox.Show("Request XML is not well-formed: " + xmlEx.Message + "\nRequest:\n" + request);
				return null;
			}

			try
			{
				return requestProcessor.ProcessRequest(sessionTicket, request);
			}
			catch (System.Runtime.InteropServices.COMException ce)
			{
				// Surface COM error details and include the outgoing XML to aid debugging.
				MessageBox.Show($"QuickBooks COM error parsing request: {ce.Message}\nErrorCode=0x{ce.ErrorCode:X}\nRequest:\n" + request);
				return null;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return null;
			}
		}

		// Build CustomerTypeQueryRq to check for existing CustomerType by full name.
		public string buildCustomerTypeQueryRqXML(string fullName)
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
			qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
			XmlElement ctQueryRq = xmlDoc.CreateElement("CustomerTypeQueryRq");
			qbXMLMsgsRq.AppendChild(ctQueryRq);

			if (!string.IsNullOrEmpty(fullName))
			{
				XmlElement fullNameElement = xmlDoc.CreateElement("FullName");
				ctQueryRq.AppendChild(fullNameElement).InnerText = fullName;
			}

			ctQueryRq.SetAttribute("requestID", "1");
			return xmlDoc.OuterXml;
		}

		// Quick check: returns true if CustomerTypeRet exists in response XML.
		private bool parseCustomerTypeQueryRsHasMatch(string xml)
		{
			try
			{
				if (string.IsNullOrEmpty(xml)) return false;
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(xml);
				XmlNode node = doc.SelectSingleNode("//CustomerTypeRet");
				return node != null;
			}
			catch
			{
				return false;
			}
		}

		// Build CustomerTypeAddRq XML. Provide parentFullName when creating a child.
		public string buildCustomerTypeAddRqXML(string name, string parentFullName = null)
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
			qbXMLMsgsRq.SetAttribute("onError", "stopOnError");
			XmlElement ctAddRq = xmlDoc.CreateElement("CustomerTypeAddRq");
			qbXMLMsgsRq.AppendChild(ctAddRq);
			XmlElement ctAdd = xmlDoc.CreateElement("CustomerTypeAdd");
			ctAddRq.AppendChild(ctAdd);

			ctAdd.AppendChild(xmlDoc.CreateElement("Name")).InnerText = name;

			if (!string.IsNullOrEmpty(parentFullName))
			{
				XmlElement parentRef = xmlDoc.CreateElement("ParentRef");
				parentRef.AppendChild(xmlDoc.CreateElement("FullName")).InnerText = parentFullName;
				ctAdd.AppendChild(parentRef);
			}

			return xmlDoc.OuterXml;
		}

		// Ensure a hierarchical CustomerType (e.g. "Club Member:M00-X11") exists.
		// Returns true if the type exists or was successfully created.
		// Requires an active session (connectToQB called).6
		public bool EnsureCustomerTypeExists(string fullName)
		{
			if (string.IsNullOrWhiteSpace(fullName)) return false;

			// Walk cumulative path: "A:B:C" => ensure "A", then "A:B", then "A:B:C"
			string parentFull = null;
			string[] parts = fullName.Split(':');
			foreach (string part in parts)
			{
				string cumulative = parentFull == null ? part : parentFull + ":" + part;

				// Query cumulative full name
				string queryXml = buildCustomerTypeQueryRqXML(cumulative);
				string queryResp = processRequestFromQB(queryXml);
				if (parseCustomerTypeQueryRsHasMatch(queryResp))
				{
					parentFull = cumulative;
					continue;
				}

				// Not found: add node (name = part) with ParentRef = parentFull (if any)
				string addXml = buildCustomerTypeAddRqXML(part, parentFull);
				string addResp = processRequestFromQB(addXml);

				// Confirm it exists now
				string confirmResp = processRequestFromQB(queryXml);
				if (!parseCustomerTypeQueryRsHasMatch(confirmResp))
				{
					// creation failed - you can inspect addResp for QB error details if needed
					return false;
				}

				parentFull = cumulative;
			}

			return true;
		}


		// Returns true when the customer (identified by badge = FullName) has an Email on file
		// and the PreferredDeliveryMethod is NOT set to Email (so an update is needed).
		// Use manageSession=false when you already called connectToQB() for a batch operation.
		public bool NeedsPreferredDeliveryMethodUpdate(string badge, bool manageSession = true)
		{
			if (string.IsNullOrWhiteSpace(badge)) return false;
			if (manageSession) connectToQB();
			try
			{
				// Query both Email and PreferredDeliveryMethod in one request
				var xml = buildCustomerQueryRqXML(
					new string[] { "Email", "PreferredDeliveryMethod" },
					badge, "", "", "", false);

				var resp = processRequestFromQB(xml);
				if (string.IsNullOrEmpty(resp)) return false;

				try
				{
					var doc = new XmlDocument();
					doc.LoadXml(resp);

					var emailNode = doc.SelectSingleNode("//CustomerRet/Email");
					var prefNode = doc.SelectSingleNode("//CustomerRet/PreferredDeliveryMethod");

					string email = emailNode?.InnerText?.Trim();
					string pref = prefNode?.InnerText?.Trim();

					bool hasEmail = !string.IsNullOrWhiteSpace(email);

					// Treat common QB literals as Email preference. Be tolerant of "E-Mail" or any string containing "mail".
					bool isEmailPref = false;
					if (!string.IsNullOrWhiteSpace(pref))
					{
						if (string.Equals(pref, "Email", StringComparison.OrdinalIgnoreCase) ||
							string.Equals(pref, "E-Mail", StringComparison.OrdinalIgnoreCase) ||
							pref.IndexOf("mail", StringComparison.OrdinalIgnoreCase) >= 0)
						{
							isEmailPref = true;
						}
					}

					// Needs update when there's a main email and the pref is not already Email
					return hasEmail && !isEmailPref;
				}
				catch
				{
					// If parsing fails, be conservative and return false
					return false;
				}
			}
			finally
			{
				if (manageSession) disconnectFromQB();
			}
		}

		// Set PreferredDeliveryMethod for the customer identified by badge (FullName).
		// preferredMethod should be the QB literal (e.g. "Email", "Print", "None").
		// If manageSession is true this method will open/close the QB session; set false if caller already connected.
		public bool SetCustomerPreferredDeliveryMethod(string badge, string preferredMethod, bool manageSession = true)
		{
			if (string.IsNullOrWhiteSpace(badge)) return false;
			if (string.IsNullOrWhiteSpace(preferredMethod)) return false;

			if (manageSession) connectToQB();
			try
			{
				// get ListID/EditSequence
				string queryXml = buildCustomerQueryRqXML(new string[] { "ListID", "EditSequence" }, badge, "", "", "", false);
				string queryResp = processRequestFromQB(queryXml);
				CustomerIdData cid;
				try
				{
					cid = parseCustomerQueryRsShort(queryResp);
				}
				catch
				{
					return false;
				}
				if (string.IsNullOrEmpty(cid.ListId) || string.IsNullOrEmpty(cid.EditSequence)) return false;

				// build CustomerMod with PreferredDeliveryMethod
				XmlDocument xmlDoc = new XmlDocument();
				XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
				qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

				XmlElement customerModRq = xmlDoc.CreateElement("CustomerModRq");
				qbXMLMsgsRq.AppendChild(customerModRq);
				XmlElement customerMod = xmlDoc.CreateElement("CustomerMod");
				customerModRq.AppendChild(customerMod);

				customerMod.AppendChild(xmlDoc.CreateElement("ListID")).InnerText = cid.ListId;
				customerMod.AppendChild(xmlDoc.CreateElement("EditSequence")).InnerText = cid.EditSequence;

				// set PreferredDeliveryMethod (QB expects the literal)
				customerMod.AppendChild(xmlDoc.CreateElement("PreferredDeliveryMethod")).InnerText = preferredMethod;

				string req = xmlDoc.OuterXml;
				string resp = processRequestFromQB(req);

				// check response status code on CustomerModRs
				try
				{
					var doc = new XmlDocument();
					doc.LoadXml(resp);
					var rs = doc.SelectSingleNode("//CustomerModRs");
					if (rs != null && rs.Attributes != null)
					{
						var sc = rs.Attributes["statusCode"];
						return sc != null && sc.Value == "0";
					}
				}
				catch { /* ignore parse error */ }

				return false;
			}
			finally
			{
				if (manageSession) disconnectFromQB();
			}
		}

		/// <summary>
		/// Builds an InvoiceQueryRq to find invoices containing a specific item within a date range
		/// </summary>
		/// <param name="itemFullName">Item name to search for (e.g., "X06")</param>
		/// <param name="fromDate">Start date for the search</param>
		/// <param name="toDate">End date for the search</param>
		/// <param name="paidStatus">Optional: filter by paid status (PaidOnly, NotPaidOnly, or null for all)</param>
		/// <returns>QBXML request string</returns>
		public string buildInvoiceQueryByItemRqXML(string itemFullName, DateTime fromDate, DateTime toDate, string paidStatus = null)
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement qbXMLMsgsRq = buildRqEnvelope(xmlDoc, maxVersion);
			qbXMLMsgsRq.SetAttribute("onError", "stopOnError");

			XmlElement invQueryRq = xmlDoc.CreateElement("InvoiceQueryRq");
			qbXMLMsgsRq.AppendChild(invQueryRq);

			// Add date range filter
			XmlElement txnDateRangeFilter = xmlDoc.CreateElement("TxnDateRangeFilter");
			invQueryRq.AppendChild(txnDateRangeFilter);

			XmlElement fromDateElem = xmlDoc.CreateElement("FromTxnDate");
			fromDateElem.InnerText = fromDate.ToString("yyyy-MM-dd");
			txnDateRangeFilter.AppendChild(fromDateElem);

			XmlElement toDateElem = xmlDoc.CreateElement("ToTxnDate");
			toDateElem.InnerText = toDate.ToString("yyyy-MM-dd");
			txnDateRangeFilter.AppendChild(toDateElem);

			// Filter by paid status if specified
			if (!string.IsNullOrEmpty(paidStatus))
			{
				XmlElement paidStatusElem = xmlDoc.CreateElement("PaidStatus");
				invQueryRq.AppendChild(paidStatusElem).InnerText = paidStatus;
			}

			XmlElement includeLineItems = xmlDoc.CreateElement("IncludeLineItems");
			invQueryRq.AppendChild(includeLineItems).InnerText = "true";

			XmlElement includeLikedTxns = xmlDoc.CreateElement("IncludeLinkedTxns");
			invQueryRq.AppendChild(includeLikedTxns).InnerText = "true";

			invQueryRq.SetAttribute("requestID", "1");
			return xmlDoc.OuterXml;
		}

		/// <summary>
		/// Gets summary statistics for invoices containing a specific item
		/// </summary>
		/// <param name="itemFullName">Item to search for (e.g., "X06")</param>
		/// <param name="fromDate">Start date</param>
		/// <param name="toDate">End date</param>
		/// <returns>Dictionary with statistics</returns>
		public Dictionary<string, object> GetInvoiceStatsByItem(string itemFullName, DateTime fromDate, DateTime toDate)
		{
			connectToQB();

			try
			{
				// Query all invoices in the date range
				string queryXml = buildInvoiceQueryByItemRqXML(itemFullName, fromDate, toDate);
				string response = processRequestFromQB(queryXml);

				if (string.IsNullOrEmpty(response))
					return null;

				// Parse response and filter by item
				var allInvoices = parseInvoiceQueryRs(response);
				var matchingInvoices = FilterInvoicesByItem(response, itemFullName);

				// Calculate statistics
				int totalInvoices = matchingInvoices.Count;
				int paidInvoices = matchingInvoices.Count(inv =>
					string.IsNullOrEmpty(inv.BalanceRemaining) ||
					Convert.ToDouble(inv.BalanceRemaining) == 0);
				int unpaidInvoices = totalInvoices - paidInvoices;

				double totalBilled = 0;
				double totalPaid = 0;
				double totalUnpaid = 0;

				foreach (var inv in matchingInvoices)
				{
					double subtotal = string.IsNullOrEmpty(inv.Subtotal) ? 0 : Convert.ToDouble(inv.Subtotal);
					double balance = string.IsNullOrEmpty(inv.BalanceRemaining) ? 0 : Convert.ToDouble(inv.BalanceRemaining);

					totalBilled += subtotal;
					totalUnpaid += balance;
					totalPaid += (subtotal - balance);
				}

				return new Dictionary<string, object>
		{
			{ "TotalInvoices", totalInvoices },
			{ "PaidInvoices", paidInvoices },
			{ "UnpaidInvoices", unpaidInvoices },
			{ "TotalBilled", totalBilled },
			{ "TotalPaid", totalPaid },
			{ "TotalUnpaid", totalUnpaid },
			{ "Invoices", matchingInvoices }
		};
			}
			finally
			{
				disconnectFromQB();
			}
		}

		/// <summary>
		/// Filters parsed invoices to only those containing a specific item
		/// </summary>
		private List<InvoiceData> FilterInvoicesByItem(string invoiceQueryResponseXml, string itemFullName)
		{
			var filteredList = new List<InvoiceData>();

			if (string.IsNullOrEmpty(invoiceQueryResponseXml))
				return filteredList;

			try
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(invoiceQueryResponseXml);

				XmlNodeList invNodes = doc.SelectNodes("//InvoiceRet");

				foreach (XmlNode invNode in invNodes)
				{
					// Check if this invoice contains the specified item
					XmlNodeList lineItems = invNode.SelectNodes(".//InvoiceLineRet");
					bool hasMatchingItem = false;

					foreach (XmlNode lineItem in lineItems)
					{
						XmlNode itemRefNode = lineItem.SelectSingleNode("ItemRef/FullName");
						if (itemRefNode != null && itemRefNode.InnerText == itemFullName)
						{
							hasMatchingItem = true;
							break;
						}
					}

			if (hasMatchingItem)
			{
				// Parse this invoice
				InvoiceData ivd = new InvoiceData();
				ivd.TxnID = getInnerText(invNode.SelectSingleNode("TxnID"));
				ivd.EditSequence = getInnerText(invNode.SelectSingleNode("EditSequence"));
				ivd.Badge = getInnerText(invNode.SelectSingleNode("CustomerRef/FullName"));
				ivd.Subtotal = getInnerText(invNode.SelectSingleNode("Subtotal"));
				ivd.BalanceRemaining = getInnerText(invNode.SelectSingleNode("BalanceRemaining"));
				ivd.DueDate = getInnerText(invNode.SelectSingleNode("DueDate"));
				
				// Get payment date from LinkedTxn (ReceivePayment transactions)
				// If invoice is paid (balance = 0), find the most recent payment date
				double balance = string.IsNullOrEmpty(ivd.BalanceRemaining) ? 0 : Convert.ToDouble(ivd.BalanceRemaining);
				if (balance == 0)
				{
					XmlNodeList linkedTxns = invNode.SelectNodes(".//LinkedTxn");
					DateTime? latestPaymentDate = null;
					
					foreach (XmlNode linkedTxn in linkedTxns)
					{
						string txnType = getInnerText(linkedTxn.SelectSingleNode("TxnType"));
						if (txnType == "ReceivePayment")
						{
							string txnDateStr = getInnerText(linkedTxn.SelectSingleNode("TxnDate"));
							if (!string.IsNullOrEmpty(txnDateStr))
							{
								DateTime txnDate;
								if (DateTime.TryParse(txnDateStr, out txnDate))
								{
									if (!latestPaymentDate.HasValue || txnDate > latestPaymentDate.Value)
									{
										latestPaymentDate = txnDate;
									}
								}
							}
						}
					}
					
					// Store the latest payment date
					if (latestPaymentDate.HasValue)
					{
						ivd.AppliedAmount = latestPaymentDate.Value.ToString("yyyy-MM-dd");
					}
				}

				filteredList.Add(ivd);
			}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error filtering invoices by item: " + ex.Message);
			}

			return filteredList;
		}

		/// <summary>
		/// Gets a list of customers who have paid their X06 (club dues) invoice
		/// </summary>
		/// <param name="itemFullName">Item name (e.g., "X06" for club dues)</param>
		/// <param name="fromDate">Start date for invoice search</param>
		/// <param name="toDate">End date for invoice search</param>
		/// <returns>List of CustomerData with Badge, FirstName, LastName, and paid status</returns>
		public List<CustomerData> GetPaidMembersByItem(string itemFullName, DateTime fromDate, DateTime toDate)
		{
			var paidMembers = new List<CustomerData>();

			connectToQB();

			try
			{
				// Query all invoices in the date range with paid status filter
				string queryXml = buildInvoiceQueryByItemRqXML(itemFullName, fromDate, toDate, "PaidOnly");
				string response = processRequestFromQB(queryXml);

				if (string.IsNullOrEmpty(response))
					return paidMembers;

				// Filter by item and extract customer info
				var matchingInvoices = FilterInvoicesByItem(response, itemFullName);

				// Build list of unique customers (badge) with paid status
				var distinctBadges = matchingInvoices.Select(inv => inv.Badge).Distinct();

				foreach (var badge in distinctBadges)
				{
					// Get customer details for each badge
					var customerList = loadCustomers(badge, string.Empty, string.Empty, string.Empty, false, false);

				if (customerList != null && customerList.Count > 0)
				{
					var customer = customerList[0];
					// Find the invoice for this badge to get the paid date
					var invoice = matchingInvoices.FirstOrDefault(inv => inv.Badge == badge);
					paidMembers.Add(new CustomerData
					{
						FullName = customer.FullName,
						FirstName = customer.FirstName,
						LastName = customer.LastName,
						Balance = "0", // Paid invoice means balance is 0
						PaidDate = invoice != null ? invoice.AppliedAmount : string.Empty
					});
				}
				else
				{
					// If customer details not found, use badge only
					var invoice = matchingInvoices.FirstOrDefault(inv => inv.Badge == badge);
					paidMembers.Add(new CustomerData
					{
						FullName = badge,
						Balance = "0",
						PaidDate = invoice != null ? invoice.AppliedAmount : string.Empty
					});
				}
				}
			}
			finally
			{
				disconnectFromQB();
			}

			return paidMembers;
		}

		/// <summary>
		/// Gets a simplified list of paid/unpaid members for X06 invoice
		/// </summary>
		/// <param name="itemFullName">Item name (e.g., "X06")</param>
		/// <param name="fromDate">Start date</param>
		/// <param name="toDate">End date</param>
		/// <returns>Dictionary with "Paid" and "Unpaid" lists of CustomerData</returns>
		public Dictionary<string, List<CustomerData>> GetMemberPaymentStatus(string itemFullName, DateTime fromDate, DateTime toDate)
		{
			var result = new Dictionary<string, List<CustomerData>>
			{
				{ "Paid", new List<CustomerData>() },
				{ "Unpaid", new List<CustomerData>() }
			};

			connectToQB();

			try
			{
				// Get all invoices with X06 (both paid and unpaid)
				string queryXml = buildInvoiceQueryByItemRqXML(itemFullName, fromDate, toDate);
				string response = processRequestFromQB(queryXml);

				if (string.IsNullOrEmpty(response))
					return result;

				// Filter by item
				var matchingInvoices = FilterInvoicesByItem(response, itemFullName);

				// Group by badge and determine paid status
				var groupedByBadge = matchingInvoices.GroupBy(inv => inv.Badge);

				foreach (var group in groupedByBadge)
				{
					string badge = group.Key;

					// Check if all invoices for this customer are paid (balance = 0)
					bool isPaid = group.All(inv =>
						string.IsNullOrEmpty(inv.BalanceRemaining) ||
						Convert.ToDouble(inv.BalanceRemaining) == 0);

					// Get customer details
					var customerList = loadCustomers(badge, string.Empty, string.Empty, string.Empty, false, false);

				CustomerData customerData;
				if (customerList != null && customerList.Count > 0)
				{
					var customer = customerList[0];
					customerData = new CustomerData
					{
						FullName = customer.FullName,
						FirstName = customer.FirstName,
						LastName = customer.LastName,
						Balance = isPaid ? "0" : group.First().BalanceRemaining,
						PaidDate = isPaid ? group.First().AppliedAmount : string.Empty
					};
				}
				else
				{
					customerData = new CustomerData
					{
						FullName = badge,
						Balance = isPaid ? "0" : group.First().BalanceRemaining,
						PaidDate = isPaid ? group.First().AppliedAmount : string.Empty
					};
				}

					// Add to appropriate list
					if (isPaid)
						result["Paid"].Add(customerData);
					else
						result["Unpaid"].Add(customerData);
				}
			}
			finally
			{
				disconnectFromQB();
			}

			return result;
		}
	}
}

﻿using Interop.QBXMLRP2;
using System;
using System.Collections.Generic;
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
		string balanceAmount)
		{
			List<CustomerData> customerData = new List<CustomerData>();
			string request = "CustomerQueryRq";
			try
			{
				connectToQB();
				int count = getCount(request);
				string xml = buildCustomerQueryRqXML(
					new string[] { "FullName", "FirstName", "LastName", "Balance", "AccountNumber", "CustomerTypeRef", "DataExtRet" },
					fullName,
					status,
					balanceFilter,
					balanceAmount);
				string response = processRequestFromQB(xml);
				customerData = parseCustomerQueryRs(response);
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }

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

			XmlElement customerTypeRef = xmlDoc.CreateElement("CustomerTypeRef");
			customerTypeRef.AppendChild(xmlDoc.CreateElement("FullName")).InnerText = "Club Member:X06F";
			customerAdd.AppendChild(customerTypeRef);

			customerAdd.AppendChild(xmlDoc.CreateElement("AccountNumber")).InnerText = newMember.RecNo;

			customerAddRq.AppendChild(customerAdd);

			xml = xmlDoc.OuterXml;
			return xml;
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

	}
}

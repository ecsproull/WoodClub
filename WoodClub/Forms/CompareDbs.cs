using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Contexts;

namespace WoodClub

{
	public partial class CompareDbs : Form
	{
		private	List<CompareResults> results = new List<CompareResults>();
		public CompareDbs()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{ 
			
			QBFunctions qbf = new QBFunctions();
			qbf.connectToQB();
			List<CustomerData> qbCustomers = qbf.loadCustomers(string.Empty, string.Empty, string.Empty, string.Empty, true, false);
			using (WoodClubEntities context = new WoodClubEntities())
			{
				if (qbCustomers.Count > 0)
				{
					List<MemberRoster> roster = (from m in context.MemberRosters
												 where m.Badge != "20001"
												 select m).ToList();

					compareProgressBar.Maximum = roster.Count + qbCustomers.Count;
					compareProgressBar.Step = 1;
					compareProgressBar.Value = 0;
					foreach (CustomerData customer in qbCustomers)
					{
						List<MemberRoster> members = (from m in context.MemberRosters
													  where m.Badge == customer.FullName
													  select m).ToList();
						if (members.Count == 0)
						{
							if (Regex.Match(customer.FullName, "^[0-9]{4}$", RegexOptions.Multiline).Success)
							{
								int balanceOwed = 0;
								foreach (InvoiceData invoice in customer.OpenInvoices)
								{
									balanceOwed += (int)Convert.ToDouble(invoice.BalanceRemaining);
								}
								results.Add(new CompareResults
								{
									Badge = customer.FullName,
									First = customer.FirstName,
									Last = customer.LastName,
									OnlyDb = "QB",
									OpenInvoices = customer.OpenInvoices != null && customer.OpenInvoices.Count > 0 ?
										customer.OpenInvoices.Count.ToString() : "0",
									Balance = balanceOwed.ToString()
								});
							}
						}
						compareProgressBar.PerformStep();
					}
				
					foreach (MemberRoster member in roster)
					{
						List<CustomerData> customer = qbf.loadCustomers(member.Badge, string.Empty, string.Empty, string.Empty, false);
						if (customer.Count == 0)
						{
							results.Add(new CompareResults
							{
								Badge = member.Badge,
								First = member.FirstName,
								Last = member.LastName,
								OnlyDb = "Shop",
								OpenInvoices = "0",
								Balance = "0"
							});
						}

						compareProgressBar.PerformStep();

					}
				}
				compareBindingSource.DataSource = results;
				qbf.disconnectFromQB();
			}
		}
		
		private void ProcessInactiveCustomers()
		{
			if (results == null || results.Count == 0)
			{
				MessageBox.Show("No results to process.");
				return;
			}

			QBFunctions qbf = new QBFunctions();
			qbf.connectToQB();
			
			int successCount = 0;
			int errorCount = 0;
			System.Text.StringBuilder errorLog = new System.Text.StringBuilder();

			try
			{
				foreach (CompareResults result in results)
				{
					if (string.IsNullOrEmpty(result.Badge))
						continue;

					try
					{
						// Load customer with open invoices
						List<CustomerData> customers = qbf.loadCustomers(result.Badge, string.Empty, string.Empty, string.Empty, true, false);
						
						if (customers.Count == 0)
						{
							errorLog.AppendLine($"Customer {result.Badge} not found in QuickBooks.");
							errorCount++;
							continue;
						}

						CustomerData customer = customers[0];

						// Delete open invoices if any exist
						if (customer.OpenInvoices != null && customer.OpenInvoices.Count > 0)
						{
							foreach (InvoiceData invoice in customer.OpenInvoices)
							{
								string deleteXml = qbf.buildInvoiceDeleteRqXML(invoice.TxnID, invoice.EditSequence);
								string deleteResponse = qbf.processRequestFromQB(deleteXml);
								
								// Check for QB errors in delete response
								string qberr = qbf.ParseQbError(deleteResponse);
								if (!string.IsNullOrEmpty(qberr))
								{
									errorLog.AppendLine($"Failed to delete invoice for {result.Badge}: {qberr}");
								}
							}
						}

						// Set customer inactive
						string inactiveXml = qbf.buildCustomerSetInactiveRqXML(result.Badge);
						string inactiveResponse = qbf.processRequestFromQB(inactiveXml);
						
						string inactiveErr = qbf.ParseQbError(inactiveResponse);
						if (!string.IsNullOrEmpty(inactiveErr))
						{
							errorLog.AppendLine($"Failed to set {result.Badge} inactive: {inactiveErr}");
							errorCount++;
						}
						else
						{
							successCount++;
						}
					}
					catch (Exception ex)
					{
						errorLog.AppendLine($"Error processing {result.Badge}: {ex.Message}");
						errorCount++;
					}
				}
			}
			finally
			{
				qbf.disconnectFromQB();
			}

			// Show summary
			string summary = $"Processing Complete:\n\nSuccessfully processed: {successCount}\nErrors: {errorCount}";
			if (errorLog.Length > 0)
			{
				summary += "\n\nError Details:\n" + errorLog.ToString();
			}
			MessageBox.Show(summary);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			ProcessInactiveCustomers();
		}
	}
}

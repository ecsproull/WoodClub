using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WoodClub
{
	public partial class UpdateDuesPaid : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private readonly List<BadgeDate> paidList = new List<BadgeDate>();
		private SortableBindingList<UnpaidMemberData> ds_Unpaid;

		private class BadgeDate
		{
			public string PaidDate { get; set; }
			public string Badge { get; set; }
		}

		public UpdateDuesPaid()
		{
			InitializeComponent();
			dataGridView1.RowPostPaint += DataGridView1_RowPostPaint;
		}

		private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			var grid = sender as DataGridView;
			var rowIdx = (e.RowIndex + 1).ToString();

			var centerFormat = new StringFormat()
			{
				// right alignment might actually make more sense for numbers
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};

			var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
			e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
		}

		private void UpdateDuesPaid_Load(object sender, EventArgs e)
		{
			Stream flStream = null;
			OpenFileDialog theDialog = new OpenFileDialog();
			theDialog.Title = "Open SCW Paid Members (.csv) File";
			theDialog.Filter = "CSV files|*.csv";
			theDialog.InitialDirectory = theDialog.InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Documents";
			if (theDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((flStream = theDialog.OpenFile()) != null)
					{
						using (flStream)
						{
							using (StreamReader sr = new StreamReader(flStream))
							{
								while (!sr.EndOfStream)
								{
									string line = sr.ReadLine();
									string[] parts = line.Split(',');
									if (!string.IsNullOrEmpty(parts[0]) && !string.IsNullOrEmpty(parts[1]))
									{
										paidList.Add(new BadgeDate
										{
											PaidDate = parts[0],
											Badge = parts[1]
										});
									}
								}
							}

							if (paidList.Count > 0)
							{
								updatePaidButton.Enabled = true;
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}

				//Reconcile();
				unpaidMemberBindingSource.DataSource = paidList;
				dataGridView1.DataSource = unpaidMemberBindingSource.DataSource;
				dataGridView1.Invalidate();
				log.Info("Scan complete");
			}
			else
			{
				FindUnPaid();
				unpaidMemberBindingSource.DataSource = ds_Unpaid;
				dataGridView1.DataSource = unpaidMemberBindingSource.DataSource;
				dataGridView1.Invalidate();
				log.Info("Scan complete"); ;
			}
		}
		
		private void UpdatePaidDataBase()
		{
			ds_Unpaid = new SortableBindingList<UnpaidMemberData>();
			using (WoodClubEntities context = new WoodClubEntities())
			{
				List<MemberRoster> members = (from m in context.MemberRosters
											  select m).OrderBy(mem => mem.Badge).ToList();
				foreach (MemberRoster member in members)
				{
					if (member.Badge == "20001")
					{
						continue;
					}

					BadgeDate mrFound = paidList.Find(item => item.Badge == member.Badge);

					if (mrFound != null)        // found item
					{
						member.ClubDuesPaid = true;
						member.ClubDuesPaidDate = DateTime.Parse(mrFound.PaidDate);
					}
					else
					{
						member.ClubDuesPaid = false;
						AddToList(member);
					}
				}
				context.SaveChanges();

				foreach (BadgeDate bd in paidList)
				{
					MemberRoster member = (from m in context.MemberRosters
											where m.Badge == bd.Badge
											select m).FirstOrDefault();
					if (member == null)
					{
						MessageBox.Show("Missing Badge : " + bd.Badge);
					}
				}
			}
		}

		private void FindUnPaid()
		{
			ds_Unpaid = new SortableBindingList<UnpaidMemberData>();
			using (WoodClubEntities context = new WoodClubEntities())
			{
				List<MemberRoster> members = (from m in context.MemberRosters
											  where m.ClubDuesPaid == false
											  select m).OrderBy(mem => mem.Badge).ToList();
				foreach (MemberRoster member in members)
				{
					AddToList(member, true);
				}
			}
		}

		private void Reconcile()
		{
			ds_Unpaid = new SortableBindingList<UnpaidMemberData>();
			using (WoodClubEntities context = new WoodClubEntities())
			{
				List<MemberRoster> members = (from m in context.MemberRosters
											  select m).OrderBy(mem => mem.Badge).ToList();
				foreach (MemberRoster member in members)
				{
					BadgeDate mrFound = paidList.Find(item => item.Badge == member.Badge);
					if (mrFound != null && !member.ClubDuesPaid.Value)        // found item
					{
						AddToList(member, false);
					}
					else if (mrFound == null && member.ClubDuesPaid.Value)
					{
						AddToList(member, true);
					}

					if (mrFound != null)
                    {
						paidList.Remove(mrFound);
                    }
				}

				if (paidList.Count > 0)
                {
					foreach (BadgeDate bd in paidList)
                    {
						if (!string.IsNullOrEmpty(bd.Badge))
						{
							MemberRoster mr = (from m in context.MemberRosters
											   where m.Badge == bd.Badge
											   select m).FirstOrDefault();
							if (mr != null)
							{
								MessageBox.Show("Missed Badge Number : " + mr.Badge + " Dues Paid: " + mr.ClubDuesPaid.Value.ToString());
							}
							else
							{
								MessageBox.Show("Not in database, Badge Number : " + bd.Badge);
							}
						}
					}
                }
			}
		}

		private void AddToList(MemberRoster member, bool delete = false)
		{
			UnpaidMemberData upm = new UnpaidMemberData
			{
				Badge = member.Badge,
				FirstName = member.FirstName,
				LastName = member.LastName,
				MemberDate = member.MemberDate,
				RecCard = member.RecCard,
				Address = member.Address,
				ClubDuesPaid = member.ClubDuesPaid,
				ClubDuesPaidDate = member.ClubDuesPaidDate,
				Phone = member.Phone,
				Email = member.Email,
				State = member.State,
				Delete = delete
			};

			ds_Unpaid.Add(upm);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				foreach (UnpaidMemberData unpaid in ds_Unpaid)
				{
					if (unpaid.Delete == true)
					{
						var member = (from rn in context.MemberRosters
									where rn.Badge == unpaid.Badge && rn.RecCard != "20001"
									select rn).FirstOrDefault();
						// query.Single().NewBadge = false;
						if (member != null)
						{
							context.MemberRosters.Remove(member);
						}
					}
				}

				context.SaveChanges();
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			UpdatePaidDataBase();
		}

		private void CheckX06InvoiceStatus()
		{
			QBFunctions qbf = new QBFunctions();

			try
			{
				// Query invoices from December 28, 2025 to January 5, 2026 to catch any issued around Jan 1
				DateTime fromDate = new DateTime(2025, 12, 31);
				DateTime toDate = new DateTime(2026, 1, 2);

				var stats = qbf.GetInvoiceStatsByItem("X06", fromDate, toDate);

				if (stats != null)
				{
					string summary = $"2026 Club Dues (X06) Invoice Status:\n\n" +
									$"Total Invoices Issued: {stats["TotalInvoices"]}\n" +
									$"Paid: {stats["PaidInvoices"]}\n" +
									$"Unpaid: {stats["UnpaidInvoices"]}\n\n" +
									$"Total Billed: ${stats["TotalBilled"]:F2}\n" +
									$"Total Paid: ${stats["TotalPaid"]:F2}\n" +
									$"Total Outstanding: ${stats["TotalUnpaid"]:F2}";

					MessageBox.Show(summary, "X06 Invoice Status");

					// Optionally populate a grid with the individual invoices
					var invoices = stats["Invoices"] as List<InvoiceData>;
					if (invoices != null)
					{
						// You can bind this to a DataGridView to see individual customer status
						// dataGridView1.DataSource = invoices;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error checking invoice status: " + ex.Message);
				log.Error("Error checking X06 invoice status", ex);
			}
		}

		private void statsButton_Click(object sender, EventArgs e)
		{
			CheckX06InvoiceStatus();
		}

		private void LoadPaidMembersFromQB()
		{
			QBFunctions qbf = new QBFunctions();

			try
			{
				// Get members who paid their 2026 dues (X06 invoice from Jan 1)
				DateTime fromDate = new DateTime(2025, 12, 28);
				DateTime toDate = new DateTime(2026, 1, 5);

				// Get paid members from QuickBooks
				List<CustomerData> paidMembers = qbf.GetPaidMembersByItem("X06", fromDate, toDate);

				// Clear the existing paidList and populate it with QB data
				paidList.Clear();

				// Convert CustomerData to BadgeDate format
				foreach (var paidMember in paidMembers)
				{
					paidList.Add(new BadgeDate
					{
						Badge = paidMember.FullName,
						PaidDate = paidMember.PaidDate // This is the payment date from QB
					});
				}

				// Set the datasource
				unpaidMemberBindingSource.DataSource = paidList;
				dataGridView1.DataSource = unpaidMemberBindingSource.DataSource;
				dataGridView1.Invalidate();

				MessageBox.Show($"Loaded {paidList.Count} paid members from QuickBooks", "Load Complete");
				log.Info($"Loaded {paidList.Count} paid members from QuickBooks");

				// Enable the update button if we have data
				if (paidList.Count > 0)
				{
					updatePaidButton.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading paid members from QuickBooks: " + ex.Message);
				log.Error("Error loading paid members from QuickBooks", ex);
			}
		}

		private void LoadUnpaidMembersFromQB()
		{
			QBFunctions qbf = new QBFunctions();

			try
			{
				// Get members who have unpaid X06 invoices (2026 dues)
				DateTime fromDate = new DateTime(2025, 12, 28);
				DateTime toDate = new DateTime(2026, 1, 10);

				// Get payment status from QuickBooks
				var paymentStatus = qbf.GetMemberPaymentStatus("X06", fromDate, toDate);

				// Clear and populate the unpaid list
				ds_Unpaid = new SortableBindingList<UnpaidMemberData>();

				using (WoodClubEntities context = new WoodClubEntities())
				{
					// Process unpaid members from QB
					foreach (var unpaidMember in paymentStatus["Unpaid"])
					{
						// Find the member in the local database by badge
						MemberRoster member = (from m in context.MemberRosters
											   where m.Badge == unpaidMember.FullName
											   select m).FirstOrDefault();

						if (member != null)
						{
							// Add to the unpaid list
							UnpaidMemberData upm = new UnpaidMemberData
							{
								Badge = member.Badge,
								FirstName = member.FirstName,
								LastName = member.LastName,
								MemberDate = member.MemberDate,
								RecCard = member.RecCard,
								Address = member.Address,
								ClubDuesPaid = member.ClubDuesPaid,
								ClubDuesPaidDate = member.ClubDuesPaidDate,
								Phone = member.Phone,
								Email = member.Email,
								State = member.State,
								Delete = false,
								UnpaidInvoiceId = unpaidMember.UnpaidInvoiceId
							};

							ds_Unpaid.Add(upm);
						}
						else
						{
							// Member exists in QB but not in local DB - log warning
							log.Warn($"Member {unpaidMember.FullName} found in QB with unpaid X06 invoice but not in local database");
						}
					}
				}

				// Bind to the view
				unpaidMemberBindingSource.DataSource = ds_Unpaid;
				dataGridView1.DataSource = unpaidMemberBindingSource.DataSource;
				dataGridView1.Invalidate();

				MessageBox.Show($"Loaded {ds_Unpaid.Count} unpaid members from QuickBooks", "Load Complete");
				log.Info($"Loaded {ds_Unpaid.Count} unpaid members from QuickBooks");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error loading unpaid members from QuickBooks: " + ex.Message);
				log.Error("Error loading unpaid members from QuickBooks", ex);
			}
		}

		private void paidListButton_Click(object sender, EventArgs e)
		{
			LoadPaidMembersFromQB();
		}

		private void unpaidMemberBindingSource_CurrentChanged(object sender, EventArgs e)
		{

		}

		private void sendTextButton_Click(object sender, EventArgs e)
		{
			SendText st = new SendText();
			foreach (UnpaidMemberData upm in ds_Unpaid)
			{
				if (string.IsNullOrEmpty(upm.Phone) || string.IsNullOrEmpty(upm.UnpaidInvoiceId))
				{
					continue;
				}

				StringBuilder sb = new StringBuilder();
				sb.Append(upm.FirstName);
				sb.Append(", unless you paid today, you have an open invoice for 2026 dues that is due by Feb 1st. You can pay here: https://scwwoodshop.com/?pay=");
				sb.Append(upm.UnpaidInvoiceId);
				sb.Append(" This was initially sent to ");
				sb.Append(upm.Email);
				sb.Append(" On Jan 1st. You may also come in to the lumber room and pay.");
				
				string message = sb.ToString();
				st.CreateText(message, upm.Phone);
			}

			MessageBox.Show("Text messages sent for unpaid members with phone numbers.", "Texting Complete");
		}

		private void unPaidListButton_Click(object sender, EventArgs e)
		{
			LoadUnpaidMembersFromQB();
		}
	}
}

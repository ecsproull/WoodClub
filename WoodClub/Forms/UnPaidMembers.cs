using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Windows.Forms;

namespace WoodClub
{
	public partial class UpdateDuesPaid : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private readonly List<MemberDuesData> paidList = new List<MemberDuesData>();
		private SortableBindingList<MemberDuesData> ds_Unpaid;

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

		}

		private void UpdatePaidDataBase()
		{
			ds_Unpaid = new SortableBindingList<MemberDuesData>();
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

					MemberDuesData mrFound = paidList.Find(item => item.Badge == member.Badge);

					if (mrFound != null)        // found item
					{
						member.ClubDuesPaid = true;
						member.ClubDuesPaidDate = mrFound.ClubDuesPaidDate;
					}
					else
					{
						member.ClubDuesPaid = false;
						AddToList(member);
					}
				}
				context.SaveChanges();

				foreach (MemberDuesData bd in paidList)
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

		private void AddToList(MemberRoster member, bool delete = false)
		{
			MemberDuesData upm = new MemberDuesData
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
				foreach (MemberDuesData unpaid in ds_Unpaid)
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

		private void updatePaidButton_Click(object sender, EventArgs e)
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
				DateTime toDate = new DateTime(2026, 1, 31);

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
				DateTime toDate = new DateTime(2026, 1, 31);

				// Get paid members from QuickBooks
				List<CustomerData> paidMembers = qbf.GetPaidMembersByItem("X06", fromDate, toDate);

				// Clear the existing paidList and populate it with QB data
				paidList.Clear();
				using (WoodClubEntities context = new WoodClubEntities())
				{
					// Convert CustomerData to BadgeDate format
					foreach (var paidMember in paidMembers)
					{
						// Find the member in the local database by badge
						MemberRoster member = (from m in context.MemberRosters
											   where m.Badge == paidMember.FullName
											   select m).FirstOrDefault();

						if (member != null)
						{
							// Add to the unpaid list
							MemberDuesData upm = new MemberDuesData
							{
								Badge = member.Badge,
								FirstName = member.FirstName,
								LastName = member.LastName,
								MemberDate = member.MemberDate,
								RecCard = member.RecCard,
								Address = member.Address,
								ClubDuesPaid = member.ClubDuesPaid,
								ClubDuesPaidDate = DateTime.Parse(paidMember.PaidDate),
								Phone = member.Phone,
								Email = member.Email,
								State = member.State,
								Delete = false,
								DuesInvoiceId = paidMember.InvoiceId
							};

							paidList.Add(upm);
						}
					}
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
				DateTime toDate = new DateTime(2026, 1, 31);

				// Get payment status from QuickBooks
				var paymentStatus = qbf.GetMemberPaymentStatus("X06", fromDate, toDate);

				// Clear and populate the unpaid list
				ds_Unpaid = new SortableBindingList<MemberDuesData>();

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
							MemberDuesData upm = new MemberDuesData
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
								DuesInvoiceId = unpaidMember.InvoiceId
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

		private void unPaidListButton_Click(object sender, EventArgs e)
		{
			LoadUnpaidMembersFromQB();
		}

		private void emailButton_Click(object sender, EventArgs e)
		{
			notifyUnPaidDues();

		}

		private void sendTextButton_Click(object sender, EventArgs e)
		{
			notifyUnPaidDues(false);
		}

		private async void notifyUnPaidDues(bool sendEmail = true)
		{
			SendMail sm = new SendMail();
			SendText st = new SendText();
			foreach (MemberDuesData upm in ds_Unpaid)
			{
				if (string.IsNullOrEmpty(upm.DuesInvoiceId))
				{
					continue;
				}
				StringBuilder sb = new StringBuilder();
				sb.Append(upm.FirstName);
				sb.Append(", unless you paid today, you have an open invoice for 2026 dues that is due by Feb 1st. You can pay here: https://scwwoodshop.com/?pay=");
				sb.Append(upm.DuesInvoiceId);
				sb.Append(" This was initially sent to ");
				sb.Append(upm.Email);
				sb.Append(" On Jan 1st. A $10 late fee will be applied at midnight, Jan 31st.");
				string message = sb.ToString();
				if (sendEmail && !string.IsNullOrEmpty(upm.Email))
				{
					string toName = upm.FirstName + " " + upm.LastName;
					await sm.SendSingleEmailAsync(upm.Email, toName, "2026 Woodshop Dues Payment Reminder", message);
				}
				else
				{
					st.CreateText(message, upm.Phone);
				}
			}
			MessageBox.Show("Messages sent for unpaid members with email addresses.", "Sending Complete");
		}
	}
}

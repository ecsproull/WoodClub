using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class QbXml : Form
	{
		private List<QbAccountData> accounts;
		private List<QbInventoryItem> items;
		private List<QbVendorData> vendors;
		public QbXml()
		{
			InitializeComponent();
		}

		private void loadInvbutton_Click(object sender, EventArgs e)
		{
			this.dataGridQbItems.AutoGenerateColumns = true;
			QBFunctions qbXml = new QBFunctions();
			items = qbXml.GetActiveItems();
			qbItemsBindingSource.DataSource = items;
		}

		private void accountsButton_Click(object sender, EventArgs e)
		{
			this.dataGridQbItems.AutoGenerateColumns = true;
			QBFunctions qbXml = new QBFunctions();
			accounts = qbXml.GetAccounts(true);
			qbItemsBindingSource.DataSource = new SortableBindingList<QbAccountData>(accounts);
		}

		private string[] GetAcctTypeAndNormBalance(string qbtype)
		{
			if (string.IsNullOrWhiteSpace(qbtype))
			{
				return new[] { string.Empty, string.Empty, string.Empty };
			}

			switch (qbtype.Trim())
			{
				// ===== ASSETS =====
				case "Bank":
					return new[] { "Asset", "DEBIT", "BANK" };

				case "AccountsReceivable":
					return new[] { "Asset", "DEBIT", "ACCOUNTS_RECEIVABLE" };

				case "OtherCurrentAsset":
					return new[] { "Asset", "DEBIT", "OTHER_CURRENT_ASSET" };

				case "FixedAsset":
					return new[] { "Fixed Asset", "DEBIT", "FIXED_ASSET" };

				// ===== LIABILITIES =====
				case "AccountsPayable":
					return new[] { "Liability", "CREDIT", "ACCOUNTS_PAYABLE" };

				case "CreditCard":
					return new[] { "Liability", "CREDIT", "CREDIT_CARD" };

				case "OtherCurrentLiability":
					return new[] { "Liability", "CREDIT", "OTHER_CURRENT_LIABILITY" };

				case "LongTermLiability":
					return new[] { "Liability", "CREDIT", "LONG_TERM_LIABILITY" };

				// ===== EQUITY =====
				case "Equity":
					return new[] { "Equity", "CREDIT", "EQUITY_GENERAL" };

				// ===== INCOME =====
				case "Income":
					return new[] { "Income", "CREDIT", "OPERATING_INCOME" };	
				case "OtherIncome":
					return new[] { "Income", "CREDIT", "OTHER_INCOME" };

				// ===== EXPENSES =====
				case "CostOfGoodsSold":
					return new[] { "Expense COGS", "DEBIT", "COGS" };

				case "Expense":
					return new[] { "Expense", "DEBIT", "OPERATING_EXPENSE" };

				case "OtherExpense":
					return new[] { "Expense", "DEBIT", "OTHER_EXPENSE" };

				default:
					// Unknown QB type – preserve QB type as subtype for debugging
					return new[] { qbtype.Trim(), string.Empty, qbtype.Trim().ToUpper() };
			}
		}


		private string GetParentAccount(string fullName)
		{
			int index = fullName.LastIndexOf(':');
			if (index > 0)
			{
				return fullName.Substring(0, index);
			}
			else
			{
				return fullName;
			}
		}

		private void addAcctsButton_Click(object sender, EventArgs e)
		{
			using (AccountingEntities context = new AccountingEntities())
			{
				foreach (QbAccountData acct in accounts)
				{
					string[] acct_type = GetAcctTypeAndNormBalance(acct.AccountType);
					DateTime? externalTimeModified = null;
					if (!string.IsNullOrWhiteSpace(acct.TimeModified))
					{
						if (DateTime.TryParse(acct.TimeModified, out DateTime parsedDate))
						{
							externalTimeModified = parsedDate;
						}
					}

					string parentAccountFullName = GetParentAccount(acct.FullName);
					long? parentaccount_id = null;
					if (parentAccountFullName != acct.FullName)
					{
						account parentAccount = context.accounts.FirstOrDefault(a => a.full_name == parentAccountFullName);
						if (parentAccount != null)
						{
							// Set the parent_account_id for the current account
							parentaccount_id = parentAccount.account_id;
						}
						else
						{
							// Handle the case where the parent account is not found in the database
							MessageBox.Show($"Parent account '{parentAccountFullName}' not found for account '{acct.FullName}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}

					decimal bal_temp = decimal.Parse(acct.Balance, CultureInfo.InvariantCulture);
					long ext_bal = (long)(bal_temp * 100m);

					decimal tot_bal_temp = decimal.Parse(acct.TotalBalance, CultureInfo.InvariantCulture);
					long ext_tot_bal = (long)(tot_bal_temp * 100m);


					context.accounts.Add(new account
					{
						account_number = acct.AccountNumber,
						account_name = acct.Name,
						full_name = acct.FullName,
						description = acct.Desc,
						parent_account_id = parentaccount_id,
						account_type = acct_type[0],
						is_active = acct.IsActive,
						allow_posting = true,
						normal_balance = acct_type[1],
						account_subtype = acct_type[2],
						external_source = "QB",
						external_source_id = acct.ListID,
						external_time_modified = externalTimeModified,
						external_balance = ext_bal,
						external_total_balance = ext_tot_bal,
						created_at = DateTime.Now,
						updated_at = DateTime.Now
					});
					context.SaveChanges();
				}

			}
		}

		private void addItemButton_Click(object sender, EventArgs e)
		{
			ItemImport(items);
		}


		private int? ResolveAccountId(string accountName)
		{
			using (AccountingEntities context = new AccountingEntities())
			{
				var acct = context.accounts.FirstOrDefault(a => a.full_name == accountName);
				return acct != null ? (int)acct.account_id : (int?)null;
			}
		}
		private void ItemImport(List<QbInventoryItem> qbItems)
		{
			var now = DateTime.UtcNow;
			using (AccountingEntities context = new AccountingEntities())
			{
				foreach (var qb in qbItems)
				{
					int? incomeAcctId = ResolveAccountId(qb.IncomeAccountRef);
					int? cogsAcctId = ResolveAccountId(qb.COGSAccountRef);
					int? assetAcctId = ResolveAccountId(qb.AssetAccountRef);

					decimal sale_price_dec = 0.0m;
					if (!string.IsNullOrEmpty(qb.Price))
					{
						sale_price_dec = decimal.Parse(qb.Price, CultureInfo.InvariantCulture);
					}

					decimal cost_dec = 0.0m;
					if (!string.IsNullOrEmpty(qb.Cost))
					{
						cost_dec = decimal.Parse(qb.Cost, CultureInfo.InvariantCulture);
					}

					decimal avg_cost_dec = 0.0m;
					if (!string.IsNullOrEmpty(qb.AverageCost))
					{
						avg_cost_dec = decimal.Parse(qb.AverageCost, CultureInfo.InvariantCulture);
					}

					context.items.Add(new item
					{
						external_id = qb.ListID,
						external_edit_sequence = qb.EditSequence,
						item_type = qb.Type,
						name = qb.Name,
						description = qb.Description,

						income_account_id = incomeAcctId,
						cogs_account_id = cogsAcctId,
						asset_account_id = assetAcctId,
						sales_price = sale_price_dec,
						purchase_cost = cost_dec,
						average_cost = avg_cost_dec,

						is_active = qb.IsActive,
						created_at = now
					});

					context.SaveChanges();

					if (qb.Type == "Inventory")
					{
						item item = context.items.FirstOrDefault(i => i.external_id == qb.ListID);
						context.inventory.Add(new inventory
						{
							item_id = item.item_id,
							quantity_on_hand = decimal.Parse(qb.QuantityOnHand, CultureInfo.InvariantCulture),
							last_sync_time = now
						});

						context.SaveChanges();
					}
				}
			}
		}

		private void loadVendorsButton_Click(object sender, EventArgs e)
		{
			QBFunctions qbXml = new QBFunctions();
			vendors = qbXml.GetVendors();

			this.dataGridQbItems.AutoGenerateColumns = true;
			qbItemsBindingSource.DataSource = new SortableBindingList<QbVendorData>(vendors);
		}

		private void addVendorsButton_Click(object sender, EventArgs e)
		{
			using (AccountingEntities context = new AccountingEntities())
			{
				foreach (QbVendorData vendor in vendors)
				{
					if (!vendor.IsActive)
					{
						continue; // Skip inactive vendors
					}
					context.external_vendors.Add(new external_vendors
					{
						display_name = vendor.Name,
						normalized_name = vendor.Name.ToLowerInvariant(),
						email = vendor.Email,
						phone = vendor.Phone,
						address_line1 = vendor.AddressLine1,
						address_line2 = vendor.AddressLine2,
						city = vendor.City,
						state = vendor.State,
						postal_code = vendor.PostalCode,
						is_active = vendor.IsActive,
						created_at = DateTime.Now,
						updated_at = DateTime.Now
					});
					
				}
				context.SaveChanges();
			}
		}
	}
}

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
		public CompareDbs()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{ List<CompareResults> results = new List<CompareResults>();
			QBFunctions qbf = new QBFunctions();
			List<CustomerData> qbCustomers = qbf.loadCustomers(string.Empty, string.Empty, string.Empty, string.Empty);
			using (WoodClubEntities context = new WoodClubEntities())
			{
				if (qbCustomers.Count > 0)
				{
					List<MemberRoster> roster = (from m in context.MemberRosters
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
								results.Add(new CompareResults
								{
									Badge = customer.FullName,
									First = customer.FirstName,
									Last = customer.LastName,
									OnlyDb = "QB"
								});
							}
						}
						compareProgressBar.PerformStep();
					}
				
					foreach (MemberRoster member in roster)
					{
						List<CustomerData> customer = qbf.loadCustomers(member.Badge, string.Empty, string.Empty, string.Empty);
						if (customer.Count == 0)
						{
							results.Add(new CompareResults
							{
								Badge = member.Badge,
								First = member.FirstName,
								Last = member.LastName,
								OnlyDb = "Shop"
							});
						}

						compareProgressBar.PerformStep();

					}
				}
				compareBindingSource.DataSource = results;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	public partial class scwForm : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private readonly List<BadgeDate> paidList = new List<BadgeDate>();
		private SortableBindingList<UnpaidMember> ds_Unpaid;

		private class BadgeDate
		{
			public string PaidDate { get; set; }
			public string Badge { get; set; }
		}


		public scwForm()
		{
			InitializeComponent();
		}

		private void scwForm_Load(object sender, EventArgs e)
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
									paidList.Add(new BadgeDate
									{
										PaidDate = parts[0],
										Badge = parts[1]
									});
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}

				//ScanSCW();
				Reconcile();
				unpaidMemberBindingSource.DataSource = ds_Unpaid;
				dataGridView1.DataSource = unpaidMemberBindingSource.DataSource;
				dataGridView1.Invalidate();
				log.Info("Scan complete");
			}
			else
			{
				MessageBox.Show("Select Rec Paid File...");
			}
		}
		private void ScanSCW()
		{
			ds_Unpaid = new SortableBindingList<UnpaidMember>();
			using (WoodclubEntities context = new WoodclubEntities())
			{
				List<MemberRoster> members = (from m in context.MemberRosters
											  select m).OrderBy(mem => mem.Badge).ToList();
				foreach (MemberRoster member in members)
				{
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
				//context.SaveChanges();
			}
		}

		private void Reconcile()
		{
			ds_Unpaid = new SortableBindingList<UnpaidMember>();
			using (WoodclubEntities context = new WoodclubEntities())
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
			UnpaidMember upm = new UnpaidMember
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
			using (WoodclubEntities context = new WoodclubEntities())
			{
				foreach (UnpaidMember unpaid in ds_Unpaid)
				{
					if (unpaid.Delete == true)
					{
						var query = from rn in context.MemberRosters
									where rn.Badge == unpaid.Badge && rn.RecCard != null && rn.RecCard != ""
									select rn;
						// query.Single().NewBadge = false;
						if (query.Any())
						{
							int id = query.Single().id;

							var entity = context.MemberRosters.Find(id);
							if (id != 0 && entity != null)
							{
								context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
							}
						}
					}
				}
				//context.SaveChanges();
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}

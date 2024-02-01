using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	public partial class UpdateDuesPaid : Form
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
									paidList.Add(new BadgeDate
									{
										PaidDate = parts[0],
										Badge = parts[1]
									});
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

				Reconcile();
				unpaidMemberBindingSource.DataSource = ds_Unpaid;
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
			ds_Unpaid = new SortableBindingList<UnpaidMember>();
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
			}
		}

		private void FindUnPaid()
		{
			ds_Unpaid = new SortableBindingList<UnpaidMember>();
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
			ds_Unpaid = new SortableBindingList<UnpaidMember>();
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
			using (WoodClubEntities context = new WoodClubEntities())
			{
				foreach (UnpaidMember unpaid in ds_Unpaid)
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
	}
}

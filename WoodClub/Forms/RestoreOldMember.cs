using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class RestoreOldMember : Form
	{
		private SortableBindingList<MembersShort> blMembers;
		private readonly BindingSource bsMembers;

		public RestoreOldMember()
		{
			this.blMembers = new SortableBindingList<MembersShort>();
			this.bsMembers = new BindingSource();
			InitializeComponent();
			this.LoadBackupMembers();
			toolStripSearchBox.KeyUp += TextBoxSearch_KeyUp;


		}

		private void TextBoxSearch_KeyUp(object sender, KeyEventArgs e)
		{
			string filter = toolStripSearchBox.Text;
			if (filter == string.Empty)
			{
				bsMembers.DataSource = blMembers;
			}
			else
			{
				var filteredBindingList = new SortableBindingList<MembersShort>(blMembers.Where(
					x => x.FirstName.ToUpper().Contains(filter.ToUpper()) ||
					x.LastName.ToUpper().Contains(filter.ToUpper()) ||
					x.Phone.Contains(filter) ||
					x.Email.ToUpper().Contains(filter.ToUpper()) ||
					x.Badge.Contains(filter)).ToList());
				bsMembers.DataSource = filteredBindingList;
				dataGridView2.Refresh();
			}
		}

		private void LoadBackupMembers()
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				List<MemberBackup> data = context.MemberBackups.Select(mem => mem)
					.Distinct()
					.OrderBy(mem => mem.Badge)
					.ToList();

				foreach (MemberBackup mr in data)
				{
					MemberRoster activeMember = (from m in context.MemberRosters
												 where m.Badge == mr.Badge
												 select m).FirstOrDefault();
					if (activeMember == null)
					{
						MembersShort member = new MembersShort
						{
							Badge = mr.Badge,
							FirstName = mr.FirstName ?? "",
							LastName = mr.LastName ?? "",
							Address = mr.Address ?? "",
							Phone = mr.Phone ?? "",
							Email = mr.Email ?? ""
						};

						blMembers.Add(member);
					}
				}
				dataGridView2.DataSource = bsMembers;
				this.bsMembers.DataSource = blMembers;
				this.bindingNavigator1.BindingSource = bsMembers;
			}
		}

		private void TransferMember(string badge)
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				MemberBackup r = (from x in context.MemberBackups
								  where x.Badge == badge
								  select x).FirstOrDefault();

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
					Zip = r.Zip,
					RecCard = r.RecCard,
					MemberDate = r.MemberDate,
					ClubDuesPaidDate = r.MemberDate,
					OneTime = true,
					EntryCodes = "F",
					GroupTime = "Members",
					RecDuesPaid = true,
					ClubDuesPaid = true,
					CreditBank = r.CreditBank,
					AuthorizedTimeZone = 3,
					ExemptModDate = r.MemberDate,
					Authorized = false,
					ExtHour = false,
					Exempt = false,
					Locker = String.Empty,
					NewBadge = false,
					LastDayValid = r.LastDayValid,
					Photo = r.Photo
				});
				context.SaveChanges();
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void buttonRestore_Click(object sender, EventArgs e)
		{
			DataGridViewRow memRow = dataGridView2.SelectedRows[0];
			string badge = (string)memRow.Cells["Badge"].Value;
			this.TransferMember(badge);
			this.DialogResult = DialogResult.OK;
		}
	}
}

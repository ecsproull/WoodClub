using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WoodClub
{
	public partial class FormNewMembers : Form
	{
		private List<NewMember> members = new List<NewMember>();
		public FormNewMembers()
		{
			InitializeComponent();
		}

		private async void Form1_Load(object sender, EventArgs e)
		{
			SignUpGenisus sug = new SignUpGenisus();
			Rootobject ro =  await sug.GetSignup(DateTime.Now);

			List<SignupSlot> slots = new List<SignupSlot>();
			string startDate = string.Empty;
			using (WoodclubEntities context = new WoodclubEntities())
			{
				int badge = 9999;
				foreach (SignupSlot sl in ro.data.signup)
				{
					string recNo = sl.customfields[1].value;
					var member = (from m in context.MemberRosters
								  where m.RecCard == recNo
								  select m).FirstOrDefault();

					if (startDate == string.Empty)
					{
						startDate = sl.startdatestring;
					}

					if (startDate == sl.startdatestring)
					{
						string number = Regex.Replace(sl.phone, "[^a-zA-Z0-9]", String.Empty);
						members.Add(new NewMember
						{
							Add = member == null,
							FirstName = sl.firstname,
							LastName = sl.lastname,
							Email = sl.email,
							Phone = number.Substring(0, 3) + "-" +
								 number.Substring(3, 3) + "-" +
								 number.Substring(6, 4),
							Address = sl.address1,
							City = sl.city,
							State = sl.state,
							ZipCode = sl.zipcode,
							RecNo = sl.customfields[1].value,
							MemberDate = DateTimeOffset.FromUnixTimeSeconds((long)sl.startdate).Date,
							Badge = badge.ToString(),
							CardNo = string.Empty
						});
					}
					badge--;
				}
			}

			bs_newmember.DataSource = members;
			this.dataGridView1.DataSource = bs_newmember;
		}

		private void buttonAddToDb_Click(object sender, EventArgs e)
		{
			bs_newmember.EndEdit();

			using (WoodclubEntities context = new WoodclubEntities())
			{
				
				foreach (NewMember r in members)
				{
					context.MemberRosters.Add(new MemberRoster
					{
						Title = "New Member",
						Badge = r.Badge,
						CardNo = r.CardNo,
						FirstName = r.FirstName,
						LastName = r.LastName,
						Email = r.Email,
						Phone = r.Phone,
						Address = r.Address,
						State = r.State,
						Zip = r.ZipCode,
						RecCard = r.RecNo,
						MemberDate = r.MemberDate,
						ClubDuesPaidDate = r.MemberDate,
						OneTime = true,
						EntryCodes = "F",
						GroupTime = "Members",
						RecDuesPaid = true,
						ClubDuesPaid = true,
						CreditBank = "0",
						AuthorizedTimeZone = 3,
						ExemptModDate = r.MemberDate,
						Authorized = false,
						ExtHour = false,
						Exempt = false,
						Locker = String.Empty,
						NewBadge = false,
						LastDayValid = DateTime.Now
					});
				}

				context.SaveChanges();
				DialogResult = DialogResult.OK;
			}
		}
	}
}

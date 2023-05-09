using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WoodClub
{
	public partial class NewMembers : Form
	{
		private List<NewMember> members = new List<NewMember>();
		public NewMembers()
		{
			InitializeComponent();
			dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
			dataGridView1.CellClick += DataGridView1_CellClick;
		}

		private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 0 && dataGridView1.Rows[e.RowIndex].Cells[0].ReadOnly)
			{
				MessageBox.Show("Rec Card is already in the database");
			}
		}

		private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 12)
			{
				dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
					dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().TrimStart('0');
			}
		}

		private async void FormNewMembers_Load(object sender, EventArgs e)
		{
			SignUpGenisus sug = new SignUpGenisus();
			Rootobject ro = await sug.GetSignup(DateTime.Now);
			List<SignupSlot> slots = new List<SignupSlot>();
			string startDate = string.Empty;
			DateTime startScanDate = DateTime.Now.AddDays(-10);
			DateTime endScanDate = DateTime.Now.AddDays(+10);
			using (WoodclubEntities context = new WoodclubEntities())
			{
				foreach (SignupSlot sl in ro.data.signup)
				{
					string recNo = sl.customfields[1].value;
					var member = (from m in context.MemberRosters
								  where m.RecCard == recNo
								  select m).FirstOrDefault();

					if (DateTime.Parse(sl.startdatestring) < startScanDate ||
						DateTime.Parse(sl.startdatestring) > endScanDate)
                    {
						continue;
                    }

					if (startDate == string.Empty)
					{
						startDate = sl.startdatestring;
					}

					if (startDate == sl.startdatestring)
					{
						string number = Regex.Replace(sl.phone, "[^a-zA-Z0-9]", String.Empty);
						string phone = string.Empty;
						if (!string.IsNullOrEmpty(number))
						{
							phone = number.Substring(0, 3) + "-" +
									 number.Substring(3, 3) + "-" +
									 number.Substring(6, 4);
						}

						if (!string.IsNullOrEmpty(sl.firstname))
						{
							for (int i = 0; i < sl.myqty; i++)
							{
								members.Add(new NewMember
								{
									Add = member == null,
									FirstName = this.FormatProperName(sl.firstname),
									LastName = this.FormatProperName(sl.lastname),
									Email = sl.email,
									Phone = phone,
									Address = this.FormatAddress(sl.address1),
									City = this.FormatCity(sl.city),
									State = sl.state,
									ZipCode = sl.zipcode,
									RecNo = sl.customfields[1].value,
									MemberDate = DateTimeOffset.FromUnixTimeSeconds((long)sl.startdate).Date,
									Badge = string.Empty,
									CardNo = string.Empty
								});
							}
						}
					}
				}
			}

			bs_newmember.DataSource = members;
			this.dataGridView1.DataSource = bs_newmember;

			for (int i = 0; i < members.Count; i++)
			{
				if (!members[i].Add)
				{
					this.dataGridView1.Rows[i].Cells[0].ReadOnly = true;
				}
			}
		}

		private string FormatProperName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}

			string ret = name.ToLower();
			string firstChar = ret.Substring(0, 1).ToUpper();
			string rest = ret.Substring(1, ret.Length - 1);
			ret = firstChar + rest;
			return ret;
		}

		private string FormatAddress(string addr)
		{
			if (string.IsNullOrEmpty(addr))
			{
				return addr;
			}

			addr = addr.Replace(".", "").Replace(",", "");

			var parts = addr.Split(' ');
			string ret = parts[0];
			for (int i = 1; i < parts.Length; i++)
			{
				ret += " " + FormatProperName(parts[i]);
			}

			return ret;
		}

		private string FormatCity(string city)
		{
			if (string.IsNullOrEmpty(city))
			{
				return city;
			}

			city = city.Replace(".", "");

			if (city.ToLower() == "scw")
			{
				return "Sun City West";
			}

			var parts = city.Split(' ');
			string ret = FormatProperName(parts[0]);
			for (int i = 1; i < parts.Length; i++)
			{
				ret += " " + FormatProperName(parts[i]);
			}

			return ret;
		}

		private void buttonAddToDb_Click(object sender, EventArgs e)
		{
			bs_newmember.EndEdit();

			using (WoodclubEntities context = new WoodclubEntities())
			{

				foreach (NewMember r in members)
				{
					if (r.Add)
					{
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
							Zip = r.ZipCode,
							RecCard = r.RecNo,
							MemberDate = r.MemberDate,
							ClubDuesPaidDate = r.MemberDate,
							OneTime = true,
							EntryCodes = "F",
							GroupTime = "Members",
							RecDuesPaid = true,
							ClubDuesPaid = true,
							CreditBank = r.Badge == String.Empty ? "0" : "1",
							AuthorizedTimeZone = 3,
							ExemptModDate = r.MemberDate,
							Authorized = false,
							ExtHour = false,
							Exempt = false,
							Locker = String.Empty,
							NewBadge = false,
							LastDayValid = r.MemberDate.AddDays(2),
						});

						if (Convert.ToInt32(r.Badge) < 9000)
						{
							context.Transactions.Add(new Transaction
							{
								Badge = r.Badge,
								Code = "Q4",
								TransDate = DateTime.Now,
								CreditAmt = 1,
								EventType = "Orientation 1 Credit",
								RecCard = r.RecNo
							});
						}
					}
				}

				context.SaveChanges();
				DialogResult = DialogResult.OK;
			}
		}
	}
}

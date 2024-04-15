using System;

namespace WoodClub
{
	internal class NewMember
	{
		public bool Add { get; set; } = true;
		public bool CreateInvoice { get; set; } = false;
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string City { get; set; } = "Sun City West";
		public string State { get; set; } = "AZ";
		public string ZipCode { get; set; } = "98008";
		public string RecNo { get; set; }
		public DateTime MemberDate { get; set; }
		public string Badge { get; set; }
		public string CardNo { get; set; }


	}

	internal class NewMemberRaw
	{
		public string new_member_rec_card { get; set; }
		public string new_member_first { get; set; }
		public string new_member_last { get; set; }
			
		public string new_member_phone { get; set; }
			
		public string new_member_email { get; set; }
		public string new_member_street { get; set; }

		public string session_start_formatted { get; set; }
	}
}


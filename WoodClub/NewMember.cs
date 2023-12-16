using System;

namespace WoodClub
{
	internal class NewMember
	{
		public bool Add { get; set; } = true;
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
}

using System;

namespace WoodClub
{
	class UnpaidMember
	{
		public int id { get; set; }
		public Nullable<bool> Delete { get; set; }
		public string Badge { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Title { get; set; }
		public string RecCard { get; set; }
		public Nullable<System.DateTime> MemberDate { get; set; }
		public Nullable<bool> ClubDuesPaid { get; set; }
		public Nullable<System.DateTime> ClubDuesPaidDate { get; set; }
		public Nullable<bool> RecDuesPaid { get; set; }
	}
}

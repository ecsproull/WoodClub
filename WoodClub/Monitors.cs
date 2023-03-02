using System;

namespace WoodClub
{
	class Monitors
	{
		public string Badge { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool Exempt { get; set; }
		public bool ClubDuesPaid { get; set; }
		public DateTime ClubDuesPaidDate { get; set; }
		public string CreditBank { get; set; }
		public string LastDayValid { get; set; }
		public float CreditAmt { get; set; }
		public DateTime LastMonitor { get; set; }
		public string Code { get; set; }
		public int ShopVisits { get; set; }
		public string Lockers { get; set; }
	}
}

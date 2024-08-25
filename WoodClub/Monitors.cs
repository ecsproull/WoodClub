using System;

namespace WoodClub
{
	/// <summary>
	/// Used by the <see cref="MonitorForm"/> to display monitor statistics.
	/// </summary>
	class Monitors
	{
		/// <summary>
		/// Gets or sets the badge.
		/// </summary>
		/// <value>
		/// The badge.
		/// </value>
		public string Badge { get; set; }

		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>
		/// The first name.
		/// </value>
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the last name.
		/// </summary>
		/// <value>
		/// The last name.
		/// </value>
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Monitors"/> is exempt.
		/// </summary>
		/// <value>
		///   <c>true</c> if exempt; otherwise, <c>false</c>.
		/// </value>
		public bool Exempt { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [club dues paid].
		/// </summary>
		/// <value>
		///   <c>true</c> if [club dues paid]; otherwise, <c>false</c>.
		/// </value>
		public bool ClubDuesPaid { get; set; }

		/// <summary>
		/// Gets or sets the club dues paid date.
		/// </summary>
		/// <value>
		/// The club dues paid date.
		/// </value>
		public DateTime ClubDuesPaidDate { get; set; }

		/// <summary>
		/// Gets or sets the credit bank.
		/// </summary>
		/// <value>
		/// The credit bank.
		/// </value>
		public string CreditBank { get; set; }

		/// <summary>
		/// Gets or sets the last day valid.
		/// </summary>
		/// <value>
		/// The last day valid.
		/// </value>
		public DateTime LastDayValid { get; set; }

		/// <summary>
		/// Gets or sets the credit amt.
		/// </summary>
		/// <value>
		/// The credit amt.
		/// </value>
		public string CreditAmt { get; set; }

		/// <summary>
		/// Gets or sets the last monitor.
		/// </summary>
		/// <value>
		/// The last monitor.
		/// </value>
		public string LastMonitor { get; set; }

		/// <summary>
		/// Gets or sets the shop visits.
		/// </summary>
		/// <value>
		/// The shop visits.
		/// </value>
		public string ShopVisits { get; set; }

		/// <summary>
		/// Gets or sets the lockers.
		/// </summary>
		/// <value>
		/// The lockers.
		/// </value>
		public string Lockers { get; set; }
	}
}

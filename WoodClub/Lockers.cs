namespace WoodClub
{
	/// <summary>
	/// Data type for the locker report.
	/// <see cref="LockerRpt"/>
	/// </summary>
	class Lockers
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int id { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [print tag].
		/// </summary>
		/// <value>
		///   <c>true</c> if [print tag]; otherwise, <c>false</c>.
		/// </value>
		public bool PrintTag { get; set; }

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
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the phone.
		/// </summary>
		/// <value>
		/// The phone.
		/// </value>
		public string Phone { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [club dues paid].
		/// </summary>
		/// <value>
		///   <c>true</c> if [club dues paid]; otherwise, <c>false</c>.
		/// </value>
		public bool ClubDuesPaid { get; set; }

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
		public string LastDayValid { get; set; }

		/// <summary>
		/// Gets or sets the shop visits.
		/// </summary>
		/// <value>
		/// The shop visits.
		/// </value>
		public string ShopVisits { get; set; }

		/// <summary>
		/// Gets or sets the has locker.
		/// </summary>
		/// <value>
		/// The has locker.
		/// </value>
		public string HasLocker { get; set; }

		/// <summary>
		/// Gets or sets the cost.
		/// </summary>
		/// <value>
		/// The cost.
		/// </value>
		public int Cost { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>
		/// The location.
		/// </value>
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets the project.
		/// </summary>
		/// <value>
		/// The project.
		/// </value>
		public string Project { get; set; }
	}
}

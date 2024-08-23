using System;

namespace WoodClub
{
	/// <summary>
	/// Holds data when updating members that haven't paid their dues.
	/// </summary>
	class UnpaidMemberData
	{
		/// <summary>
		/// Gets or sets the delete.
		/// </summary>
		/// <value>
		/// The delete.
		/// </value>
		public Nullable<bool> Delete { get; set; }

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
		/// Gets or sets the address.
		/// </summary>
		/// <value>
		/// The address.
		/// </value>
		public string Address { get; set; }

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		public string State { get; set; }

		/// <summary>
		/// Gets or sets the phone.
		/// </summary>
		/// <value>
		/// The phone.
		/// </value>
		public string Phone { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the record card.
		/// </summary>
		/// <value>
		/// The record card.
		/// </value>
		public string RecCard { get; set; }

		/// <summary>
		/// Gets or sets the member date.
		/// </summary>
		/// <value>
		/// The member date.
		/// </value>
		public Nullable<System.DateTime> MemberDate { get; set; }

		/// <summary>
		/// Gets or sets the club dues paid.
		/// </summary>
		/// <value>
		/// The club dues paid.
		/// </value>
		public Nullable<bool> ClubDuesPaid { get; set; }

		/// <summary>
		/// Gets or sets the club dues paid date.
		/// </summary>
		/// <value>
		/// The club dues paid date.
		/// </value>
		public Nullable<System.DateTime> ClubDuesPaidDate { get; set; }
	}
}

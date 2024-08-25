using System;

namespace WoodClub
{
	/// <summary>
	/// Used by the <see cref="MultipleForm"/> to edit multiple member's credits at one time.
	/// </summary>
	internal class MultipleEditMember
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
		/// Gets or sets the credits.
		/// </summary>
		/// <value>
		/// The credits.
		/// </value>
		public string Credits { get; set; }

		/// <summary>
		/// Gets or sets the last credit awarded.
		/// </summary>
		/// <value>
		/// The last credit awarded.
		/// </value>
		public string LastCreditAwarded { get; set; }

		/// <summary>
		/// Gets or sets the transaction date.
		/// </summary>
		/// <value>
		/// The transaction date.
		/// </value>
		public string TransactionDate { get; set; }
	}
}

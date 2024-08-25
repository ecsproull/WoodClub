using System;

namespace WoodClub
{
	/// <summary>
	/// Data structures used to add new members that are
	/// retrieved from the signup system.
	/// <see cref="NewMember"/>
	/// </summary>
	internal class NewMember
	{
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NewMember"/> is add.
		/// </summary>
		/// <value>
		///   <c>true</c> if add; otherwise, <c>false</c>.
		/// </value>
		public bool Add { get; set; } = true;

		/// <summary>
		/// Gets or sets a value indicating whether [create invoice].
		/// </summary>
		/// <value>
		///   <c>true</c> if [create invoice]; otherwise, <c>false</c>.
		/// </value>
		public bool CreateInvoice { get; set; } = false;

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
		/// Gets or sets the address.
		/// </summary>
		/// <value>
		/// The address.
		/// </value>
		public string Address { get; set; }

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		/// <value>
		/// The city.
		/// </value>
		public string City { get; set; } = "Sun City West";

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		public string State { get; set; } = "AZ";

		/// <summary>
		/// Gets or sets the zip code.
		/// </summary>
		/// <value>
		/// The zip code.
		/// </value>
		public string ZipCode { get; set; } = "85375";

		/// <summary>
		/// Gets or sets the record no.
		/// </summary>
		/// <value>
		/// The record no.
		/// </value>
		public string RecNo { get; set; }

		/// <summary>
		/// Gets or sets the member date.
		/// </summary>
		/// <value>
		/// The member date.
		/// </value>
		public DateTime MemberDate { get; set; }

		/// <summary>
		/// Gets or sets the badge.
		/// </summary>
		/// <value>
		/// The badge.
		/// </value>
		public string Badge { get; set; }

		/// <summary>
		/// Gets or sets the card no.
		/// </summary>
		/// <value>
		/// The card no.
		/// </value>
		public string CardNo { get; set; }


	}

	/// <summary>
	/// Data structures used to add new members that are
	/// retrieved from the signup system.
	/// <see cref="NewMember"/>
	/// </summary>
	internal class NewMemberRaw
	{
		/// <summary>
		/// Gets or sets the new member record card.
		/// </summary>
		/// <value>
		/// The new member record card.
		/// </value>
		public string new_member_rec_card { get; set; }

		/// <summary>
		/// Gets or sets the new member first.
		/// </summary>
		/// <value>
		/// The new member first.
		/// </value>
		public string new_member_first { get; set; }

		/// <summary>
		/// Gets or sets the new member last.
		/// </summary>
		/// <value>
		/// The new member last.
		/// </value>
		public string new_member_last { get; set; }

		/// <summary>
		/// Gets or sets the new member phone.
		/// </summary>
		/// <value>
		/// The new member phone.
		/// </value>
		public string new_member_phone { get; set; }

		/// <summary>
		/// Gets or sets the new member email.
		/// </summary>
		/// <value>
		/// The new member email.
		/// </value>
		public string new_member_email { get; set; }

		/// <summary>
		/// Gets or sets the new member street.
		/// </summary>
		/// <value>
		/// The new member street.
		/// </value>
		public string new_member_street { get; set; }

		/// <summary>
		/// Gets or sets the session start formatted.
		/// </summary>
		/// <value>
		/// The session start formatted.
		/// </value>
		public string session_start_formatted { get; set; }
	}
}


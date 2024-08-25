using System;

namespace WoodClub
{
	/// <summary>
	/// Member's permissions item. Used when creating or editing member permissions. 
	/// </summary>
	internal class MemberPermissionsItem
	{
		/// <summary>
		/// Gets or sets the name of the permission.
		/// </summary>
		/// <value>
		/// The name of the permission.
		/// </value>
		public string PermissionName { get; set; }

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
		/// Gets or sets the approved by.
		/// </summary>
		/// <value>
		/// The approved by.
		/// </value>
		public string ApprovedBy { get; set; }

		/// <summary>
		/// Gets or sets the approved date.
		/// </summary>
		/// <value>
		/// The approved date.
		/// </value>
		public DateTime ApprovedDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="MemberPermissionsItem"/> is blocked.
		/// </summary>
		/// <value>
		///   <c>true</c> if blocked; otherwise, <c>false</c>.
		/// </value>
		public bool Blocked { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="MemberPermissionsItem"/> is delete.
		/// </summary>
		/// <value>
		///   <c>true</c> if delete; otherwise, <c>false</c>.
		/// </value>
		public bool Delete { get; set; } = false;
	}
}

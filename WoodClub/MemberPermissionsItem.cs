using System;

namespace WoodClub
{
	internal class MemberPermissionsItem
	{
		public string PermissionName { get; set; }
		public string Badge { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ApprovedBy { get; set; }
		public DateTime ApprovedDate { get; set; }
		public bool Blocked { get; set; }
		public bool Delete { get; set; } = false;
	}
}

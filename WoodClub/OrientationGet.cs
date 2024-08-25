namespace WoodClub
{
	/// <summary>
	/// Holds data that requests the list of orientation attendees from the server.
	/// <see cref="NewMembers"/>
	/// 
	/// </summary>
	internal class OrientationGet
	{
		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>
		/// The key.
		/// </value>
		public string key { get; set; }

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>
		/// The start date.
		/// </value>
		public int start_date { get; set; }

		/// <summary>
		/// Gets or sets the end date.
		/// </summary>
		/// <value>
		/// The end date.
		/// </value>
		public int end_date { get; set; }
	}
}

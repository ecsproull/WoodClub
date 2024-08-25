using System;

namespace WoodClub
{
	/// <summary>
	/// Activity event data. Used by the <see cref="Editor"/> Form when
	/// editing an individual member.
	/// </summary>
	public class Activity
	{
		/// <summary>
		/// Gets or sets the event.
		/// </summary>
		/// <value>
		/// The event.
		/// </value>
		public String Event { get; set; }

		/// <summary>
		/// Gets or sets the code.
		/// </summary>
		/// <value>
		/// The code.
		/// </value>
		public String Code { get; set; }

		/// <summary>
		/// Gets or sets the credits.
		/// </summary>
		/// <value>
		/// The credits.
		/// </value>
		public String Credits { get; set; }

		/// <summary>
		/// Gets or sets the date time.
		/// </summary>
		/// <value>
		/// The date time.
		/// </value>
		public DateTime dateTime { get; set; }
	}
}

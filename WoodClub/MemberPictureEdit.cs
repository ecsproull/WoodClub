using System.Drawing;

namespace WoodClub
{
	/// <summary>
	/// Holds the data associated with a member's picture.
	/// </summary>
	internal class MemberPictureEdit
	{
		/// <summary>
		/// Gets or sets the original path.
		/// </summary>
		/// <value>
		/// The original path.
		/// </value>
		public string OriginalPath { get; set; }

		/// <summary>
		/// Gets or sets the save path.
		/// </summary>
		/// <value>
		/// The save path.
		/// </value>
		public string SavePath { get; set; }

		/// <summary>
		/// Gets or sets the badge.
		/// </summary>
		/// <value>
		/// The badge.
		/// </value>
		public string Badge { get; set; }

		/// <summary>
		/// Gets or sets the full size.
		/// </summary>
		/// <value>
		/// The full size.
		/// </value>
		public Image FullSize { get; set; }

		/// <summary>
		/// Gets or sets the cropped.
		/// </summary>
		/// <value>
		/// The cropped.
		/// </value>
		public Image Cropped { get; set; }

		/// <summary>
		/// Gets or sets the size of the badge.
		/// </summary>
		/// <value>
		/// The size of the badge.
		/// </value>
		public Image BadgeSize { get; set; }

		/// <summary>
		/// Gets or sets the center x.
		/// </summary>
		/// <value>
		/// The center x.
		/// </value>
		public int CenterX { get; set; } = -1000;

		/// <summary>
		/// Gets or sets the center y.
		/// </summary>
		/// <value>
		/// The center y.
		/// </value>
		public int CenterY { get; set; }
	}
}

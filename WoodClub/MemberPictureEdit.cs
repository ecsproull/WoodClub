using System.Drawing;

namespace WoodClub
{
	internal class MemberPictureEdit
	{
		public string SaveText { get; set; } = "Save Row";
		public string OriginalPath { get; set; }
		public string SavePath { get; set; }
		public string Badge { get; set; }
		public Image FullSize { get; set; }
		public Image Cropped { get; set; }
		public Image BadgeSize { get; set; }
		public int CenterX { get; set; } = -1000;
		public int CenterY { get; set; }
	}
}

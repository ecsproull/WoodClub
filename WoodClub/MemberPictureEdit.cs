using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoodClub
{
	internal class MemberPictureEdit
	{
		public string Badge { get; set; }
		public PictureBox FullSize { get; set; }
		public PictureBox Cropped { get; set; }
		public PictureBox BadgeSize { get; set; }
		public PictureBox AppSize { get; set; }
	}
}

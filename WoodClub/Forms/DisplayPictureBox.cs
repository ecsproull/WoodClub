using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	public partial class DisplayPictureBox : Form
	{
		public DisplayPictureBox(string badge)
		{
			InitializeComponent();

			using (WoodClubEntities context = new WoodClubEntities())
			{
				var member = (from m in context.MemberRosters
							  where badge == m.Badge
							  select m).FirstOrDefault();

				if (member == null)
				{
					MessageBox.Show("Set Badge Number to a valid number");
					DialogResult = DialogResult.Cancel;
					return;
				}


				if (member.Photo != null)
				{
					try
					{
						MemoryStream ms = new MemoryStream(member.Photo);
						Image img = Image.FromStream(ms);
						pictureBox1.Image = img;
					}
					catch (Exception e) { MessageBox.Show(e.Message); }
				}
			}
		}
	}
}

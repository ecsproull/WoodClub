using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class PhotoImport : Form
	{
		public PhotoImport()
		{
			InitializeComponent();
		}

		private void GetImages()
		{
			OpenFileDialog theDialog = new OpenFileDialog
			{
				Title = "Open Image (.jpg) File",
				Filter = "JPG files|*.jpg",
				InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Images",
				Multiselect = true,
			};

			if (theDialog.ShowDialog() == DialogResult.OK)
			{
				foreach (String file in theDialog.FileNames)
				{
					// Create a PictureBox.
					try
					{
						PictureBox pb = new PictureBox();
						Image loadedImage = Image.FromFile(file);
						pb.Height = loadedImage.Height;
						pb.Width = loadedImage.Width;
						pb.Image = loadedImage;
						//flowLayoutPanel1.Controls.Add(pb);
					}
					catch (SecurityException ex)
					{
						// The user lacks appropriate permissions to read files, discover paths, etc.
						MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
							"Error message: " + ex.Message + "\n\n" +
							"Details (send to Support):\n\n" + ex.StackTrace
						);
					}
					catch (Exception ex)
					{
						// Could not load the image - probably related to Windows file system permissions.
						MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
							+ ". You may not have permission to read the file, or " +
							"it may be corrupt.\n\nReported error: " + ex.Message);
					}
				}
			}

			}

		private void BtnLoadPhoto_Click(object sender, EventArgs e)
		{
			Stream flStream;
			OpenFileDialog theDialog = new OpenFileDialog
			{
				Title = "Open Image (.jpg) File",
				Filter = "JPG files|*.jpg",
				InitialDirectory = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\Images"
			};

			if (theDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((flStream = theDialog.OpenFile()) != null)
					{
						using (flStream)
						{
							/*Image img = Image.FromStream(flStream);
							Image newImage = ScaleImage(img, 200, 200);
							pictureBox1.Image = newImage;
							Image newImg = ScaleImage(img, 800, 420);

							using (var myStream = new MemoryStream())
							{
								newImg.Save(myStream, System.Drawing.Imaging.ImageFormat.Jpeg);
								this.member.Photo = myStream.ToArray();
							}*/
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}
	}
}

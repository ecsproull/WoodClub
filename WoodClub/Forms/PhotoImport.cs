using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WoodClub.Forms
{
	public partial class PhotoImport : Form
	{
		private static double multiplier = 0.0;
		private static double increment = 0.03;
		private const int width = 1700;
		private const int height = 2100;
		private List<MemberPictureEdit> photos = new List<MemberPictureEdit>();
		public PhotoImport()
		{
			InitializeComponent();
			dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;
			dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
			savePathTextBox.Text = @"c:\Images\Badge # 5300 and up\";
		}

		private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				if (savePathTextBox.Text[savePathTextBox.Text.Length - 1] != '\\')
				{
					savePathTextBox.Text += "\\";
				}
				photos[e.RowIndex].SavePath = savePathTextBox.Text +
					dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + ".jpg";
			}
		}

		private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			switch (e.ColumnIndex)
			{
				case 0:
					MemberPictureEdit photo = photos[e.RowIndex];
					if (string.IsNullOrEmpty(photo.SavePath) || !photo.SavePath.EndsWith(".jpg"))
					{
						MessageBox.Show("Set the badge number before saving");
						break;
					}
					using (WoodClubEntities context = new WoodClubEntities())
					{
						MemberRoster member = (from m in context.MemberRosters
											   where m.Badge == photo.Badge
											   select m).FirstOrDefault();
						if (member == null)
						{
							MessageBox.Show("Member badge not found");
							break;
						}
						else
						{
							ImageCodecInfo jpgCodec = ImageCodecInfo.GetImageEncoders()
								.Where(codec => codec.FormatID.Equals(ImageFormat.Jpeg.Guid)).FirstOrDefault();
							if (jpgCodec != null)
							{
								EncoderParameters parameters = new EncoderParameters(2);
								parameters.Param[0] = new EncoderParameter(Encoder.ColorDepth, 24);
								parameters.Param[1] = new EncoderParameter(Encoder.Quality, 50L);
								photo.Cropped.Save(photo.SavePath, jpgCodec, parameters);
							}
							else
							{
								photo.Cropped.Save(photo.SavePath);
							}

							using(var myStream = new MemoryStream())
							{
								photo.Cropped.Save(myStream, System.Drawing.Imaging.ImageFormat.Jpeg);
								member.Photo = myStream.ToArray();
								context.SaveChanges();
							}
						}
					}
					break;
				case 2:
					multiplier = 0.0;
					photos[e.RowIndex].CenterX = e.X;
					photos[e.RowIndex].CenterY = e.Y;
					ResizeRowImage(e, multiplier, e.X, e.Y);
					break;

				case 3:
					if (photos[e.RowIndex].CenterX == -1000)
					{
						break;
					}

					multiplier += increment;
					ResizeRowImage(e, multiplier, photos[e.RowIndex].CenterX, photos[e.RowIndex].CenterY);
					break;
				default:
					break;
			}
		}

		private void ResizeRowImage(DataGridViewCellMouseEventArgs e, 
			double multiplier, 
			int centerX, 
			int centerY)
		{
            System.Drawing.Image imgToResize = photos[e.RowIndex].FullSize;
			var originalWidth = imgToResize.Width;
			var originalHeight = imgToResize.Height;

			Size destinationSize = new Size(width, height);

			var hRatio = (float)originalHeight / destinationSize.Height;
			var wRatio = (float)originalWidth / destinationSize.Width;

			var ratio = Math.Min(hRatio, wRatio);

			var hScale = Convert.ToInt32(destinationSize.Height * (ratio - multiplier));
			var wScale = Convert.ToInt32(destinationSize.Width * (ratio - multiplier));

			var startX = ((originalWidth - wScale) / 2) - ((originalHeight / 2) - centerX);
			var startY = ((originalHeight - hScale) / 2) - ((originalHeight / 2) - centerY);

			var sourceRectangle = new Rectangle(startX, startY, wScale, hScale);

			Bitmap bitmap = new Bitmap(destinationSize.Width, destinationSize.Height);
			bitmap.SetResolution(350, 350);
			var destinationRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

			using (var g = Graphics.FromImage(bitmap))
			{
				g.DrawImage(imgToResize, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
			}

			photos[e.RowIndex].Cropped = bitmap;
			photos[e.RowIndex].BadgeSize = bitmap;
			newImagesBindingSource.ResetBindings(false);
		}

		private void loadImagesButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog theDialog = new OpenFileDialog
			{
				Title = "Open Image (.jpg) File",
				Filter = "JPG files|*.jpg",
				InitialDirectory = @"C:\Images",
				Multiselect = true,
			};

			if (theDialog.ShowDialog() == DialogResult.OK)
			{
				foreach (String file in theDialog.FileNames)
				{
					try
					{
						MemberPictureEdit photo = new MemberPictureEdit();
						photo.OriginalPath = file;
						photo.Badge = "1111";
                        System.Drawing.Image loadedImage = System.Drawing.Image.FromFile(file);
						double dbl = (double)loadedImage.Width / (double)loadedImage.Height;
						double boxHeight = 500;
						Bitmap resizedImage = new System.Drawing.Bitmap(loadedImage, (int)(double)(boxHeight * dbl), (int)boxHeight);
						photo.FullSize = resizedImage;
						photos.Add(photo);
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
			
			newImagesBindingSource.DataSource = photos;
		}

		private void OpenEditor(string imagePath)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo(imagePath);
			var process = Process.Start(startInfo);
		}
	}
}

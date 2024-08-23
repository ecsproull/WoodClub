using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// From used to import multiple photos. Developed to be used
	/// when importing new members for orientation.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class PhotoImport : Form
	{
		private static double multiplier = 0.0;
		private static double increment = 0.03;
		private const int width = 1700;
		private const int height = 2100;
		private static List<MemberPictureEdit> photos = new List<MemberPictureEdit>();
		private static PhotoImport staticThis;
		private static BindingSource staticBS;

		/// <summary>
		/// Initializes a new instance of the <see cref="PhotoImport"/> class.
		/// </summary>
		public PhotoImport()
		{
			InitializeComponent();
			dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;
			dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
			savePathTextBox.Text = @"c:\Images\Badge # 5300 and up\";
			staticThis = this;
			staticBS = newImagesBindingSource;
			LoadImages();
		}

		/// <summary>
		/// Handles the CellValueChanged event of the DataGridView1 control.
		/// The name of the photo has changed. Generally this is to the members badge number.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
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

		/// <summary>
		/// Handles the CellMouseClick event of the DataGridView1 control.
		/// A click in the first cell saves the photo.
		/// A click on the photo, the second cell, centers the photo at the mouse position.
		/// The next cell slightly zooms.
		/// The last cell opens an editor to manually crop the photo.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
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
							if (photo.Cropped == null)
							{
								MessageBox.Show("Crop Image to continue.");
								break;
							}

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
								Image cropped = MemberEditor.ScaleImage(photo.Cropped, 800, 420);
								cropped.Save(myStream, System.Drawing.Imaging.ImageFormat.Jpeg);
								member.Photo = myStream.ToArray();
								context.SaveChanges();
							}
						}

						SetSaveButtonState(e.RowIndex, true);
					}
					break;
				case 2:
					SetSaveButtonState(e.RowIndex, false);
					multiplier = 0.0;
					photos[e.RowIndex].CenterX = e.X;
					photos[e.RowIndex].CenterY = e.Y;
					ResizeRowImage(e, multiplier, e.X, e.Y);
					break;

				case 3:
					SetSaveButtonState(e.RowIndex, false);
					if (photos[e.RowIndex].CenterX == -1000)
					{
						break;
					}

					multiplier += increment;
					ResizeRowImage(e, multiplier, photos[e.RowIndex].CenterX, photos[e.RowIndex].CenterY);
					break;

				case 4:
					OpenEditor(photos[e.RowIndex].OriginalPath);
					break;

					case 5:
					DisplayPictureBox dpb = new DisplayPictureBox(photos[e.RowIndex].Badge);
					dpb.ShowDialog();
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// Resizes the row image.
		/// </summary>
		/// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
		/// <param name="multiplier">The multiplier.</param>
		/// <param name="centerX">The center x.</param>
		/// <param name="centerY">The center y.</param>
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

		/// <summary>
		/// Handles the Click event of the load images button control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void loadImagesButton_Click(object sender, EventArgs e)
		{
			LoadImages();
		}

		/// <summary>
		/// Loads the images. Uses the OpenFileDialog to open one or more images.
		/// </summary>
		private void LoadImages()
		{
			bool photosCleared = false;
			OpenFileDialog theDialog = new OpenFileDialog
			{
				Title = "Open Image (.jpg) File",
				Filter = "JPG files|*.jpg",
				InitialDirectory = @"C:\Images",
				Multiselect = true,
			};

			if (theDialog.ShowDialog() == DialogResult.OK)
			{
				photosCleared = photos.Count > 0;
				if (photosCleared)
				{
					photos = new List<MemberPictureEdit>();
				}

				foreach (String file in theDialog.FileNames)
				{
					try
					{
						MemberPictureEdit photo = new MemberPictureEdit();
						photo.OriginalPath = file;
						photo.Badge = "1111";
						using (FileStream fs = new FileStream(file, FileMode.Open))
						{
							System.Drawing.Image loadedImage = Image.FromStream(fs);
							double dbl = (double)loadedImage.Width / (double)loadedImage.Height;
							double boxHeight = 500;
							Bitmap resizedImage = new System.Drawing.Bitmap(loadedImage, (int)(double)(boxHeight * dbl), (int)boxHeight);
							photo.FullSize = resizedImage;
							photos.Add(photo);
						}
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

			if (photosCleared)
			{
				newImagesBindingSource.ResetBindings(false);
			}
		}

		/// <summary>
		/// Opens the individual image editor.
		/// </summary>
		/// <param name="imagePath">The image path.</param>
		private void OpenEditor(string imagePath)
		{
			CreateFileWatcher(imagePath);
			ProcessStartInfo startInfo = new ProcessStartInfo(imagePath);
			var process = Process.Start(startInfo);
		}

		/// <summary>
		/// Sets the state of the save button.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="saved">if set to <c>true</c> [saved].</param>
		private void SetSaveButtonState(int row, bool saved)
		{
			dataGridView1.Rows[row].Cells[0].Value = saved ? "Saved" : "Save Row" ;
			
			DataGridViewCellStyle style = new DataGridViewCellStyle();
			style.BackColor = saved ? Color.LightBlue : Color.White;
			style.ForeColor = Color.Black;
			dataGridView1.Rows[row].Cells[0].Style = style;
		}

		/// <summary>
		/// Creates the file watcher. Fires an event when a file changes.
		/// After an individual image is edited and saved that notification
		/// causes the application to reopen the image.
		/// </summary>
		/// <param name="path">The path.</param>
		public void CreateFileWatcher(string path)
		{
			// Create a new FileSystemWatcher and set its properties.
			FileSystemWatcher watcher = new FileSystemWatcher();
			watcher.Path = Path.GetDirectoryName(path);
			watcher.Filter = Path.GetFileName(path);
			watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
			   | NotifyFilters.FileName | NotifyFilters.DirectoryName;

			// Add event handlers.
			watcher.Changed += new FileSystemEventHandler(OnChanged);

			// Begin watching.
			watcher.EnableRaisingEvents = true;
		}

		/// <summary>
		/// Called when the image [changed].
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
		private static void OnChanged(object source, FileSystemEventArgs e)
		{
			foreach (MemberPictureEdit mpe in photos)
			{
				if (mpe.OriginalPath == e.FullPath)
				{
					FileInfo file = new FileInfo(mpe.OriginalPath);
					WaitForFile(file, mpe);
				}
			}
		}

		/// <summary>
		/// Waits for file to be ready to be reloaded.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="mpe">The mpe.</param>
		private static void WaitForFile(FileInfo file, MemberPictureEdit mpe)
		{
			bool FileReady = false;
			while (!FileReady)
			{
				try
				{
					using (FileStream fs = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
					{
						System.Drawing.Image loadedImage = Image.FromStream(fs);
						double dbl = (double)loadedImage.Width / (double)loadedImage.Height;
						double boxHeight = 500;
						Bitmap resizedImage = new System.Drawing.Bitmap(loadedImage, (int)(double)(boxHeight * dbl), (int)boxHeight);
						mpe.FullSize = resizedImage;
						FileReady = true;
						staticThis.Invoke(new Action(() => staticBS.ResetBindings(false)));
					}
				}
				catch (IOException)
				{
				}
				//We'll want to wait a bit between polls, if the file isn't ready.
				if (!FileReady) Thread.Sleep(1000);
			}
		}
	}
}

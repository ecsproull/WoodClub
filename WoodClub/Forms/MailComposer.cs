using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace WoodClub.Forms
{
    /// <summary>
    /// Compose and send a business-information email to a mailing list, to every
    /// active member, and/or to a set of free-text addresses. The body is edited
    /// as HTML in a contentEditable WebBrowser document. Every recipient receives
    /// the identical Subject/Body.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MailComposer : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The static list of allowed From addresses. Not editable in the UI and
        /// not stored in the database.
        /// </summary>
        private static readonly string[] FromAddresses =
        {
            "president@scwwoodshop.com",
            "treasurer@scwwoodshop.com",
            "classes@scwwoodshop.com"
        };

        private static readonly Regex EmailPattern =
            new Regex(@"^[^@\s;]+@[^@\s;]+\.[^@\s;]+$", RegexOptions.Compiled);

        /// <summary>
        /// Inline-image size-reduction settings for the "embed inline (base64)"
        /// path. These constants are the only place the thresholds are adjustable
        /// (by editing code) - there is no UI for per-image sizing.
        /// </summary>
        // Largest allowed width or height in pixels (the larger side is capped
        // here; aspect ratio is preserved).
        private const int MaxImageDimension = 800;

        // Pre-base64, on-disk-equivalent byte threshold that triggers reduction
        // (1.5 MB).
        private const int MaxImageFileSizeBytes = 1_572_864;

        // JPEG encoder quality (0-100) used for the first re-encode pass.
        private const int JpegQuality = 80;

        // Amount JpegQuality is dropped for the single extra re-encode pass when
        // the first pass is still over the size threshold.
        private const int JpegQualityFallbackStep = 15;

        /// <summary>
        /// An address to pre-populate into the extra addresses box on load, e.g.
        /// when opened via "Mail To" for a single selected member.
        /// </summary>
        private readonly string initialExtraAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailComposer"/> class.
        /// </summary>
        public MailComposer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailComposer"/> class
        /// with an address pre-filled in the extra addresses box.
        /// </summary>
        /// <param name="extraAddress">The address to pre-fill.</param>
        public MailComposer(string extraAddress)
            : this()
        {
            initialExtraAddress = extraAddress;
        }

        /// <summary>
        /// Handles the Load event of the MailComposer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MailComposer_Load(object sender, EventArgs e)
        {
            cbFrom.Items.AddRange(FromAddresses);
            cbFrom.SelectedIndex = 0;

            tscFontName.ComboBox.Items.AddRange(new object[]
            {
                "Arial", "Calibri", "Courier New", "Georgia", "Tahoma", "Times New Roman", "Verdana"
            });
            tscFontName.SelectedIndex = 0;

            tscFontSize.ComboBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7" });
            tscFontSize.SelectedIndex = 2;

            toolStripEditor.Enabled = false;
            webEditor.DocumentText = EditorHtmlTemplate();
            BuildToolbarIcons();

            LoadMailingLists();

            if (!string.IsNullOrWhiteSpace(initialExtraAddress))
            {
                txtExtra.Text = initialExtraAddress;
            }

            UpdateSendEnabled();
        }

        /// <summary>
        /// The MailingListId used for the "None" entry in the selector.
        /// </summary>
        private const int NoListId = 0;

        /// <summary>
        /// Loads the active mailing lists into the selector, with a "None" entry
        /// first (the default). Also useful to reload after creating a new list.
        /// </summary>
        private void LoadMailingLists(int? selectId = null)
        {
            using (WoodClubEntities context = new WoodClubEntities())
            {
                List<ListItem> lists = (from l in context.MailingLists
                                        where l.IsActive
                                        orderby l.Name
                                        select new ListItem { MailingListId = l.MailingListId, Name = l.Name }).ToList();

                lists.Insert(0, new ListItem { MailingListId = NoListId, Name = "None" });

                cbMailingList.DataSource = lists;
                cbMailingList.DisplayMember = "Name";
                cbMailingList.ValueMember = "MailingListId";
            }

            cbMailingList.SelectedValue = selectId ?? NoListId;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbMailingList control.
        /// The action button is "Create List" while "None" is selected and
        /// "Edit List" once a real list is chosen.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbMailingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? listId = cbMailingList.SelectedValue as int?;
            bool realList = listId.HasValue && listId.Value != NoListId;
            btnCreateList.Text = realList ? "Edit List" : "Create List";
        }

        /// <summary>
        /// Handles the Click event of the btnCreateList control. Opens the create
        /// list dialog when "None" is selected, or the mailing list editor for the
        /// selected list otherwise. Either way the dropdown is refreshed with the
        /// resulting list selected.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCreateList_Click(object sender, EventArgs e)
        {
            int? listId = cbMailingList.SelectedValue as int?;
            if (listId.HasValue && listId.Value != NoListId)
            {
                MailingListEditor editor = new MailingListEditor(listId.Value);
                try
                {
                    editor.ShowDialog();
                }
                finally
                {
                    editor.Dispose();
                }

                LoadMailingLists(listId.Value);
                return;
            }

            CreateMailingList frm = new CreateMailingList();
            try
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadMailingLists(frm.NewListId);
                }
            }
            finally
            {
                frm.Dispose();
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkSendToAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chkSendToAll_CheckedChanged(object sender, EventArgs e)
        {
            cbMailingList.Enabled = !chkSendToAll.Checked;
        }

        /// <summary>
        /// Handles the TextChanged event of the txtSubject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtSubject_TextChanged(object sender, EventArgs e)
        {
            UpdateSendEnabled();
        }

        /// <summary>
        /// Enables the Send button only when a subject has been entered.
        /// </summary>
        private void UpdateSendEnabled()
        {
            btnSend.Enabled = !string.IsNullOrWhiteSpace(txtSubject.Text);
        }

        #region Rich text editor

        /// <summary>
        /// The initial HTML document for the contentEditable body. The
        /// "saved from url" marker keeps the WebBrowser control from blocking the
        /// helper script.
        /// </summary>
        private static string EditorHtmlTemplate()
        {
            return
                "<!-- saved from url=(0016)http://localhost -->\r\n" +
                "<html><head>\r\n" +
                "<style>body{font-family:Arial;font-size:12pt;margin:8px;}</style>\r\n" +
                "<script type=\"text/javascript\">\r\n" +
                "function formatDoc(cmd, val){ try{ document.execCommand(cmd, false, val); }catch(e){} }\r\n" +
                "function insertHtml(html){\r\n" +
                "  try{\r\n" +
                "    if (document.selection && document.selection.createRange){\r\n" +
                "      document.selection.createRange().pasteHTML(html);\r\n" +
                "    } else if (window.getSelection && window.getSelection().rangeCount){\r\n" +
                "      var range = window.getSelection().getRangeAt(0);\r\n" +
                "      range.deleteContents();\r\n" +
                "      var div = document.createElement('div'); div.innerHTML = html;\r\n" +
                "      var frag = document.createDocumentFragment(), node;\r\n" +
                "      while ((node = div.firstChild)){ frag.appendChild(node); }\r\n" +
                "      range.insertNode(frag);\r\n" +
                "    } else {\r\n" +
                "      document.body.innerHTML += html;\r\n" +
                "    }\r\n" +
                "  }catch(e){ document.body.innerHTML += html; }\r\n" +
                "}\r\n" +
                "function getBody(){ return document.body.innerHTML; }\r\n" +
                "</script>\r\n" +
                "</head><body contenteditable=\"true\"></body></html>";
        }

        /// <summary>
        /// Handles the DocumentCompleted event of the webEditor control. Enables
        /// the formatting toolbar once the editable document is ready.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="WebBrowserDocumentCompletedEventArgs"/> instance containing the event data.</param>
        private void webEditor_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            toolStripEditor.Enabled = true;
        }

        /// <summary>
        /// Runs a document.execCommand in the editor via the helper script.
        /// </summary>
        private void Format(string command, string value = null)
        {
            if (webEditor.Document == null)
            {
                return;
            }

            try
            {
                webEditor.Document.InvokeScript("formatDoc", new object[] { command, value ?? string.Empty });
                webEditor.Focus();
            }
            catch (Exception ex)
            {
                log.Error("Editor command '" + command + "' failed..", ex);
            }
        }

        /// <summary>
        /// Inserts a fragment of HTML at the caret in the editor.
        /// </summary>
        private void InsertHtml(string html)
        {
            if (webEditor.Document == null)
            {
                return;
            }

            try
            {
                webEditor.Document.InvokeScript("insertHtml", new object[] { html });
                webEditor.Focus();
            }
            catch (Exception ex)
            {
                log.Error("Editor insert failed..", ex);
            }
        }

        private void tscFontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Format("FontName", tscFontName.Text);
        }

        private void tscFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Format("FontSize", tscFontSize.Text);
        }

        private void tsbColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Format("ForeColor", System.Drawing.ColorTranslator.ToHtml(dlg.Color));
                }
            }
        }

        private void tsbBold_Click(object sender, EventArgs e)
        {
            Format("Bold");
        }

        private void tsbItalic_Click(object sender, EventArgs e)
        {
            Format("Italic");
        }

        private void tsbUnderline_Click(object sender, EventArgs e)
        {
            Format("Underline");
        }

        private void tsbAlignLeft_Click(object sender, EventArgs e)
        {
            Format("JustifyLeft");
        }

        private void tsbAlignCenter_Click(object sender, EventArgs e)
        {
            Format("JustifyCenter");
        }

        private void tsbAlignRight_Click(object sender, EventArgs e)
        {
            Format("JustifyRight");
        }

        private void tsbNumberedList_Click(object sender, EventArgs e)
        {
            Format("InsertOrderedList");
        }

        private void tsbBulletedList_Click(object sender, EventArgs e)
        {
            Format("InsertUnorderedList");
        }

        private void tsbLink_Click(object sender, EventArgs e)
        {
            string url = PromptForString("Insert Hyperlink", "Link URL:", "https://");
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            string text = PromptForString("Insert Hyperlink", "Text to display:", url);
            if (text == null)
            {
                return;
            }

            if (text.Length == 0)
            {
                text = url;
            }

            InsertHtml("<a href=\"" + HttpUtility.HtmlAttributeEncode(url) + "\">" + HttpUtility.HtmlEncode(text) + "</a>");
        }

        private void tsbImageFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Image files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp";
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    byte[] bytes = File.ReadAllBytes(dlg.FileName);
                    string mimeType;
                    bool reduced = false;

                    if (bytes.Length > MaxImageFileSizeBytes)
                    {
                        bytes = ReduceImageForEmail(bytes);
                        mimeType = "image/jpeg";
                        reduced = true;
                    }
                    else
                    {
                        string ext = Path.GetExtension(dlg.FileName).TrimStart('.').ToLowerInvariant();
                        if (ext == "jpg")
                        {
                            ext = "jpeg";
                        }

                        mimeType = "image/" + ext;
                    }

                    string dataUri = "data:" + mimeType + ";base64," + Convert.ToBase64String(bytes);
                    InsertHtml("<img src=\"" + dataUri + "\" />");

                    tslImageNote.Text = reduced
                        ? "Last image was resized/compressed for email size."
                        : string.Empty;
                }
                catch (Exception ex)
                {
                    log.Error("Embed image failed..", ex);
                    MessageBox.Show("Could not embed image: " + ex.Message);
                }
            }
        }

        private void tsbImageUrl_Click(object sender, EventArgs e)
        {
            string url = PromptForString("Insert Image", "Image URL:", "https://");
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            InsertHtml("<img src=\"" + HttpUtility.HtmlAttributeEncode(url) + "\" />");
        }

        /// <summary>
        /// The JPEG encoder used to re-compress over-size inline images. Resolved
        /// once; null on the (unexpected) chance GDI+ reports no JPEG encoder, in
        /// which case <see cref="EncodeJpeg"/> falls back to the default save.
        /// </summary>
        private static readonly ImageCodecInfo JpegCodec = ImageCodecInfo.GetImageEncoders()
            .FirstOrDefault(codec => codec.FormatID.Equals(ImageFormat.Jpeg.Guid));

        /// <summary>
        /// Shrinks an over-size local image so it is reasonable to embed inline in
        /// an email: if either dimension exceeds <see cref="MaxImageDimension"/>
        /// the image is resized proportionally, then it is re-encoded as JPEG at
        /// <see cref="JpegQuality"/>. If the result is still above
        /// <see cref="MaxImageFileSizeBytes"/>, one more pass is made at
        /// <see cref="JpegQuality"/> minus <see cref="JpegQualityFallbackStep"/>.
        /// The bytes are returned regardless of whether that final pass cleared
        /// the threshold, so the user is never blocked from inserting an image.
        /// </summary>
        private static byte[] ReduceImageForEmail(byte[] originalBytes)
        {
            using (MemoryStream source = new MemoryStream(originalBytes))
            using (Image original = Image.FromStream(source))
            {
                int width = original.Width;
                int height = original.Height;
                int longestSide = Math.Max(width, height);

                if (longestSide > MaxImageDimension)
                {
                    double scale = (double)MaxImageDimension / longestSide;
                    width = Math.Max(1, (int)Math.Round(width * scale));
                    height = Math.Max(1, (int)Math.Round(height * scale));
                }

                using (Bitmap resized = new Bitmap(width, height))
                {
                    using (Graphics g = Graphics.FromImage(resized))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        g.DrawImage(original, new Rectangle(0, 0, width, height));
                    }

                    byte[] encoded = EncodeJpeg(resized, JpegQuality);
                    if (encoded.Length > MaxImageFileSizeBytes)
                    {
                        encoded = EncodeJpeg(resized, JpegQuality - JpegQualityFallbackStep);
                    }

                    return encoded;
                }
            }
        }

        /// <summary>
        /// Encodes an image as JPEG at the given quality (0-100) and returns the
        /// bytes.
        /// </summary>
        private static byte[] EncodeJpeg(Image image, int quality)
        {
            using (MemoryStream output = new MemoryStream())
            {
                if (JpegCodec != null)
                {
                    using (EncoderParameters parameters = new EncoderParameters(1))
                    {
                        parameters.Param[0] = new EncoderParameter(Encoder.Quality, (long)quality);
                        image.Save(output, JpegCodec, parameters);
                    }
                }
                else
                {
                    image.Save(output, ImageFormat.Jpeg);
                }

                return output.ToArray();
            }
        }

        /// <summary>
        /// Reads the current HTML from the editor body.
        /// </summary>
        private string GetEditorHtml()
        {
            try
            {
                object html = webEditor.Document?.InvokeScript("getBody");
                if (html != null)
                {
                    return html.ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error("Reading editor html failed..", ex);
            }

            return webEditor.Document?.Body?.InnerHtml ?? string.Empty;
        }

        /// <summary>
        /// Draws and assigns the standard formatting icons (list, alignment,
        /// link and image glyphs) for the toolbar buttons that don't rely on a
        /// styled letter (Bold/Italic/Underline already read as "B"/"I"/"U").
        /// Drawn in code rather than embedded as resources since the toolbar
        /// has no image resx of its own.
        /// </summary>
        private void BuildToolbarIcons()
        {
            tsbAlignLeft.Image = CreateAlignIcon(HorizontalAlignment.Left);
            tsbAlignCenter.Image = CreateAlignIcon(HorizontalAlignment.Center);
            tsbAlignRight.Image = CreateAlignIcon(HorizontalAlignment.Right);
            tsbNumberedList.Image = CreateListIcon(numbered: true);
            tsbBulletedList.Image = CreateListIcon(numbered: false);
            tsbLink.Image = CreateLinkIcon();
            tsbImageFile.Image = CreateImageIcon(online: false);
            tsbImageUrl.Image = CreateImageIcon(online: true);
        }

        /// <summary>
        /// A 16x16 icon of horizontal bars of decreasing width, anchored to the
        /// given side (or centered), matching the classic paragraph-alignment
        /// glyph set.
        /// </summary>
        private static Bitmap CreateAlignIcon(HorizontalAlignment alignment)
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.Clear(Color.Transparent);
                int[] widths = { 14, 9, 14, 7 };
                int y = 2;
                foreach (int width in widths)
                {
                    int x;
                    switch (alignment)
                    {
                        case HorizontalAlignment.Left:
                            x = 1;
                            break;
                        case HorizontalAlignment.Right:
                            x = 15 - width;
                            break;
                        default:
                            x = (16 - width) / 2;
                            break;
                    }

                    g.FillRectangle(brush, x, y, width, 2);
                    y += 4;
                }
            }

            return bmp;
        }

        /// <summary>
        /// A 16x16 icon showing three list rows, each with a number ("1.",
        /// "2.", "3.") or a bullet dot followed by a line standing in for text.
        /// </summary>
        private static Bitmap CreateListIcon(bool numbered)
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            using (Brush brush = new SolidBrush(Color.Black))
            using (Font font = new Font("Segoe UI", 5.5f, FontStyle.Bold))
            {
                g.Clear(Color.Transparent);
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                int[] rowTops = { -2, 4, 10 };
                for (int row = 0; row < rowTops.Length; row++)
                {
                    int top = rowTops[row];
                    if (numbered)
                    {
                        g.DrawString((row + 1).ToString(), font, brush, -2f, top);
                    }
                    else
                    {
                        g.FillEllipse(brush, 1, top + 3, 3, 3);
                    }

                    g.FillRectangle(brush, 7, top + 3, 8, 2);
                }
            }

            return bmp;
        }

        /// <summary>
        /// A 16x16 chain-link icon: two rounded rectangles crossing at 45
        /// degrees, matching the standard hyperlink glyph.
        /// </summary>
        private static Bitmap CreateLinkIcon()
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            using (Pen pen = new Pen(Color.Black, 1.6f))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TranslateTransform(8, 8);
                g.RotateTransform(-45);

                using (GraphicsPath link1 = RoundedRectangle(new RectangleF(-7, -3, 7, 6), 3))
                using (GraphicsPath link2 = RoundedRectangle(new RectangleF(0, -3, 7, 6), 3))
                {
                    g.DrawPath(pen, link1);
                    g.DrawPath(pen, link2);
                }
            }

            return bmp;
        }

        /// <summary>
        /// A 16x16 "insert picture" icon (frame, sun, mountains). The "online"
        /// variant used for the Image URL button adds a small chain-link badge
        /// in the corner to distinguish it from the local-file variant.
        /// </summary>
        private static Bitmap CreateImageIcon(bool online)
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            using (Pen framePen = new Pen(Color.Black, 1.3f))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.DrawRectangle(framePen, 1, 2, 12, 10);
                g.FillEllipse(brush, 3, 4, 3, 3);

                Point[] mountains =
                {
                    new Point(2, 11),
                    new Point(6, 6),
                    new Point(8, 8),
                    new Point(10, 5),
                    new Point(13, 11)
                };
                g.FillPolygon(brush, mountains);

                if (online)
                {
                    using (Pen badgePen = new Pen(Color.Black, 1.2f))
                    {
                        g.TranslateTransform(11.5f, 11.5f);
                        g.RotateTransform(-45);
                        using (GraphicsPath badge1 = RoundedRectangle(new RectangleF(-3f, -1.3f, 3f, 2.6f), 1.2f))
                        using (GraphicsPath badge2 = RoundedRectangle(new RectangleF(0f, -1.3f, 3f, 2.6f), 1.2f))
                        {
                            g.DrawPath(badgePen, badge1);
                            g.DrawPath(badgePen, badge2);
                        }
                    }
                }
            }

            return bmp;
        }

        /// <summary>
        /// Builds a rounded-rectangle path, used by the link/badge icons.
        /// </summary>
        private static GraphicsPath RoundedRectangle(RectangleF rect, float radius)
        {
            float diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the btnSend control. Validates, resolves the
        /// recipient list and sends one email per recipient.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnSend_Click(object sender, EventArgs e)
        {
            string from = cbFrom.SelectedItem as string;
            if (string.IsNullOrEmpty(from))
            {
                MessageBox.Show("Please select a From address.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Please enter a subject.");
                return;
            }

            string htmlBody = GetEditorHtml();
            if (string.IsNullOrWhiteSpace(Regex.Replace(htmlBody, "<[^>]+>", string.Empty).Replace("&nbsp;", " ")))
            {
                MessageBox.Show("The message body is empty.");
                return;
            }

            List<string> invalid;
            List<string> recipients = ResolveRecipients(out invalid);

            if (invalid.Count > 0)
            {
                DialogResult proceed = MessageBox.Show(
                    "These extra addresses look malformed and will be skipped:\r\n\r\n" +
                    string.Join("\r\n", invalid) + "\r\n\r\nContinue?",
                    "Invalid addresses", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (proceed != DialogResult.OK)
                {
                    return;
                }
            }

            if (recipients.Count == 0)
            {
                MessageBox.Show("Select a mailing list, check \"Send to all active members\", or enter at least one email address.");
                return;
            }

            if (MessageBox.Show($"Send this email to {recipients.Count} recipient(s) from {from}?",
                    "Confirm Send", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string subject = txtSubject.Text.Trim();
            SendMail mailer = new SendMail();
            int sent = 0;
            List<string> failed = new List<string>();

            btnSend.Enabled = false;
            btnCancel.Enabled = false;
            Cursor = Cursors.WaitCursor;

            foreach (string addr in recipients)
            {
                try
                {
                    var response = await mailer.SendSingleEmailAsync(from, addr, addr, subject, htmlBody);
                    if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                    {
                        sent++;
                    }
                    else
                    {
                        failed.Add(addr + " (" + response.StatusCode + ")");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Send failed for " + addr, ex);
                    failed.Add(addr + " (" + ex.Message + ")");
                }
            }

            Cursor = Cursors.Default;
            btnSend.Enabled = true;
            btnCancel.Enabled = true;

            string summary = $"Sent {sent} of {recipients.Count} email(s).";
            if (failed.Count > 0)
            {
                summary += "\r\n\r\nFailed:\r\n" + string.Join("\r\n", failed);
            }

            MessageBox.Show(summary, "Send Complete");

            if (failed.Count == 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Merges the selected recipient source (mailing list or all active
        /// members) with the parsed free-text addresses, trims, validates the
        /// free-text entries and de-dupes case-insensitively.
        /// </summary>
        /// <param name="invalid">Receives the malformed free-text entries.</param>
        private List<string> ResolveRecipients(out List<string> invalid)
        {
            List<string> addresses = new List<string>();
            invalid = new List<string>();

            using (WoodClubEntities context = new WoodClubEntities())
            {
                int? listId = cbMailingList.SelectedValue as int?;
                if (chkSendToAll.Checked)
                {
                    addresses.AddRange(from m in context.MemberRosters
                                       where m.ClubDuesPaid == true && m.Badge != "20001"
                                             && m.Email != null && m.Email != ""
                                       select m.Email);
                }
                else if (listId.HasValue && listId.Value != NoListId)
                {
                    int id = listId.Value;
                    addresses.AddRange(from mlm in context.MailingListMembers
                                       join mr in context.MemberRosters on mlm.MemberId equals mr.id
                                       where mlm.MailingListId == id && mlm.IsSubscribed
                                             && mr.Email != null && mr.Email != ""
                                       select mr.Email);
                }
            }

            foreach (string raw in txtExtra.Text.Split(new[] { ';', ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string address = raw.Trim();
                if (address.Length == 0)
                {
                    continue;
                }

                if (EmailPattern.IsMatch(address))
                {
                    addresses.Add(address);
                }
                else
                {
                    invalid.Add(address);
                }
            }

            HashSet<string> seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            List<string> result = new List<string>();
            foreach (string address in addresses)
            {
                string trimmed = (address ?? string.Empty).Trim();
                if (trimmed.Length > 0 && seen.Add(trimmed))
                {
                    result.Add(trimmed);
                }
            }

            return result;
        }

        /// <summary>
        /// A small modal text prompt (there is no built-in InputBox in this
        /// project). Returns the trimmed text, or null if cancelled.
        /// </summary>
        private static string PromptForString(string title, string prompt, string defaultValue)
        {
            using (Form form = new Form())
            {
                form.Text = title;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ClientSize = new System.Drawing.Size(400, 112);

                Label label = new Label { Left = 12, Top = 12, Width = 376, Text = prompt };
                TextBox textBox = new TextBox { Left = 12, Top = 34, Width = 376, Text = defaultValue };
                Button ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Left = 232, Top = 72, Width = 75 };
                Button cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Left = 313, Top = 72, Width = 75 };

                form.Controls.Add(label);
                form.Controls.Add(textBox);
                form.Controls.Add(ok);
                form.Controls.Add(cancel);
                form.AcceptButton = ok;
                form.CancelButton = cancel;

                return form.ShowDialog() == DialogResult.OK ? textBox.Text.Trim() : null;
            }
        }

        /// <summary>
        /// A mailing list entry for the selector dropdown.
        /// </summary>
        private class ListItem
        {
            public int MailingListId { get; set; }
            public string Name { get; set; }
        }
    }
}

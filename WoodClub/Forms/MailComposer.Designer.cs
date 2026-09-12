namespace WoodClub.Forms
{
    partial class MailComposer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMailingList = new System.Windows.Forms.Label();
            this.cbMailingList = new System.Windows.Forms.ComboBox();
            this.btnCreateList = new System.Windows.Forms.Button();
            this.chkSendToAll = new System.Windows.Forms.CheckBox();
            this.lblExtra = new System.Windows.Forms.Label();
            this.txtExtra = new System.Windows.Forms.TextBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.cbFrom = new System.Windows.Forms.ComboBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.pnlEditor = new System.Windows.Forms.Panel();
            this.webEditor = new System.Windows.Forms.WebBrowser();
            this.toolStripEditor = new System.Windows.Forms.ToolStrip();
            this.tslFont = new System.Windows.Forms.ToolStripLabel();
            this.tscFontName = new System.Windows.Forms.ToolStripComboBox();
            this.tscFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.tsbColor = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBold = new System.Windows.Forms.ToolStripButton();
            this.tsbItalic = new System.Windows.Forms.ToolStripButton();
            this.tsbUnderline = new System.Windows.Forms.ToolStripButton();
            this.tsSepAlign = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAlignLeft = new System.Windows.Forms.ToolStripButton();
            this.tsbAlignCenter = new System.Windows.Forms.ToolStripButton();
            this.tsbAlignRight = new System.Windows.Forms.ToolStripButton();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNumberedList = new System.Windows.Forms.ToolStripButton();
            this.tsbBulletedList = new System.Windows.Forms.ToolStripButton();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLink = new System.Windows.Forms.ToolStripButton();
            this.tsbImageFile = new System.Windows.Forms.ToolStripButton();
            this.tsbAttachFile = new System.Windows.Forms.ToolStripButton();
            this.tslImageNote = new System.Windows.Forms.ToolStripLabel();
            this.lblAttachSummary = new System.Windows.Forms.Label();
            this.lstAttachments = new System.Windows.Forms.ListBox();
            this.btnRemoveAttachment = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlEditor.SuspendLayout();
            this.toolStripEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMailingList
            // 
            this.lblMailingList.AutoSize = true;
            this.lblMailingList.Location = new System.Drawing.Point(18, 23);
            this.lblMailingList.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMailingList.Name = "lblMailingList";
            this.lblMailingList.Size = new System.Drawing.Size(91, 20);
            this.lblMailingList.TabIndex = 0;
            this.lblMailingList.Text = "Mailing List:";
            // 
            // cbMailingList
            // 
            this.cbMailingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMailingList.FormattingEnabled = true;
            this.cbMailingList.Location = new System.Drawing.Point(150, 18);
            this.cbMailingList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbMailingList.Name = "cbMailingList";
            this.cbMailingList.Size = new System.Drawing.Size(388, 28);
            this.cbMailingList.TabIndex = 1;
            this.cbMailingList.SelectedIndexChanged += new System.EventHandler(this.cbMailingList_SelectedIndexChanged);
            // 
            // btnCreateList
            // 
            this.btnCreateList.Location = new System.Drawing.Point(552, 17);
            this.btnCreateList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCreateList.Name = "btnCreateList";
            this.btnCreateList.Size = new System.Drawing.Size(135, 35);
            this.btnCreateList.TabIndex = 2;
            this.btnCreateList.Text = "Create List";
            this.btnCreateList.UseVisualStyleBackColor = true;
            this.btnCreateList.Click += new System.EventHandler(this.btnCreateList_Click);
            // 
            // chkSendToAll
            // 
            this.chkSendToAll.AutoSize = true;
            this.chkSendToAll.Location = new System.Drawing.Point(150, 65);
            this.chkSendToAll.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkSendToAll.Name = "chkSendToAll";
            this.chkSendToAll.Size = new System.Drawing.Size(299, 24);
            this.chkSendToAll.TabIndex = 3;
            this.chkSendToAll.Text = "Send to all active members (ignore list)";
            this.chkSendToAll.UseVisualStyleBackColor = true;
            this.chkSendToAll.CheckedChanged += new System.EventHandler(this.chkSendToAll_CheckedChanged);
            // 
            // lblExtra
            // 
            this.lblExtra.AutoSize = true;
            this.lblExtra.Location = new System.Drawing.Point(18, 105);
            this.lblExtra.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExtra.Name = "lblExtra";
            this.lblExtra.Size = new System.Drawing.Size(289, 20);
            this.lblExtra.TabIndex = 4;
            this.lblExtra.Text = "Extra addresses (semicolon separated):";
            // 
            // txtExtra
            // 
            this.txtExtra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExtra.Location = new System.Drawing.Point(18, 129);
            this.txtExtra.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtExtra.Multiline = true;
            this.txtExtra.Name = "txtExtra";
            this.txtExtra.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExtra.Size = new System.Drawing.Size(1162, 59);
            this.txtExtra.TabIndex = 5;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(18, 211);
            this.lblFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(50, 20);
            this.lblFrom.TabIndex = 6;
            this.lblFrom.Text = "From:";
            // 
            // cbFrom
            // 
            this.cbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFrom.FormattingEnabled = true;
            this.cbFrom.Location = new System.Drawing.Point(150, 206);
            this.cbFrom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbFrom.Name = "cbFrom";
            this.cbFrom.Size = new System.Drawing.Size(388, 28);
            this.cbFrom.TabIndex = 7;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(18, 257);
            this.lblSubject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(67, 20);
            this.lblSubject.TabIndex = 8;
            this.lblSubject.Text = "Subject:";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(150, 252);
            this.txtSubject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(1030, 26);
            this.txtSubject.TabIndex = 9;
            this.txtSubject.TextChanged += new System.EventHandler(this.txtSubject_TextChanged);
            // 
            // pnlEditor
            // 
            this.pnlEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEditor.Controls.Add(this.webEditor);
            this.pnlEditor.Controls.Add(this.toolStripEditor);
            this.pnlEditor.Location = new System.Drawing.Point(18, 302);
            this.pnlEditor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Size = new System.Drawing.Size(1163, 476);
            this.pnlEditor.TabIndex = 10;
            // 
            // webEditor
            // 
            this.webEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webEditor.Location = new System.Drawing.Point(0, 25);
            this.webEditor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.webEditor.MinimumSize = new System.Drawing.Size(30, 31);
            this.webEditor.Name = "webEditor";
            this.webEditor.ScriptErrorsSuppressed = true;
            this.webEditor.Size = new System.Drawing.Size(1161, 449);
            this.webEditor.TabIndex = 1;
            this.webEditor.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webEditor_DocumentCompleted);
            // 
            // toolStripEditor
            // 
            this.toolStripEditor.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslFont,
            this.tscFontName,
            this.tscFontSize,
            this.tsbColor,
            this.tsSep1,
            this.tsbBold,
            this.tsbItalic,
            this.tsbUnderline,
            this.tsSepAlign,
            this.tsbAlignLeft,
            this.tsbAlignCenter,
            this.tsbAlignRight,
            this.tsSep2,
            this.tsbNumberedList,
            this.tsbBulletedList,
            this.tsSep3,
            this.tsbLink,
            this.tsbImageFile,
            this.tsbAttachFile,
            this.tslImageNote});
            this.toolStripEditor.Location = new System.Drawing.Point(0, 0);
            this.toolStripEditor.Name = "toolStripEditor";
            this.toolStripEditor.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripEditor.Size = new System.Drawing.Size(1161, 25);
            this.toolStripEditor.TabIndex = 0;
            // 
            // tslFont
            // 
            this.tslFont.Name = "tslFont";
            this.tslFont.Size = new System.Drawing.Size(34, 22);
            this.tslFont.Text = "Font:";
            // 
            // tscFontName
            // 
            this.tscFontName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscFontName.Name = "tscFontName";
            this.tscFontName.Size = new System.Drawing.Size(193, 25);
            this.tscFontName.SelectedIndexChanged += new System.EventHandler(this.tscFontName_SelectedIndexChanged);
            // 
            // tscFontSize
            // 
            this.tscFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscFontSize.Name = "tscFontSize";
            this.tscFontSize.Size = new System.Drawing.Size(110, 25);
            this.tscFontSize.SelectedIndexChanged += new System.EventHandler(this.tscFontSize_SelectedIndexChanged);
            // 
            // tsbColor
            // 
            this.tsbColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbColor.Name = "tsbColor";
            this.tsbColor.Size = new System.Drawing.Size(40, 22);
            this.tsbColor.Text = "Color";
            this.tsbColor.Click += new System.EventHandler(this.tsbColor_Click);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbBold
            // 
            this.tsbBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbBold.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsbBold.Name = "tsbBold";
            this.tsbBold.Size = new System.Drawing.Size(23, 22);
            this.tsbBold.Text = "B";
            this.tsbBold.Click += new System.EventHandler(this.tsbBold_Click);
            // 
            // tsbItalic
            // 
            this.tsbItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbItalic.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.tsbItalic.Name = "tsbItalic";
            this.tsbItalic.Size = new System.Drawing.Size(23, 22);
            this.tsbItalic.Text = "I";
            this.tsbItalic.Click += new System.EventHandler(this.tsbItalic_Click);
            // 
            // tsbUnderline
            // 
            this.tsbUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbUnderline.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.tsbUnderline.Name = "tsbUnderline";
            this.tsbUnderline.Size = new System.Drawing.Size(23, 22);
            this.tsbUnderline.Text = "U";
            this.tsbUnderline.Click += new System.EventHandler(this.tsbUnderline_Click);
            // 
            // tsSepAlign
            // 
            this.tsSepAlign.Name = "tsSepAlign";
            this.tsSepAlign.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbAlignLeft
            // 
            this.tsbAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAlignLeft.Name = "tsbAlignLeft";
            this.tsbAlignLeft.Size = new System.Drawing.Size(23, 22);
            this.tsbAlignLeft.Text = "Align Left";
            this.tsbAlignLeft.ToolTipText = "Align Left";
            this.tsbAlignLeft.Click += new System.EventHandler(this.tsbAlignLeft_Click);
            // 
            // tsbAlignCenter
            // 
            this.tsbAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAlignCenter.Name = "tsbAlignCenter";
            this.tsbAlignCenter.Size = new System.Drawing.Size(23, 22);
            this.tsbAlignCenter.Text = "Center";
            this.tsbAlignCenter.ToolTipText = "Center";
            this.tsbAlignCenter.Click += new System.EventHandler(this.tsbAlignCenter_Click);
            // 
            // tsbAlignRight
            // 
            this.tsbAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAlignRight.Name = "tsbAlignRight";
            this.tsbAlignRight.Size = new System.Drawing.Size(23, 22);
            this.tsbAlignRight.Text = "Align Right";
            this.tsbAlignRight.ToolTipText = "Align Right";
            this.tsbAlignRight.Click += new System.EventHandler(this.tsbAlignRight_Click);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbNumberedList
            // 
            this.tsbNumberedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNumberedList.Name = "tsbNumberedList";
            this.tsbNumberedList.Size = new System.Drawing.Size(23, 22);
            this.tsbNumberedList.Text = "Numbered List";
            this.tsbNumberedList.ToolTipText = "Numbered List";
            this.tsbNumberedList.Click += new System.EventHandler(this.tsbNumberedList_Click);
            // 
            // tsbBulletedList
            // 
            this.tsbBulletedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBulletedList.Name = "tsbBulletedList";
            this.tsbBulletedList.Size = new System.Drawing.Size(23, 22);
            this.tsbBulletedList.Text = "Bulleted List";
            this.tsbBulletedList.ToolTipText = "Bulleted List";
            this.tsbBulletedList.Click += new System.EventHandler(this.tsbBulletedList_Click);
            // 
            // tsSep3
            // 
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLink
            // 
            this.tsbLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLink.Name = "tsbLink";
            this.tsbLink.Size = new System.Drawing.Size(23, 22);
            this.tsbLink.Text = "Link";
            this.tsbLink.ToolTipText = "Insert Hyperlink";
            this.tsbLink.Click += new System.EventHandler(this.tsbLink_Click);
            // 
            // tsbImageFile
            // 
            this.tsbImageFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImageFile.Name = "tsbImageFile";
            this.tsbImageFile.Size = new System.Drawing.Size(23, 22);
            this.tsbImageFile.Text = "Image File";
            this.tsbImageFile.ToolTipText = "Insert Image from File";
            this.tsbImageFile.Click += new System.EventHandler(this.tsbImageFile_Click);
            // 
            // tsbAttachFile
            // 
            this.tsbAttachFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAttachFile.Name = "tsbAttachFile";
            this.tsbAttachFile.Size = new System.Drawing.Size(23, 22);
            this.tsbAttachFile.Text = "Attach File";
            this.tsbAttachFile.ToolTipText = "Attach File";
            this.tsbAttachFile.Click += new System.EventHandler(this.tsbAttachFile_Click);
            // 
            // tslImageNote
            // 
            this.tslImageNote.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslImageNote.ForeColor = System.Drawing.SystemColors.GrayText;
            this.tslImageNote.Name = "tslImageNote";
            this.tslImageNote.Size = new System.Drawing.Size(0, 22);
            // 
            // lblAttachSummary
            // 
            this.lblAttachSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAttachSummary.AutoSize = true;
            this.lblAttachSummary.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAttachSummary.Location = new System.Drawing.Point(18, 788);
            this.lblAttachSummary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttachSummary.Name = "lblAttachSummary";
            this.lblAttachSummary.Size = new System.Drawing.Size(128, 20);
            this.lblAttachSummary.TabIndex = 20;
            this.lblAttachSummary.Text = "No files attached";
            this.lblAttachSummary.Visible = false;
            // 
            // lstAttachments
            // 
            this.lstAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAttachments.FormattingEnabled = true;
            this.lstAttachments.ItemHeight = 20;
            this.lstAttachments.Location = new System.Drawing.Point(18, 835);
            this.lstAttachments.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstAttachments.Name = "lstAttachments";
            this.lstAttachments.Size = new System.Drawing.Size(1018, 64);
            this.lstAttachments.TabIndex = 21;
            this.lstAttachments.Visible = false;
            // 
            // btnRemoveAttachment
            // 
            this.btnRemoveAttachment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAttachment.Location = new System.Drawing.Point(1047, 815);
            this.btnRemoveAttachment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRemoveAttachment.Name = "btnRemoveAttachment";
            this.btnRemoveAttachment.Size = new System.Drawing.Size(135, 38);
            this.btnRemoveAttachment.TabIndex = 22;
            this.btnRemoveAttachment.Text = "Remove";
            this.btnRemoveAttachment.UseVisualStyleBackColor = true;
            this.btnRemoveAttachment.Visible = false;
            this.btnRemoveAttachment.Click += new System.EventHandler(this.btnRemoveAttachment_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(948, 929);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(112, 35);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(826, 929);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 35);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1070, 929);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MailComposer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1200, 983);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnRemoveAttachment);
            this.Controls.Add(this.lstAttachments);
            this.Controls.Add(this.lblAttachSummary);
            this.Controls.Add(this.pnlEditor);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.cbFrom);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.txtExtra);
            this.Controls.Add(this.lblExtra);
            this.Controls.Add(this.chkSendToAll);
            this.Controls.Add(this.btnCreateList);
            this.Controls.Add(this.cbMailingList);
            this.Controls.Add(this.lblMailingList);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(952, 717);
            this.Name = "MailComposer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compose Email";
            this.Load += new System.EventHandler(this.MailComposer_Load);
            this.pnlEditor.ResumeLayout(false);
            this.pnlEditor.PerformLayout();
            this.toolStripEditor.ResumeLayout(false);
            this.toolStripEditor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMailingList;
        private System.Windows.Forms.ComboBox cbMailingList;
        private System.Windows.Forms.Button btnCreateList;
        private System.Windows.Forms.CheckBox chkSendToAll;
        private System.Windows.Forms.Label lblExtra;
        private System.Windows.Forms.TextBox txtExtra;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.ComboBox cbFrom;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Panel pnlEditor;
        private System.Windows.Forms.WebBrowser webEditor;
        private System.Windows.Forms.ToolStrip toolStripEditor;
        private System.Windows.Forms.ToolStripLabel tslFont;
        private System.Windows.Forms.ToolStripComboBox tscFontName;
        private System.Windows.Forms.ToolStripComboBox tscFontSize;
        private System.Windows.Forms.ToolStripButton tsbColor;
        private System.Windows.Forms.ToolStripSeparator tsSep1;
        private System.Windows.Forms.ToolStripButton tsbBold;
        private System.Windows.Forms.ToolStripButton tsbItalic;
        private System.Windows.Forms.ToolStripButton tsbUnderline;
        private System.Windows.Forms.ToolStripSeparator tsSepAlign;
        private System.Windows.Forms.ToolStripButton tsbAlignLeft;
        private System.Windows.Forms.ToolStripButton tsbAlignCenter;
        private System.Windows.Forms.ToolStripButton tsbAlignRight;
        private System.Windows.Forms.ToolStripSeparator tsSep2;
        private System.Windows.Forms.ToolStripButton tsbNumberedList;
        private System.Windows.Forms.ToolStripButton tsbBulletedList;
        private System.Windows.Forms.ToolStripSeparator tsSep3;
        private System.Windows.Forms.ToolStripButton tsbLink;
        private System.Windows.Forms.ToolStripButton tsbImageFile;
        private System.Windows.Forms.ToolStripButton tsbAttachFile;
        private System.Windows.Forms.ToolStripLabel tslImageNote;
        private System.Windows.Forms.Label lblAttachSummary;
        private System.Windows.Forms.ListBox lstAttachments;
        private System.Windows.Forms.Button btnRemoveAttachment;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}

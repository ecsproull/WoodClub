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
            this.components = new System.ComponentModel.Container();
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
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNumberedList = new System.Windows.Forms.ToolStripButton();
            this.tsbBulletedList = new System.Windows.Forms.ToolStripButton();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLink = new System.Windows.Forms.ToolStripButton();
            this.tsbImageFile = new System.Windows.Forms.ToolStripButton();
            this.tsbImageUrl = new System.Windows.Forms.ToolStripButton();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlEditor.SuspendLayout();
            this.toolStripEditor.SuspendLayout();
            this.SuspendLayout();
            //
            // lblMailingList
            //
            this.lblMailingList.AutoSize = true;
            this.lblMailingList.Location = new System.Drawing.Point(12, 15);
            this.lblMailingList.Name = "lblMailingList";
            this.lblMailingList.Size = new System.Drawing.Size(66, 13);
            this.lblMailingList.TabIndex = 0;
            this.lblMailingList.Text = "Mailing List:";
            //
            // cbMailingList
            //
            this.cbMailingList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMailingList.FormattingEnabled = true;
            this.cbMailingList.Location = new System.Drawing.Point(100, 12);
            this.cbMailingList.Name = "cbMailingList";
            this.cbMailingList.Size = new System.Drawing.Size(260, 21);
            this.cbMailingList.TabIndex = 1;
            //
            // btnCreateList
            //
            this.btnCreateList.Location = new System.Drawing.Point(368, 11);
            this.btnCreateList.Name = "btnCreateList";
            this.btnCreateList.Size = new System.Drawing.Size(90, 23);
            this.btnCreateList.TabIndex = 2;
            this.btnCreateList.Text = "Create List";
            this.btnCreateList.UseVisualStyleBackColor = true;
            this.btnCreateList.Click += new System.EventHandler(this.btnCreateList_Click);
            //
            // chkSendToAll
            //
            this.chkSendToAll.AutoSize = true;
            this.chkSendToAll.Location = new System.Drawing.Point(100, 42);
            this.chkSendToAll.Name = "chkSendToAll";
            this.chkSendToAll.Size = new System.Drawing.Size(215, 17);
            this.chkSendToAll.TabIndex = 3;
            this.chkSendToAll.Text = "Send to all active members (ignore list)";
            this.chkSendToAll.UseVisualStyleBackColor = true;
            this.chkSendToAll.CheckedChanged += new System.EventHandler(this.chkSendToAll_CheckedChanged);
            //
            // lblExtra
            //
            this.lblExtra.AutoSize = true;
            this.lblExtra.Location = new System.Drawing.Point(12, 68);
            this.lblExtra.Name = "lblExtra";
            this.lblExtra.Size = new System.Drawing.Size(205, 13);
            this.lblExtra.TabIndex = 4;
            this.lblExtra.Text = "Extra addresses (semicolon separated):";
            //
            // txtExtra
            //
            this.txtExtra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExtra.Location = new System.Drawing.Point(12, 84);
            this.txtExtra.Multiline = true;
            this.txtExtra.Name = "txtExtra";
            this.txtExtra.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExtra.Size = new System.Drawing.Size(776, 40);
            this.txtExtra.TabIndex = 5;
            //
            // lblFrom
            //
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(12, 137);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(33, 13);
            this.lblFrom.TabIndex = 6;
            this.lblFrom.Text = "From:";
            //
            // cbFrom
            //
            this.cbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFrom.FormattingEnabled = true;
            this.cbFrom.Location = new System.Drawing.Point(100, 134);
            this.cbFrom.Name = "cbFrom";
            this.cbFrom.Size = new System.Drawing.Size(260, 21);
            this.cbFrom.TabIndex = 7;
            //
            // lblSubject
            //
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(12, 167);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(46, 13);
            this.lblSubject.TabIndex = 8;
            this.lblSubject.Text = "Subject:";
            //
            // txtSubject
            //
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(100, 164);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(688, 20);
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
            this.pnlEditor.Location = new System.Drawing.Point(12, 196);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Size = new System.Drawing.Size(776, 396);
            this.pnlEditor.TabIndex = 10;
            //
            // webEditor
            //
            this.webEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webEditor.Location = new System.Drawing.Point(0, 25);
            this.webEditor.MinimumSize = new System.Drawing.Size(20, 20);
            this.webEditor.Name = "webEditor";
            this.webEditor.ScriptErrorsSuppressed = true;
            this.webEditor.Size = new System.Drawing.Size(774, 369);
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
            this.tsSep2,
            this.tsbNumberedList,
            this.tsbBulletedList,
            this.tsSep3,
            this.tsbLink,
            this.tsbImageFile,
            this.tsbImageUrl});
            this.toolStripEditor.Location = new System.Drawing.Point(0, 0);
            this.toolStripEditor.Name = "toolStripEditor";
            this.toolStripEditor.Size = new System.Drawing.Size(774, 25);
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
            this.tscFontName.Size = new System.Drawing.Size(130, 25);
            this.tscFontName.SelectedIndexChanged += new System.EventHandler(this.tscFontName_SelectedIndexChanged);
            //
            // tscFontSize
            //
            this.tscFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscFontSize.Name = "tscFontSize";
            this.tscFontSize.Size = new System.Drawing.Size(50, 25);
            this.tscFontSize.SelectedIndexChanged += new System.EventHandler(this.tscFontSize_SelectedIndexChanged);
            //
            // tsbColor
            //
            this.tsbColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbColor.Name = "tsbColor";
            this.tsbColor.Size = new System.Drawing.Size(37, 22);
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
            // tsSep2
            //
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 25);
            //
            // tsbNumberedList
            //
            this.tsbNumberedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbNumberedList.Name = "tsbNumberedList";
            this.tsbNumberedList.Size = new System.Drawing.Size(91, 22);
            this.tsbNumberedList.Text = "Numbered List";
            this.tsbNumberedList.Click += new System.EventHandler(this.tsbNumberedList_Click);
            //
            // tsbBulletedList
            //
            this.tsbBulletedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbBulletedList.Name = "tsbBulletedList";
            this.tsbBulletedList.Size = new System.Drawing.Size(76, 22);
            this.tsbBulletedList.Text = "Bulleted List";
            this.tsbBulletedList.Click += new System.EventHandler(this.tsbBulletedList_Click);
            //
            // tsSep3
            //
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6, 25);
            //
            // tsbLink
            //
            this.tsbLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLink.Name = "tsbLink";
            this.tsbLink.Size = new System.Drawing.Size(35, 22);
            this.tsbLink.Text = "Link";
            this.tsbLink.Click += new System.EventHandler(this.tsbLink_Click);
            //
            // tsbImageFile
            //
            this.tsbImageFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbImageFile.Name = "tsbImageFile";
            this.tsbImageFile.Size = new System.Drawing.Size(67, 22);
            this.tsbImageFile.Text = "Image File";
            this.tsbImageFile.Click += new System.EventHandler(this.tsbImageFile_Click);
            //
            // tsbImageUrl
            //
            this.tsbImageUrl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbImageUrl.Name = "tsbImageUrl";
            this.tsbImageUrl.Size = new System.Drawing.Size(65, 22);
            this.tsbImageUrl.Text = "Image URL";
            this.tsbImageUrl.Click += new System.EventHandler(this.tsbImageUrl_Click);
            //
            // btnSend
            //
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(632, 604);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(713, 604);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // MailComposer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 639);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSend);
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
            this.MinimumSize = new System.Drawing.Size(640, 480);
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
        private System.Windows.Forms.ToolStripSeparator tsSep2;
        private System.Windows.Forms.ToolStripButton tsbNumberedList;
        private System.Windows.Forms.ToolStripButton tsbBulletedList;
        private System.Windows.Forms.ToolStripSeparator tsSep3;
        private System.Windows.Forms.ToolStripButton tsbLink;
        private System.Windows.Forms.ToolStripButton tsbImageFile;
        private System.Windows.Forms.ToolStripButton tsbImageUrl;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnCancel;
    }
}

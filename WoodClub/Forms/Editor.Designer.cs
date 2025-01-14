﻿namespace WoodClub
{
    partial class MemberEditor
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbAdminBlock = new System.Windows.Forms.CheckBox();
			this.cbRecDuesPaid = new System.Windows.Forms.CheckBox();
			this.checkBoxFreeDay = new System.Windows.Forms.CheckBox();
			this.buttonApply = new System.Windows.Forms.Button();
			this.buttonClear = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.GridViewNewCredits = new System.Windows.Forms.DataGridView();
			this.creditsOnlyChkbx = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cbMain = new System.Windows.Forms.CheckBox();
			this.cbSide = new System.Windows.Forms.CheckBox();
			this.cbOffice = new System.Windows.Forms.CheckBox();
			this.cbMachine = new System.Windows.Forms.CheckBox();
			this.cbAssembly = new System.Windows.Forms.CheckBox();
			this.cbMaint = new System.Windows.Forms.CheckBox();
			this.cbLumber = new System.Windows.Forms.CheckBox();
			this.TransDataGridView = new System.Windows.Forms.DataGridView();
			this.cbExtendHr = new System.Windows.Forms.CheckBox();
			this.dataGridViewCodes = new System.Windows.Forms.DataGridView();
			this.txtRFcard = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtLastDay = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cbClubDuesPaid = new System.Windows.Forms.CheckBox();
			this.AccessTime = new System.Windows.Forms.ListBox();
			this.txtExemptDate = new System.Windows.Forms.TextBox();
			this.lblExemptDate = new System.Windows.Forms.Label();
			this.cbExempt = new System.Windows.Forms.CheckBox();
			this.txtCredits = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtClubDuesPaid = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtJoinDate = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.lblEmail = new System.Windows.Forms.Label();
			this.txtPhone = new System.Windows.Forms.TextBox();
			this.lblPhone = new System.Windows.Forms.Label();
			this.txtZip = new System.Windows.Forms.TextBox();
			this.lblZip = new System.Windows.Forms.Label();
			this.txtState = new System.Windows.Forms.TextBox();
			this.lblState = new System.Windows.Forms.Label();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.lblAddress = new System.Windows.Forms.Label();
			this.txtLastNm = new System.Windows.Forms.TextBox();
			this.lblLastNm = new System.Windows.Forms.Label();
			this.txtFirstNm = new System.Windows.Forms.TextBox();
			this.lblFirstNm = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblPhoto = new System.Windows.Forms.Label();
			this.txtRecCard = new System.Windows.Forms.TextBox();
			this.lblRecCard = new System.Windows.Forms.Label();
			this.txtBadge = new System.Windows.Forms.TextBox();
			this.lblBadge = new System.Windows.Forms.Label();
			this.cbNewBadge = new System.Windows.Forms.CheckBox();
			this.btnLoadPhoto = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.txtLocker = new System.Windows.Forms.TextBox();
			this.lblLocker = new System.Windows.Forms.Label();
			this.editLocker = new System.Windows.Forms.Button();
			this.cbUpdateControllers = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.privateCheckBox = new System.Windows.Forms.CheckBox();
			this.ercFirstName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.ercPhone = new System.Windows.Forms.TextBox();
			this.memberBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.badgeCode1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.badgeCodeDescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.codeValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.badgeCodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridViewNewCredits)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TransDataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCodes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memberBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.badgeCodeBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.groupBox1.Controls.Add(this.cbAdminBlock);
			this.groupBox1.Controls.Add(this.cbRecDuesPaid);
			this.groupBox1.Controls.Add(this.checkBoxFreeDay);
			this.groupBox1.Controls.Add(this.buttonApply);
			this.groupBox1.Controls.Add(this.buttonClear);
			this.groupBox1.Controls.Add(this.buttonCancel);
			this.groupBox1.Controls.Add(this.GridViewNewCredits);
			this.groupBox1.Controls.Add(this.creditsOnlyChkbx);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.TransDataGridView);
			this.groupBox1.Controls.Add(this.cbExtendHr);
			this.groupBox1.Controls.Add(this.dataGridViewCodes);
			this.groupBox1.Controls.Add(this.txtRFcard);
			this.groupBox1.Controls.Add(this.btnSave);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.txtLastDay);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.cbClubDuesPaid);
			this.groupBox1.Controls.Add(this.AccessTime);
			this.groupBox1.Controls.Add(this.txtExemptDate);
			this.groupBox1.Controls.Add(this.lblExemptDate);
			this.groupBox1.Controls.Add(this.cbExempt);
			this.groupBox1.Controls.Add(this.txtCredits);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtClubDuesPaid);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtJoinDate);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(38, 247);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(656, 637);
			this.groupBox1.TabIndex = 41;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Club Information";
			// 
			// cbAdminBlock
			// 
			this.cbAdminBlock.AutoSize = true;
			this.cbAdminBlock.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.memberBindingSource, "AdminBlock", true));
			this.cbAdminBlock.Location = new System.Drawing.Point(577, 44);
			this.cbAdminBlock.Name = "cbAdminBlock";
			this.cbAdminBlock.Size = new System.Drawing.Size(65, 17);
			this.cbAdminBlock.TabIndex = 61;
			this.cbAdminBlock.Text = "Blocked";
			this.cbAdminBlock.UseVisualStyleBackColor = true;
			// 
			// cbRecDuesPaid
			// 
			this.cbRecDuesPaid.AutoSize = true;
			this.cbRecDuesPaid.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.memberBindingSource, "RecDuesPaid", true));
			this.cbRecDuesPaid.Location = new System.Drawing.Point(232, 24);
			this.cbRecDuesPaid.Name = "cbRecDuesPaid";
			this.cbRecDuesPaid.Size = new System.Drawing.Size(98, 17);
			this.cbRecDuesPaid.TabIndex = 60;
			this.cbRecDuesPaid.Text = "Rec Dues Paid";
			this.cbRecDuesPaid.UseVisualStyleBackColor = true;
			// 
			// checkBoxFreeDay
			// 
			this.checkBoxFreeDay.AutoSize = true;
			this.checkBoxFreeDay.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.memberBindingSource, "OneTime", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "false"));
			this.checkBoxFreeDay.Enabled = false;
			this.checkBoxFreeDay.Location = new System.Drawing.Point(526, 15);
			this.checkBoxFreeDay.Name = "checkBoxFreeDay";
			this.checkBoxFreeDay.Size = new System.Drawing.Size(69, 17);
			this.checkBoxFreeDay.TabIndex = 59;
			this.checkBoxFreeDay.Text = "Free Day";
			this.checkBoxFreeDay.UseVisualStyleBackColor = true;
			// 
			// buttonApply
			// 
			this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonApply.Location = new System.Drawing.Point(572, 595);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(75, 23);
			this.buttonApply.TabIndex = 57;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
			// 
			// buttonClear
			// 
			this.buttonClear.Location = new System.Drawing.Point(327, 355);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(75, 23);
			this.buttonClear.TabIndex = 56;
			this.buttonClear.Text = "Clear Credits";
			this.buttonClear.UseVisualStyleBackColor = true;
			this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(491, 596);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 55;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
			// 
			// GridViewNewCredits
			// 
			this.GridViewNewCredits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.GridViewNewCredits.Location = new System.Drawing.Point(13, 335);
			this.GridViewNewCredits.Name = "GridViewNewCredits";
			this.GridViewNewCredits.RowHeadersVisible = false;
			this.GridViewNewCredits.Size = new System.Drawing.Size(306, 69);
			this.GridViewNewCredits.TabIndex = 54;
			// 
			// creditsOnlyChkbx
			// 
			this.creditsOnlyChkbx.AutoSize = true;
			this.creditsOnlyChkbx.Checked = true;
			this.creditsOnlyChkbx.CheckState = System.Windows.Forms.CheckState.Checked;
			this.creditsOnlyChkbx.Location = new System.Drawing.Point(553, 398);
			this.creditsOnlyChkbx.Name = "creditsOnlyChkbx";
			this.creditsOnlyChkbx.Size = new System.Drawing.Size(82, 17);
			this.creditsOnlyChkbx.TabIndex = 53;
			this.creditsOnlyChkbx.Text = "Credits Only";
			this.creditsOnlyChkbx.UseVisualStyleBackColor = true;
			this.creditsOnlyChkbx.CheckedChanged += new System.EventHandler(this.CreditsOnlyChkbx_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.groupBox2.Controls.Add(this.cbMain);
			this.groupBox2.Controls.Add(this.cbSide);
			this.groupBox2.Controls.Add(this.cbOffice);
			this.groupBox2.Controls.Add(this.cbMachine);
			this.groupBox2.Controls.Add(this.cbAssembly);
			this.groupBox2.Controls.Add(this.cbMaint);
			this.groupBox2.Controls.Add(this.cbLumber);
			this.groupBox2.Location = new System.Drawing.Point(328, 133);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(124, 191);
			this.groupBox2.TabIndex = 51;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Door Access";
			// 
			// cbMain
			// 
			this.cbMain.AutoSize = true;
			this.cbMain.Location = new System.Drawing.Point(6, 19);
			this.cbMain.Name = "cbMain";
			this.cbMain.Size = new System.Drawing.Size(76, 17);
			this.cbMain.TabIndex = 52;
			this.cbMain.Text = "Main Front";
			this.cbMain.UseVisualStyleBackColor = true;
			// 
			// cbSide
			// 
			this.cbSide.AutoSize = true;
			this.cbSide.Location = new System.Drawing.Point(6, 42);
			this.cbSide.Name = "cbSide";
			this.cbSide.Size = new System.Drawing.Size(70, 17);
			this.cbSide.TabIndex = 51;
			this.cbSide.Text = "Assembly";
			this.cbSide.UseVisualStyleBackColor = true;
			// 
			// cbOffice
			// 
			this.cbOffice.AutoSize = true;
			this.cbOffice.Location = new System.Drawing.Point(6, 111);
			this.cbOffice.Name = "cbOffice";
			this.cbOffice.Size = new System.Drawing.Size(54, 17);
			this.cbOffice.TabIndex = 48;
			this.cbOffice.Text = "Office";
			this.cbOffice.UseVisualStyleBackColor = true;
			// 
			// cbMachine
			// 
			this.cbMachine.AutoSize = true;
			this.cbMachine.Location = new System.Drawing.Point(6, 157);
			this.cbMachine.Name = "cbMachine";
			this.cbMachine.Size = new System.Drawing.Size(93, 17);
			this.cbMachine.TabIndex = 50;
			this.cbMachine.Text = "Machine Rear";
			this.cbMachine.UseVisualStyleBackColor = true;
			// 
			// cbAssembly
			// 
			this.cbAssembly.AutoSize = true;
			this.cbAssembly.Location = new System.Drawing.Point(6, 134);
			this.cbAssembly.Name = "cbAssembly";
			this.cbAssembly.Size = new System.Drawing.Size(96, 17);
			this.cbAssembly.TabIndex = 49;
			this.cbAssembly.Text = "Assembly Rear";
			this.cbAssembly.UseVisualStyleBackColor = true;
			// 
			// cbMaint
			// 
			this.cbMaint.AutoSize = true;
			this.cbMaint.Location = new System.Drawing.Point(6, 88);
			this.cbMaint.Name = "cbMaint";
			this.cbMaint.Size = new System.Drawing.Size(114, 17);
			this.cbMaint.TabIndex = 12;
			this.cbMaint.Text = "Maintenance Door";
			this.cbMaint.UseVisualStyleBackColor = true;
			// 
			// cbLumber
			// 
			this.cbLumber.AutoSize = true;
			this.cbLumber.Location = new System.Drawing.Point(6, 65);
			this.cbLumber.Name = "cbLumber";
			this.cbLumber.Size = new System.Drawing.Size(87, 17);
			this.cbLumber.TabIndex = 13;
			this.cbLumber.Text = "Lumber Door";
			this.cbLumber.UseVisualStyleBackColor = true;
			// 
			// TransDataGridView
			// 
			this.TransDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TransDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.TransDataGridView.Location = new System.Drawing.Point(13, 419);
			this.TransDataGridView.Name = "TransDataGridView";
			this.TransDataGridView.Size = new System.Drawing.Size(632, 163);
			this.TransDataGridView.TabIndex = 47;
			// 
			// cbExtendHr
			// 
			this.cbExtendHr.AutoSize = true;
			this.cbExtendHr.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.memberBindingSource, "ExtHour", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "false"));
			this.cbExtendHr.Location = new System.Drawing.Point(468, 133);
			this.cbExtendHr.Name = "cbExtendHr";
			this.cbExtendHr.Size = new System.Drawing.Size(102, 17);
			this.cbExtendHr.TabIndex = 45;
			this.cbExtendHr.Text = "Extended Hours";
			this.cbExtendHr.UseVisualStyleBackColor = true;
			// 
			// dataGridViewCodes
			// 
			this.dataGridViewCodes.AllowUserToAddRows = false;
			this.dataGridViewCodes.AllowUserToDeleteRows = false;
			this.dataGridViewCodes.AutoGenerateColumns = false;
			this.dataGridViewCodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
			this.dataGridViewCodes.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGridViewCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.badgeCode1DataGridViewTextBoxColumn,
            this.badgeCodeDescDataGridViewTextBoxColumn,
            this.codeValueDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn});
			this.dataGridViewCodes.DataSource = this.badgeCodeBindingSource;
			this.dataGridViewCodes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewCodes.GridColor = System.Drawing.SystemColors.Control;
			this.dataGridViewCodes.Location = new System.Drawing.Point(13, 100);
			this.dataGridViewCodes.MultiSelect = false;
			this.dataGridViewCodes.Name = "dataGridViewCodes";
			this.dataGridViewCodes.ReadOnly = true;
			this.dataGridViewCodes.RowHeadersVisible = false;
			this.dataGridViewCodes.RowHeadersWidth = 5;
			this.dataGridViewCodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewCodes.Size = new System.Drawing.Size(306, 224);
			this.dataGridViewCodes.TabIndex = 20;
			this.dataGridViewCodes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataGridViewCodes_MouseDoubleClick);
			// 
			// txtRFcard
			// 
			this.txtRFcard.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "CardNo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtRFcard.Location = new System.Drawing.Point(468, 100);
			this.txtRFcard.MaxLength = 20;
			this.txtRFcard.Name = "txtRFcard";
			this.txtRFcard.Size = new System.Drawing.Size(100, 20);
			this.txtRFcard.TabIndex = 19;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.btnSave.Location = new System.Drawing.Point(410, 596);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 42;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(366, 105);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 13);
			this.label5.TabIndex = 18;
			this.label5.Text = "RF Card Number";
			// 
			// txtLastDay
			// 
			this.txtLastDay.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "LastDayValid", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtLastDay.Location = new System.Drawing.Point(468, 41);
			this.txtLastDay.MaxLength = 20;
			this.txtLastDay.Name = "txtLastDay";
			this.txtLastDay.Size = new System.Drawing.Size(100, 20);
			this.txtLastDay.TabIndex = 17;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(347, 44);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(105, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Credit Last Day Valid";
			// 
			// cbClubDuesPaid
			// 
			this.cbClubDuesPaid.AutoSize = true;
			this.cbClubDuesPaid.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.memberBindingSource, "ClubDuesPaid", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "false"));
			this.cbClubDuesPaid.Location = new System.Drawing.Point(232, 61);
			this.cbClubDuesPaid.Name = "cbClubDuesPaid";
			this.cbClubDuesPaid.Size = new System.Drawing.Size(99, 17);
			this.cbClubDuesPaid.TabIndex = 15;
			this.cbClubDuesPaid.Text = "Club Dues Paid";
			this.cbClubDuesPaid.UseVisualStyleBackColor = true;
			// 
			// AccessTime
			// 
			this.AccessTime.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.memberBindingSource, "GroupTime", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "Members"));
			this.AccessTime.FormattingEnabled = true;
			this.AccessTime.Items.AddRange(new object[] {
            "24 Hour",
            "Members",
            "Maintenance"});
			this.AccessTime.Location = new System.Drawing.Point(468, 151);
			this.AccessTime.Name = "AccessTime";
			this.AccessTime.Size = new System.Drawing.Size(134, 173);
			this.AccessTime.TabIndex = 11;
			this.AccessTime.SelectedIndexChanged += new System.EventHandler(this.AccessTime_SelectedIndexChanged);
			// 
			// txtExemptDate
			// 
			this.txtExemptDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "ExemptModDate", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtExemptDate.Location = new System.Drawing.Point(468, 70);
			this.txtExemptDate.Name = "txtExemptDate";
			this.txtExemptDate.Size = new System.Drawing.Size(100, 20);
			this.txtExemptDate.TabIndex = 10;
			// 
			// lblExemptDate
			// 
			this.lblExemptDate.AutoSize = true;
			this.lblExemptDate.Location = new System.Drawing.Point(384, 75);
			this.lblExemptDate.Name = "lblExemptDate";
			this.lblExemptDate.Size = new System.Drawing.Size(68, 13);
			this.lblExemptDate.TabIndex = 9;
			this.lblExemptDate.Text = "Exempt Date";
			// 
			// cbExempt
			// 
			this.cbExempt.AutoSize = true;
			this.cbExempt.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.memberBindingSource, "Exempt", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "false"));
			this.cbExempt.Location = new System.Drawing.Point(577, 72);
			this.cbExempt.Name = "cbExempt";
			this.cbExempt.Size = new System.Drawing.Size(61, 17);
			this.cbExempt.TabIndex = 8;
			this.cbExempt.Text = "Exempt";
			this.cbExempt.UseVisualStyleBackColor = true;
			// 
			// txtCredits
			// 
			this.txtCredits.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "CreditBank", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "0"));
			this.txtCredits.Location = new System.Drawing.Point(468, 13);
			this.txtCredits.MaxLength = 2;
			this.txtCredits.Name = "txtCredits";
			this.txtCredits.ReadOnly = true;
			this.txtCredits.Size = new System.Drawing.Size(46, 20);
			this.txtCredits.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(375, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Monitor Credits";
			// 
			// txtClubDuesPaid
			// 
			this.txtClubDuesPaid.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "ClubDuesPaidDate", true));
			this.txtClubDuesPaid.Location = new System.Drawing.Point(125, 58);
			this.txtClubDuesPaid.Name = "txtClubDuesPaid";
			this.txtClubDuesPaid.Size = new System.Drawing.Size(100, 20);
			this.txtClubDuesPaid.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Club Dues Paid Date";
			// 
			// txtJoinDate
			// 
			this.txtJoinDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "MemberDate", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtJoinDate.Location = new System.Drawing.Point(125, 24);
			this.txtJoinDate.Name = "txtJoinDate";
			this.txtJoinDate.Size = new System.Drawing.Size(100, 20);
			this.txtJoinDate.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Member Joined Date";
			// 
			// txtEmail
			// 
			this.txtEmail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "Email", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtEmail.Location = new System.Drawing.Point(287, 131);
			this.txtEmail.MaxLength = 50;
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(156, 20);
			this.txtEmail.TabIndex = 40;
			// 
			// lblEmail
			// 
			this.lblEmail.AutoSize = true;
			this.lblEmail.Location = new System.Drawing.Point(245, 135);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(36, 13);
			this.lblEmail.TabIndex = 39;
			this.lblEmail.Text = "E-Mail";
			// 
			// txtPhone
			// 
			this.txtPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "Phone", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtPhone.Location = new System.Drawing.Point(104, 128);
			this.txtPhone.MaxLength = 15;
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.Size = new System.Drawing.Size(100, 20);
			this.txtPhone.TabIndex = 38;
			// 
			// lblPhone
			// 
			this.lblPhone.AutoSize = true;
			this.lblPhone.Location = new System.Drawing.Point(60, 131);
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.Size = new System.Drawing.Size(38, 13);
			this.lblPhone.TabIndex = 37;
			this.lblPhone.Text = "Phone";
			// 
			// txtZip
			// 
			this.txtZip.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "Zip", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "85375"));
			this.txtZip.Location = new System.Drawing.Point(287, 105);
			this.txtZip.MaxLength = 10;
			this.txtZip.Name = "txtZip";
			this.txtZip.Size = new System.Drawing.Size(100, 20);
			this.txtZip.TabIndex = 36;
			// 
			// lblZip
			// 
			this.lblZip.AutoSize = true;
			this.lblZip.Location = new System.Drawing.Point(259, 108);
			this.lblZip.Name = "lblZip";
			this.lblZip.Size = new System.Drawing.Size(22, 13);
			this.lblZip.TabIndex = 35;
			this.lblZip.Text = "Zip";
			// 
			// txtState
			// 
			this.txtState.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "State", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "AZ"));
			this.txtState.Location = new System.Drawing.Point(104, 102);
			this.txtState.MaxLength = 20;
			this.txtState.Name = "txtState";
			this.txtState.Size = new System.Drawing.Size(100, 20);
			this.txtState.TabIndex = 34;
			// 
			// lblState
			// 
			this.lblState.AutoSize = true;
			this.lblState.Location = new System.Drawing.Point(66, 105);
			this.lblState.Name = "lblState";
			this.lblState.Size = new System.Drawing.Size(32, 13);
			this.lblState.TabIndex = 33;
			this.lblState.Text = "State";
			// 
			// txtAddress
			// 
			this.txtAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "Address", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtAddress.Location = new System.Drawing.Point(104, 76);
			this.txtAddress.MaxLength = 50;
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(283, 20);
			this.txtAddress.TabIndex = 32;
			// 
			// lblAddress
			// 
			this.lblAddress.AutoSize = true;
			this.lblAddress.Location = new System.Drawing.Point(53, 79);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(45, 13);
			this.lblAddress.TabIndex = 31;
			this.lblAddress.Text = "Address";
			// 
			// txtLastNm
			// 
			this.txtLastNm.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "LastName", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtLastNm.Location = new System.Drawing.Point(287, 50);
			this.txtLastNm.Name = "txtLastNm";
			this.txtLastNm.Size = new System.Drawing.Size(100, 20);
			this.txtLastNm.TabIndex = 30;
			// 
			// lblLastNm
			// 
			this.lblLastNm.AutoSize = true;
			this.lblLastNm.Location = new System.Drawing.Point(223, 57);
			this.lblLastNm.Name = "lblLastNm";
			this.lblLastNm.Size = new System.Drawing.Size(58, 13);
			this.lblLastNm.TabIndex = 29;
			this.lblLastNm.Text = "Last Name";
			// 
			// txtFirstNm
			// 
			this.txtFirstNm.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "FirstName", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtFirstNm.Location = new System.Drawing.Point(104, 50);
			this.txtFirstNm.Name = "txtFirstNm";
			this.txtFirstNm.Size = new System.Drawing.Size(100, 20);
			this.txtFirstNm.TabIndex = 28;
			// 
			// lblFirstNm
			// 
			this.lblFirstNm.AutoSize = true;
			this.lblFirstNm.Location = new System.Drawing.Point(41, 53);
			this.lblFirstNm.Name = "lblFirstNm";
			this.lblFirstNm.Size = new System.Drawing.Size(57, 13);
			this.lblFirstNm.TabIndex = 27;
			this.lblFirstNm.Text = "First Name";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new System.Drawing.Point(506, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(184, 200);
			this.pictureBox1.TabIndex = 26;
			this.pictureBox1.TabStop = false;
			// 
			// lblPhoto
			// 
			this.lblPhoto.AutoSize = true;
			this.lblPhoto.Location = new System.Drawing.Point(464, 79);
			this.lblPhoto.Name = "lblPhoto";
			this.lblPhoto.Size = new System.Drawing.Size(35, 13);
			this.lblPhoto.TabIndex = 25;
			this.lblPhoto.Text = "Photo";
			// 
			// txtRecCard
			// 
			this.txtRecCard.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "RecCard", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtRecCard.Location = new System.Drawing.Point(287, 12);
			this.txtRecCard.MaxLength = 10;
			this.txtRecCard.Name = "txtRecCard";
			this.txtRecCard.Size = new System.Drawing.Size(100, 20);
			this.txtRecCard.TabIndex = 24;
			// 
			// lblRecCard
			// 
			this.lblRecCard.AutoSize = true;
			this.lblRecCard.Location = new System.Drawing.Point(219, 15);
			this.lblRecCard.Name = "lblRecCard";
			this.lblRecCard.Size = new System.Drawing.Size(62, 13);
			this.lblRecCard.TabIndex = 23;
			this.lblRecCard.Text = "Rec Card #";
			// 
			// txtBadge
			// 
			this.txtBadge.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "Badge", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtBadge.Location = new System.Drawing.Point(104, 12);
			this.txtBadge.MaxLength = 5;
			this.txtBadge.Name = "txtBadge";
			this.txtBadge.Size = new System.Drawing.Size(100, 20);
			this.txtBadge.TabIndex = 22;
			// 
			// lblBadge
			// 
			this.lblBadge.AutoSize = true;
			this.lblBadge.CausesValidation = false;
			this.lblBadge.Location = new System.Drawing.Point(40, 15);
			this.lblBadge.Name = "lblBadge";
			this.lblBadge.Size = new System.Drawing.Size(38, 13);
			this.lblBadge.TabIndex = 21;
			this.lblBadge.Text = "Badge";
			// 
			// cbNewBadge
			// 
			this.cbNewBadge.AutoSize = true;
			this.cbNewBadge.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.memberBindingSource, "NewBadge", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "false"));
			this.cbNewBadge.Location = new System.Drawing.Point(395, 159);
			this.cbNewBadge.Name = "cbNewBadge";
			this.cbNewBadge.Size = new System.Drawing.Size(82, 17);
			this.cbNewBadge.TabIndex = 43;
			this.cbNewBadge.Text = "New Badge";
			this.cbNewBadge.UseVisualStyleBackColor = true;
			// 
			// btnLoadPhoto
			// 
			this.btnLoadPhoto.Location = new System.Drawing.Point(396, 187);
			this.btnLoadPhoto.Name = "btnLoadPhoto";
			this.btnLoadPhoto.Size = new System.Drawing.Size(75, 23);
			this.btnLoadPhoto.TabIndex = 44;
			this.btnLoadPhoto.Text = "Load Photo";
			this.btnLoadPhoto.UseVisualStyleBackColor = true;
			this.btnLoadPhoto.Click += new System.EventHandler(this.BtnLoadPhoto_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Location = new System.Drawing.Point(71, 157);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(27, 13);
			this.lblTitle.TabIndex = 45;
			this.lblTitle.Text = "Title";
			// 
			// txtTitle
			// 
			this.txtTitle.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "Title", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtTitle.Location = new System.Drawing.Point(104, 154);
			this.txtTitle.MaxLength = 20;
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(100, 20);
			this.txtTitle.TabIndex = 46;
			// 
			// txtLocker
			// 
			this.txtLocker.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "Locker", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, ""));
			this.txtLocker.Location = new System.Drawing.Point(104, 189);
			this.txtLocker.MaxLength = 20;
			this.txtLocker.Name = "txtLocker";
			this.txtLocker.ReadOnly = true;
			this.txtLocker.Size = new System.Drawing.Size(115, 20);
			this.txtLocker.TabIndex = 47;
			// 
			// lblLocker
			// 
			this.lblLocker.AutoSize = true;
			this.lblLocker.Location = new System.Drawing.Point(58, 189);
			this.lblLocker.Name = "lblLocker";
			this.lblLocker.Size = new System.Drawing.Size(40, 13);
			this.lblLocker.TabIndex = 48;
			this.lblLocker.Text = "Locker";
			// 
			// editLocker
			// 
			this.editLocker.Location = new System.Drawing.Point(226, 187);
			this.editLocker.Name = "editLocker";
			this.editLocker.Size = new System.Drawing.Size(75, 23);
			this.editLocker.TabIndex = 49;
			this.editLocker.Text = "Edit Locker";
			this.editLocker.UseVisualStyleBackColor = true;
			this.editLocker.Click += new System.EventHandler(this.EditLocker_Click);
			// 
			// cbUpdateControllers
			// 
			this.cbUpdateControllers.AutoSize = true;
			this.cbUpdateControllers.Location = new System.Drawing.Point(244, 160);
			this.cbUpdateControllers.Name = "cbUpdateControllers";
			this.cbUpdateControllers.Size = new System.Drawing.Size(113, 17);
			this.cbUpdateControllers.TabIndex = 50;
			this.cbUpdateControllers.Text = "Update Controllers";
			this.cbUpdateControllers.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(311, 187);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 51;
			this.button1.Text = "Permissions";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.permissions_Click);
			// 
			// privateCheckBox
			// 
			this.privateCheckBox.AutoSize = true;
			this.privateCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.memberBindingSource, "Private", true));
			this.privateCheckBox.Location = new System.Drawing.Point(397, 15);
			this.privateCheckBox.Name = "privateCheckBox";
			this.privateCheckBox.Size = new System.Drawing.Size(59, 17);
			this.privateCheckBox.TabIndex = 52;
			this.privateCheckBox.Text = "Private";
			this.privateCheckBox.UseVisualStyleBackColor = true;
			// 
			// ercFirstName
			// 
			this.ercFirstName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "ERContactFirstName", true));
			this.ercFirstName.Location = new System.Drawing.Point(230, 220);
			this.ercFirstName.Margin = new System.Windows.Forms.Padding(2);
			this.ercFirstName.Name = "ercFirstName";
			this.ercFirstName.Size = new System.Drawing.Size(83, 20);
			this.ercFirstName.TabIndex = 53;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(55, 222);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(153, 13);
			this.label6.TabIndex = 54;
			this.label6.Text = "Emergency Contact First Name";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(328, 222);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 13);
			this.label7.TabIndex = 55;
			this.label7.Text = "ER Phone";
			// 
			// ercPhone
			// 
			this.ercPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.memberBindingSource, "ERContactPhone", true));
			this.ercPhone.Location = new System.Drawing.Point(396, 220);
			this.ercPhone.Margin = new System.Windows.Forms.Padding(2);
			this.ercPhone.Name = "ercPhone";
			this.ercPhone.Size = new System.Drawing.Size(88, 20);
			this.ercPhone.TabIndex = 56;
			// 
			// memberBindingSource
			// 
			this.memberBindingSource.DataSource = typeof(WoodClub.MemberRoster);
			// 
			// badgeCode1DataGridViewTextBoxColumn
			// 
			this.badgeCode1DataGridViewTextBoxColumn.DataPropertyName = "BadgeCode1";
			this.badgeCode1DataGridViewTextBoxColumn.HeaderText = "Codes";
			this.badgeCode1DataGridViewTextBoxColumn.MaxInputLength = 15;
			this.badgeCode1DataGridViewTextBoxColumn.MinimumWidth = 8;
			this.badgeCode1DataGridViewTextBoxColumn.Name = "badgeCode1DataGridViewTextBoxColumn";
			this.badgeCode1DataGridViewTextBoxColumn.ReadOnly = true;
			this.badgeCode1DataGridViewTextBoxColumn.Width = 62;
			// 
			// badgeCodeDescDataGridViewTextBoxColumn
			// 
			this.badgeCodeDescDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.badgeCodeDescDataGridViewTextBoxColumn.DataPropertyName = "BadgeCodeDesc";
			this.badgeCodeDescDataGridViewTextBoxColumn.HeaderText = "Description";
			this.badgeCodeDescDataGridViewTextBoxColumn.MaxInputLength = 25;
			this.badgeCodeDescDataGridViewTextBoxColumn.MinimumWidth = 8;
			this.badgeCodeDescDataGridViewTextBoxColumn.Name = "badgeCodeDescDataGridViewTextBoxColumn";
			this.badgeCodeDescDataGridViewTextBoxColumn.ReadOnly = true;
			this.badgeCodeDescDataGridViewTextBoxColumn.Width = 180;
			// 
			// codeValueDataGridViewTextBoxColumn
			// 
			this.codeValueDataGridViewTextBoxColumn.DataPropertyName = "CodeValue";
			this.codeValueDataGridViewTextBoxColumn.HeaderText = "Value";
			this.codeValueDataGridViewTextBoxColumn.MaxInputLength = 10;
			this.codeValueDataGridViewTextBoxColumn.MinimumWidth = 8;
			this.codeValueDataGridViewTextBoxColumn.Name = "codeValueDataGridViewTextBoxColumn";
			this.codeValueDataGridViewTextBoxColumn.ReadOnly = true;
			this.codeValueDataGridViewTextBoxColumn.Width = 59;
			// 
			// idDataGridViewTextBoxColumn
			// 
			this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
			this.idDataGridViewTextBoxColumn.HeaderText = "id";
			this.idDataGridViewTextBoxColumn.MinimumWidth = 8;
			this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
			this.idDataGridViewTextBoxColumn.ReadOnly = true;
			this.idDataGridViewTextBoxColumn.Visible = false;
			this.idDataGridViewTextBoxColumn.Width = 40;
			// 
			// badgeCodeBindingSource
			// 
			this.badgeCodeBindingSource.DataSource = typeof(WoodClub.BadgeCode);
			// 
			// MemberEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(724, 891);
			this.Controls.Add(this.ercPhone);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.ercFirstName);
			this.Controls.Add(this.privateCheckBox);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cbUpdateControllers);
			this.Controls.Add(this.editLocker);
			this.Controls.Add(this.lblLocker);
			this.Controls.Add(this.txtLocker);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.btnLoadPhoto);
			this.Controls.Add(this.cbNewBadge);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.txtEmail);
			this.Controls.Add(this.lblEmail);
			this.Controls.Add(this.txtPhone);
			this.Controls.Add(this.lblPhone);
			this.Controls.Add(this.txtZip);
			this.Controls.Add(this.lblZip);
			this.Controls.Add(this.txtState);
			this.Controls.Add(this.lblState);
			this.Controls.Add(this.txtAddress);
			this.Controls.Add(this.lblAddress);
			this.Controls.Add(this.txtLastNm);
			this.Controls.Add(this.lblLastNm);
			this.Controls.Add(this.txtFirstNm);
			this.Controls.Add(this.lblFirstNm);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.lblPhoto);
			this.Controls.Add(this.txtRecCard);
			this.Controls.Add(this.lblRecCard);
			this.Controls.Add(this.txtBadge);
			this.Controls.Add(this.lblBadge);
			this.Name = "MemberEditor";
			this.Text = "Member Edit";
			this.Load += new System.EventHandler(this.Editor_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridViewNewCredits)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TransDataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCodes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memberBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.badgeCodeBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbLumber;
        private System.Windows.Forms.CheckBox cbMaint;
        private System.Windows.Forms.ListBox AccessTime;
        private System.Windows.Forms.TextBox txtExemptDate;
        private System.Windows.Forms.Label lblExemptDate;
        private System.Windows.Forms.CheckBox cbExempt;
        private System.Windows.Forms.TextBox txtCredits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtClubDuesPaid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJoinDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.Label lblZip;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtLastNm;
        private System.Windows.Forms.Label lblLastNm;
        private System.Windows.Forms.TextBox txtFirstNm;
        private System.Windows.Forms.Label lblFirstNm;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblPhoto;
        private System.Windows.Forms.TextBox txtRecCard;
        private System.Windows.Forms.Label lblRecCard;
        private System.Windows.Forms.TextBox txtBadge;
        private System.Windows.Forms.Label lblBadge;
        private System.Windows.Forms.CheckBox cbClubDuesPaid;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtLastDay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRFcard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewCodes;
        private System.Windows.Forms.CheckBox cbNewBadge;
        private System.Windows.Forms.Button btnLoadPhoto;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.BindingSource badgeCodeBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn badgeCode1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn badgeCodeDescDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.CheckBox cbExtendHr;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtLocker;
        private System.Windows.Forms.Label lblLocker;
        private System.Windows.Forms.DataGridView TransDataGridView;
        private System.Windows.Forms.CheckBox cbMachine;
        private System.Windows.Forms.CheckBox cbAssembly;
        private System.Windows.Forms.CheckBox cbOffice;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbMain;
        private System.Windows.Forms.CheckBox cbSide;
        private System.Windows.Forms.CheckBox creditsOnlyChkbx;
        private System.Windows.Forms.DataGridView GridViewNewCredits;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.Button editLocker;
		private System.Windows.Forms.BindingSource memberBindingSource;
		private System.Windows.Forms.CheckBox cbUpdateControllers;
        private System.Windows.Forms.CheckBox checkBoxFreeDay;
        private System.Windows.Forms.CheckBox cbRecDuesPaid;
        private System.Windows.Forms.CheckBox cbAdminBlock;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox privateCheckBox;
		private System.Windows.Forms.TextBox ercFirstName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox ercPhone;
	}
}
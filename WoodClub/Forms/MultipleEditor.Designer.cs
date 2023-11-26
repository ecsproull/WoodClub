namespace WoodClub
{
    partial class MultipleEditor
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
			this.buttonApply = new System.Windows.Forms.Button();
			this.buttonClear = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.GridViewNewCredits = new System.Windows.Forms.DataGridView();
			this.dataGridViewCodesMulti = new System.Windows.Forms.DataGridView();
			this.badgeCode1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.badgeCodeDescDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.codeValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnSave = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.dataGridMultiMember = new System.Windows.Forms.DataGridView();
			this.BadgeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Credits = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastCreditAwarded = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TransactionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.memberBadgesTextBox = new System.Windows.Forms.TextBox();
			this.labelMultiBadges = new System.Windows.Forms.Label();
			this.memberBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.badgeCodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GridViewNewCredits)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCodesMulti)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridMultiMember)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memberBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.badgeCodeBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.groupBox1.Controls.Add(this.buttonApply);
			this.groupBox1.Controls.Add(this.buttonClear);
			this.groupBox1.Controls.Add(this.buttonCancel);
			this.groupBox1.Controls.Add(this.GridViewNewCredits);
			this.groupBox1.Controls.Add(this.dataGridViewCodesMulti);
			this.groupBox1.Controls.Add(this.btnSave);
			this.groupBox1.Location = new System.Drawing.Point(43, 548);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(647, 262);
			this.groupBox1.TabIndex = 41;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Club Information";
			// 
			// buttonApply
			// 
			this.buttonApply.Location = new System.Drawing.Point(559, 229);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(75, 23);
			this.buttonApply.TabIndex = 57;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
			// 
			// buttonClear
			// 
			this.buttonClear.Location = new System.Drawing.Point(563, 115);
			this.buttonClear.Name = "buttonClear";
			this.buttonClear.Size = new System.Drawing.Size(75, 23);
			this.buttonClear.TabIndex = 56;
			this.buttonClear.Text = "Clear Credits";
			this.buttonClear.UseVisualStyleBackColor = true;
			this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(478, 230);
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
			this.GridViewNewCredits.Location = new System.Drawing.Point(332, 144);
			this.GridViewNewCredits.Name = "GridViewNewCredits";
			this.GridViewNewCredits.RowHeadersVisible = false;
			this.GridViewNewCredits.Size = new System.Drawing.Size(306, 69);
			this.GridViewNewCredits.TabIndex = 54;
			// 
			// dataGridViewCodesMulti
			// 
			this.dataGridViewCodesMulti.AllowUserToAddRows = false;
			this.dataGridViewCodesMulti.AllowUserToDeleteRows = false;
			this.dataGridViewCodesMulti.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
			this.dataGridViewCodesMulti.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGridViewCodesMulti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCodesMulti.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.badgeCode1DataGridViewTextBoxColumn,
            this.badgeCodeDescDataGridViewTextBoxColumn,
            this.codeValueDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn});
			this.dataGridViewCodesMulti.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewCodesMulti.GridColor = System.Drawing.SystemColors.Control;
			this.dataGridViewCodesMulti.Location = new System.Drawing.Point(6, 19);
			this.dataGridViewCodesMulti.MultiSelect = false;
			this.dataGridViewCodesMulti.Name = "dataGridViewCodesMulti";
			this.dataGridViewCodesMulti.ReadOnly = true;
			this.dataGridViewCodesMulti.RowHeadersVisible = false;
			this.dataGridViewCodesMulti.RowHeadersWidth = 5;
			this.dataGridViewCodesMulti.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewCodesMulti.Size = new System.Drawing.Size(306, 224);
			this.dataGridViewCodesMulti.TabIndex = 20;
			this.dataGridViewCodesMulti.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataGridViewCodes_MouseDoubleClick);
			// 
			// badgeCode1DataGridViewTextBoxColumn
			// 
			this.badgeCode1DataGridViewTextBoxColumn.DataPropertyName = "BadgeCode1";
			this.badgeCode1DataGridViewTextBoxColumn.HeaderText = "Codes";
			this.badgeCode1DataGridViewTextBoxColumn.MaxInputLength = 15;
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
			this.badgeCodeDescDataGridViewTextBoxColumn.Name = "badgeCodeDescDataGridViewTextBoxColumn";
			this.badgeCodeDescDataGridViewTextBoxColumn.ReadOnly = true;
			this.badgeCodeDescDataGridViewTextBoxColumn.Width = 180;
			// 
			// codeValueDataGridViewTextBoxColumn
			// 
			this.codeValueDataGridViewTextBoxColumn.DataPropertyName = "CodeValue";
			this.codeValueDataGridViewTextBoxColumn.HeaderText = "Value";
			this.codeValueDataGridViewTextBoxColumn.MaxInputLength = 10;
			this.codeValueDataGridViewTextBoxColumn.Name = "codeValueDataGridViewTextBoxColumn";
			this.codeValueDataGridViewTextBoxColumn.ReadOnly = true;
			this.codeValueDataGridViewTextBoxColumn.Width = 59;
			// 
			// idDataGridViewTextBoxColumn
			// 
			this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
			this.idDataGridViewTextBoxColumn.HeaderText = "id";
			this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
			this.idDataGridViewTextBoxColumn.ReadOnly = true;
			this.idDataGridViewTextBoxColumn.Visible = false;
			this.idDataGridViewTextBoxColumn.Width = 40;
			// 
			// btnSave
			// 
			this.btnSave.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.btnSave.Location = new System.Drawing.Point(397, 230);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 42;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// dataGridMultiMember
			// 
			this.dataGridMultiMember.AllowUserToAddRows = false;
			this.dataGridMultiMember.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridMultiMember.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridMultiMember.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BadgeNumber,
            this.FirstName,
            this.LastName,
            this.Credits,
            this.LastCreditAwarded,
            this.TransactionDate});
			this.dataGridMultiMember.Location = new System.Drawing.Point(43, 60);
			this.dataGridMultiMember.Name = "dataGridMultiMember";
			this.dataGridMultiMember.ReadOnly = true;
			this.dataGridMultiMember.Size = new System.Drawing.Size(647, 465);
			this.dataGridMultiMember.TabIndex = 42;
			// 
			// BadgeNumber
			// 
			this.BadgeNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.BadgeNumber.DataPropertyName = "Badge";
			this.BadgeNumber.HeaderText = "Badge";
			this.BadgeNumber.Name = "BadgeNumber";
			this.BadgeNumber.ReadOnly = true;
			this.BadgeNumber.Width = 50;
			// 
			// FirstName
			// 
			this.FirstName.DataPropertyName = "FirstName";
			this.FirstName.HeaderText = "First Name";
			this.FirstName.Name = "FirstName";
			this.FirstName.ReadOnly = true;
			// 
			// LastName
			// 
			this.LastName.DataPropertyName = "LastName";
			this.LastName.HeaderText = "Last Name";
			this.LastName.Name = "LastName";
			this.LastName.ReadOnly = true;
			// 
			// Credits
			// 
			this.Credits.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Credits.DataPropertyName = "Credits";
			this.Credits.HeaderText = "Credits";
			this.Credits.Name = "Credits";
			this.Credits.ReadOnly = true;
			this.Credits.Width = 50;
			// 
			// LastCreditAwarded
			// 
			this.LastCreditAwarded.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.LastCreditAwarded.DataPropertyName = "LastCreditAwarded";
			this.LastCreditAwarded.HeaderText = "Last Credit Awarded";
			this.LastCreditAwarded.Name = "LastCreditAwarded";
			this.LastCreditAwarded.ReadOnly = true;
			// 
			// TransactionDate
			// 
			this.TransactionDate.DataPropertyName = "TransactionDate";
			this.TransactionDate.HeaderText = "Date";
			this.TransactionDate.Name = "TransactionDate";
			this.TransactionDate.ReadOnly = true;
			// 
			// memberBadgesTextBox
			// 
			this.memberBadgesTextBox.Location = new System.Drawing.Point(43, 24);
			this.memberBadgesTextBox.Name = "memberBadgesTextBox";
			this.memberBadgesTextBox.Size = new System.Drawing.Size(647, 20);
			this.memberBadgesTextBox.TabIndex = 43;
			// 
			// labelMultiBadges
			// 
			this.labelMultiBadges.AutoSize = true;
			this.labelMultiBadges.Location = new System.Drawing.Point(43, 5);
			this.labelMultiBadges.Name = "labelMultiBadges";
			this.labelMultiBadges.Size = new System.Drawing.Size(159, 13);
			this.labelMultiBadges.TabIndex = 44;
			this.labelMultiBadges.Text = "Enter Badges, Period Seperated";
			// 
			// memberBindingSource
			// 
			this.memberBindingSource.DataSource = typeof(WoodClub.MemberRoster);
			// 
			// badgeCodeBindingSource
			// 
			this.badgeCodeBindingSource.DataSource = typeof(WoodClub.BadgeCode);
			// 
			// MultipleEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(720, 836);
			this.Controls.Add(this.labelMultiBadges);
			this.Controls.Add(this.memberBadgesTextBox);
			this.Controls.Add(this.dataGridMultiMember);
			this.Controls.Add(this.groupBox1);
			this.Name = "MultipleEditor";
			this.Text = "Multiple Member Edit";
			this.Load += new System.EventHandler(this.MultipleEditor_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GridViewNewCredits)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCodesMulti)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridMultiMember)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memberBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.badgeCodeBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridViewCodesMulti;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView GridViewNewCredits;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.BindingSource memberBindingSource;
		private System.Windows.Forms.DataGridView dataGridMultiMember;
		private System.Windows.Forms.TextBox memberBadgesTextBox;
		private System.Windows.Forms.Label labelMultiBadges;
		private System.Windows.Forms.DataGridViewTextBoxColumn badgeCode1DataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn badgeCodeDescDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn codeValueDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource badgeCodeBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn BadgeNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Credits;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastCreditAwarded;
		private System.Windows.Forms.DataGridViewTextBoxColumn TransactionDate;
	}
}
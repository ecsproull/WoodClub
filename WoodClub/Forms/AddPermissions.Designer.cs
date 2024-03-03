namespace WoodClub
{
    partial class AddPermissions
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
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.dataGridMultiMember = new System.Windows.Forms.DataGridView();
			this.Delete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.MachineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.BadgeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.memberBadgesTextBox = new System.Windows.Forms.TextBox();
			this.labelMultiBadges = new System.Windows.Forms.Label();
			this.permissionComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.approverComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.applyButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.permissionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.approverBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dataGridMultiMember)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.permissionsBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.approverBindingSource)).BeginInit();
			this.SuspendLayout();
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
            this.Delete,
            this.MachineName,
            this.BadgeNumber,
            this.FirstName,
            this.LastName});
			this.dataGridMultiMember.Location = new System.Drawing.Point(12, 85);
			this.dataGridMultiMember.Name = "dataGridMultiMember";
			this.dataGridMultiMember.RowHeadersVisible = false;
			this.dataGridMultiMember.Size = new System.Drawing.Size(442, 370);
			this.dataGridMultiMember.TabIndex = 42;
			// 
			// Delete
			// 
			this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Delete.DataPropertyName = "Delete";
			this.Delete.HeaderText = "Delete";
			this.Delete.Name = "Delete";
			this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.Delete.Width = 50;
			// 
			// MachineName
			// 
			this.MachineName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.MachineName.DataPropertyName = "PermissionName";
			this.MachineName.HeaderText = "Machine";
			this.MachineName.Name = "MachineName";
			this.MachineName.Width = 60;
			// 
			// BadgeNumber
			// 
			this.BadgeNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.BadgeNumber.DataPropertyName = "Badge";
			this.BadgeNumber.HeaderText = "Badge";
			this.BadgeNumber.Name = "BadgeNumber";
			this.BadgeNumber.Width = 50;
			// 
			// FirstName
			// 
			this.FirstName.DataPropertyName = "FirstName";
			this.FirstName.HeaderText = "First Name";
			this.FirstName.Name = "FirstName";
			// 
			// LastName
			// 
			this.LastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.LastName.DataPropertyName = "LastName";
			this.LastName.HeaderText = "Last Name";
			this.LastName.Name = "LastName";
			// 
			// memberBadgesTextBox
			// 
			this.memberBadgesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memberBadgesTextBox.Location = new System.Drawing.Point(12, 59);
			this.memberBadgesTextBox.Name = "memberBadgesTextBox";
			this.memberBadgesTextBox.Size = new System.Drawing.Size(442, 20);
			this.memberBadgesTextBox.TabIndex = 43;
			// 
			// labelMultiBadges
			// 
			this.labelMultiBadges.AutoSize = true;
			this.labelMultiBadges.Location = new System.Drawing.Point(17, 40);
			this.labelMultiBadges.Name = "labelMultiBadges";
			this.labelMultiBadges.Size = new System.Drawing.Size(159, 13);
			this.labelMultiBadges.TabIndex = 44;
			this.labelMultiBadges.Text = "Enter Badges, Period Seperated";
			// 
			// permissionComboBox
			// 
			this.permissionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.permissionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.permissionComboBox.FormattingEnabled = true;
			this.permissionComboBox.Location = new System.Drawing.Point(330, 32);
			this.permissionComboBox.Name = "permissionComboBox";
			this.permissionComboBox.Size = new System.Drawing.Size(124, 21);
			this.permissionComboBox.TabIndex = 45;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(346, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 13);
			this.label1.TabIndex = 46;
			this.label1.Text = "Select Permission";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(59, 466);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 47;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// approverComboBox
			// 
			this.approverComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.approverComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.approverComboBox.FormattingEnabled = true;
			this.approverComboBox.Location = new System.Drawing.Point(188, 32);
			this.approverComboBox.Name = "approverComboBox";
			this.approverComboBox.Size = new System.Drawing.Size(126, 21);
			this.approverComboBox.TabIndex = 48;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(210, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 49;
			this.label2.Text = "Select Approver";
			// 
			// applyButton
			// 
			this.applyButton.Location = new System.Drawing.Point(196, 466);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(75, 23);
			this.applyButton.TabIndex = 50;
			this.applyButton.Text = "Apply";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(333, 466);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 51;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// AddPermissions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(466, 501);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.approverComboBox);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.permissionComboBox);
			this.Controls.Add(this.labelMultiBadges);
			this.Controls.Add(this.memberBadgesTextBox);
			this.Controls.Add(this.dataGridMultiMember);
			this.Name = "AddPermissions";
			this.Text = "Multiple Member Edit";
			this.Load += new System.EventHandler(this.MultipleEditor_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridMultiMember)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.permissionsBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.approverBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.DataGridView dataGridMultiMember;
		private System.Windows.Forms.TextBox memberBadgesTextBox;
		private System.Windows.Forms.Label labelMultiBadges;
		private System.Windows.Forms.ComboBox permissionComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ComboBox approverComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.BindingSource permissionsBindingSource;
		private System.Windows.Forms.BindingSource approverBindingSource;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Delete;
		private System.Windows.Forms.DataGridViewTextBoxColumn MachineName;
		private System.Windows.Forms.DataGridViewTextBoxColumn BadgeNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
	}
}
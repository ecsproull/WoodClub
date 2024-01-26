namespace WoodClub.Forms
{
	partial class EditMachinePermissions
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
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.machPermsDataGridView = new System.Windows.Forms.DataGridView();
			this.Badge = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Machine = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ApprovedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Blocked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Delete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.addPermButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.machPermsDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// machPermsDataGridView
			// 
			this.machPermsDataGridView.AllowUserToAddRows = false;
			this.machPermsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.machPermsDataGridView.AutoGenerateColumns = false;
			this.machPermsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.machPermsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Badge,
            this.FirstName,
            this.LastName,
            this.Machine,
            this.ApprovedBy,
            this.Blocked,
            this.Delete});
			this.machPermsDataGridView.DataSource = this.bindingSource1;
			this.machPermsDataGridView.Location = new System.Drawing.Point(-2, 45);
			this.machPermsDataGridView.Name = "machPermsDataGridView";
			this.machPermsDataGridView.RowHeadersVisible = false;
			this.machPermsDataGridView.Size = new System.Drawing.Size(579, 360);
			this.machPermsDataGridView.TabIndex = 0;
			// 
			// Badge
			// 
			this.Badge.DataPropertyName = "Badge";
			this.Badge.HeaderText = "Badge";
			this.Badge.Name = "Badge";
			// 
			// FirstName
			// 
			this.FirstName.DataPropertyName = "FirstName";
			this.FirstName.HeaderText = "First Name";
			this.FirstName.Name = "FirstName";
			// 
			// LastName
			// 
			this.LastName.DataPropertyName = "LastName";
			this.LastName.HeaderText = "Last Name";
			this.LastName.Name = "LastName";
			// 
			// Machine
			// 
			this.Machine.DataPropertyName = "PermissionName";
			this.Machine.HeaderText = "Machine";
			this.Machine.Name = "Machine";
			// 
			// ApprovedBy
			// 
			this.ApprovedBy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ApprovedBy.DataPropertyName = "ApprovedBy";
			this.ApprovedBy.HeaderText = "Approver";
			this.ApprovedBy.Name = "ApprovedBy";
			// 
			// Blocked
			// 
			this.Blocked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Blocked.DataPropertyName = "Blocked";
			this.Blocked.HeaderText = "Block";
			this.Blocked.Name = "Blocked";
			this.Blocked.Width = 40;
			// 
			// Delete
			// 
			this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Delete.DataPropertyName = "Delete";
			this.Delete.HeaderText = "Delete";
			this.Delete.Name = "Delete";
			this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.Delete.Width = 40;
			// 
			// addPermButton
			// 
			this.addPermButton.Location = new System.Drawing.Point(17, 11);
			this.addPermButton.Name = "addPermButton";
			this.addPermButton.Size = new System.Drawing.Size(75, 23);
			this.addPermButton.TabIndex = 1;
			this.addPermButton.Text = "Add Perm";
			this.addPermButton.UseVisualStyleBackColor = true;
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(128, 415);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 2;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(373, 415);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// EditMachinePermissions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(576, 450);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.addPermButton);
			this.Controls.Add(this.machPermsDataGridView);
			this.Name = "EditMachinePermissions";
			this.Text = "EditMachinePermissions";
			this.Load += new System.EventHandler(this.EditMachinePermissions_Load);
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.machPermsDataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.BindingSource bindingSource1;
		private System.Windows.Forms.DataGridView machPermsDataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn Badge;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Machine;
		private System.Windows.Forms.DataGridViewTextBoxColumn ApprovedBy;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Blocked;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Delete;
		private System.Windows.Forms.Button addPermButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
	}
}
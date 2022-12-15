namespace WoodClub.Forms
{
	partial class LockerData
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
			this.dataGridLockerData = new System.Windows.Forms.DataGridView();
			this.bs_Lockers = new System.Windows.Forms.BindingSource(this.components);
			this.bs_LockerLocationSelect = new System.Windows.Forms.BindingSource(this.components);
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonApply = new System.Windows.Forms.Button();
			this.lockerTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LocationSelect = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.DeleteCheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridLockerData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_Lockers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_LockerLocationSelect)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridLockerData
			// 
			this.dataGridLockerData.AllowUserToDeleteRows = false;
			this.dataGridLockerData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridLockerData.AutoGenerateColumns = false;
			this.dataGridLockerData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridLockerData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lockerTitle,
            this.Code,
            this.LocationSelect,
            this.DeleteCheckBox});
			this.dataGridLockerData.DataSource = this.bs_Lockers;
			this.dataGridLockerData.Location = new System.Drawing.Point(12, 12);
			this.dataGridLockerData.Name = "dataGridLockerData";
			this.dataGridLockerData.RowHeadersVisible = false;
			this.dataGridLockerData.Size = new System.Drawing.Size(527, 379);
			this.dataGridLockerData.TabIndex = 0;
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Location = new System.Drawing.Point(302, 411);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 1;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Location = new System.Drawing.Point(383, 411);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonApply
			// 
			this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonApply.Location = new System.Drawing.Point(464, 411);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(75, 23);
			this.buttonApply.TabIndex = 3;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
			// 
			// lockerTitle
			// 
			this.lockerTitle.DataPropertyName = "LockerTitle";
			this.lockerTitle.HeaderText = "Locker Title";
			this.lockerTitle.Name = "lockerTitle";
			// 
			// Code
			// 
			this.Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Code.DataPropertyName = "Code";
			this.Code.HeaderText = "Code";
			this.Code.Name = "Code";
			this.Code.Width = 50;
			// 
			// LocationSelect
			// 
			this.LocationSelect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.LocationSelect.DataPropertyName = "LocationCode";
			this.LocationSelect.DataSource = this.bs_LockerLocationSelect;
			this.LocationSelect.DropDownWidth = 150;
			this.LocationSelect.HeaderText = "Location";
			this.LocationSelect.Name = "LocationSelect";
			this.LocationSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.LocationSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// DeleteCheckBox
			// 
			this.DeleteCheckBox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.DeleteCheckBox.HeaderText = "Delete";
			this.DeleteCheckBox.Name = "DeleteCheckBox";
			this.DeleteCheckBox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.DeleteCheckBox.ToolTipText = "Delete";
			this.DeleteCheckBox.Width = 75;
			// 
			// LockerData
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(551, 450);
			this.Controls.Add(this.buttonApply);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.dataGridLockerData);
			this.Name = "LockerData";
			this.Text = "LockerData";
			this.Load += new System.EventHandler(this.LockerData_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridLockerData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_Lockers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_LockerLocationSelect)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridLockerData;
		private System.Windows.Forms.BindingSource bs_Lockers;
		private System.Windows.Forms.BindingSource bs_LockerLocationSelect;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.DataGridViewTextBoxColumn lockerTitle;
		private System.Windows.Forms.DataGridViewTextBoxColumn Code;
		private System.Windows.Forms.DataGridViewComboBoxColumn LocationSelect;
		private System.Windows.Forms.DataGridViewCheckBoxColumn DeleteCheckBox;
	}
}
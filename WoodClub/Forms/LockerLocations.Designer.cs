namespace WoodClub.Forms
{
	partial class LockerLocations
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
			this.dataGridLockerLocationEdit = new System.Windows.Forms.DataGridView();
			this.bs_LockerLocation = new System.Windows.Forms.BindingSource(this.components);
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.LocationKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridLockerLocationEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_LockerLocation)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridLockerLocationEdit
			// 
			this.dataGridLockerLocationEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridLockerLocationEdit.AutoGenerateColumns = false;
			this.dataGridLockerLocationEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridLockerLocationEdit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LocationKey,
            this.Description});
			this.dataGridLockerLocationEdit.DataSource = this.bs_LockerLocation;
			this.dataGridLockerLocationEdit.Location = new System.Drawing.Point(12, 12);
			this.dataGridLockerLocationEdit.Name = "dataGridLockerLocationEdit";
			this.dataGridLockerLocationEdit.Size = new System.Drawing.Size(463, 341);
			this.dataGridLockerLocationEdit.TabIndex = 0;
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(308, 373);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 1;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(395, 373);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// LocationKey
			// 
			this.LocationKey.DataPropertyName = "Location";
			this.LocationKey.HeaderText = "Location";
			this.LocationKey.Name = "LocationKey";
			// 
			// Description
			// 
			this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Description.DataPropertyName = "Description";
			this.Description.HeaderText = "Description";
			this.Description.Name = "Description";
			// 
			// LockerLocations
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 418);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.dataGridLockerLocationEdit);
			this.Name = "LockerLocations";
			this.Text = "LockerLocations";
			this.Load += new System.EventHandler(this.LockerLocations_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridLockerLocationEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_LockerLocation)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridLockerLocationEdit;
		private System.Windows.Forms.BindingSource bs_LockerLocation;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.DataGridViewTextBoxColumn LocationKey;
		private System.Windows.Forms.DataGridViewTextBoxColumn Description;
	}
}
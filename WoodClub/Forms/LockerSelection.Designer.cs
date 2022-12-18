namespace WoodClub.Forms
{
	partial class LockerSelection
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
			context.Dispose();
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
			this.dataGridViewSelectedLockers = new System.Windows.Forms.DataGridView();
			this.bs_SelectedLockers = new System.Windows.Forms.BindingSource(this.components);
			this.dataGridViewAllLockers = new System.Windows.Forms.DataGridView();
			this.bs_AllLockers = new System.Windows.Forms.BindingSource(this.components);
			this.buttonApply = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.SelectedAll = new System.Windows.Forms.DataGridViewButtonColumn();
			this.LockerAll = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.WhereAll = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.BadgeAll = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FirstNameAll = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastNameAll = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Selected = new System.Windows.Forms.DataGridViewButtonColumn();
			this.LockerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Where = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Badge1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectedLockers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_SelectedLockers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllLockers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_AllLockers)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewSelectedLockers
			// 
			this.dataGridViewSelectedLockers.AllowUserToAddRows = false;
			this.dataGridViewSelectedLockers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewSelectedLockers.AutoGenerateColumns = false;
			this.dataGridViewSelectedLockers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridViewSelectedLockers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewSelectedLockers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.LockerName,
            this.Where,
            this.Badge1,
            this.FirstName,
            this.LastName});
			this.dataGridViewSelectedLockers.DataSource = this.bs_SelectedLockers;
			this.dataGridViewSelectedLockers.Location = new System.Drawing.Point(12, 12);
			this.dataGridViewSelectedLockers.Name = "dataGridViewSelectedLockers";
			this.dataGridViewSelectedLockers.RowHeadersVisible = false;
			this.dataGridViewSelectedLockers.Size = new System.Drawing.Size(778, 109);
			this.dataGridViewSelectedLockers.TabIndex = 0;
			// 
			// dataGridViewAllLockers
			// 
			this.dataGridViewAllLockers.AllowUserToAddRows = false;
			this.dataGridViewAllLockers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewAllLockers.AutoGenerateColumns = false;
			this.dataGridViewAllLockers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridViewAllLockers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewAllLockers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedAll,
            this.LockerAll,
            this.WhereAll,
            this.BadgeAll,
            this.FirstNameAll,
            this.LastNameAll});
			this.dataGridViewAllLockers.DataSource = this.bs_AllLockers;
			this.dataGridViewAllLockers.Location = new System.Drawing.Point(12, 162);
			this.dataGridViewAllLockers.Name = "dataGridViewAllLockers";
			this.dataGridViewAllLockers.RowHeadersVisible = false;
			this.dataGridViewAllLockers.Size = new System.Drawing.Size(778, 323);
			this.dataGridViewAllLockers.TabIndex = 1;
			// 
			// buttonApply
			// 
			this.buttonApply.Location = new System.Drawing.Point(710, 130);
			this.buttonApply.Name = "buttonApply";
			this.buttonApply.Size = new System.Drawing.Size(75, 23);
			this.buttonApply.TabIndex = 2;
			this.buttonApply.Text = "Apply";
			this.buttonApply.UseVisualStyleBackColor = true;
			this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(622, 130);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(535, 130);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 4;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// SelectedAll
			// 
			this.SelectedAll.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.SelectedAll.DataPropertyName = "Selected";
			this.SelectedAll.HeaderText = "";
			this.SelectedAll.Name = "SelectedAll";
			this.SelectedAll.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.SelectedAll.Width = 50;
			// 
			// LockerAll
			// 
			this.LockerAll.DataPropertyName = "Locker";
			this.LockerAll.HeaderText = "Locker";
			this.LockerAll.Name = "LockerAll";
			this.LockerAll.ReadOnly = true;
			// 
			// WhereAll
			// 
			this.WhereAll.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.WhereAll.DataPropertyName = "Location";
			this.WhereAll.HeaderText = "Location";
			this.WhereAll.Name = "WhereAll";
			this.WhereAll.ReadOnly = true;
			this.WhereAll.Width = 180;
			// 
			// BadgeAll
			// 
			this.BadgeAll.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.BadgeAll.DataPropertyName = "Badge";
			this.BadgeAll.HeaderText = "Badge";
			this.BadgeAll.Name = "BadgeAll";
			this.BadgeAll.ReadOnly = true;
			// 
			// FirstNameAll
			// 
			this.FirstNameAll.DataPropertyName = "FirstName";
			this.FirstNameAll.HeaderText = "First Name";
			this.FirstNameAll.Name = "FirstNameAll";
			this.FirstNameAll.ReadOnly = true;
			// 
			// LastNameAll
			// 
			this.LastNameAll.DataPropertyName = "LastName";
			this.LastNameAll.HeaderText = "Last Name";
			this.LastNameAll.Name = "LastNameAll";
			this.LastNameAll.ReadOnly = true;
			// 
			// Selected
			// 
			this.Selected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Selected.DataPropertyName = "Selected";
			this.Selected.FillWeight = 25F;
			this.Selected.HeaderText = "";
			this.Selected.Name = "Selected";
			this.Selected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Selected.Width = 50;
			// 
			// LockerName
			// 
			this.LockerName.DataPropertyName = "Locker";
			this.LockerName.FillWeight = 4.59863F;
			this.LockerName.HeaderText = "Locker";
			this.LockerName.Name = "LockerName";
			this.LockerName.ReadOnly = true;
			// 
			// Where
			// 
			this.Where.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Where.DataPropertyName = "Location";
			this.Where.FillWeight = 124.8584F;
			this.Where.HeaderText = "Location";
			this.Where.Name = "Where";
			this.Where.ReadOnly = true;
			this.Where.Width = 180;
			// 
			// Badge1
			// 
			this.Badge1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Badge1.DataPropertyName = "Badge";
			this.Badge1.FillWeight = 5.866851F;
			this.Badge1.HeaderText = "Badge";
			this.Badge1.Name = "Badge1";
			this.Badge1.ReadOnly = true;
			// 
			// FirstName
			// 
			this.FirstName.DataPropertyName = "FirstName";
			this.FirstName.FillWeight = 4.59863F;
			this.FirstName.HeaderText = "First Name";
			this.FirstName.Name = "FirstName";
			this.FirstName.ReadOnly = true;
			// 
			// LastName
			// 
			this.LastName.DataPropertyName = "LastName";
			this.LastName.FillWeight = 4.59863F;
			this.LastName.HeaderText = "Last Name";
			this.LastName.Name = "LastName";
			this.LastName.ReadOnly = true;
			// 
			// LockerSelection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(802, 497);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonApply);
			this.Controls.Add(this.dataGridViewAllLockers);
			this.Controls.Add(this.dataGridViewSelectedLockers);
			this.Name = "LockerSelection";
			this.Text = "LockerSelection";
			this.Load += new System.EventHandler(this.LockerSelection_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectedLockers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_SelectedLockers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllLockers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_AllLockers)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewSelectedLockers;
		private System.Windows.Forms.DataGridView dataGridViewAllLockers;
		private System.Windows.Forms.BindingSource bs_SelectedLockers;
		private System.Windows.Forms.BindingSource bs_AllLockers;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.DataGridViewButtonColumn Selected;
		private System.Windows.Forms.DataGridViewTextBoxColumn LockerName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Where;
		private System.Windows.Forms.DataGridViewTextBoxColumn Badge1;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
		private System.Windows.Forms.DataGridViewButtonColumn SelectedAll;
		private System.Windows.Forms.DataGridViewTextBoxColumn LockerAll;
		private System.Windows.Forms.DataGridViewTextBoxColumn WhereAll;
		private System.Windows.Forms.DataGridViewTextBoxColumn BadgeAll;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstNameAll;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastNameAll;
	}
}
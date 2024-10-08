﻿namespace WoodClub
{
	partial class LockerRpt
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
			this.dataGridViewLockers = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
			this.PrintTag = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxLocker = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Locate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Project = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bindingSourceLocker = new System.Windows.Forms.BindingSource(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxTotalRevenue = new System.Windows.Forms.TextBox();
			this.labelLockerFilter = new System.Windows.Forms.Label();
			this.textBoxLockerFilter = new System.Windows.Forms.TextBox();
			this.printLockerReport = new System.Drawing.Printing.PrintDocument();
			this.buttonPrint = new System.Windows.Forms.Button();
			this.printDialogLockers = new System.Windows.Forms.PrintDialog();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.button2 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownMinVisits = new System.Windows.Forms.NumericUpDown();
			this.buttonEmailSlackers = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewLockers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceLocker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinVisits)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewLockers
			// 
			this.dataGridViewLockers.AllowUserToAddRows = false;
			this.dataGridViewLockers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewLockers.AutoGenerateColumns = false;
			this.dataGridViewLockers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewLockers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.PrintTag,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Email,
            this.Phone,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxLocker,
            this.Cost,
            this.Locate,
            this.Project});
			this.dataGridViewLockers.DataSource = this.bindingSourceLocker;
			this.dataGridViewLockers.Location = new System.Drawing.Point(12, 51);
			this.dataGridViewLockers.Name = "dataGridViewLockers";
			this.dataGridViewLockers.RowHeadersVisible = false;
			this.dataGridViewLockers.RowHeadersWidth = 62;
			this.dataGridViewLockers.Size = new System.Drawing.Size(1398, 564);
			this.dataGridViewLockers.TabIndex = 1;
			this.dataGridViewLockers.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLockers_CellContentDoubleClick);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "Badge";
			this.dataGridViewTextBoxColumn1.HeaderText = "Badge";
			this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dataGridViewTextBoxColumn1.Text = "123456";
			this.dataGridViewTextBoxColumn1.Width = 65;
			// 
			// PrintTag
			// 
			this.PrintTag.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.PrintTag.DataPropertyName = "PrintTag";
			this.PrintTag.HeaderText = "Tag";
			this.PrintTag.MinimumWidth = 8;
			this.PrintTag.Name = "PrintTag";
			this.PrintTag.Width = 35;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "FirstName";
			this.dataGridViewTextBoxColumn2.HeaderText = "First Name";
			this.dataGridViewTextBoxColumn2.MaxInputLength = 20;
			this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = 150;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "LastName";
			this.dataGridViewTextBoxColumn3.HeaderText = "Last Name";
			this.dataGridViewTextBoxColumn3.MaxInputLength = 20;
			this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Width = 150;
			// 
			// Email
			// 
			this.Email.DataPropertyName = "Email";
			this.Email.HeaderText = "Email";
			this.Email.MinimumWidth = 8;
			this.Email.Name = "Email";
			this.Email.Width = 200;
			// 
			// Phone
			// 
			this.Phone.DataPropertyName = "Phone";
			this.Phone.HeaderText = "Phone";
			this.Phone.MinimumWidth = 8;
			this.Phone.Name = "Phone";
			this.Phone.Width = 150;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "ClubDuesPaid";
			this.dataGridViewCheckBoxColumn1.HeaderText = "Dues";
			this.dataGridViewCheckBoxColumn1.MinimumWidth = 8;
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			this.dataGridViewCheckBoxColumn1.Width = 50;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "CreditBank";
			this.dataGridViewTextBoxColumn4.HeaderText = "Credit Bank";
			this.dataGridViewTextBoxColumn4.MaxInputLength = 20;
			this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Width = 150;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "LastDayValid";
			this.dataGridViewTextBoxColumn5.HeaderText = "Last Day Valid";
			this.dataGridViewTextBoxColumn5.MaxInputLength = 20;
			this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Width = 150;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "ShopVisits";
			this.dataGridViewTextBoxColumn6.HeaderText = "Shop Visits";
			this.dataGridViewTextBoxColumn6.MaxInputLength = 10;
			this.dataGridViewTextBoxColumn6.MinimumWidth = 8;
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn6.Width = 150;
			// 
			// dataGridViewTextBoxLocker
			// 
			this.dataGridViewTextBoxLocker.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxLocker.DataPropertyName = "HasLocker";
			this.dataGridViewTextBoxLocker.HeaderText = "Locker";
			this.dataGridViewTextBoxLocker.MaxInputLength = 20;
			this.dataGridViewTextBoxLocker.MinimumWidth = 8;
			this.dataGridViewTextBoxLocker.Name = "dataGridViewTextBoxLocker";
			this.dataGridViewTextBoxLocker.ReadOnly = true;
			this.dataGridViewTextBoxLocker.Width = 75;
			// 
			// Cost
			// 
			this.Cost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Cost.DataPropertyName = "Cost";
			this.Cost.HeaderText = "Cost";
			this.Cost.MinimumWidth = 8;
			this.Cost.Name = "Cost";
			this.Cost.Width = 50;
			// 
			// Locate
			// 
			this.Locate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Locate.DataPropertyName = "Location";
			this.Locate.HeaderText = "Location";
			this.Locate.MinimumWidth = 8;
			this.Locate.Name = "Locate";
			// 
			// Project
			// 
			this.Project.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Project.DataPropertyName = "Project";
			this.Project.HeaderText = "Project";
			this.Project.MinimumWidth = 8;
			this.Project.Name = "Project";
			this.Project.Width = 120;
			// 
			// bindingSourceLocker
			// 
			this.bindingSourceLocker.DataSource = typeof(WoodClub.Lockers);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Open Excel";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(1177, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Total Locker Revenue";
			// 
			// textBoxTotalRevenue
			// 
			this.textBoxTotalRevenue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxTotalRevenue.Location = new System.Drawing.Point(1298, 10);
			this.textBoxTotalRevenue.Name = "textBoxTotalRevenue";
			this.textBoxTotalRevenue.Size = new System.Drawing.Size(72, 20);
			this.textBoxTotalRevenue.TabIndex = 4;
			// 
			// labelLockerFilter
			// 
			this.labelLockerFilter.AutoSize = true;
			this.labelLockerFilter.Location = new System.Drawing.Point(459, 9);
			this.labelLockerFilter.Name = "labelLockerFilter";
			this.labelLockerFilter.Size = new System.Drawing.Size(134, 13);
			this.labelLockerFilter.TabIndex = 5;
			this.labelLockerFilter.Text = "Filter Badge/Name/Locker";
			// 
			// textBoxLockerFilter
			// 
			this.textBoxLockerFilter.Location = new System.Drawing.Point(607, 6);
			this.textBoxLockerFilter.Name = "textBoxLockerFilter";
			this.textBoxLockerFilter.Size = new System.Drawing.Size(112, 20);
			this.textBoxLockerFilter.TabIndex = 6;
			// 
			// buttonPrint
			// 
			this.buttonPrint.Location = new System.Drawing.Point(110, 3);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(75, 23);
			this.buttonPrint.TabIndex = 7;
			this.buttonPrint.Text = "Print";
			this.buttonPrint.UseVisualStyleBackColor = true;
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
			// 
			// printDialogLockers
			// 
			this.printDialogLockers.UseEXDialog = true;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "All",
            "Assigned",
            "Open",
            "Training",
            "Special Project"});
			this.comboBox1.Location = new System.Drawing.Point(306, 4);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 21);
			this.comboBox1.TabIndex = 8;
			this.comboBox1.Text = "All";
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(208, 2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 9;
			this.button2.Text = "Print Tags";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(752, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Min Visits";
			// 
			// numericUpDownMinVisits
			// 
			this.numericUpDownMinVisits.Location = new System.Drawing.Point(810, 5);
			this.numericUpDownMinVisits.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
			this.numericUpDownMinVisits.Name = "numericUpDownMinVisits";
			this.numericUpDownMinVisits.Size = new System.Drawing.Size(47, 20);
			this.numericUpDownMinVisits.TabIndex = 11;
			this.numericUpDownMinVisits.ValueChanged += new System.EventHandler(this.numericUpDownMinVisits_ValueChanged);
			// 
			// buttonEmailSlackers
			// 
			this.buttonEmailSlackers.Enabled = false;
			this.buttonEmailSlackers.Location = new System.Drawing.Point(877, 6);
			this.buttonEmailSlackers.Name = "buttonEmailSlackers";
			this.buttonEmailSlackers.Size = new System.Drawing.Size(88, 23);
			this.buttonEmailSlackers.TabIndex = 12;
			this.buttonEmailSlackers.Text = "Email Slackers";
			this.buttonEmailSlackers.UseVisualStyleBackColor = true;
			this.buttonEmailSlackers.Click += new System.EventHandler(this.buttonEmailSlackers_Click);
			// 
			// LockerRpt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1422, 619);
			this.Controls.Add(this.buttonEmailSlackers);
			this.Controls.Add(this.numericUpDownMinVisits);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.buttonPrint);
			this.Controls.Add(this.textBoxLockerFilter);
			this.Controls.Add(this.labelLockerFilter);
			this.Controls.Add(this.textBoxTotalRevenue);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dataGridViewLockers);
			this.Name = "LockerRpt";
			this.Text = "LockerRpt";
			this.Load += new System.EventHandler(this.LockerForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewLockers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceLocker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinVisits)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.DataGridView dataGridViewLockers;
		private System.Windows.Forms.BindingSource bindingSourceLocker;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxTotalRevenue;
		private System.Windows.Forms.Label labelLockerFilter;
		private System.Windows.Forms.TextBox textBoxLockerFilter;
		private System.Drawing.Printing.PrintDocument printLockerReport;
		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.PrintDialog printDialogLockers;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.DataGridViewButtonColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn PrintTag;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Email;
		private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxLocker;
		private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
		private System.Windows.Forms.DataGridViewTextBoxColumn Locate;
		private System.Windows.Forms.DataGridViewTextBoxColumn Project;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownMinVisits;
		private System.Windows.Forms.Button buttonEmailSlackers;
	}
}
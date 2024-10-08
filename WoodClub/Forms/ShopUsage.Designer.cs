﻿namespace WoodClub
{
    partial class ShopUsage
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
			this.dataGridViewUsage = new System.Windows.Forms.DataGridView();
			this.ShopVisits = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CreditBank = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.memberRosterBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.btnRefresh = new System.Windows.Forms.Button();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.txtTotal = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.badgeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lastDayValidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.usageBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memberRosterBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.usageBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewUsage
			// 
			this.dataGridViewUsage.AutoGenerateColumns = false;
			this.dataGridViewUsage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewUsage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.badgeDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.ShopVisits,
            this.CreditBank,
            this.lastDayValidDataGridViewTextBoxColumn});
			this.dataGridViewUsage.DataSource = this.usageBindingSource;
			this.dataGridViewUsage.Location = new System.Drawing.Point(0, 68);
			this.dataGridViewUsage.Name = "dataGridViewUsage";
			this.dataGridViewUsage.RowHeadersWidth = 62;
			this.dataGridViewUsage.Size = new System.Drawing.Size(648, 482);
			this.dataGridViewUsage.TabIndex = 0;
			// 
			// ShopVisits
			// 
			this.ShopVisits.DataPropertyName = "ShopVisits";
			this.ShopVisits.HeaderText = "ShopVisits";
			this.ShopVisits.MinimumWidth = 8;
			this.ShopVisits.Name = "ShopVisits";
			this.ShopVisits.Width = 150;
			// 
			// CreditBank
			// 
			this.CreditBank.DataPropertyName = "CreditBank";
			this.CreditBank.HeaderText = "CreditBank";
			this.CreditBank.MinimumWidth = 8;
			this.CreditBank.Name = "CreditBank";
			this.CreditBank.Width = 150;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(104, 18);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 1;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.CustomFormat = "yyyy MM";
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(199, 20);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.ShowUpDown = true;
			this.dateTimePicker1.Size = new System.Drawing.Size(61, 20);
			this.dateTimePicker1.TabIndex = 2;
			this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(362, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Months Total Visits";
			// 
			// txtTotal
			// 
			this.txtTotal.Location = new System.Drawing.Point(464, 18);
			this.txtTotal.Name = "txtTotal";
			this.txtTotal.Size = new System.Drawing.Size(41, 20);
			this.txtTotal.TabIndex = 4;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 18);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "Save";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// badgeDataGridViewTextBoxColumn
			// 
			this.badgeDataGridViewTextBoxColumn.DataPropertyName = "Badge";
			this.badgeDataGridViewTextBoxColumn.HeaderText = "Badge";
			this.badgeDataGridViewTextBoxColumn.MinimumWidth = 8;
			this.badgeDataGridViewTextBoxColumn.Name = "badgeDataGridViewTextBoxColumn";
			this.badgeDataGridViewTextBoxColumn.Width = 150;
			// 
			// firstNameDataGridViewTextBoxColumn
			// 
			this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
			this.firstNameDataGridViewTextBoxColumn.HeaderText = "FirstName";
			this.firstNameDataGridViewTextBoxColumn.MinimumWidth = 8;
			this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
			this.firstNameDataGridViewTextBoxColumn.Width = 150;
			// 
			// lastNameDataGridViewTextBoxColumn
			// 
			this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
			this.lastNameDataGridViewTextBoxColumn.HeaderText = "LastName";
			this.lastNameDataGridViewTextBoxColumn.MinimumWidth = 8;
			this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
			this.lastNameDataGridViewTextBoxColumn.Width = 150;
			// 
			// lastDayValidDataGridViewTextBoxColumn
			// 
			this.lastDayValidDataGridViewTextBoxColumn.DataPropertyName = "LastDayValid";
			this.lastDayValidDataGridViewTextBoxColumn.HeaderText = "LastDayValid";
			this.lastDayValidDataGridViewTextBoxColumn.MinimumWidth = 8;
			this.lastDayValidDataGridViewTextBoxColumn.Name = "lastDayValidDataGridViewTextBoxColumn";
			this.lastDayValidDataGridViewTextBoxColumn.Width = 150;
			// 
			// usageBindingSource
			// 
			this.usageBindingSource.DataSource = typeof(WoodClub.Usage);
			// 
			// ShopUsage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(662, 551);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtTotal);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.dataGridViewUsage);
			this.Name = "ShopUsage";
			this.Text = "Monthly Usage";
			this.Load += new System.EventHandler(this.FormUsage_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memberRosterBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.usageBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewUsage;
        private System.Windows.Forms.BindingSource memberRosterBindingSource;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.BindingSource usageBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn badgeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShopVisits;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreditBank;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastDayValidDataGridViewTextBoxColumn;
    }
}
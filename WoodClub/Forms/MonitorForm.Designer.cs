namespace WoodClub
{
    partial class MonitorForm
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
            this.dataGridViewMonitor = new System.Windows.Forms.DataGridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.badgeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exemptDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clubDuesPaidDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clubDuesPaidDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creditBankDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastDayValidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creditAmtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastMonitorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shopVisitsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lockersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifiedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monitorsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMonitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewMonitor
            // 
            this.dataGridViewMonitor.AllowUserToAddRows = false;
            this.dataGridViewMonitor.AllowUserToDeleteRows = false;
            this.dataGridViewMonitor.AutoGenerateColumns = false;
            this.dataGridViewMonitor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMonitor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.badgeDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.exemptDataGridViewCheckBoxColumn,
            this.clubDuesPaidDataGridViewCheckBoxColumn,
            this.clubDuesPaidDateDataGridViewTextBoxColumn,
            this.creditBankDataGridViewTextBoxColumn,
            this.lastDayValidDataGridViewTextBoxColumn,
            this.creditAmtDataGridViewTextBoxColumn,
            this.lastMonitorDataGridViewTextBoxColumn,
            this.shopVisitsDataGridViewTextBoxColumn,
            this.lockersDataGridViewTextBoxColumn,
            this.modifiedDataGridViewTextBoxColumn});
            this.dataGridViewMonitor.DataSource = this.monitorsBindingSource;
            this.dataGridViewMonitor.Location = new System.Drawing.Point(13, 69);
            this.dataGridViewMonitor.Name = "dataGridViewMonitor";
            this.dataGridViewMonitor.ReadOnly = true;
            this.dataGridViewMonitor.Size = new System.Drawing.Size(1053, 600);
            this.dataGridViewMonitor.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(13, 29);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(94, 29);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(187, 32);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(54, 20);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // badgeDataGridViewTextBoxColumn
            // 
            this.badgeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.badgeDataGridViewTextBoxColumn.DataPropertyName = "Badge";
            this.badgeDataGridViewTextBoxColumn.HeaderText = "Badge";
            this.badgeDataGridViewTextBoxColumn.Name = "badgeDataGridViewTextBoxColumn";
            this.badgeDataGridViewTextBoxColumn.ReadOnly = true;
            this.badgeDataGridViewTextBoxColumn.Width = 63;
            // 
            // firstNameDataGridViewTextBoxColumn
            // 
            this.firstNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.HeaderText = "First Name";
            this.firstNameDataGridViewTextBoxColumn.MaxInputLength = 30;
            this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
            this.firstNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.firstNameDataGridViewTextBoxColumn.Width = 82;
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            this.lastNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            this.lastNameDataGridViewTextBoxColumn.HeaderText = "Last Name";
            this.lastNameDataGridViewTextBoxColumn.MaxInputLength = 30;
            this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            this.lastNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastNameDataGridViewTextBoxColumn.Width = 83;
            // 
            // exemptDataGridViewCheckBoxColumn
            // 
            this.exemptDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.exemptDataGridViewCheckBoxColumn.DataPropertyName = "Exempt";
            this.exemptDataGridViewCheckBoxColumn.HeaderText = "Exempt";
            this.exemptDataGridViewCheckBoxColumn.Name = "exemptDataGridViewCheckBoxColumn";
            this.exemptDataGridViewCheckBoxColumn.ReadOnly = true;
            this.exemptDataGridViewCheckBoxColumn.Width = 48;
            // 
            // clubDuesPaidDataGridViewCheckBoxColumn
            // 
            this.clubDuesPaidDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.clubDuesPaidDataGridViewCheckBoxColumn.DataPropertyName = "ClubDuesPaid";
            this.clubDuesPaidDataGridViewCheckBoxColumn.HeaderText = "ClubDuesPaid";
            this.clubDuesPaidDataGridViewCheckBoxColumn.Name = "clubDuesPaidDataGridViewCheckBoxColumn";
            this.clubDuesPaidDataGridViewCheckBoxColumn.ReadOnly = true;
            this.clubDuesPaidDataGridViewCheckBoxColumn.Width = 80;
            // 
            // clubDuesPaidDateDataGridViewTextBoxColumn
            // 
            this.clubDuesPaidDateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.clubDuesPaidDateDataGridViewTextBoxColumn.DataPropertyName = "ClubDuesPaidDate";
            this.clubDuesPaidDateDataGridViewTextBoxColumn.HeaderText = "Club Dues Paid Date";
            this.clubDuesPaidDateDataGridViewTextBoxColumn.MaxInputLength = 20;
            this.clubDuesPaidDateDataGridViewTextBoxColumn.Name = "clubDuesPaidDateDataGridViewTextBoxColumn";
            this.clubDuesPaidDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.clubDuesPaidDateDataGridViewTextBoxColumn.Width = 99;
            // 
            // creditBankDataGridViewTextBoxColumn
            // 
            this.creditBankDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.creditBankDataGridViewTextBoxColumn.DataPropertyName = "CreditBank";
            this.creditBankDataGridViewTextBoxColumn.HeaderText = "Credit Bank";
            this.creditBankDataGridViewTextBoxColumn.MaxInputLength = 10;
            this.creditBankDataGridViewTextBoxColumn.Name = "creditBankDataGridViewTextBoxColumn";
            this.creditBankDataGridViewTextBoxColumn.ReadOnly = true;
            this.creditBankDataGridViewTextBoxColumn.Width = 80;
            // 
            // lastDayValidDataGridViewTextBoxColumn
            // 
            this.lastDayValidDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.lastDayValidDataGridViewTextBoxColumn.DataPropertyName = "LastDayValid";
            this.lastDayValidDataGridViewTextBoxColumn.HeaderText = "Last Day Valid";
            this.lastDayValidDataGridViewTextBoxColumn.MaxInputLength = 20;
            this.lastDayValidDataGridViewTextBoxColumn.Name = "lastDayValidDataGridViewTextBoxColumn";
            this.lastDayValidDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastDayValidDataGridViewTextBoxColumn.ToolTipText = "Last Day Credit Used is Valid";
            this.lastDayValidDataGridViewTextBoxColumn.Width = 92;
            // 
            // creditAmtDataGridViewTextBoxColumn
            // 
            this.creditAmtDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.creditAmtDataGridViewTextBoxColumn.DataPropertyName = "CreditAmt";
            this.creditAmtDataGridViewTextBoxColumn.HeaderText = "Credit Amt";
            this.creditAmtDataGridViewTextBoxColumn.MaxInputLength = 10;
            this.creditAmtDataGridViewTextBoxColumn.Name = "creditAmtDataGridViewTextBoxColumn";
            this.creditAmtDataGridViewTextBoxColumn.ReadOnly = true;
            this.creditAmtDataGridViewTextBoxColumn.Width = 74;
            // 
            // lastMonitorDataGridViewTextBoxColumn
            // 
            this.lastMonitorDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.lastMonitorDataGridViewTextBoxColumn.DataPropertyName = "LastMonitor";
            this.lastMonitorDataGridViewTextBoxColumn.HeaderText = "Last Monitor";
            this.lastMonitorDataGridViewTextBoxColumn.MaxInputLength = 30;
            this.lastMonitorDataGridViewTextBoxColumn.Name = "lastMonitorDataGridViewTextBoxColumn";
            this.lastMonitorDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastMonitorDataGridViewTextBoxColumn.Width = 83;
            // 
            // shopVisitsDataGridViewTextBoxColumn
            // 
            this.shopVisitsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.shopVisitsDataGridViewTextBoxColumn.DataPropertyName = "ShopVisits";
            this.shopVisitsDataGridViewTextBoxColumn.HeaderText = "Shop Visits";
            this.shopVisitsDataGridViewTextBoxColumn.MaxInputLength = 10;
            this.shopVisitsDataGridViewTextBoxColumn.Name = "shopVisitsDataGridViewTextBoxColumn";
            this.shopVisitsDataGridViewTextBoxColumn.ReadOnly = true;
            this.shopVisitsDataGridViewTextBoxColumn.Width = 78;
            // 
            // lockersDataGridViewTextBoxColumn
            // 
            this.lockersDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.lockersDataGridViewTextBoxColumn.DataPropertyName = "Lockers";
            this.lockersDataGridViewTextBoxColumn.HeaderText = "Lockers";
            this.lockersDataGridViewTextBoxColumn.MaxInputLength = 10;
            this.lockersDataGridViewTextBoxColumn.Name = "lockersDataGridViewTextBoxColumn";
            this.lockersDataGridViewTextBoxColumn.ReadOnly = true;
            this.lockersDataGridViewTextBoxColumn.Width = 70;
            // 
            // modifiedDataGridViewTextBoxColumn
            // 
            this.modifiedDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.modifiedDataGridViewTextBoxColumn.DataPropertyName = "Modified";
            this.modifiedDataGridViewTextBoxColumn.HeaderText = "Modified";
            this.modifiedDataGridViewTextBoxColumn.Name = "modifiedDataGridViewTextBoxColumn";
            this.modifiedDataGridViewTextBoxColumn.ReadOnly = true;
            this.modifiedDataGridViewTextBoxColumn.Width = 72;
            // 
            // monitorsBindingSource
            // 
            this.monitorsBindingSource.DataSource = typeof(WoodClub.Monitors);
            // 
            // MonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1078, 761);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridViewMonitor);
            this.Name = "MonitorForm";
            this.Text = "MonitorForm";
            this.Load += new System.EventHandler(this.MonitorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewMonitor;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.BindingSource monitorsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn badgeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn exemptDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clubDuesPaidDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clubDuesPaidDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creditBankDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastDayValidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creditAmtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastMonitorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shopVisitsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lockersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifiedDataGridViewTextBoxColumn;
    }
}
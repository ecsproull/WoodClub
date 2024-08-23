namespace WoodClub
{
    partial class FormDaily
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
            this.dataGridViewDaily = new System.Windows.Forms.DataGridView();
            this.bindingSourceDaily = new System.Windows.Forms.BindingSource(this.components);
            this.DailydateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.btnDailyRefresh = new System.Windows.Forms.Button();
            this.txtDailyTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DailySave = new System.Windows.Forms.Button();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sixamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sevenamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eightamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nineamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.elevenamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.twelvepmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.onepmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.twopmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.threepmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fourpmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fivepmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sixpmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dailyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dailyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDaily
            // 
            this.dataGridViewDaily.AutoGenerateColumns = false;
            this.dataGridViewDaily.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDaily.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateDataGridViewTextBoxColumn,
            this.totalDayDataGridViewTextBoxColumn,
            this.sixamDataGridViewTextBoxColumn,
            this.sevenamDataGridViewTextBoxColumn,
            this.eightamDataGridViewTextBoxColumn,
            this.nineamDataGridViewTextBoxColumn,
            this.tenamDataGridViewTextBoxColumn,
            this.elevenamDataGridViewTextBoxColumn,
            this.twelvepmDataGridViewTextBoxColumn,
            this.onepmDataGridViewTextBoxColumn,
            this.twopmDataGridViewTextBoxColumn,
            this.threepmDataGridViewTextBoxColumn,
            this.fourpmDataGridViewTextBoxColumn,
            this.fivepmDataGridViewTextBoxColumn,
            this.sixpmDataGridViewTextBoxColumn});
            this.dataGridViewDaily.DataSource = this.dailyBindingSource;
            this.dataGridViewDaily.Location = new System.Drawing.Point(12, 35);
            this.dataGridViewDaily.Name = "dataGridViewDaily";
            this.dataGridViewDaily.Size = new System.Drawing.Size(1012, 569);
            this.dataGridViewDaily.TabIndex = 0;
            // 
            // DailydateTimePicker
            // 
            this.DailydateTimePicker.CustomFormat = "yyyy MM";
            this.DailydateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DailydateTimePicker.Location = new System.Drawing.Point(187, 5);
            this.DailydateTimePicker.Name = "DailydateTimePicker";
            this.DailydateTimePicker.ShowUpDown = true;
            this.DailydateTimePicker.Size = new System.Drawing.Size(61, 20);
            this.DailydateTimePicker.TabIndex = 3;
            this.DailydateTimePicker.ValueChanged += new System.EventHandler(this.DailydateTimePicker_ValueChanged);
            // 
            // btnDailyRefresh
            // 
            this.btnDailyRefresh.Location = new System.Drawing.Point(106, 5);
            this.btnDailyRefresh.Name = "btnDailyRefresh";
            this.btnDailyRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnDailyRefresh.TabIndex = 4;
            this.btnDailyRefresh.Text = "Refresh";
            this.btnDailyRefresh.UseVisualStyleBackColor = true;
            this.btnDailyRefresh.Click += new System.EventHandler(this.btnDailyRefresh_Click);
            // 
            // txtDailyTotal
            // 
            this.txtDailyTotal.Location = new System.Drawing.Point(741, 8);
            this.txtDailyTotal.Name = "txtDailyTotal";
            this.txtDailyTotal.Size = new System.Drawing.Size(41, 20);
            this.txtDailyTotal.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(639, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Months Total Visits";
            // 
            // DailySave
            // 
            this.DailySave.Location = new System.Drawing.Point(12, 5);
            this.DailySave.Name = "DailySave";
            this.DailySave.Size = new System.Drawing.Size(75, 23);
            this.DailySave.TabIndex = 7;
            this.DailySave.Text = "Save";
            this.DailySave.UseVisualStyleBackColor = true;
            this.DailySave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            // 
            // totalDayDataGridViewTextBoxColumn
            // 
            this.totalDayDataGridViewTextBoxColumn.DataPropertyName = "TotalDay";
            this.totalDayDataGridViewTextBoxColumn.HeaderText = "Total Day";
            this.totalDayDataGridViewTextBoxColumn.MaxInputLength = 10;
            this.totalDayDataGridViewTextBoxColumn.Name = "totalDayDataGridViewTextBoxColumn";
            this.totalDayDataGridViewTextBoxColumn.Width = 60;
            // 
            // sixamDataGridViewTextBoxColumn
            // 
            this.sixamDataGridViewTextBoxColumn.DataPropertyName = "six_am";
            this.sixamDataGridViewTextBoxColumn.HeaderText = "6 AM";
            this.sixamDataGridViewTextBoxColumn.Name = "sixamDataGridViewTextBoxColumn";
            this.sixamDataGridViewTextBoxColumn.Width = 60;
            // 
            // sevenamDataGridViewTextBoxColumn
            // 
            this.sevenamDataGridViewTextBoxColumn.DataPropertyName = "seven_am";
            this.sevenamDataGridViewTextBoxColumn.HeaderText = "7 AM";
            this.sevenamDataGridViewTextBoxColumn.Name = "sevenamDataGridViewTextBoxColumn";
            this.sevenamDataGridViewTextBoxColumn.Width = 60;
            // 
            // eightamDataGridViewTextBoxColumn
            // 
            this.eightamDataGridViewTextBoxColumn.DataPropertyName = "eight_am";
            this.eightamDataGridViewTextBoxColumn.HeaderText = "8 AM";
            this.eightamDataGridViewTextBoxColumn.Name = "eightamDataGridViewTextBoxColumn";
            this.eightamDataGridViewTextBoxColumn.Width = 60;
            // 
            // nineamDataGridViewTextBoxColumn
            // 
            this.nineamDataGridViewTextBoxColumn.DataPropertyName = "nine_am";
            this.nineamDataGridViewTextBoxColumn.HeaderText = "9 AM";
            this.nineamDataGridViewTextBoxColumn.Name = "nineamDataGridViewTextBoxColumn";
            this.nineamDataGridViewTextBoxColumn.Width = 60;
            // 
            // tenamDataGridViewTextBoxColumn
            // 
            this.tenamDataGridViewTextBoxColumn.DataPropertyName = "ten_am";
            this.tenamDataGridViewTextBoxColumn.HeaderText = "10 AM";
            this.tenamDataGridViewTextBoxColumn.Name = "tenamDataGridViewTextBoxColumn";
            this.tenamDataGridViewTextBoxColumn.Width = 65;
            // 
            // elevenamDataGridViewTextBoxColumn
            // 
            this.elevenamDataGridViewTextBoxColumn.DataPropertyName = "eleven_am";
            this.elevenamDataGridViewTextBoxColumn.HeaderText = "11 AM";
            this.elevenamDataGridViewTextBoxColumn.Name = "elevenamDataGridViewTextBoxColumn";
            this.elevenamDataGridViewTextBoxColumn.Width = 65;
            // 
            // twelvepmDataGridViewTextBoxColumn
            // 
            this.twelvepmDataGridViewTextBoxColumn.DataPropertyName = "twelve_pm";
            this.twelvepmDataGridViewTextBoxColumn.HeaderText = "12 PM";
            this.twelvepmDataGridViewTextBoxColumn.Name = "twelvepmDataGridViewTextBoxColumn";
            this.twelvepmDataGridViewTextBoxColumn.Width = 65;
            // 
            // onepmDataGridViewTextBoxColumn
            // 
            this.onepmDataGridViewTextBoxColumn.DataPropertyName = "one_pm";
            this.onepmDataGridViewTextBoxColumn.HeaderText = "1 PM";
            this.onepmDataGridViewTextBoxColumn.Name = "onepmDataGridViewTextBoxColumn";
            this.onepmDataGridViewTextBoxColumn.Width = 60;
            // 
            // twopmDataGridViewTextBoxColumn
            // 
            this.twopmDataGridViewTextBoxColumn.DataPropertyName = "two_pm";
            this.twopmDataGridViewTextBoxColumn.HeaderText = "2 PM";
            this.twopmDataGridViewTextBoxColumn.Name = "twopmDataGridViewTextBoxColumn";
            this.twopmDataGridViewTextBoxColumn.Width = 60;
            // 
            // threepmDataGridViewTextBoxColumn
            // 
            this.threepmDataGridViewTextBoxColumn.DataPropertyName = "three_pm";
            this.threepmDataGridViewTextBoxColumn.HeaderText = "3 PM";
            this.threepmDataGridViewTextBoxColumn.Name = "threepmDataGridViewTextBoxColumn";
            this.threepmDataGridViewTextBoxColumn.Width = 60;
            // 
            // fourpmDataGridViewTextBoxColumn
            // 
            this.fourpmDataGridViewTextBoxColumn.DataPropertyName = "four_pm";
            this.fourpmDataGridViewTextBoxColumn.HeaderText = "4 PM";
            this.fourpmDataGridViewTextBoxColumn.Name = "fourpmDataGridViewTextBoxColumn";
            this.fourpmDataGridViewTextBoxColumn.Width = 60;
            // 
            // fivepmDataGridViewTextBoxColumn
            // 
            this.fivepmDataGridViewTextBoxColumn.DataPropertyName = "five_pm";
            this.fivepmDataGridViewTextBoxColumn.HeaderText = "5 PM";
            this.fivepmDataGridViewTextBoxColumn.Name = "fivepmDataGridViewTextBoxColumn";
            this.fivepmDataGridViewTextBoxColumn.Width = 60;
            // 
            // sixpmDataGridViewTextBoxColumn
            // 
            this.sixpmDataGridViewTextBoxColumn.DataPropertyName = "six_pm";
            this.sixpmDataGridViewTextBoxColumn.HeaderText = "6 PM +";
            this.sixpmDataGridViewTextBoxColumn.Name = "sixpmDataGridViewTextBoxColumn";
            this.sixpmDataGridViewTextBoxColumn.Width = 70;
            // 
            // dailyBindingSource
            // 
            this.dailyBindingSource.DataSource = typeof(WoodClub.Daily);
            // 
            // FormDaily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 616);
            this.Controls.Add(this.DailySave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDailyTotal);
            this.Controls.Add(this.btnDailyRefresh);
            this.Controls.Add(this.DailydateTimePicker);
            this.Controls.Add(this.dataGridViewDaily);
            this.Name = "FormDaily";
            this.Text = "FormDaily";
            this.Load += new System.EventHandler(this.FormDaily_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dailyBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDaily;
        private System.Windows.Forms.BindingSource bindingSourceDaily;
        private System.Windows.Forms.BindingSource dailyBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sixamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sevenamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eightamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nineamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn elevenamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn twelvepmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn onepmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn twopmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn threepmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fourpmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fivepmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sixpmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DateTimePicker DailydateTimePicker;
        private System.Windows.Forms.Button btnDailyRefresh;
        private System.Windows.Forms.TextBox txtDailyTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DailySave;
    }
}
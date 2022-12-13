namespace WoodClub
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
			this.button1 = new System.Windows.Forms.Button();
			this.bindingSourceLocker = new System.Windows.Forms.BindingSource(this.components);
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewLockers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceLocker)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewLockers
			// 
			this.dataGridViewLockers.AutoGenerateColumns = false;
			this.dataGridViewLockers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewLockers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Email,
            this.Phone,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
			this.dataGridViewLockers.DataSource = this.bindingSourceLocker;
			this.dataGridViewLockers.Location = new System.Drawing.Point(0, 41);
			this.dataGridViewLockers.Name = "dataGridViewLockers";
			this.dataGridViewLockers.Size = new System.Drawing.Size(1243, 512);
			this.dataGridViewLockers.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Save";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// bindingSourceLocker
			// 
			this.bindingSourceLocker.DataSource = typeof(WoodClub.Lockers);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "Badge";
			this.dataGridViewTextBoxColumn1.HeaderText = "Badge";
			this.dataGridViewTextBoxColumn1.MaxInputLength = 10;
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "FirstName";
			this.dataGridViewTextBoxColumn2.HeaderText = "First Name";
			this.dataGridViewTextBoxColumn2.MaxInputLength = 20;
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "LastName";
			this.dataGridViewTextBoxColumn3.HeaderText = "Last Name";
			this.dataGridViewTextBoxColumn3.MaxInputLength = 20;
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			// 
			// Email
			// 
			this.Email.DataPropertyName = "Email";
			this.Email.HeaderText = "Email";
			this.Email.Name = "Email";
			this.Email.Width = 200;
			// 
			// Phone
			// 
			this.Phone.DataPropertyName = "Phone";
			this.Phone.HeaderText = "Phone";
			this.Phone.Name = "Phone";
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.DataPropertyName = "ClubDuesPaid";
			this.dataGridViewCheckBoxColumn1.HeaderText = "Club Dues Paid";
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "CreditBank";
			this.dataGridViewTextBoxColumn4.HeaderText = "Credit Bank";
			this.dataGridViewTextBoxColumn4.MaxInputLength = 20;
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "LastDayValid";
			this.dataGridViewTextBoxColumn5.HeaderText = "Last Day Valid";
			this.dataGridViewTextBoxColumn5.MaxInputLength = 20;
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "ShopVisits";
			this.dataGridViewTextBoxColumn6.HeaderText = "Shop Visits";
			this.dataGridViewTextBoxColumn6.MaxInputLength = 10;
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "HasLocker";
			this.dataGridViewTextBoxColumn7.HeaderText = "Has Locker";
			this.dataGridViewTextBoxColumn7.MaxInputLength = 20;
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.DataPropertyName = "Modified";
			this.dataGridViewTextBoxColumn8.HeaderText = "Modified";
			this.dataGridViewTextBoxColumn8.MaxInputLength = 20;
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.ReadOnly = true;
			// 
			// LockerRpt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1241, 565);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dataGridViewLockers);
			this.Name = "LockerRpt";
			this.Text = "LockerRpt";
			this.Load += new System.EventHandler(this.LockerForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewLockers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceLocker)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewLockers;
        private System.Windows.Forms.BindingSource bindingSourceLocker;
        private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Email;
		private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
	}
}
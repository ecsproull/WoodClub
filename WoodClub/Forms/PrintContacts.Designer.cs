namespace WoodClub
{
	partial class PrintContacts
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ContactName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ContactNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bindingSourceContacts = new System.Windows.Forms.BindingSource(this.components);
			this.openPrint = new System.Windows.Forms.Button();
			this.printDocumentContacts = new System.Drawing.Printing.PrintDocument();
			this.buttonPrint = new System.Windows.Forms.Button();
			this.printContactsDialog = new System.Windows.Forms.PrintDialog();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceContacts)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FirstName,
            this.LastName,
            this.Phone,
            this.ContactName,
            this.ContactNumber});
			this.dataGridView1.DataSource = this.bindingSourceContacts;
			this.dataGridView1.Location = new System.Drawing.Point(-1, 65);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowHeadersWidth = 62;
			this.dataGridView1.Size = new System.Drawing.Size(756, 608);
			this.dataGridView1.TabIndex = 0;
			// 
			// FirstName
			// 
			this.FirstName.DataPropertyName = "FirstName";
			this.FirstName.HeaderText = "First Name";
			this.FirstName.MinimumWidth = 8;
			this.FirstName.Name = "FirstName";
			this.FirstName.Width = 150;
			// 
			// LastName
			// 
			this.LastName.DataPropertyName = "LastName";
			this.LastName.HeaderText = "LastName";
			this.LastName.MinimumWidth = 8;
			this.LastName.Name = "LastName";
			this.LastName.Width = 150;
			// 
			// Phone
			// 
			this.Phone.DataPropertyName = "Phone";
			this.Phone.HeaderText = "Phone";
			this.Phone.MinimumWidth = 8;
			this.Phone.Name = "Phone";
			this.Phone.Width = 150;
			// 
			// ContactName
			// 
			this.ContactName.DataPropertyName = "ERContactFirstName";
			this.ContactName.HeaderText = "Contact Name";
			this.ContactName.MinimumWidth = 8;
			this.ContactName.Name = "ContactName";
			this.ContactName.Width = 150;
			// 
			// ContactNumber
			// 
			this.ContactNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ContactNumber.DataPropertyName = "ERContactPhone";
			this.ContactNumber.HeaderText = "Contact Number";
			this.ContactNumber.MinimumWidth = 8;
			this.ContactNumber.Name = "ContactNumber";
			// 
			// openPrint
			// 
			this.openPrint.Location = new System.Drawing.Point(35, 12);
			this.openPrint.Name = "openPrint";
			this.openPrint.Size = new System.Drawing.Size(82, 37);
			this.openPrint.TabIndex = 1;
			this.openPrint.Text = "Excel";
			this.openPrint.UseVisualStyleBackColor = true;
			this.openPrint.Click += new System.EventHandler(this.openPrint_Click);
			// 
			// buttonPrint
			// 
			this.buttonPrint.Location = new System.Drawing.Point(161, 12);
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.Size = new System.Drawing.Size(82, 37);
			this.buttonPrint.TabIndex = 2;
			this.buttonPrint.Text = "Print";
			this.buttonPrint.UseVisualStyleBackColor = true;
			this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click_1);
			// 
			// printContactsDialog
			// 
			this.printContactsDialog.UseEXDialog = true;
			// 
			// PrintContacts
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(755, 672);
			this.Controls.Add(this.buttonPrint);
			this.Controls.Add(this.openPrint);
			this.Controls.Add(this.dataGridView1);
			this.Name = "PrintContacts";
			this.Text = "PrintContacts";
			this.Load += new System.EventHandler(this.PrintContacts_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSourceContacts)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button openPrint;
		private System.Windows.Forms.BindingSource bindingSourceContacts;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
		private System.Windows.Forms.DataGridViewTextBoxColumn ContactName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ContactNumber;
		private System.Drawing.Printing.PrintDocument printDocumentContacts;
		private System.Windows.Forms.Button buttonPrint;
		private System.Windows.Forms.PrintDialog printContactsDialog;
	}
}
namespace WoodClub
{
	partial class NewMembers
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
			this.Add = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Invoice = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Badge = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RecNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.BillTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Zip = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.MemberDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bs_newmember = new System.Windows.Forms.BindingSource(this.components);
			this.buttonAddToDb = new System.Windows.Forms.Button();
			this.quickBooksButton = new System.Windows.Forms.Button();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.printNewMembersButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_newmember)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowDrop = true;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Add,
            this.Invoice,
            this.Badge,
            this.RecNo,
            this.FirstName,
            this.LastName,
            this.BillTo,
            this.Address,
            this.Email,
            this.Phone,
            this.City,
            this.State,
            this.Zip,
            this.MemberDate,
            this.CardNo});
			this.dataGridView1.Location = new System.Drawing.Point(3, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(1155, 397);
			this.dataGridView1.TabIndex = 0;
			// 
			// Add
			// 
			this.Add.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Add.DataPropertyName = "Add";
			this.Add.HeaderText = "Add";
			this.Add.Name = "Add";
			this.Add.Width = 60;
			// 
			// Invoice
			// 
			this.Invoice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Invoice.DataPropertyName = "CreateInvoice";
			this.Invoice.HeaderText = "Invoice";
			this.Invoice.Name = "Invoice";
			this.Invoice.Width = 60;
			// 
			// Badge
			// 
			this.Badge.DataPropertyName = "Badge";
			this.Badge.HeaderText = "Badge";
			this.Badge.Name = "Badge";
			// 
			// RecNo
			// 
			this.RecNo.DataPropertyName = "RecNo";
			this.RecNo.HeaderText = "Rec Card";
			this.RecNo.Name = "RecNo";
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
			// BillTo
			// 
			this.BillTo.HeaderText = "Bill To";
			this.BillTo.Name = "BillTo";
			// 
			// Address
			// 
			this.Address.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Address.DataPropertyName = "Address";
			this.Address.HeaderText = "Address";
			this.Address.Name = "Address";
			// 
			// Email
			// 
			this.Email.DataPropertyName = "Email";
			this.Email.HeaderText = "Email";
			this.Email.Name = "Email";
			// 
			// Phone
			// 
			this.Phone.DataPropertyName = "Phone";
			this.Phone.HeaderText = "Phone";
			this.Phone.Name = "Phone";
			// 
			// City
			// 
			this.City.DataPropertyName = "City";
			this.City.HeaderText = "City";
			this.City.Name = "City";
			// 
			// State
			// 
			this.State.DataPropertyName = "State";
			this.State.HeaderText = "State";
			this.State.Name = "State";
			// 
			// Zip
			// 
			this.Zip.DataPropertyName = "ZipCode";
			this.Zip.HeaderText = "Zip Code";
			this.Zip.Name = "Zip";
			// 
			// MemberDate
			// 
			this.MemberDate.DataPropertyName = "MemberDate";
			this.MemberDate.HeaderText = "Member Date";
			this.MemberDate.Name = "MemberDate";
			// 
			// CardNo
			// 
			this.CardNo.DataPropertyName = "CardNo";
			this.CardNo.HeaderText = "CardNo";
			this.CardNo.Name = "CardNo";
			// 
			// buttonAddToDb
			// 
			this.buttonAddToDb.Location = new System.Drawing.Point(951, 418);
			this.buttonAddToDb.Name = "buttonAddToDb";
			this.buttonAddToDb.Size = new System.Drawing.Size(75, 23);
			this.buttonAddToDb.TabIndex = 1;
			this.buttonAddToDb.Text = "Add To DB";
			this.buttonAddToDb.UseVisualStyleBackColor = true;
			this.buttonAddToDb.Click += new System.EventHandler(this.buttonAddToDb_Click);
			// 
			// quickBooksButton
			// 
			this.quickBooksButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.quickBooksButton.Location = new System.Drawing.Point(115, 418);
			this.quickBooksButton.Name = "quickBooksButton";
			this.quickBooksButton.Size = new System.Drawing.Size(75, 23);
			this.quickBooksButton.TabIndex = 2;
			this.quickBooksButton.Text = "Add To QB";
			this.quickBooksButton.UseVisualStyleBackColor = true;
			this.quickBooksButton.Click += new System.EventHandler(this.quickBooksButton_Click);
			// 
			// printNewMembersButton
			// 
			this.printNewMembersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.printNewMembersButton.Location = new System.Drawing.Point(543, 417);
			this.printNewMembersButton.Name = "printNewMembersButton";
			this.printNewMembersButton.Size = new System.Drawing.Size(75, 23);
			this.printNewMembersButton.TabIndex = 3;
			this.printNewMembersButton.Text = "Print";
			this.printNewMembersButton.UseVisualStyleBackColor = true;
			this.printNewMembersButton.Click += new System.EventHandler(this.printNewMembersButton_Click);
			// 
			// NewMembers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1156, 450);
			this.Controls.Add(this.printNewMembersButton);
			this.Controls.Add(this.quickBooksButton);
			this.Controls.Add(this.buttonAddToDb);
			this.Controls.Add(this.dataGridView1);
			this.Name = "NewMembers";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.FormNewMembers_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_newmember)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.BindingSource bs_newmember;
		private System.Windows.Forms.Button buttonAddToDb;
		private System.Windows.Forms.Button quickBooksButton;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Add;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Invoice;
		private System.Windows.Forms.DataGridViewTextBoxColumn Badge;
		private System.Windows.Forms.DataGridViewTextBoxColumn RecNo;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
		private System.Windows.Forms.DataGridViewTextBoxColumn BillTo;
		private System.Windows.Forms.DataGridViewTextBoxColumn Address;
		private System.Windows.Forms.DataGridViewTextBoxColumn Email;
		private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
		private System.Windows.Forms.DataGridViewTextBoxColumn City;
		private System.Windows.Forms.DataGridViewTextBoxColumn State;
		private System.Windows.Forms.DataGridViewTextBoxColumn Zip;
		private System.Windows.Forms.DataGridViewTextBoxColumn MemberDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn CardNo;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.Button printNewMembersButton;
	}
}


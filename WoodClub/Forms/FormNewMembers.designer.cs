namespace WoodClub
{
	partial class FormNewMembers
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
			this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Zip = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RecNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.MemberDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Badge = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bs_newmember = new System.Windows.Forms.BindingSource(this.components);
			this.buttonAddToDb = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_newmember)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Add,
            this.FirstName,
            this.LastName,
            this.Email,
            this.Phone,
            this.Address,
            this.City,
            this.State,
            this.Zip,
            this.RecNo,
            this.MemberDate,
            this.Badge,
            this.CardNo});
			this.dataGridView1.Location = new System.Drawing.Point(3, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(1141, 388);
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
			// Address
			// 
			this.Address.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Address.DataPropertyName = "Address";
			this.Address.HeaderText = "Address";
			this.Address.Name = "Address";
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
			// RecNo
			// 
			this.RecNo.DataPropertyName = "RecNo";
			this.RecNo.HeaderText = "Rec Card";
			this.RecNo.Name = "RecNo";
			// 
			// MemberDate
			// 
			this.MemberDate.DataPropertyName = "MemberDate";
			this.MemberDate.HeaderText = "Member Date";
			this.MemberDate.Name = "MemberDate";
			// 
			// Badge
			// 
			this.Badge.DataPropertyName = "Badge";
			this.Badge.HeaderText = "Badge";
			this.Badge.Name = "Badge";
			// 
			// CardNo
			// 
			this.CardNo.DataPropertyName = "CardNo";
			this.CardNo.HeaderText = "CardNo";
			this.CardNo.Name = "CardNo";
			// 
			// buttonAddToDb
			// 
			this.buttonAddToDb.Location = new System.Drawing.Point(951, 415);
			this.buttonAddToDb.Name = "buttonAddToDb";
			this.buttonAddToDb.Size = new System.Drawing.Size(75, 23);
			this.buttonAddToDb.TabIndex = 1;
			this.buttonAddToDb.Text = "Add To DB";
			this.buttonAddToDb.UseVisualStyleBackColor = true;
			this.buttonAddToDb.Click += new System.EventHandler(this.buttonAddToDb_Click);
			// 
			// FormNewMembers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1156, 450);
			this.Controls.Add(this.buttonAddToDb);
			this.Controls.Add(this.dataGridView1);
			this.Name = "FormNewMembers";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bs_newmember)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.BindingSource bs_newmember;
		private System.Windows.Forms.Button buttonAddToDb;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Add;
		private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
		private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Email;
		private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
		private System.Windows.Forms.DataGridViewTextBoxColumn Address;
		private System.Windows.Forms.DataGridViewTextBoxColumn City;
		private System.Windows.Forms.DataGridViewTextBoxColumn State;
		private System.Windows.Forms.DataGridViewTextBoxColumn Zip;
		private System.Windows.Forms.DataGridViewTextBoxColumn RecNo;
		private System.Windows.Forms.DataGridViewTextBoxColumn MemberDate;
		private System.Windows.Forms.DataGridViewTextBoxColumn Badge;
		private System.Windows.Forms.DataGridViewTextBoxColumn CardNo;
	}
}


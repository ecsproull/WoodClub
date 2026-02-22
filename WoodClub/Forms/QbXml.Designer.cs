namespace WoodClub.Forms
{
	partial class QbXml
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
			this.loadInvbutton = new System.Windows.Forms.Button();
			this.dataGridQbItems = new System.Windows.Forms.DataGridView();
			this.qbItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.accountsButton = new System.Windows.Forms.Button();
			this.addAcctsButton = new System.Windows.Forms.Button();
			this.addItemButton = new System.Windows.Forms.Button();
			this.loadVendorsButton = new System.Windows.Forms.Button();
			this.addVendorsButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridQbItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.qbItemsBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// loadInvbutton
			// 
			this.loadInvbutton.Location = new System.Drawing.Point(26, 12);
			this.loadInvbutton.Name = "loadInvbutton";
			this.loadInvbutton.Size = new System.Drawing.Size(75, 23);
			this.loadInvbutton.TabIndex = 0;
			this.loadInvbutton.Text = "Laod Inv";
			this.loadInvbutton.UseVisualStyleBackColor = true;
			this.loadInvbutton.Click += new System.EventHandler(this.loadInvbutton_Click);
			// 
			// dataGridQbItems
			// 
			this.dataGridQbItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridQbItems.AutoGenerateColumns = false;
			this.dataGridQbItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridQbItems.DataSource = this.qbItemsBindingSource;
			this.dataGridQbItems.Location = new System.Drawing.Point(12, 42);
			this.dataGridQbItems.Name = "dataGridQbItems";
			this.dataGridQbItems.Size = new System.Drawing.Size(776, 396);
			this.dataGridQbItems.TabIndex = 1;
			// 
			// accountsButton
			// 
			this.accountsButton.Location = new System.Drawing.Point(214, 12);
			this.accountsButton.Name = "accountsButton";
			this.accountsButton.Size = new System.Drawing.Size(75, 23);
			this.accountsButton.TabIndex = 2;
			this.accountsButton.Text = "Accounts";
			this.accountsButton.UseVisualStyleBackColor = true;
			this.accountsButton.Click += new System.EventHandler(this.accountsButton_Click);
			// 
			// addAcctsButton
			// 
			this.addAcctsButton.Location = new System.Drawing.Point(308, 12);
			this.addAcctsButton.Name = "addAcctsButton";
			this.addAcctsButton.Size = new System.Drawing.Size(93, 23);
			this.addAcctsButton.TabIndex = 3;
			this.addAcctsButton.Text = "Add Accounts";
			this.addAcctsButton.UseVisualStyleBackColor = true;
			this.addAcctsButton.Click += new System.EventHandler(this.addAcctsButton_Click);
			// 
			// addItemButton
			// 
			this.addItemButton.Location = new System.Drawing.Point(120, 12);
			this.addItemButton.Name = "addItemButton";
			this.addItemButton.Size = new System.Drawing.Size(75, 23);
			this.addItemButton.TabIndex = 4;
			this.addItemButton.Text = "Add Items";
			this.addItemButton.UseVisualStyleBackColor = true;
			this.addItemButton.Click += new System.EventHandler(this.addItemButton_Click);
			// 
			// loadVendorsButton
			// 
			this.loadVendorsButton.Location = new System.Drawing.Point(420, 12);
			this.loadVendorsButton.Name = "loadVendorsButton";
			this.loadVendorsButton.Size = new System.Drawing.Size(88, 23);
			this.loadVendorsButton.TabIndex = 5;
			this.loadVendorsButton.Text = "Load Vendors";
			this.loadVendorsButton.UseVisualStyleBackColor = true;
			this.loadVendorsButton.Click += new System.EventHandler(this.loadVendorsButton_Click);
			// 
			// addVendorsButton
			// 
			this.addVendorsButton.Location = new System.Drawing.Point(526, 13);
			this.addVendorsButton.Name = "addVendorsButton";
			this.addVendorsButton.Size = new System.Drawing.Size(91, 23);
			this.addVendorsButton.TabIndex = 6;
			this.addVendorsButton.Text = "Add Vendors";
			this.addVendorsButton.UseVisualStyleBackColor = true;
			this.addVendorsButton.Click += new System.EventHandler(this.addVendorsButton_Click);
			// 
			// QbXml
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.addVendorsButton);
			this.Controls.Add(this.loadVendorsButton);
			this.Controls.Add(this.addItemButton);
			this.Controls.Add(this.addAcctsButton);
			this.Controls.Add(this.accountsButton);
			this.Controls.Add(this.dataGridQbItems);
			this.Controls.Add(this.loadInvbutton);
			this.Name = "QbXml";
			this.Text = "QbXml";
			((System.ComponentModel.ISupportInitialize)(this.dataGridQbItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.qbItemsBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button loadInvbutton;
		private System.Windows.Forms.DataGridView dataGridQbItems;
		private System.Windows.Forms.BindingSource qbItemsBindingSource;
		private System.Windows.Forms.Button accountsButton;
		private System.Windows.Forms.Button addAcctsButton;
		private System.Windows.Forms.Button addItemButton;
		private System.Windows.Forms.Button loadVendorsButton;
		private System.Windows.Forms.Button addVendorsButton;
	}
}
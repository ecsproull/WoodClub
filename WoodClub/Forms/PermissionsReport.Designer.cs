namespace WoodClub.Forms
{
	partial class PermissionsReport
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.excel = new System.Windows.Forms.Button();
			this.print = new System.Windows.Forms.Button();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.email_list = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(3, 36);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(793, 378);
			this.dataGridView1.TabIndex = 0;
			// 
			// excel
			// 
			this.excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.excel.Location = new System.Drawing.Point(240, 422);
			this.excel.Name = "excel";
			this.excel.Size = new System.Drawing.Size(86, 23);
			this.excel.TabIndex = 1;
			this.excel.Text = "Excel";
			this.excel.UseVisualStyleBackColor = true;
			this.excel.Click += new System.EventHandler(this.excel_Click);
			// 
			// print
			// 
			this.print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.print.Location = new System.Drawing.Point(485, 424);
			this.print.Name = "print";
			this.print.Size = new System.Drawing.Size(75, 20);
			this.print.TabIndex = 2;
			this.print.Text = "Print";
			this.print.UseVisualStyleBackColor = true;
			this.print.Click += new System.EventHandler(this.print_Click);
			// 
			// email_list
			// 
			this.email_list.Location = new System.Drawing.Point(374, 422);
			this.email_list.Name = "email_list";
			this.email_list.Size = new System.Drawing.Size(66, 21);
			this.email_list.TabIndex = 3;
			this.email_list.Text = "Email List";
			this.email_list.UseVisualStyleBackColor = true;
			this.email_list.Click += new System.EventHandler(this.email_list_Click);
			// 
			// PermissionsReport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.email_list);
			this.Controls.Add(this.print);
			this.Controls.Add(this.excel);
			this.Controls.Add(this.dataGridView1);
			this.Name = "PermissionsReport";
			this.Text = "PermissionsReport";
			this.Load += new System.EventHandler(this.PermissionsReport_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button excel;
		private System.Windows.Forms.Button print;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.Button email_list;
	}
}
namespace WoodClub
{
	partial class CompareDbs
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
			this.Badge = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.First = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Last = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.db_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.compareBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.compareProgressBar = new System.Windows.Forms.ProgressBar();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.compareBindingSource)).BeginInit();
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
            this.Badge,
            this.First,
            this.Last,
            this.db_name});
			this.dataGridView1.DataSource = this.compareBindingSource;
			this.dataGridView1.Location = new System.Drawing.Point(14, 28);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(403, 382);
			this.dataGridView1.TabIndex = 0;
			// 
			// Badge
			// 
			this.Badge.DataPropertyName = "Badge";
			this.Badge.HeaderText = "Badge";
			this.Badge.Name = "Badge";
			// 
			// First
			// 
			this.First.DataPropertyName = "First";
			this.First.HeaderText = "First";
			this.First.Name = "First";
			// 
			// Last
			// 
			this.Last.DataPropertyName = "Last";
			this.Last.HeaderText = "Last";
			this.Last.Name = "Last";
			// 
			// db_name
			// 
			this.db_name.DataPropertyName = "OnlyDb";
			this.db_name.HeaderText = "Only In";
			this.db_name.Name = "db_name";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(133, 416);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(163, 27);
			this.button1.TabIndex = 1;
			this.button1.Text = "Compare";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// compareProgressBar
			// 
			this.compareProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.compareProgressBar.Location = new System.Drawing.Point(14, 12);
			this.compareProgressBar.Name = "compareProgressBar";
			this.compareProgressBar.Size = new System.Drawing.Size(403, 10);
			this.compareProgressBar.TabIndex = 2;
			// 
			// CompareDbs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(429, 450);
			this.Controls.Add(this.compareProgressBar);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.dataGridView1);
			this.Name = "CompareDbs";
			this.Text = "CompareDbs";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.compareBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.BindingSource compareBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn Badge;
		private System.Windows.Forms.DataGridViewTextBoxColumn First;
		private System.Windows.Forms.DataGridViewTextBoxColumn Last;
		private System.Windows.Forms.DataGridViewTextBoxColumn db_name;
		private System.Windows.Forms.ProgressBar compareProgressBar;
	}
}
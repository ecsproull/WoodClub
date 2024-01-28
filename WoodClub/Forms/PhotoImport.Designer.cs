namespace WoodClub.Forms
{
	partial class PhotoImport
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.loadImagesButton = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.newImagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.savePathTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SaveRow = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Badge = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FullSize = new System.Windows.Forms.DataGridViewImageColumn();
			this.Cropped = new System.Windows.Forms.DataGridViewImageColumn();
			this.BadgeSize = new System.Windows.Forms.DataGridViewImageColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.newImagesBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// loadImagesButton
			// 
			this.loadImagesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.loadImagesButton.Location = new System.Drawing.Point(87, 929);
			this.loadImagesButton.Name = "loadImagesButton";
			this.loadImagesButton.Size = new System.Drawing.Size(86, 23);
			this.loadImagesButton.TabIndex = 0;
			this.loadImagesButton.Text = "Load Images";
			this.loadImagesButton.UseVisualStyleBackColor = true;
			this.loadImagesButton.Click += new System.EventHandler(this.loadImagesButton_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SaveRow,
            this.Badge,
            this.FullSize,
            this.Cropped,
            this.BadgeSize});
			this.dataGridView1.DataSource = this.newImagesBindingSource;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.Height = 500;
			this.dataGridView1.Size = new System.Drawing.Size(1220, 923);
			this.dataGridView1.TabIndex = 1;
			// 
			// savePathTextBox
			// 
			this.savePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.savePathTextBox.Location = new System.Drawing.Point(320, 932);
			this.savePathTextBox.Name = "savePathTextBox";
			this.savePathTextBox.Size = new System.Drawing.Size(208, 20);
			this.savePathTextBox.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(247, 936);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Save Folder";
			// 
			// SaveRow
			// 
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SaveRow.DefaultCellStyle = dataGridViewCellStyle1;
			this.SaveRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SaveRow.HeaderText = "Save";
			this.SaveRow.Name = "SaveRow";
			this.SaveRow.ReadOnly = true;
			this.SaveRow.Text = "Save Row";
			this.SaveRow.ToolTipText = "Save Row";
			// 
			// Badge
			// 
			this.Badge.DataPropertyName = "Badge";
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Badge.DefaultCellStyle = dataGridViewCellStyle2;
			this.Badge.HeaderText = "Badge";
			this.Badge.Name = "Badge";
			// 
			// FullSize
			// 
			this.FullSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.FullSize.DataPropertyName = "FullSize";
			this.FullSize.HeaderText = "Full Size";
			this.FullSize.Name = "FullSize";
			this.FullSize.Width = 500;
			// 
			// Cropped
			// 
			this.Cropped.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Cropped.DataPropertyName = "Cropped";
			this.Cropped.HeaderText = "Cropped";
			this.Cropped.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
			this.Cropped.Name = "Cropped";
			this.Cropped.Width = 420;
			// 
			// BadgeSize
			// 
			this.BadgeSize.DataPropertyName = "BadgeSize";
			this.BadgeSize.HeaderText = "Badge Size";
			this.BadgeSize.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
			this.BadgeSize.Name = "BadgeSize";
			// 
			// PhotoImport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1219, 961);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.savePathTextBox);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.loadImagesButton);
			this.Name = "PhotoImport";
			this.Text = "PhotoImport";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.newImagesBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button loadImagesButton;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.BindingSource newImagesBindingSource;
		private System.Windows.Forms.TextBox savePathTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridViewButtonColumn SaveRow;
		private System.Windows.Forms.DataGridViewTextBoxColumn Badge;
		private System.Windows.Forms.DataGridViewImageColumn FullSize;
		private System.Windows.Forms.DataGridViewImageColumn Cropped;
		private System.Windows.Forms.DataGridViewImageColumn BadgeSize;
	}
}
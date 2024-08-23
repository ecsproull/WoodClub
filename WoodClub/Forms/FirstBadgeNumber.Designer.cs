namespace WoodClub
{
	partial class FirstBadgeNumber
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
			this.label1 = new System.Windows.Forms.Label();
			this.id_ok = new System.Windows.Forms.Button();
			this.startingBadgeNumber = new System.Windows.Forms.NumericUpDown();
			this.id_skip = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.startingBadgeNumber)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(30, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(145, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter Starting Badge Number";
			// 
			// id_ok
			// 
			this.id_ok.Location = new System.Drawing.Point(25, 106);
			this.id_ok.Name = "id_ok";
			this.id_ok.Size = new System.Drawing.Size(63, 23);
			this.id_ok.TabIndex = 2;
			this.id_ok.Text = "OK";
			this.id_ok.UseVisualStyleBackColor = true;
			this.id_ok.Click += new System.EventHandler(this.id_ok_Click);
			// 
			// startingBadgeNumber
			// 
			this.startingBadgeNumber.Location = new System.Drawing.Point(65, 71);
			this.startingBadgeNumber.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.startingBadgeNumber.Minimum = new decimal(new int[] {
            5480,
            0,
            0,
            0});
			this.startingBadgeNumber.Name = "startingBadgeNumber";
			this.startingBadgeNumber.Size = new System.Drawing.Size(75, 20);
			this.startingBadgeNumber.TabIndex = 3;
			this.startingBadgeNumber.Value = new decimal(new int[] {
            5480,
            0,
            0,
            0});
			// 
			// id_skip
			// 
			this.id_skip.Location = new System.Drawing.Point(106, 106);
			this.id_skip.Name = "id_skip";
			this.id_skip.Size = new System.Drawing.Size(65, 23);
			this.id_skip.TabIndex = 4;
			this.id_skip.Text = "Skip";
			this.id_skip.UseVisualStyleBackColor = true;
			this.id_skip.Click += new System.EventHandler(this.id_skip_Click);
			// 
			// FirstBadgeNumber
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(206, 176);
			this.Controls.Add(this.id_skip);
			this.Controls.Add(this.startingBadgeNumber);
			this.Controls.Add(this.id_ok);
			this.Controls.Add(this.label1);
			this.Name = "FirstBadgeNumber";
			this.Text = "FirstBadgeNumber";
			((System.ComponentModel.ISupportInitialize)(this.startingBadgeNumber)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button id_ok;
		private System.Windows.Forms.NumericUpDown startingBadgeNumber;
		private System.Windows.Forms.Button id_skip;
	}
}
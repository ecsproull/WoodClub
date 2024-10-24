namespace WoodClub.Forms
{
	partial class SyncToQB
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
			this.syncProgressBar = new System.Windows.Forms.ProgressBar();
			this.startButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(34, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(358, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "This is a long running process. QuickBooks must be open on this machine.";
			// 
			// syncProgressBar
			// 
			this.syncProgressBar.Location = new System.Drawing.Point(36, 58);
			this.syncProgressBar.Name = "syncProgressBar";
			this.syncProgressBar.Size = new System.Drawing.Size(355, 23);
			this.syncProgressBar.TabIndex = 1;
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(37, 103);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(75, 23);
			this.startButton.TabIndex = 2;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.Location = new System.Drawing.Point(315, 103);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 4;
			this.closeButton.Text = "Close";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// SyncToQB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(426, 154);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.syncProgressBar);
			this.Controls.Add(this.label1);
			this.Name = "SyncToQB";
			this.Text = "SyncToQB";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ProgressBar syncProgressBar;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button closeButton;
	}
}
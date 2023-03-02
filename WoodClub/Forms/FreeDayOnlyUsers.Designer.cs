
namespace WoodClub.Forms
{
    partial class FreeDayOnlyUsers
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
            this.dataGridViewFreeDay = new System.Windows.Forms.DataGridView();
            this.bs_FreeDayOnly = new System.Windows.Forms.BindingSource(this.components);
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.nMaxCredits = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.Badge = new System.Windows.Forms.DataGridViewButtonColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_FreeDayOnly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxCredits)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridViewFreeDay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFreeDay.AutoGenerateColumns = false;
            this.dataGridViewFreeDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFreeDay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Badge,
            this.FirstName,
            this.LastName});
            this.dataGridViewFreeDay.DataSource = this.bs_FreeDayOnly;
            this.dataGridViewFreeDay.Location = new System.Drawing.Point(13, 39);
            this.dataGridViewFreeDay.Name = "dataGridView1";
            this.dataGridViewFreeDay.RowHeadersVisible = false;
            this.dataGridViewFreeDay.Size = new System.Drawing.Size(303, 399);
            this.dataGridViewFreeDay.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(13, 13);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // nMaxCredits
            // 
            this.nMaxCredits.Location = new System.Drawing.Point(285, 13);
            this.nMaxCredits.Name = "nMaxCredits";
            this.nMaxCredits.Size = new System.Drawing.Size(30, 20);
            this.nMaxCredits.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Max Credits";
            // 
            // Badge
            // 
            this.Badge.DataPropertyName = "Badge";
            this.Badge.HeaderText = "Badge";
            this.Badge.Name = "Badge";
            this.Badge.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Badge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
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
            // FreeDayOnlyUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nMaxCredits);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataGridViewFreeDay);
            this.Name = "FreeDayOnlyUsers";
            this.Text = "FreeDayOnlyUsers";
            this.Load += new System.EventHandler(this.FreeDayOnlyUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFreeDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bs_FreeDayOnly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nMaxCredits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFreeDay;
        private System.Windows.Forms.BindingSource bs_FreeDayOnly;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		public System.Windows.Forms.NumericUpDown nMaxCredits;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewButtonColumn Badge;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
    }
}
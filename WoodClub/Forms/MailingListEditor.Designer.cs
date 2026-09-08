namespace WoodClub.Forms
{
    partial class MailingListEditor
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
            this.cbLists = new System.Windows.Forms.ComboBox();
            this.btnNewList = new System.Windows.Forms.Button();
            this.btnDeleteList = new System.Windows.Forms.Button();
            this.lblFilter = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lblAvailable = new System.Windows.Forms.Label();
            this.lblMembers = new System.Windows.Forms.Label();
            this.dgvAvailable = new System.Windows.Forms.DataGridView();
            this.colSelectA = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colFirstNameA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastNameA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBadgeA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmailA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMembers = new System.Windows.Forms.DataGridView();
            this.colSelectM = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colFirstNameM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastNameM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBadgeM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmailM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnMoveRight = new System.Windows.Forms.Button();
            this.btnMoveLeft = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).BeginInit();
            this.SuspendLayout();
            //
            // cbLists
            //
            this.cbLists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLists.FormattingEnabled = true;
            this.cbLists.Location = new System.Drawing.Point(12, 12);
            this.cbLists.Name = "cbLists";
            this.cbLists.Size = new System.Drawing.Size(300, 21);
            this.cbLists.TabIndex = 0;
            this.cbLists.SelectedIndexChanged += new System.EventHandler(this.cbLists_SelectedIndexChanged);
            //
            // btnNewList
            //
            this.btnNewList.Location = new System.Drawing.Point(318, 11);
            this.btnNewList.Name = "btnNewList";
            this.btnNewList.Size = new System.Drawing.Size(100, 23);
            this.btnNewList.TabIndex = 1;
            this.btnNewList.Text = "New List...";
            this.btnNewList.UseVisualStyleBackColor = true;
            this.btnNewList.Click += new System.EventHandler(this.btnNewList_Click);
            //
            // btnDeleteList
            //
            this.btnDeleteList.Location = new System.Drawing.Point(424, 11);
            this.btnDeleteList.Name = "btnDeleteList";
            this.btnDeleteList.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteList.TabIndex = 2;
            this.btnDeleteList.Text = "Delete List";
            this.btnDeleteList.UseVisualStyleBackColor = true;
            this.btnDeleteList.Click += new System.EventHandler(this.btnDeleteList_Click);
            //
            // lblFilter
            //
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(12, 45);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(32, 13);
            this.lblFilter.TabIndex = 2;
            this.lblFilter.Text = "Filter:";
            //
            // txtFilter
            //
            this.txtFilter.Location = new System.Drawing.Point(50, 42);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(300, 20);
            this.txtFilter.TabIndex = 3;
            //
            // lblAvailable
            //
            this.lblAvailable.AutoSize = true;
            this.lblAvailable.Location = new System.Drawing.Point(12, 72);
            this.lblAvailable.Name = "lblAvailable";
            this.lblAvailable.Size = new System.Drawing.Size(96, 13);
            this.lblAvailable.TabIndex = 4;
            this.lblAvailable.Text = "Available Members";
            //
            // lblMembers
            //
            this.lblMembers.AutoSize = true;
            this.lblMembers.Location = new System.Drawing.Point(508, 72);
            this.lblMembers.Name = "lblMembers";
            this.lblMembers.Size = new System.Drawing.Size(67, 13);
            this.lblMembers.TabIndex = 5;
            this.lblMembers.Text = "List Members";
            //
            // dgvAvailable
            //
            this.dgvAvailable.AllowUserToAddRows = false;
            this.dgvAvailable.AllowUserToDeleteRows = false;
            this.dgvAvailable.AllowUserToResizeRows = false;
            this.dgvAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvAvailable.AutoGenerateColumns = false;
            this.dgvAvailable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelectA,
            this.colFirstNameA,
            this.colLastNameA,
            this.colBadgeA,
            this.colEmailA});
            this.dgvAvailable.Location = new System.Drawing.Point(12, 90);
            this.dgvAvailable.MultiSelect = false;
            this.dgvAvailable.Name = "dgvAvailable";
            this.dgvAvailable.RowHeadersVisible = false;
            this.dgvAvailable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvailable.Size = new System.Drawing.Size(430, 470);
            this.dgvAvailable.TabIndex = 6;
            this.dgvAvailable.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAvailable_CellMouseDoubleClick);
            //
            // colSelectA
            //
            this.colSelectA.DataPropertyName = "Selected";
            this.colSelectA.HeaderText = "";
            this.colSelectA.Name = "colSelectA";
            this.colSelectA.Width = 30;
            //
            // colFirstNameA
            //
            this.colFirstNameA.DataPropertyName = "FirstName";
            this.colFirstNameA.HeaderText = "First Name";
            this.colFirstNameA.Name = "colFirstNameA";
            this.colFirstNameA.ReadOnly = true;
            this.colFirstNameA.Width = 110;
            //
            // colLastNameA
            //
            this.colLastNameA.DataPropertyName = "LastName";
            this.colLastNameA.HeaderText = "Last Name";
            this.colLastNameA.Name = "colLastNameA";
            this.colLastNameA.ReadOnly = true;
            this.colLastNameA.Width = 110;
            //
            // colBadgeA
            //
            this.colBadgeA.DataPropertyName = "Badge";
            this.colBadgeA.HeaderText = "Badge";
            this.colBadgeA.Name = "colBadgeA";
            this.colBadgeA.ReadOnly = true;
            this.colBadgeA.Width = 60;
            //
            // colEmailA
            //
            this.colEmailA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEmailA.DataPropertyName = "Email";
            this.colEmailA.HeaderText = "Email";
            this.colEmailA.Name = "colEmailA";
            this.colEmailA.ReadOnly = true;
            //
            // dgvMembers
            //
            this.dgvMembers.AllowUserToAddRows = false;
            this.dgvMembers.AllowUserToDeleteRows = false;
            this.dgvMembers.AllowUserToResizeRows = false;
            this.dgvMembers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMembers.AutoGenerateColumns = false;
            this.dgvMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMembers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelectM,
            this.colFirstNameM,
            this.colLastNameM,
            this.colBadgeM,
            this.colEmailM});
            this.dgvMembers.Location = new System.Drawing.Point(502, 90);
            this.dgvMembers.MultiSelect = false;
            this.dgvMembers.Name = "dgvMembers";
            this.dgvMembers.RowHeadersVisible = false;
            this.dgvMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMembers.Size = new System.Drawing.Size(426, 470);
            this.dgvMembers.TabIndex = 8;
            this.dgvMembers.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMembers_CellMouseDoubleClick);
            //
            // colSelectM
            //
            this.colSelectM.DataPropertyName = "Selected";
            this.colSelectM.HeaderText = "";
            this.colSelectM.Name = "colSelectM";
            this.colSelectM.Width = 30;
            //
            // colFirstNameM
            //
            this.colFirstNameM.DataPropertyName = "FirstName";
            this.colFirstNameM.HeaderText = "First Name";
            this.colFirstNameM.Name = "colFirstNameM";
            this.colFirstNameM.ReadOnly = true;
            this.colFirstNameM.Width = 110;
            //
            // colLastNameM
            //
            this.colLastNameM.DataPropertyName = "LastName";
            this.colLastNameM.HeaderText = "Last Name";
            this.colLastNameM.Name = "colLastNameM";
            this.colLastNameM.ReadOnly = true;
            this.colLastNameM.Width = 110;
            //
            // colBadgeM
            //
            this.colBadgeM.DataPropertyName = "Badge";
            this.colBadgeM.HeaderText = "Badge";
            this.colBadgeM.Name = "colBadgeM";
            this.colBadgeM.ReadOnly = true;
            this.colBadgeM.Width = 60;
            //
            // colEmailM
            //
            this.colEmailM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEmailM.DataPropertyName = "Email";
            this.colEmailM.HeaderText = "Email";
            this.colEmailM.Name = "colEmailM";
            this.colEmailM.ReadOnly = true;
            //
            // btnMoveRight
            //
            this.btnMoveRight.Location = new System.Drawing.Point(450, 240);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(44, 28);
            this.btnMoveRight.TabIndex = 7;
            this.btnMoveRight.Text = "→";
            this.btnMoveRight.UseVisualStyleBackColor = true;
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            //
            // btnMoveLeft
            //
            this.btnMoveLeft.Location = new System.Drawing.Point(450, 278);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(44, 28);
            this.btnMoveLeft.TabIndex = 9;
            this.btnMoveLeft.Text = "←";
            this.btnMoveLeft.UseVisualStyleBackColor = true;
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            //
            // btnSubmit
            //
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(772, 572);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 10;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(853, 572);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // MailingListEditor
            //
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(940, 607);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnMoveLeft);
            this.Controls.Add(this.btnMoveRight);
            this.Controls.Add(this.dgvMembers);
            this.Controls.Add(this.dgvAvailable);
            this.Controls.Add(this.lblMembers);
            this.Controls.Add(this.lblAvailable);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.btnDeleteList);
            this.Controls.Add(this.btnNewList);
            this.Controls.Add(this.cbLists);
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "MailingListEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mailing List Editor";
            this.Load += new System.EventHandler(this.MailingListEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLists;
        private System.Windows.Forms.Button btnNewList;
        private System.Windows.Forms.Button btnDeleteList;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label lblAvailable;
        private System.Windows.Forms.Label lblMembers;
        private System.Windows.Forms.DataGridView dgvAvailable;
        private System.Windows.Forms.DataGridView dgvMembers;
        private System.Windows.Forms.Button btnMoveRight;
        private System.Windows.Forms.Button btnMoveLeft;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelectA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFirstNameA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastNameA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBadgeA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmailA;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelectM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFirstNameM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastNameM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBadgeM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmailM;
    }
}

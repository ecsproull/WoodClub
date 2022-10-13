namespace WoodClub
{
    partial class scwForm
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
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.badgeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zipDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recCardDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clubDuesPaidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clubDuesPaidDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recDuesPaidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblDelete = new System.Windows.Forms.Label();
            this.unpaidMemberBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unpaidMemberBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.deleteDataGridViewTextBoxColumn,
            this.badgeDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.addressDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.zipDataGridViewTextBoxColumn,
            this.phoneDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.recCardDataGridViewTextBoxColumn,
            this.memberDateDataGridViewTextBoxColumn,
            this.clubDuesPaidDataGridViewTextBoxColumn,
            this.clubDuesPaidDateDataGridViewTextBoxColumn,
            this.recDuesPaidDataGridViewTextBoxColumn});
            this.dataGridView1.Location = new System.Drawing.Point(3, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1000, 600);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.MaxInputLength = 10;
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // deleteDataGridViewTextBoxColumn
            // 
            this.deleteDataGridViewTextBoxColumn.DataPropertyName = "Delete";
            this.deleteDataGridViewTextBoxColumn.HeaderText = "Delete";
            this.deleteDataGridViewTextBoxColumn.Name = "deleteDataGridViewTextBoxColumn";
            this.deleteDataGridViewTextBoxColumn.ReadOnly = true;
            this.deleteDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.deleteDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.deleteDataGridViewTextBoxColumn.Width = 50;
            // 
            // badgeDataGridViewTextBoxColumn
            // 
            this.badgeDataGridViewTextBoxColumn.DataPropertyName = "Badge";
            this.badgeDataGridViewTextBoxColumn.HeaderText = "Badge";
            this.badgeDataGridViewTextBoxColumn.MaxInputLength = 20;
            this.badgeDataGridViewTextBoxColumn.Name = "badgeDataGridViewTextBoxColumn";
            this.badgeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstNameDataGridViewTextBoxColumn
            // 
            this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.HeaderText = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.MaxInputLength = 30;
            this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
            this.firstNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            this.lastNameDataGridViewTextBoxColumn.HeaderText = "LastName";
            this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            this.lastNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // addressDataGridViewTextBoxColumn
            // 
            this.addressDataGridViewTextBoxColumn.DataPropertyName = "Address";
            this.addressDataGridViewTextBoxColumn.HeaderText = "Address";
            this.addressDataGridViewTextBoxColumn.Name = "addressDataGridViewTextBoxColumn";
            this.addressDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "State";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // zipDataGridViewTextBoxColumn
            // 
            this.zipDataGridViewTextBoxColumn.DataPropertyName = "Zip";
            this.zipDataGridViewTextBoxColumn.HeaderText = "Zip";
            this.zipDataGridViewTextBoxColumn.MaxInputLength = 20;
            this.zipDataGridViewTextBoxColumn.Name = "zipDataGridViewTextBoxColumn";
            this.zipDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // phoneDataGridViewTextBoxColumn
            // 
            this.phoneDataGridViewTextBoxColumn.DataPropertyName = "Phone";
            this.phoneDataGridViewTextBoxColumn.HeaderText = "Phone";
            this.phoneDataGridViewTextBoxColumn.MaxInputLength = 20;
            this.phoneDataGridViewTextBoxColumn.Name = "phoneDataGridViewTextBoxColumn";
            this.phoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.MaxInputLength = 30;
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.MaxInputLength = 20;
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // recCardDataGridViewTextBoxColumn
            // 
            this.recCardDataGridViewTextBoxColumn.DataPropertyName = "RecCard";
            this.recCardDataGridViewTextBoxColumn.HeaderText = "RecCard";
            this.recCardDataGridViewTextBoxColumn.MaxInputLength = 20;
            this.recCardDataGridViewTextBoxColumn.Name = "recCardDataGridViewTextBoxColumn";
            this.recCardDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // memberDateDataGridViewTextBoxColumn
            // 
            this.memberDateDataGridViewTextBoxColumn.DataPropertyName = "MemberDate";
            this.memberDateDataGridViewTextBoxColumn.HeaderText = "MemberDate";
            this.memberDateDataGridViewTextBoxColumn.MaxInputLength = 20;
            this.memberDateDataGridViewTextBoxColumn.Name = "memberDateDataGridViewTextBoxColumn";
            this.memberDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // clubDuesPaidDataGridViewTextBoxColumn
            // 
            this.clubDuesPaidDataGridViewTextBoxColumn.DataPropertyName = "ClubDuesPaid";
            this.clubDuesPaidDataGridViewTextBoxColumn.HeaderText = "ClubDuesPaid";
            this.clubDuesPaidDataGridViewTextBoxColumn.Name = "clubDuesPaidDataGridViewTextBoxColumn";
            this.clubDuesPaidDataGridViewTextBoxColumn.ReadOnly = true;
            this.clubDuesPaidDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clubDuesPaidDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // clubDuesPaidDateDataGridViewTextBoxColumn
            // 
            this.clubDuesPaidDateDataGridViewTextBoxColumn.DataPropertyName = "ClubDuesPaidDate";
            this.clubDuesPaidDateDataGridViewTextBoxColumn.HeaderText = "ClubDuesPaidDate";
            this.clubDuesPaidDateDataGridViewTextBoxColumn.MaxInputLength = 20;
            this.clubDuesPaidDateDataGridViewTextBoxColumn.Name = "clubDuesPaidDateDataGridViewTextBoxColumn";
            this.clubDuesPaidDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // recDuesPaidDataGridViewTextBoxColumn
            // 
            this.recDuesPaidDataGridViewTextBoxColumn.DataPropertyName = "RecDuesPaid";
            this.recDuesPaidDataGridViewTextBoxColumn.HeaderText = "RecDuesPaid";
            this.recDuesPaidDataGridViewTextBoxColumn.Name = "recDuesPaidDataGridViewTextBoxColumn";
            this.recDuesPaidDataGridViewTextBoxColumn.ReadOnly = true;
            this.recDuesPaidDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.recDuesPaidDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(13, 24);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblDelete
            // 
            this.lblDelete.AutoSize = true;
            this.lblDelete.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDelete.ForeColor = System.Drawing.Color.Red;
            this.lblDelete.Location = new System.Drawing.Point(94, 27);
            this.lblDelete.Name = "lblDelete";
            this.lblDelete.Size = new System.Drawing.Size(454, 20);
            this.lblDelete.TabIndex = 2;
            this.lblDelete.Text = "Records with DELETE checked will be removed from database!";
            // 
            // unpaidMemberBindingSource
            // 
            this.unpaidMemberBindingSource.DataSource = typeof(WoodClub.UnpaidMember);
            // 
            // scwForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.lblDelete);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dataGridView1);
            this.Name = "scwForm";
            this.Text = "SCW Paid Members";
            this.Load += new System.EventHandler(this.scwForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unpaidMemberBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bsUnpaid;
        private System.Windows.Forms.BindingSource unpaidMemberBindingSource;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn deleteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn badgeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zipDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn recCardDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clubDuesPaidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clubDuesPaidDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn recDuesPaidDataGridViewTextBoxColumn;
    }
}
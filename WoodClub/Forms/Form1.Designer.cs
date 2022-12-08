namespace WoodClub
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.monitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sCWPaidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyClubUsageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lockerSummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailySummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.memberRosterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.woodclubDataSet = new WoodClub.WoodclubDataSet();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.badgeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zipDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recCardDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lockerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memberDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qBmodifiedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exemptDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.exemptModDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extHourDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.earlyAMDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clubDuesPaidDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clubDuesPaidDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recDuesPaidDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.creditBankDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entryCodesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.authorizedTimeZoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.authorizedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.oneTimeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lastDayValidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newBadgeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.photoDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.memberRosterTableAdapter = new WoodClub.WoodclubDataSetTableAdapters.MemberRosterTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memberRosterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.woodclubDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.badgeDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.addressDataGridViewTextBoxColumn,
            this.cityDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.zipDataGridViewTextBoxColumn,
            this.phoneDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.recCardDataGridViewTextBoxColumn,
            this.lockerDataGridViewTextBoxColumn,
            this.memberDateDataGridViewTextBoxColumn,
            this.qBmodifiedDataGridViewTextBoxColumn,
            this.exemptDataGridViewCheckBoxColumn,
            this.exemptModDateDataGridViewTextBoxColumn,
            this.extHourDataGridViewCheckBoxColumn,
            this.earlyAMDataGridViewCheckBoxColumn,
            this.clubDuesPaidDataGridViewCheckBoxColumn,
            this.clubDuesPaidDateDataGridViewTextBoxColumn,
            this.recDuesPaidDataGridViewCheckBoxColumn,
            this.creditBankDataGridViewTextBoxColumn,
            this.cardNoDataGridViewTextBoxColumn,
            this.entryCodesDataGridViewTextBoxColumn,
            this.authorizedTimeZoneDataGridViewTextBoxColumn,
            this.authorizedDataGridViewCheckBoxColumn,
            this.oneTimeDataGridViewCheckBoxColumn,
            this.lastDayValidDataGridViewTextBoxColumn,
            this.newBadgeDataGridViewCheckBoxColumn,
            this.photoDataGridViewImageColumn});
            this.dataGridView1.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.memberRosterBindingSource, "id", true));
            this.dataGridView1.DataSource = this.memberRosterBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1455, 600);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.bsMembers_PositionChanged);
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.BindingSource = this.memberRosterBindingSource;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.toolStripButton1,
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.toolStripTextBox1});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(1458, 25);
            this.bindingNavigator1.TabIndex = 1;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.DeleteItemClick);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(67, 22);
            this.toolStripLabel1.Text = "New Badge";
            this.toolStripLabel1.Click += new System.EventHandler(this.btnRFcard_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monitorToolStripMenuItem,
            this.sCWPaidToolStripMenuItem,
            this.monthlyClubUsageToolStripMenuItem,
            this.lockerSummaryToolStripMenuItem,
            this.dailySummaryToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripDropDownButton1.Text = "Reports";
            // 
            // monitorToolStripMenuItem
            // 
            this.monitorToolStripMenuItem.Name = "monitorToolStripMenuItem";
            this.monitorToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.monitorToolStripMenuItem.Text = "Monitor";
            this.monitorToolStripMenuItem.Click += new System.EventHandler(this.monitorsToolStripMenuItem_Click);
            // 
            // sCWPaidToolStripMenuItem
            // 
            this.sCWPaidToolStripMenuItem.Name = "sCWPaidToolStripMenuItem";
            this.sCWPaidToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.sCWPaidToolStripMenuItem.Text = "SCW Paid";
            this.sCWPaidToolStripMenuItem.Click += new System.EventHandler(this.sCWPaidListToolStripMenuItem_Click);
            // 
            // monthlyClubUsageToolStripMenuItem
            // 
            this.monthlyClubUsageToolStripMenuItem.Name = "monthlyClubUsageToolStripMenuItem";
            this.monthlyClubUsageToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.monthlyClubUsageToolStripMenuItem.Text = "Monthly Club Usage";
            this.monthlyClubUsageToolStripMenuItem.Click += new System.EventHandler(this.monthlyClubUsageToolStripMenuItem_Click);
            // 
            // lockerSummaryToolStripMenuItem
            // 
            this.lockerSummaryToolStripMenuItem.Name = "lockerSummaryToolStripMenuItem";
            this.lockerSummaryToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.lockerSummaryToolStripMenuItem.Text = "Locker Summary";
            this.lockerSummaryToolStripMenuItem.Click += new System.EventHandler(this.lockerSummaryToolStripMenuItem_Click);
            // 
            // dailySummaryToolStripMenuItem
            // 
            this.dailySummaryToolStripMenuItem.Name = "dailySummaryToolStripMenuItem";
            this.dailySummaryToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.dailySummaryToolStripMenuItem.Text = "Daily Summary";
            this.dailySummaryToolStripMenuItem.Click += new System.EventHandler(this.dailySummaryToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel2.Image")));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(67, 22);
            this.toolStripLabel2.Text = "Badge =";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBox1.ToolTipText = "Enter badge number to find, then enter key...";
            this.toolStripTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // memberRosterBindingSource
            // 
            this.memberRosterBindingSource.DataMember = "MemberRoster";
            this.memberRosterBindingSource.DataSource = this.woodclubDataSet;
            // 
            // woodclubDataSet
            // 
            this.woodclubDataSet.DataSetName = "WoodclubDataSet";
            this.woodclubDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // badgeDataGridViewTextBoxColumn
            // 
            this.badgeDataGridViewTextBoxColumn.DataPropertyName = "Badge";
            this.badgeDataGridViewTextBoxColumn.HeaderText = "Badge";
            this.badgeDataGridViewTextBoxColumn.Name = "badgeDataGridViewTextBoxColumn";
            this.badgeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstNameDataGridViewTextBoxColumn
            // 
            this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.HeaderText = "FirstName";
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
            // cityDataGridViewTextBoxColumn
            // 
            this.cityDataGridViewTextBoxColumn.DataPropertyName = "City";
            this.cityDataGridViewTextBoxColumn.HeaderText = "City";
            this.cityDataGridViewTextBoxColumn.Name = "cityDataGridViewTextBoxColumn";
            this.cityDataGridViewTextBoxColumn.ReadOnly = true;
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
            this.zipDataGridViewTextBoxColumn.Name = "zipDataGridViewTextBoxColumn";
            this.zipDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // phoneDataGridViewTextBoxColumn
            // 
            this.phoneDataGridViewTextBoxColumn.DataPropertyName = "Phone";
            this.phoneDataGridViewTextBoxColumn.HeaderText = "Phone";
            this.phoneDataGridViewTextBoxColumn.Name = "phoneDataGridViewTextBoxColumn";
            this.phoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // recCardDataGridViewTextBoxColumn
            // 
            this.recCardDataGridViewTextBoxColumn.DataPropertyName = "RecCard";
            this.recCardDataGridViewTextBoxColumn.HeaderText = "RecCard";
            this.recCardDataGridViewTextBoxColumn.Name = "recCardDataGridViewTextBoxColumn";
            this.recCardDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lockerDataGridViewTextBoxColumn
            // 
            this.lockerDataGridViewTextBoxColumn.DataPropertyName = "Locker";
            this.lockerDataGridViewTextBoxColumn.HeaderText = "Locker";
            this.lockerDataGridViewTextBoxColumn.Name = "lockerDataGridViewTextBoxColumn";
            this.lockerDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // memberDateDataGridViewTextBoxColumn
            // 
            this.memberDateDataGridViewTextBoxColumn.DataPropertyName = "MemberDate";
            this.memberDateDataGridViewTextBoxColumn.HeaderText = "MemberDate";
            this.memberDateDataGridViewTextBoxColumn.Name = "memberDateDataGridViewTextBoxColumn";
            this.memberDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qBmodifiedDataGridViewTextBoxColumn
            // 
            this.qBmodifiedDataGridViewTextBoxColumn.DataPropertyName = "QBmodified";
            this.qBmodifiedDataGridViewTextBoxColumn.HeaderText = "QBmodified";
            this.qBmodifiedDataGridViewTextBoxColumn.Name = "qBmodifiedDataGridViewTextBoxColumn";
            this.qBmodifiedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // exemptDataGridViewCheckBoxColumn
            // 
            this.exemptDataGridViewCheckBoxColumn.DataPropertyName = "Exempt";
            this.exemptDataGridViewCheckBoxColumn.HeaderText = "Exempt";
            this.exemptDataGridViewCheckBoxColumn.Name = "exemptDataGridViewCheckBoxColumn";
            this.exemptDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // exemptModDateDataGridViewTextBoxColumn
            // 
            this.exemptModDateDataGridViewTextBoxColumn.DataPropertyName = "ExemptModDate";
            this.exemptModDateDataGridViewTextBoxColumn.HeaderText = "ExemptModDate";
            this.exemptModDateDataGridViewTextBoxColumn.Name = "exemptModDateDataGridViewTextBoxColumn";
            this.exemptModDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // extHourDataGridViewCheckBoxColumn
            // 
            this.extHourDataGridViewCheckBoxColumn.DataPropertyName = "ExtHour";
            this.extHourDataGridViewCheckBoxColumn.HeaderText = "ExtHour";
            this.extHourDataGridViewCheckBoxColumn.Name = "extHourDataGridViewCheckBoxColumn";
            this.extHourDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // earlyAMDataGridViewCheckBoxColumn
            // 
            this.earlyAMDataGridViewCheckBoxColumn.DataPropertyName = "EarlyAM";
            this.earlyAMDataGridViewCheckBoxColumn.HeaderText = "EarlyAM";
            this.earlyAMDataGridViewCheckBoxColumn.Name = "earlyAMDataGridViewCheckBoxColumn";
            this.earlyAMDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // clubDuesPaidDataGridViewCheckBoxColumn
            // 
            this.clubDuesPaidDataGridViewCheckBoxColumn.DataPropertyName = "ClubDuesPaid";
            this.clubDuesPaidDataGridViewCheckBoxColumn.HeaderText = "ClubDuesPaid";
            this.clubDuesPaidDataGridViewCheckBoxColumn.Name = "clubDuesPaidDataGridViewCheckBoxColumn";
            this.clubDuesPaidDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // clubDuesPaidDateDataGridViewTextBoxColumn
            // 
            this.clubDuesPaidDateDataGridViewTextBoxColumn.DataPropertyName = "ClubDuesPaidDate";
            this.clubDuesPaidDateDataGridViewTextBoxColumn.HeaderText = "ClubDuesPaidDate";
            this.clubDuesPaidDateDataGridViewTextBoxColumn.Name = "clubDuesPaidDateDataGridViewTextBoxColumn";
            this.clubDuesPaidDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // recDuesPaidDataGridViewCheckBoxColumn
            // 
            this.recDuesPaidDataGridViewCheckBoxColumn.DataPropertyName = "RecDuesPaid";
            this.recDuesPaidDataGridViewCheckBoxColumn.HeaderText = "RecDuesPaid";
            this.recDuesPaidDataGridViewCheckBoxColumn.Name = "recDuesPaidDataGridViewCheckBoxColumn";
            this.recDuesPaidDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // creditBankDataGridViewTextBoxColumn
            // 
            this.creditBankDataGridViewTextBoxColumn.DataPropertyName = "CreditBank";
            this.creditBankDataGridViewTextBoxColumn.HeaderText = "CreditBank";
            this.creditBankDataGridViewTextBoxColumn.Name = "creditBankDataGridViewTextBoxColumn";
            this.creditBankDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cardNoDataGridViewTextBoxColumn
            // 
            this.cardNoDataGridViewTextBoxColumn.DataPropertyName = "CardNo";
            this.cardNoDataGridViewTextBoxColumn.HeaderText = "CardNo";
            this.cardNoDataGridViewTextBoxColumn.Name = "cardNoDataGridViewTextBoxColumn";
            this.cardNoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // entryCodesDataGridViewTextBoxColumn
            // 
            this.entryCodesDataGridViewTextBoxColumn.DataPropertyName = "EntryCodes";
            this.entryCodesDataGridViewTextBoxColumn.HeaderText = "EntryCodes";
            this.entryCodesDataGridViewTextBoxColumn.Name = "entryCodesDataGridViewTextBoxColumn";
            this.entryCodesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // authorizedTimeZoneDataGridViewTextBoxColumn
            // 
            this.authorizedTimeZoneDataGridViewTextBoxColumn.DataPropertyName = "AuthorizedTimeZone";
            this.authorizedTimeZoneDataGridViewTextBoxColumn.HeaderText = "AuthorizedTimeZone";
            this.authorizedTimeZoneDataGridViewTextBoxColumn.Name = "authorizedTimeZoneDataGridViewTextBoxColumn";
            this.authorizedTimeZoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // authorizedDataGridViewCheckBoxColumn
            // 
            this.authorizedDataGridViewCheckBoxColumn.DataPropertyName = "Authorized";
            this.authorizedDataGridViewCheckBoxColumn.HeaderText = "Authorized";
            this.authorizedDataGridViewCheckBoxColumn.Name = "authorizedDataGridViewCheckBoxColumn";
            this.authorizedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // oneTimeDataGridViewCheckBoxColumn
            // 
            this.oneTimeDataGridViewCheckBoxColumn.DataPropertyName = "OneTime";
            this.oneTimeDataGridViewCheckBoxColumn.HeaderText = "OneTime";
            this.oneTimeDataGridViewCheckBoxColumn.Name = "oneTimeDataGridViewCheckBoxColumn";
            this.oneTimeDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // lastDayValidDataGridViewTextBoxColumn
            // 
            this.lastDayValidDataGridViewTextBoxColumn.DataPropertyName = "LastDayValid";
            this.lastDayValidDataGridViewTextBoxColumn.HeaderText = "LastDayValid";
            this.lastDayValidDataGridViewTextBoxColumn.Name = "lastDayValidDataGridViewTextBoxColumn";
            this.lastDayValidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // newBadgeDataGridViewCheckBoxColumn
            // 
            this.newBadgeDataGridViewCheckBoxColumn.DataPropertyName = "NewBadge";
            this.newBadgeDataGridViewCheckBoxColumn.HeaderText = "NewBadge";
            this.newBadgeDataGridViewCheckBoxColumn.Name = "newBadgeDataGridViewCheckBoxColumn";
            this.newBadgeDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // photoDataGridViewImageColumn
            // 
            this.photoDataGridViewImageColumn.DataPropertyName = "Photo";
            this.photoDataGridViewImageColumn.HeaderText = "Photo";
            this.photoDataGridViewImageColumn.Name = "photoDataGridViewImageColumn";
            this.photoDataGridViewImageColumn.ReadOnly = true;
            // 
            // memberRosterTableAdapter
            // 
            this.memberRosterTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1458, 661);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Woodclub Members";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memberRosterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.woodclubDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private WoodclubDataSet woodclubDataSet;
        private System.Windows.Forms.BindingSource memberRosterBindingSource;
        private WoodclubDataSetTableAdapters.MemberRosterTableAdapter memberRosterTableAdapter;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem monitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sCWPaidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monthlyClubUsageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lockerSummaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dailySummaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn badgeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zipDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn recCardDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lockerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn memberDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qBmodifiedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn exemptDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn exemptModDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn extHourDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn earlyAMDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clubDuesPaidDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clubDuesPaidDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn recDuesPaidDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creditBankDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn entryCodesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn authorizedTimeZoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn authorizedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn oneTimeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastDayValidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn newBadgeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn photoDataGridViewImageColumn;
    }
}


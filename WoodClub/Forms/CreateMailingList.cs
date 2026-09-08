using System;
using System.Windows.Forms;

namespace WoodClub.Forms
{
    /// <summary>
    /// A small dialog to create a new <see cref="MailingList"/> row. The user
    /// supplies a name (required) and an optional description; everything else is
    /// left to the database defaults.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class CreateMailingList : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets the MailingListId of the row created by this dialog. Only valid
        /// when <see cref="Form.DialogResult"/> is <see cref="DialogResult.OK"/>.
        /// </summary>
        public int NewListId { get; private set; }

        private readonly bool navigateToEditor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMailingList"/> class.
        /// After a successful save the mailing list editor is opened for the new
        /// list.
        /// </summary>
        public CreateMailingList() : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMailingList"/> class.
        /// </summary>
        /// <param name="navigateToEditor">
        /// When true, opens the mailing list editor for the new list after a
        /// successful save. Callers that are already in the editor pass false.
        /// </param>
        public CreateMailingList(bool navigateToEditor)
        {
            InitializeComponent();
            this.navigateToEditor = navigateToEditor;
        }

        /// <summary>
        /// Handles the Click event of the btnOk ("Save &amp; Edit") control.
        /// Validates the name, inserts the row via EF, then opens the mailing
        /// list editor for the new list before closing.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name for the list.");
                return;
            }

            try
            {
                using (WoodClubEntities context = new WoodClubEntities())
                {
                    MailingList ml = new MailingList
                    {
                        Name = txtName.Text.Trim(),
                        Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };

                    context.MailingLists.Add(ml);
                    context.SaveChanges();
                    NewListId = ml.MailingListId;
                }
            }
            catch (Exception ex)
            {
                log.Error("Create mailing list failed..", ex);
                MessageBox.Show("Create failed: " + ex.Message);
                return;
            }

            if (navigateToEditor)
            {
                Hide();
                MailingListEditor editor = new MailingListEditor(NewListId);
                try
                {
                    editor.ShowDialog();
                }
                finally
                {
                    editor.Dispose();
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control. Closes without
        /// saving.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

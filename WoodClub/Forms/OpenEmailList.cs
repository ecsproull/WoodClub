using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub.Forms
{
    /// <summary>
    /// Lists saved and previously-sent emails (title and creation date) so one
    /// can be reopened in the <see cref="MailComposer"/> for reuse.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class OpenEmailList : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenEmailList"/> class.
        /// </summary>
        public OpenEmailList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the OpenEmailList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OpenEmailList_Load(object sender, EventArgs e)
        {
            using (WoodClubEntities context = new WoodClubEntities())
            {
                List<Row> rows = (from s in context.SavedEmails
                                  orderby s.CreatedAt descending
                                  select new Row { SavedEmailId = s.SavedEmailId, Title = s.Subject, CreatedAt = s.CreatedAt }).ToList();

                dgvEmails.DataSource = rows;
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the dgvEmails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvEmails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                OpenSelected();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenSelected();
        }

        /// <summary>
        /// Opens the selected saved email in the mail composer, then closes this
        /// list.
        /// </summary>
        private void OpenSelected()
        {
            Row row = dgvEmails.CurrentRow?.DataBoundItem as Row;
            if (row == null)
            {
                MessageBox.Show("Please select an email to open.");
                return;
            }

            Hide();
            MailComposer frm = new MailComposer(row.SavedEmailId);
            try
            {
                frm.ShowDialog();
            }
            finally
            {
                frm.Dispose();
            }

            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// A row in the saved email grid.
        /// </summary>
        private class Row
        {
            public int SavedEmailId { get; set; }
            public string Title { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}

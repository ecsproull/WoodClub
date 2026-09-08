using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub.Forms
{
    /// <summary>
    /// Lets the user pick an active mailing list and move members between the
    /// "Available Members" grid (not on the list) and the "List Members" grid
    /// (subscribed to the list). Changes are held in memory and written to the
    /// database on Submit.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MailingListEditor : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SortableBindingList<MemberRow> blAvailable = new SortableBindingList<MemberRow>();
        private SortableBindingList<MemberRow> blMembers = new SortableBindingList<MemberRow>();
        private SortableBindingList<MemberRow> filteredAvailable = new SortableBindingList<MemberRow>();
        private readonly BindingSource bsAvailable = new BindingSource();
        private readonly BindingSource bsMembers = new BindingSource();

        private int? currentListId;
        private readonly int? initialListId;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailingListEditor"/> class.
        /// </summary>
        public MailingListEditor() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailingListEditor"/> class
        /// with a list pre-selected in the dropdown.
        /// </summary>
        /// <param name="initialListId">MailingListId to select on open, or null.</param>
        public MailingListEditor(int? initialListId)
        {
            InitializeComponent();
            this.initialListId = initialListId;
        }

        /// <summary>
        /// Handles the Load event of the MailingListEditor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MailingListEditor_Load(object sender, EventArgs e)
        {
            dgvAvailable.DataSource = bsAvailable;
            dgvMembers.DataSource = bsMembers;
            txtFilter.KeyUp += TextBoxFilter_KeyUp;

            LoadLists(initialListId);
        }

        /// <summary>
        /// Loads the active mailing lists into the selector. Also useful to reload
        /// the list after adding a new one.
        /// </summary>
        private void LoadLists(int? selectId = null)
        {
            using (WoodClubEntities context = new WoodClubEntities())
            {
                List<ListItem> lists = (from l in context.MailingLists
                                        where l.IsActive
                                        orderby l.Name
                                        select new ListItem { MailingListId = l.MailingListId, Name = l.Name }).ToList();

                cbLists.DataSource = lists;
                cbLists.DisplayMember = "Name";
                cbLists.ValueMember = "MailingListId";
            }

            if (selectId.HasValue)
            {
                cbLists.SelectedValue = selectId.Value;
            }

            LoadGridsForCurrentList();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbLists control. Reloads
        /// both grids for the newly selected list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridsForCurrentList();
        }

        /// <summary>
        /// Loads the "Available Members" and "List Members" grids for the list
        /// currently selected in the dropdown. Any pending in-memory moves are
        /// discarded.
        /// </summary>
        private void LoadGridsForCurrentList()
        {
            currentListId = cbLists.SelectedValue as int?;
            txtFilter.Text = string.Empty;

            if (currentListId == null)
            {
                blAvailable = new SortableBindingList<MemberRow>();
                blMembers = new SortableBindingList<MemberRow>();
                bsAvailable.DataSource = blAvailable;
                bsMembers.DataSource = blMembers;
                return;
            }

            using (WoodClubEntities context = new WoodClubEntities())
            {
                int listId = currentListId.Value;
                HashSet<int> subscribed = new HashSet<int>(
                    from mlm in context.MailingListMembers
                    where mlm.MailingListId == listId && mlm.IsSubscribed
                    select mlm.MemberId);

                List<MemberRow> allMembers = (from m in context.MemberRosters
                                              select new MemberRow
                                              {
                                                  MemberId = m.id,
                                                  FirstName = m.FirstName,
                                                  LastName = m.LastName,
                                                  Badge = m.Badge,
                                                  Email = m.Email
                                              }).ToList();

                blMembers = new SortableBindingList<MemberRow>(allMembers
                    .Where(m => subscribed.Contains(m.MemberId))
                    .OrderBy(m => m.LastName).ThenBy(m => m.FirstName).ToList());
                blAvailable = new SortableBindingList<MemberRow>(allMembers
                    .Where(m => !subscribed.Contains(m.MemberId))
                    .OrderBy(m => m.LastName).ThenBy(m => m.FirstName).ToList());
            }

            bsMembers.DataSource = blMembers;
            setBsAvailableDataSource();
        }

        /// <summary>
        /// Handles the KeyUp event of the filter TextBox. This is the filter
        /// control handler, matching the mechanism used on MainMembers.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void TextBoxFilter_KeyUp(object sender, KeyEventArgs e)
        {
            setBsAvailableDataSource();
        }

        /// <summary>
        /// Sets or clears the "Available Members" binding source data source. When
        /// entering text into the filter box, this is where the updating of the
        /// left grid takes place.
        /// </summary>
        private void setBsAvailableDataSource()
        {
            string filter = txtFilter.Text;
            if (filter == string.Empty)
            {
                bsAvailable.DataSource = blAvailable;
            }
            else
            {
                filteredAvailable = new SortableBindingList<MemberRow>(blAvailable.Where(
                    x => (x.FirstName ?? string.Empty).ToUpper().Contains(filter.ToUpper()) ||
                    (x.LastName ?? string.Empty).ToUpper().Contains(filter.ToUpper()) ||
                    (x.Badge ?? string.Empty).ToUpper().Contains(filter.ToUpper()) ||
                    (x.Email ?? string.Empty).ToLower().Contains(filter.ToLower())).ToList());
                bsAvailable.DataSource = filteredAvailable;
                dgvAvailable.Refresh();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnNewList control. Opens the create
        /// list dialog and, on success, reloads the selector with the new list
        /// selected.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnNewList_Click(object sender, EventArgs e)
        {
            CreateMailingList frm = new CreateMailingList(false);
            try
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadLists(frm.NewListId);
                }
            }
            finally
            {
                frm.Dispose();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDeleteList control. After a
        /// confirmation prompt, deletes the selected mailing list and removes all
        /// of its member rows.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDeleteList_Click(object sender, EventArgs e)
        {
            if (currentListId == null)
            {
                return;
            }

            ListItem selected = cbLists.SelectedItem as ListItem;
            string name = selected != null ? selected.Name : "this list";

            if (MessageBox.Show(
                    "Delete the mailing list \"" + name + "\" and remove all of its members?\r\n\r\nThis cannot be undone.",
                    "Delete Mailing List", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (WoodClubEntities context = new WoodClubEntities())
                {
                    int listId = currentListId.Value;

                    List<MailingListMember> members = context.MailingListMembers
                        .Where(m => m.MailingListId == listId).ToList();
                    context.MailingListMembers.RemoveRange(members);

                    MailingList list = context.MailingLists.Find(listId);
                    if (list != null)
                    {
                        context.MailingLists.Remove(list);
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error("Mailing list delete failed..", ex);
                MessageBox.Show("Delete failed: " + ex.Message);
                return;
            }

            LoadLists();
        }

        /// <summary>
        /// Handles the CellMouseDoubleClick event of the dgvAvailable control.
        /// Moves the double-clicked member onto the list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void dgvAvailable_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            MoveToMembers(dgvAvailable.Rows[e.RowIndex].DataBoundItem as MemberRow);
        }

        /// <summary>
        /// Handles the CellMouseDoubleClick event of the dgvMembers control.
        /// Moves the double-clicked member back off the list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void dgvMembers_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            MoveToAvailable(dgvMembers.Rows[e.RowIndex].DataBoundItem as MemberRow);
        }

        /// <summary>
        /// Handles the Click event of the btnMoveRight control. Moves every
        /// checked member in the left grid onto the list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            dgvAvailable.EndEdit();
            foreach (MemberRow row in blAvailable.Where(r => r.Selected).ToList())
            {
                MoveToMembers(row);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnMoveLeft control. Moves every checked
        /// member in the right grid back off the list.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            dgvMembers.EndEdit();
            foreach (MemberRow row in blMembers.Where(r => r.Selected).ToList())
            {
                MoveToAvailable(row);
            }
        }

        /// <summary>
        /// Moves a member from the "Available Members" grid to the "List Members"
        /// grid in memory.
        /// </summary>
        private void MoveToMembers(MemberRow row)
        {
            if (row == null || !blAvailable.Contains(row))
            {
                return;
            }

            row.Selected = false;
            blAvailable.Remove(row);
            blMembers.Add(row);
            setBsAvailableDataSource();
        }

        /// <summary>
        /// Moves a member from the "List Members" grid back to the "Available
        /// Members" grid in memory.
        /// </summary>
        private void MoveToAvailable(MemberRow row)
        {
            if (row == null || !blMembers.Contains(row))
            {
                return;
            }

            row.Selected = false;
            blMembers.Remove(row);
            blAvailable.Add(row);
            setBsAvailableDataSource();
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control. Persists all pending
        /// membership changes for the selected list via EF, then closes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (currentListId == null)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            try
            {
                using (WoodClubEntities context = new WoodClubEntities())
                {
                    int listId = currentListId.Value;
                    HashSet<int> desired = new HashSet<int>(blMembers.Select(m => m.MemberId));
                    List<MailingListMember> existing = context.MailingListMembers
                        .Where(m => m.MailingListId == listId).ToList();

                    foreach (int memberId in desired)
                    {
                        MailingListMember mlm = existing.FirstOrDefault(m => m.MemberId == memberId);
                        if (mlm == null)
                        {
                            context.MailingListMembers.Add(new MailingListMember
                            {
                                MailingListId = listId,
                                MemberId = memberId,
                                AddedAt = DateTime.UtcNow,
                                IsSubscribed = true
                            });
                        }
                        else if (!mlm.IsSubscribed)
                        {
                            mlm.IsSubscribed = true;
                        }
                    }

                    foreach (MailingListMember mlm in existing)
                    {
                        if (mlm.IsSubscribed && !desired.Contains(mlm.MemberId))
                        {
                            mlm.IsSubscribed = false;
                        }
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error("Mailing list save failed..", ex);
                MessageBox.Show("Save failed: " + ex.Message);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control. Discards any unsaved
        /// in-memory changes and closes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// A mailing list entry for the selector dropdown.
        /// </summary>
        private class ListItem
        {
            public int MailingListId { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// A member row shown in the two grids.
        /// </summary>
        private class MemberRow
        {
            public bool Selected { get; set; }
            public int MemberId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Badge { get; set; }
            public string Email { get; set; }
        }
    }
}

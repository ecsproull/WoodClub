using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// From to edit locker data. Add, Delete and update are supported.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class LockerData : Form
	{
		List<Locker> mLockers;
		private int originalCount = -1;
		private WoodClubEntities context = new WoodClubEntities();

		/// <summary>
		/// Initializes a new instance of the <see cref="LockerData"/> class.
		/// </summary>
		public LockerData()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Handles the Load event of the LockerData control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void LockerData_Load(object sender, EventArgs e)
		{
			LoadLockers();
			dataGridLockerData.CellEndEdit += dataGridView_CellEndEdit;
			dataGridLockerData.CellValidating += dataGridView_CellValidating;
		}

		/// <summary>
		/// Loads the locker data from the database.
		/// </summary>
		private void LoadLockers()
		{
			this.mLockers = (from l in context.Lockers
							 select l).ToList();
			this.originalCount = mLockers.Count;
			List<LockerLocation> lockerLocationList = (from ll in context.LockerLocations
													   select ll).ToList();
			bs_LockerLocationSelect.DataSource = lockerLocationList;
			this.LocationSelect.ValueMember = "Location";
			this.LocationSelect.DisplayMember = "Description";
			bs_Lockers.DataSource = new SortableBindingList<Locker>(this.mLockers);

			for (int i = 0; i < originalCount; i++)
			{
				dataGridLockerData.Rows[i].Cells[0].ReadOnly = true;
				dataGridLockerData.Rows[i].Cells[1].ReadOnly = true;
			}
		}

		/// <summary>
		/// Handles the CellValidating event of the dataGridView control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellValidatingEventArgs"/> instance containing the event data.</param>
		private void dataGridView_CellValidating(object sender,
			DataGridViewCellValidatingEventArgs e)
		{
			if (e.ColumnIndex == 0)
			{
				if (e.FormattedValue.ToString() == string.Empty || e.RowIndex < this.originalCount)
					return;

				bool match = Regex.IsMatch(e.FormattedValue.ToString(), "^[A-Z]*-[0-9.]*$", RegexOptions.Multiline);
				if (!match)
				{
					MessageBox.Show("Locker Title begin with capitol letters, then a dash followed by numbers. XX-1234");
					e.Cancel = true;
				}
				else
				{
					string[] parts = e.FormattedValue.ToString().Split('-');
					string code = parts[0];
					LockerCost lockerCost = (from lc in context.LockerCosts
											 where lc.Code == code
											 select lc).FirstOrDefault();
					if (lockerCost == null)
					{
						MessageBox.Show("Create a Locker Cost for " + parts[0] + "before adding this locker");
						e.Cancel = true;
						return;
					}

					for (int i = 0; i < this.mLockers.Count; i++)
					{
						if (i == e.RowIndex)
							continue;

						if (this.mLockers[i].LockerTitle == e.FormattedValue.ToString())
						{
							MessageBox.Show("Locker Title: " + e.FormattedValue.ToString() + " already exists");
							e.Cancel = true;
						}
					}
				}
			}
		}

		/// <summary>
		/// Handles the CellEndEdit event of the dataGridView control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= this.originalCount &&
				e.ColumnIndex == 0 &&
				dataGridLockerData.Rows[e.RowIndex].Cells[0].Value != null)
			{
				string newLocker = dataGridLockerData.Rows[e.RowIndex].Cells[0].Value.ToString();
				string[] parts = newLocker.Split('-');
				dataGridLockerData.Rows[e.RowIndex].Cells[1].Value = parts[0];
				dataGridLockerData.Rows[e.RowIndex].Cells[1].ReadOnly = true;
				dataGridLockerData.Rows[e.RowIndex].Cells[2].Value = "TBD";
				DataGridViewCell project = dataGridLockerData.Rows[e.RowIndex].Cells[3];

				context.Lockers.Add(new Locker
				{
					LockerTitle = newLocker,
					LocationCode = "TBD",
					Code = parts[0],
					Badge = String.Empty,
					Project = project.Value == null ? "" : project.Value.ToString()
				});
				context.SaveChanges();
				LoadLockers();
			}
		}

		/// <summary>
		/// Applies the changes.
		/// </summary>
		private void ApplyChanges()
		{
			List<string> toDelete = new List<string>();
			foreach (DataGridViewRow dgvr in dataGridLockerData.Rows)
			{
				if (dgvr.Cells[4].Value != null && (bool)dgvr.Cells[4].Value == true)
				{
					toDelete.Add(dgvr.Cells[0].Value.ToString());
				}
			}
			context.SaveChanges();

			foreach (string lockerTitle in toDelete)
			{
				Locker lockerData = (from ld in context.Lockers
									 where ld.LockerTitle == lockerTitle
									 select ld).FirstOrDefault();
				context.Lockers.Remove(lockerData);
			}

			if (toDelete.Count > 0)
			{
				context.SaveChanges();
			}
		}

		/// <summary>
		/// Handles the Click event of the Save button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void buttonSave_Click(object sender, EventArgs e)
		{
			ApplyChanges();
			DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// Handles the Click event of the Cancel button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		/// <summary>
		/// Handles the Click event of the Apply button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void buttonApply_Click(object sender, EventArgs e)
		{
			ApplyChanges();
			LoadLockers();
		}
	}
}

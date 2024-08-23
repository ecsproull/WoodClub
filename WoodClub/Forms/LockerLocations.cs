using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Form used to edit the list of locker locations.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class LockerLocations : Form
	{
		List<LockerLocation> mLockerLocations;
		private int originalCount = -1;
		private WoodClubEntities context = new WoodClubEntities();

		/// <summary>
		/// Initializes a new instance of the <see cref="LockerLocations"/> class.
		/// </summary>
		public LockerLocations()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Handles the Load event of the LockerLocations control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void LockerLocations_Load(object sender, EventArgs e)
		{
			this.mLockerLocations = (from ll in context.LockerLocations
									 select ll).ToList();
			this.originalCount = mLockerLocations.Count();
			bs_LockerLocation.DataSource = mLockerLocations;
			for (int i = 0; i < originalCount; i++)
			{
				dataGridLockerLocationEdit.Rows[i].Cells[0].ReadOnly = true;
			}
		}

		/// <summary>
		/// Handles the Click event of the Save button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void buttonSave_Click(object sender, EventArgs e)
		{
			for (int i = originalCount; i < mLockerLocations.Count; i++)
			{
				context.LockerLocations.Add(mLockerLocations[i]);
			}

			context.SaveChanges();
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
	}
}

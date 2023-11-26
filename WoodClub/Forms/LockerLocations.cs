using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class LockerLocations : Form
	{
		List<LockerLocation> mLockerLocations;
		private int originalCount = -1;
		private WoodClubEntities context = new WoodClubEntities();
		public LockerLocations()
		{
			InitializeComponent();
		}

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

		private void buttonSave_Click(object sender, EventArgs e)
		{
			for (int i = originalCount; i < mLockerLocations.Count; i++)
			{
				context.LockerLocations.Add(mLockerLocations[i]);
			}

			context.SaveChanges();
			DialogResult = DialogResult.OK;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}

	public partial class LockerLocationBind
	{
		public string LocationKey { get; set; }
		public string Description { get; set; }
	}
}

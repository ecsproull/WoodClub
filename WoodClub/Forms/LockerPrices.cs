using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class LockerPrices : Form
	{
		WoodClubEntities context = new WoodClubEntities();
		List<LockerCost> lockerCostList;
		int originalCount;
		public LockerPrices()
		{
			InitializeComponent();
			dataGridPrices.EditingControlShowing += dataGridPrices_EditingControlShowing;
		}

		private void LockerPrices_Load(object sender, EventArgs e)
		{
			LoadCostData();
			dataGridPrices.CellEndEdit += dataGridView_CellEndEdit;
			dataGridPrices.CellValidating += dataGridView_CellValidating;
		}

		private void LoadCostData()
		{
			this.lockerCostList = (from lc in context.LockerCosts
								   select lc).ToList();
			this.originalCount = this.lockerCostList.Count;
			this.bs_LockerPrices.DataSource = new SortableBindingList<LockerCost>(lockerCostList);
			for (int i = 0; i < originalCount; i++)
			{
				dataGridPrices.Rows[i].Cells[0].ReadOnly = true;
			}
		}

		private void dataGridPrices_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
			if (dataGridPrices.CurrentCell.ColumnIndex == 1) //Desired Column
			{
				TextBox tb = e.Control as TextBox;
				if (tb != null)
				{
					tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
				}
			}
		}

		private void dataGridView_CellValidating(object sender,
			DataGridViewCellValidatingEventArgs e)
		{
			if (e.ColumnIndex == 0)
			{
				if (e.FormattedValue.ToString() == string.Empty || e.RowIndex < this.originalCount)
					return;

				bool match = Regex.IsMatch(e.FormattedValue.ToString(), "^[A-Z]*$", RegexOptions.Multiline);
				if (!match)
				{
					MessageBox.Show("Prefixs can only be capitol letters. e.g. ABC");
					e.Cancel = true;
				}

				string priceCode = e.FormattedValue.ToString();
				var locker = (from lp in context.LockerCosts
							  where lp.Code == priceCode
							  select lp).FirstOrDefault();
				if (locker != null)
				{
					MessageBox.Show("Prefixs already exists.");
					e.Cancel = true;
				}
			}
		}

		private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= this.originalCount &&
				e.ColumnIndex == 0 &&
				dataGridPrices.Rows[e.RowIndex].Cells[0].Value != null)
			{
				string newPriceCode = dataGridPrices.Rows[e.RowIndex].Cells[0].Value.ToString();
				context.LockerCosts.Add(new LockerCost
				{
					Code = newPriceCode,
					Cost = 0
				});
				context.SaveChanges();
				LoadCostData();
			}
		}

		private void ApplyChanges()
		{
			List<string> toDelete = new List<string>();
			foreach (DataGridViewRow dgvr in dataGridPrices.Rows)
			{
				if (dgvr.Cells[2].Value != null && (bool)dgvr.Cells[2].Value == true)
				{
					string priceCode = dgvr.Cells[0].Value.ToString();
					var locker = (from lp in context.Lockers
								  where lp.Code == priceCode
								  select lp).FirstOrDefault();
					if (locker != null)
					{
						MessageBox.Show("Remove all lockers that reference this code before you can remove it");
					}
					else
					{
						toDelete.Add(dgvr.Cells[0].Value.ToString());
					}
				}
			}
			context.SaveChanges();

			foreach (string priceCode in toDelete)
			{
				LockerCost lockerCost = (from ld in context.LockerCosts
										 where ld.Code == priceCode
										 select ld).FirstOrDefault();
				context.LockerCosts.Remove(lockerCost);
			}

			if (toDelete.Count > 0)
			{
				context.SaveChanges();
			}
		}

		private void Column1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			ApplyChanges();
			DialogResult = DialogResult.OK;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void buttonApply_Click(object sender, EventArgs e)
		{
			ApplyChanges();
			LoadCostData();
		}
	}
}

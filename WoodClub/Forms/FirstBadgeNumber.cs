using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Used as a simple popup for the user to accept the starting 
	/// badge number when importing members from the list of this registered
	/// to take Orientation Calss.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class FirstBadgeNumber : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FirstBadgeNumber"/> class.
		/// </summary>
		public FirstBadgeNumber()
		{
			InitializeComponent();
			startingBadgeNumber.ValueChanged += StartingBadgeNumber_ValueChanged;
			using (WoodClubEntities context = new WoodClubEntities())
			{
				long nextBadge = (from m in context.MemberRosters
											 where m.Badge != "20001"
											 select m).ToList().Max(e => Convert.ToInt32(e.Badge));
				startingBadgeNumber.Value = ++nextBadge;
			}
		}

		/// <summary>
		/// Handles the ValueChanged event of the StartingBadgeNumber control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void StartingBadgeNumber_ValueChanged(object sender, EventArgs e)
		{
			this.BadgeNumber = (int)((NumericUpDown)sender).Value;
		}

		/// <summary>
		/// Handles the Click event of the OK button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void id_ok_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
		
		public int BadgeNumber {get;set;}

		/// <summary>
		/// Handles the Click event of the Skip button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void id_skip_Click(object sender, EventArgs e)
		{
			this.BadgeNumber = -1;
			this.DialogResult = DialogResult.OK;
		}
	}
}

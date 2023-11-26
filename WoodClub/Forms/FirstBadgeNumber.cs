using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoodClub.Forms
{
	public partial class FirstBadgeNumber : Form
	{
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

		private void StartingBadgeNumber_ValueChanged(object sender, EventArgs e)
		{
			this.BadgeNumber = (int)((NumericUpDown)sender).Value;
		}

		private void id_ok_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
		
		public int BadgeNumber {get;set;}

		private void id_skip_Click(object sender, EventArgs e)
		{
			this.BadgeNumber = -1;
			this.DialogResult = DialogResult.OK;
		}
	}
}

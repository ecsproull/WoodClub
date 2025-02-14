using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Unused Form! This was used to list all of the member that 
	/// were selected to have a new badge printed. Will keep the code in
	/// case we ever print directly from our database again.
	/// </summary>
	/// <seealso cref="System.Windows.Forms.Form" />
	public partial class RFBadge : Form
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger
				  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private List<MemberRFcard> DataSource;
		private BindingSource bsRFcards = new BindingSource();

		/// <summary>
		/// Initializes a new instance of the <see cref="RFBadge"/> class.
		/// </summary>
		public RFBadge()
		{
			InitializeComponent();
			dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
		}

		/// <summary>
		/// Handles the CellEndEdit event of the DataGridView1 control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
		private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			MemberRFcard rfnc = DataSource[e.RowIndex];
			using (WoodClubEntities context = new WoodClubEntities())
			{
				MemberRFcard newBadgeMember = (from rn in context.MemberRFcards
								where (string)rn.Badge == rfnc.Badge
								select rn).FirstOrDefault();
				newBadgeMember.FirstName = rfnc.FirstName;
				context.SaveChanges();
			}
		}

		/// <summary>
		/// Handles the Load event of the formRFbadge control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void formRFbadge_Load(object sender, EventArgs e)
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				try
				{
					DataSource = context.MemberRFcards.Select(c => c).ToList();
				}
				catch (Exception ex)
				{
					log.Fatal("Unable to get data...", ex);         // Capture exception
				}
			}
			BindingList<MemberRFcard> blRFcards = new BindingList<MemberRFcard>(DataSource);
			bsRFcards.DataSource = blRFcards;
			dataGridView1.DataSource = bsRFcards;
			bsRFcards.Position = 0;
			bindingNavigator1.BindingSource = bsRFcards;
		}


		/// <summary>
		/// Handles the Click event of the toolStripButton2 control.
		/// In case we want to add a new card.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			int id;
			using (WoodClubEntities context = new WoodClubEntities())
			{
				foreach (MemberRFcard c in DataSource)
				{
					var query = from rn in context.MemberRosters
								where (string)rn.Badge == c.Badge
								select rn;
					if (query.Any())     // Entered into member 
					{
						query.Single().NewBadge = false;
					}
					id = c.RECORD_NR;
					var entity = context.MemberRFcards.Find(id);
					if (id != 0 && entity != null)
					{
						context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
					}

				}
				context.SaveChanges();
				bsRFcards.Clear();
				dataGridView1.Invalidate();
			}
		}

		/// <summary>
		/// Handles the Click event of the Export button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void exportButton_Click(object sender, EventArgs e)
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				List<MemberRFcard> members = (from m in context.MemberRFcards
											  select m).ToList();
				Export(members);
			}
		}

		/// <summary>
		/// Exports the specified members.
		/// </summary>
		/// <param name="members">The members.</param>
		public static void Export(List<MemberRFcard> members)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("FirstName, LastName, Badge, Title, RecCard");
			foreach (MemberRFcard m in members)
			{
				if (m.Photo != null)
				{
					try
					{
						using (Image image = Image.FromStream(new MemoryStream(m.Photo)))
						{
							image.Save(@"c:\CardPrint\images\" + m.Badge + ".jpg", ImageFormat.Jpeg);
						}
					}
					catch (Exception)
					{
						MessageBox.Show("Photo Error, Badge : " + m.Badge);
					}
				}

				stringBuilder.AppendLine(string.Format("{0},{1},{2},{3},{4}",
					m.FirstName, m.LastName, m.Badge, m.Title, m.RecCard));
			}

			string filename = @"c:\CardPrint\CardsToPrint.csv";
			System.IO.File.WriteAllText(filename, stringBuilder.ToString());
			MessageBox.Show("All Done");
		}
	}
}

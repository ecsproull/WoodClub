using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WoodClub
{
    public partial class RFBadge : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<MemberRFcard> DataSource;
        private BindingSource bsRFcards = new BindingSource();
        public RFBadge()
        {
            InitializeComponent();
        }

        private void formRFbadge_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'woodclubDataSet.MemberRoster' table. You can move, or remove it, as needed.
            //this.memberRosterTableAdapter.Fill(this.woodclubDataSet.MemberRoster);
            using (WoodclubEntities context = new WoodclubEntities())
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


        //
        //  Here for new RF card - delete record
        //
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int id;
            using (WoodclubEntities context = new WoodclubEntities())
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
                woodclubDataSet.AcceptChanges();
                dataGridView1.Invalidate();
            }
        }
    }
}

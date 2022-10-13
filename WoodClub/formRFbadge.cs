using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WoodClub
{
    public partial class formRFbadge : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<MemberRFcard> DataSource;
        private BindingSource bsRFcards = new BindingSource();
        public formRFbadge()
        {
            InitializeComponent();
        }

        private void formRFbadge_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'woodclubDataSet1.MemberRFcard' table. You can move, or remove it, as needed.
            this.memberRFcardTableAdapter.Fill(this.woodclubDataSet1.MemberRFcard);
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

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            MemberRFcard current = (MemberRFcard)bsRFcards.Current;
            if(current != null)
            {
                log.Info("Deleted " + current.Badge);
                int id = current.RECORD_NR;
                using (WoodclubEntities context = new WoodclubEntities())
                {
                    try
                    {
                        var entity = context.MemberRFcards.Find(id);
                        var query = from rn in context.MemberRosters
                                    where rn.Badge == entity.Badge
                                    select rn;
                        query.Single().NewBadge = false;
                        context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                        woodclubDataSet1.AcceptChanges();
                        dataGridView1.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        log.Error("Delete failed..", ex);
                    }
                }
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int id;
            using (WoodclubEntities context = new WoodclubEntities())
            {
                foreach (MemberRFcard c in DataSource)
                {
                    var query = from rn in context.MemberRosters
                                where rn.Badge == c.Badge
                                select rn;
                    query.Single().NewBadge = false;
                    id = c.RECORD_NR;
                    var entity = context.MemberRFcards.Find(id);
                    if (id != 0 && entity != null)
                    {
                        context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    }        
                }
                context.SaveChanges();
                bsRFcards.Clear();
                woodclubDataSet1.AcceptChanges();
                dataGridView1.Invalidate();
            }
        }
    }
}

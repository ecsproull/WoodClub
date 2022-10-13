using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodClub
{
    public enum FilterOptions
    {
        StartsWith,
        Contains,
        EndsWith,
        Equals
    }
    // <param name="init"> false does not initialize list </param>
    public class Members : MemberRoster
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Members(bool init = false)
        {
            if (init)
            {
                GetMembers();
            }
        }

        public List<MemberRoster> DataSource { get; set; }
        public List<string> ColumnNames { get; set; }
        public List<string> BadgeList { get; set; }

        public void GetMembers()
        {
            using (WoodclubEntities context = new WoodclubEntities())
            {
                try
                {
                    DataSource = context.MemberRosters.Select(mem => mem)
                        .Distinct()
                        .OrderBy(mem => mem.Badge)
                        .ToList();
                }
                catch (Exception ex)
                {
                    log.Fatal("Unable to get data...", ex);         // Capture exception
                }
            }
            
        }
        //
        //  Validation
        //
        public string ValidationMessage { get; set; }

        //
        //  Remove record
        //
        public void Remove(int id)
        {
            using (WoodclubEntities context = new WoodclubEntities())
            {
                try
                {
                    var entity = context.MemberRosters.Find(id);
                    context.MemberRosters.Remove(entity);
                    context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    log.Error("Delete failed...", e);
                }
            }
        }
        //
        //  Update Member record
        //
        public bool UpdateMember(MemberRoster EditedMember)
        {

            using (WoodclubEntities context = new WoodclubEntities())
            {
                int _id = EditedMember.id;
                NewIdentifier = _id;
                try
                {        
                        var entity = context.MemberRosters.Find(_id);
                        context.Entry(entity).CurrentValues.SetValues(EditedMember);
                        context.SaveChanges();   
                        ValidationMessage = "";
                        return true;
                }
                catch(Exception ex)
                {
                    log.Error("Update failed..",ex);
                    return false;
                }
            }
        }
        public int NewIdentifier { get; set; }

        public bool AddNew(MemberRoster NewMember)
        {
            int _id = NewMember.id;
            using (WoodclubEntities context = new WoodclubEntities())
            {
                try
                {
                    context.MemberRosters.Add(NewMember);
                    context.SaveChanges();
                    ValidationMessage = "";
                    return true;
                }
                catch (Exception ex)
                {
                    ValidationMessage = "Failed adding new member.";
                    log.Error(ValidationMessage, ex);
                    return false;
                }
            }
        }
        /// <summary>
        /// Will revert modfied entries to their former values so if there are many changes
        /// they all will be reverted while in this demo project saves are done at once so this
        /// will not happen here but in other situtation this might be an issue.
        /// </summary>
        /// <param name="context"></param>
        public static void UndoPendingChanges(DbContext context)
        {
            //detect all changes (probably not required if AutoDetectChanges is set to true)
            context.ChangeTracker.DetectChanges();

            //get all entries that are changed
            var entries = context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();

            //somehow try to discard changes on every entry
            foreach (var dbEntityEntry in entries)
            {
                var entity = dbEntityEntry.Entity;

                if (entity == null) continue;

                if (dbEntityEntry.State == EntityState.Added)
                {
                    // if entity is in Added state, remove it. 
                    // (there will be problems with Set methods if entity is of proxy type, 
                    // in that case you need entity base type
                    var set = context.Set(entity.GetType());
                    set.Remove(entity);
                }
                else if (dbEntityEntry.State == EntityState.Modified)
                {
                    //entity is modified... you can set it to Unchanged or Reload it form Db??
                    dbEntityEntry.Reload();
                }
                else if (dbEntityEntry.State == EntityState.Deleted)
                    //entity is deleted...
                    dbEntityEntry.State = EntityState.Modified;
            }
        }
    }
}

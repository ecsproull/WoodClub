using System;
using System.Collections.Generic;
using System.Linq;

namespace WoodClub
{
    // <param name="init"> false does not initialize list </param>
    public class Members : MemberRoster
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                  (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string ValidationMessage { get; set; }
        public int NewIdentifier { get; set; }

        public Members(bool init = false)
        {
            if (init)
            {
                GetMembers();
            }
        }

        public List<MemberRoster> DataSource { get; set; }

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
    }
}

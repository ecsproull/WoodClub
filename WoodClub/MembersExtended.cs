using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodClub
{
    public class MembersExtended : MemberRoster
    {
        public MembersExtended(MemberRoster mr)
        {
            this.id = mr.id;
            this.Badge = mr.Badge;
            this.FirstName = mr.FirstName;
            this.LastName = mr.LastName;
            this.Address = mr.Address;
            this.City = mr.City;
            this.State = mr.State;
            this.Zip = mr.Zip;
            this.Phone = mr.Phone;
            this.Email = mr.Email;
            this.Title = mr.Title;
            this.RecCard = mr.RecCard;
            this.Locker = mr.Locker;
            this.MemberDate = mr.MemberDate;
            this.QBmodified = mr.QBmodified;
            this.Exempt = mr.Exempt;
            this.ExemptModDate = mr.ExemptModDate;
            this.ExtHour = mr.ExtHour;
            this.EarlyAM = mr.EarlyAM;
            this.ClubDuesPaid = mr.ClubDuesPaid;
            this.ClubDuesPaidDate = mr.ClubDuesPaidDate;
            this.RecDuesPaid = mr.RecDuesPaid;
            this.CreditBank = mr.CreditBank;
            this.CardNo = mr.CardNo;
            this.EntryCodes = mr.EntryCodes;
            this.AuthorizedTimeZone = mr.AuthorizedTimeZone;
            this.Authorized = mr.Authorized;
            this.OneTime = mr.OneTime;
            this.LastDayValid = mr.LastDayValid;
            this.NewBadge = mr.NewBadge;
            this.Photo = mr.Photo;
            this.GroupTime = mr.GroupTime;
            this.AdminBlock = mr.AdminBlock;
        }
    }
}

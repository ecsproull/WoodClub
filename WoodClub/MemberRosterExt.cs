namespace WoodClub
{
	internal class MemberRosterExt : MemberRoster
	{
		public int Visits { get; set; }
		public MemberRosterExt(MemberRoster member, int visits)
		{
			id = member.id;
			Badge = member.Badge;
			FirstName = member.FirstName;
			LastName = member.LastName;
			Address = member.Address;
			Phone = member.Phone;
			Email = member.Email;
			Title = member.Title;
			RecCard = member.RecCard;
			Locker = member.Locker;
			CardNo = member.CardNo;
			EntryCodes = member.EntryCodes;
			Visits = visits;
			CreditBank = member.CreditBank;
			OneTime = member.OneTime;
			LastDayValid = member.LastDayValid;
			Exempt = member.Exempt;
			ClubDuesPaid = member.ClubDuesPaid;
		}
	}
}

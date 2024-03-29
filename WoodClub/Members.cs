﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WoodClub
{
	// <param name="init"> false does not initialize list </param>
	public class Members
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

		public List<MembersExtended> DataSource { get; set; } = new List<MembersExtended>();
		private List<MemberRoster> data;

		public void GetMembers()
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				try
				{
					data = context.MemberRosters.Select(mem => mem)
						.Distinct()
						.OrderBy(mem => mem.Badge)
						.ToList();
				}
				catch (Exception ex)
				{
					log.Fatal("Unable to get data...", ex);         // Capture exception
				}
			}

			foreach (MemberRoster mr in data)
            {
				DataSource.Add(new MembersExtended(mr));
            }

		}

		//
		//  Update Member record
		//
		public bool UpdateMember(MemberRoster EditedMember)
		{

			using (WoodClubEntities context = new WoodClubEntities())
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
				catch (Exception ex)
				{
					log.Error("Update failed..", ex);
					return false;
				}
			}
		}



		public bool AddNew(MemberRoster NewMember)
		{
			int _id = NewMember.id;
			using (WoodClubEntities context = new WoodClubEntities())
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

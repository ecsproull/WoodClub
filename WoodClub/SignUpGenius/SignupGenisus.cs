using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WoodClub
{
	internal class SignUpGenisus
	{
		private string userKey = "user_key=Z29KUVJjdjdKeUZmTCtiZXFpaTJSQT09";
		public List<SignupSlot> GetSlotsForDay(Rootobject signUp, DateTime day)
		{
			List<SignupSlot> slots = new List<SignupSlot>();
			foreach (SignupSlot slot in signUp.data.signup)
			{
				DateTime slotTime = UnixTimeStampToDateTime((long)slot.startdate);
				if (slotTime.Date == day.Date && slot.item.ToLower() != "cleaning")
				{
					slots.Add(slot);
				}
			}

			return slots;
		}

		public async Task<Rootobject> GetSignup(DateTime dt)
		{
			string month = dt.ToString("MMM").ToLower();
			string signupsAll_url = $"https://api.signupgenius.com/v2/k/signups/created/active/?{userKey}";
			try
			{
				HttpClient client = new HttpClient();
				var stream = await client.GetStreamAsync(signupsAll_url);
				var signuplist = await JsonSerializer.DeserializeAsync<SignupList>(stream);
				string title = "SCW Woodshop Orientation Sign Up";
				foreach (SignUp datum in signuplist.data)
				{
					if (datum.title == title)
					{
						string signup_url = $"https://api.signupgenius.com/v2/k/signups/report/all/{datum.signupid}/?{userKey}";
						stream = await client.GetStreamAsync(signup_url);
						return await JsonSerializer.DeserializeAsync<Rootobject>(stream);
					}
					else
					{
						Console.WriteLine(datum.title);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return null;
		}

		private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dateTime;
		}
	}
}

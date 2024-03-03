using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace WoodClub
{
	internal class PostToGoDaddy
	{
		public async void PostMembersToGoDaddy()
		{
			using (HttpClient client = new HttpClient())
			{
				var contentType = new MediaTypeWithQualityHeaderValue("application/json");
				var baseAddress = "https://scwwoodshop.com";
				var api = "/wp-json/scwmembers/v1/members";
				client.BaseAddress = new Uri(baseAddress);
				client.DefaultRequestHeaders.Accept.Add(contentType);

				PermsData pd = new PermsData();
				pd.action = "Update";
				using (WoodClubEntities context = new WoodClubEntities())
				{
					var members = (from m in context.MemberRosters
								   where m.ClubDuesPaid == true && m.Badge != "20001"
								   select m).OrderBy(o => o.Badge).ToArray();
					int length = members.Length;
					pd.members = new Member[length];
					for (int i = 0; i < length; i++)
					{
						pd.members[i] = new Member
						{
							badge = members[i].Badge,
							first = members[i].FirstName,
							last = members[i].LastName,
							phone = members[i].Phone,
							email = members[i].Email,
							secret = Guid.NewGuid().ToString("N")
						};
					}

					var permissions = (from m in context.MachinePerms
									   select m).ToArray();
					length = permissions.Length;
					pd.permissions = new Permission[length];
					for (int i = 0; i < length; i++)
					{
						pd.permissions[i] = new Permission
						{
							badge = permissions[i].Badge,
							machine_name = permissions[i].MachineName
						};
					}

					pd.clean_permissions = true;
				}

				var jsonData = JsonConvert.SerializeObject(pd);
				var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");

				var response = await client.PostAsync(api, contentData);

				if (response.IsSuccessStatusCode)
				{
					var stringData = await response.Content.ReadAsStringAsync();
					var result = JsonConvert.DeserializeObject<object>(stringData);
					MessageBox.Show("Update Complete");
				}
				else
				{
					MessageBox.Show("Update Possible Failure.");
				}
			}
		}

		public class PermsData
		{
			public string key = "8c62a157-7ee8-4104-9f91-930eac39fe2f";
			public bool clean_permissions { get; set; }
			public string action { get; set; }
			public Member[] members { get; set; }
			public Permission[] permissions { get; set; }
		}
		public class Member
		{
			public string badge { get; set; }
			public string first { get; set; }
			public string last { get; set; }
			public string phone { get; set; }
			public string email { get; set; }
			public string secret { get; set; }
		}

		public class Permission
		{
			public string badge { get; set; }
			public string machine_name { get; set; }
		}
	}
}


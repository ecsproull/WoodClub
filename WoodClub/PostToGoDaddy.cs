using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace WoodClub
{
	/// <summary>
	/// Used when updating the member data on the club website that
	/// is hosted on GoDaddy. <see cref="MainMembers"/>
	/// </summary>
	internal class PostToGoDaddy
	{
		/// <summary>
		/// Posts the members to go daddy.
		/// </summary>
		public async void PostMembersToGoDaddy()
		{
            using (HttpClient client = new HttpClient())
			{
				var contentType = new MediaTypeWithQualityHeaderValue("application/json");
				var baseAddress = "https://scwwoodshop.com";
				//var baseAddress = "https://woodclubtest.site";
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
						string badgeNumber = members[i].Badge;
						var monitorParams = (from m in context.MonitorParams
											 where m.Monitor_Badge == badgeNumber
											 select m).FirstOrDefault();
						pd.members[i] = new Member
						{
							badge = members[i].Badge,
							first = members[i].FirstName,
							last = members[i].LastName,
							phone = members[i].Phone,
							email = members[i].Email,
							secret = Guid.NewGuid().ToString("N"),
							email_secret = monitorParams.Monitor_Secret
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

        /// <summary>
        /// Send member photos as individual multipart/form-data requests.
        /// Server endpoint should accept: key, badge (string fields) and photo (file field).
        /// </summary>
        public async void PostMemberPhotosMultipart()
        {
            var confirm = MessageBox.Show(
			"Upload member photos to the website now? This takes about 40 minutes to run and the app must stay open. Photos are seen and used by administrators only.",
			"Confirm Upload",
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question,
			MessageBoxDefaultButton.Button2);

			if (confirm == DialogResult.No)
			{
				return;
			}
            var baseAddress = "https://scwwoodshop.com";
            //var baseAddress = "https://woodclubtest.site";
			string apiPath = "/wp-json/scwmembers/v1/photos";

            using (HttpClient client = new HttpClient { BaseAddress = new Uri(baseAddress) })
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // build members list (same selection you already use)
                using (WoodClubEntities context = new WoodClubEntities())
                {
                    var members = (from m in context.MemberRosters
                                   where m.ClubDuesPaid == true && m.Badge != "20001"
                                   select m).OrderBy(o => o.Badge).ToArray();

                    foreach (var dbMember in members)
                    {
                        // skip empty photos
                        if (dbMember.Photo == null || dbMember.Photo.Length == 0)
                        continue;

                        using (var content = new MultipartFormDataContent())
                        {
                            // required fields
                            content.Add(new StringContent("8c62a157-7ee8-4104-9f91-930eac39fe2f"), "key");
                            content.Add(new StringContent(dbMember.Badge ?? ""), "badge");
                            // optional metadata
                            content.Add(new StringContent(dbMember.Email ?? ""), "email");
                            // file part
                            var mime = GetImageMimeType(dbMember.Photo) ?? "application/octet-stream";
                            var fileContent = new ByteArrayContent(dbMember.Photo);
                            fileContent.Headers.ContentType = new MediaTypeHeaderValue(mime);
                            // field name "photo" (server expects this). filename helps server treat it as file
                            var ext = GetExtensionForMime(mime);
                            content.Add(fileContent, "photo", $"{dbMember.Badge}{ext}");

                            try
                            {
                                var resp = await client.PostAsync(apiPath, content);
                                var respBody = await resp.Content.ReadAsStringAsync();
								Console.WriteLine($"Uploaded photo for {dbMember.Badge}, response: {(int)resp.StatusCode} {resp.ReasonPhrase}");
                                if (!resp.IsSuccessStatusCode)
                                {
                                    // Log or show server error for this member
                                    MessageBox.Show($"Photo upload failed for {dbMember.Badge}: {(int)resp.StatusCode} {resp.ReasonPhrase}\n{respBody}");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Network error uploading photo for {dbMember.Badge}: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Try to detect basic image mime type from the leading bytes.
        /// </summary>
        private static string GetImageMimeType(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 4) return null;
            // JPEG FF D8
            if (bytes[0] == 0xFF && bytes[1] == 0xD8) return "image/jpeg";
            // PNG 89 50 4E 47
            if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47) return "image/png";
            // GIF "GIF8"
            if (bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46) return "image/gif";
            // BMP "BM"
            if (bytes[0] == 0x42 && bytes[1] == 0x4D) return "image/bmp";
            // fallback
            return null;
        }

        private static string GetExtensionForMime(string mime)
        {
            switch (mime)
            {
                case "image/jpeg": return ".jpg";
                case "image/png": return ".png";
                case "image/gif": return ".gif";
                case "image/bmp": return ".bmp";
                default: return ".bin";
            }
        }

        /// <summary>
        /// Data structure used to pass data to GoDaddy.
        /// These data structures have to match what is expected on the server.
        /// Don't fuck with this unless you really know what you are doing!
        /// </summary>
        private class PermsData
		{
			/// <summary>
			/// The key
			/// </summary>
			public string key = "8c62a157-7ee8-4104-9f91-930eac39fe2f";

			/// <summary>
			/// Gets or sets a value indicating whether [clean permissions].
			/// </summary>
			/// <value>
			///   <c>true</c> if [clean permissions]; otherwise, <c>false</c>.
			/// </value>
			public bool clean_permissions { get; set; }

			/// <summary>
			/// Gets or sets the action.
			/// </summary>
			/// <value>
			/// The action.
			/// </value>
			public string action { get; set; }

			/// <summary>
			/// Gets or sets the members.
			/// </summary>
			/// <value>
			/// The members.
			/// </value>
			public Member[] members { get; set; }

			/// <summary>
			/// Gets or sets the permissions.
			/// </summary>
			/// <value>
			/// The permissions.
			/// </value>
			public Permission[] permissions { get; set; }
		}

		/// <summary>
		/// Data structure used to pass data to GoDaddy.
		/// </summary>
		private class Member
		{
			/// <summary>
			/// Gets or sets the badge.
			/// </summary>
			/// <value>
			/// The badge.
			/// </value>
			public string badge { get; set; }

			/// <summary>
			/// Gets or sets the first.
			/// </summary>
			/// <value>
			/// The first.
			/// </value>
			public string first { get; set; }

			/// <summary>
			/// Gets or sets the last.
			/// </summary>
			/// <value>
			/// The last.
			/// </value>
			public string last { get; set; }

			/// <summary>
			/// Gets or sets the phone.
			/// </summary>
			/// <value>
			/// The phone.
			/// </value>
			public string phone { get; set; }

			/// <summary>
			/// Gets or sets the email.
			/// </summary>
			/// <value>
			/// The email.
			/// </value>
			public string email { get; set; }

			/// <summary>
			/// Gets or sets the secret.
			/// </summary>
			/// <value>
			/// The secret.
			/// </value>
			public string secret { get; set; }


			/// <summary>
			/// Gets or sets the email secret.
			/// </summary>
			/// <value>
			/// The email secret.
			/// </value>
			public string email_secret { get; set; }
		}

		/// <summary>
		/// Data structure used to pass data to GoDaddy.
		/// </summary>
		private class Permission
		{
			/// <summary>
			/// Gets or sets the badge.
			/// </summary>
			/// <value>
			/// The badge.
			/// </value>
			public string badge { get; set; }

			/// <summary>
			/// Gets or sets the name of the machine.
			/// </summary>
			/// <value>
			/// The name of the machine.
			/// </value>
			public string machine_name { get; set; }
		}
	}
}
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WoodClub
{
	internal class SendMail
	{
		public async Task<Response> SendMailAsync(string subject, string htmlBody, List<EmailAddress> recpts)
		{
			var apiKey = Environment.GetEnvironmentVariable("SendGrid");
			var client = new SendGridClient(apiKey);

			//string htmlContent = htmlBody + "<br><br><a href='" + registerUrl + "'>Monitor Signup</a>";
			string htmlContent = htmlBody;

			var mailMulti = MailHelper.CreateSingleEmailToMultipleRecipients(
			   new EmailAddress("treasurer@scwwoodshop.com", "Locker Notifier"),
			   recpts,
			   subject,
			   "",
			   htmlContent
			   );

			mailMulti.ReplyTo = new EmailAddress("mkayvidal@gmail.com", "Locker Committee");

			return await client.SendEmailAsync(mailMulti);
		}

		/// <summary>
		/// Sends a single email to one recipient using SendGrid
		/// </summary>
		/// <param name="fromEmail">Sender email address</param>
		/// <param name="toEmail">Recipient email address</param>
		/// <param name="toName">Recipient name</param>
		/// <param name="subject">Email subject</param>
		/// <param name="htmlBody">HTML body content</param>
		/// <param name="plainTextBody">Optional plain text body (default: empty)</param>
		/// <param name="attachments">Optional file attachments (default: none)</param>
		/// <returns>SendGrid Response</returns>
		public async Task<Response> SendSingleEmailAsync(string fromEmail, string toEmail, string toName, string subject, string htmlBody, string plainTextBody = "", List<EmailAttachment> attachments = null)
		{
			var apiKey = Environment.GetEnvironmentVariable("SendGrid");
			var client = new SendGridClient(apiKey);

			var from = new EmailAddress(fromEmail);
			var to = new EmailAddress(toEmail, toName);

			var msg = MailHelper.CreateSingleEmail(
				from,
				to,
				subject,
				plainTextBody,
				htmlBody
			);

			msg.ReplyTo = new EmailAddress("treasurer@scwwoodshop.com", "Finance Committee");

			if (attachments != null && attachments.Count > 0)
			{
				var sendGridAttachments = new List<Attachment>();
				foreach (var attachment in attachments)
				{
					sendGridAttachments.Add(new Attachment
					{
						Filename = attachment.FileName,
						Type = attachment.MimeType,
						Content = Convert.ToBase64String(attachment.Content),
						Disposition = "attachment"
					});
				}

				msg.AddAttachments(sendGridAttachments);
			}

			return await client.SendEmailAsync(msg);
		}
	}
}

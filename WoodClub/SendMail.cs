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
	}
}

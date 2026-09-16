using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
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
		/// Records one recipient of an outgoing email batch in Communications
		/// and returns its database-generated CommunicationID. Call once per
		/// recipient (every recipient gets its own row - <paramref name="subject"/>,
		/// <paramref name="sentAt"/> and <paramref name="recipientCount"/> are the
		/// same across every row of a given batch), then pass the returned id
		/// into that recipient's <see cref="SendSingleEmailAsync"/> call - it's
		/// attached as the "EmailID" custom arg so the downstream Go
		/// event-processing service can tie delivery/open/click webhook events
		/// back to this row.
		/// </summary>
		/// <param name="subject">The subject shared by every email in the batch.</param>
		/// <param name="sentAt">The send time shared by every email in the batch.</param>
		/// <param name="recipientCount">How many recipients the whole batch is going to.</param>
		/// <param name="recipient">This row's recipient - a member's Badge number when known, otherwise their raw email address.</param>
		/// <param name="sentBy">The From address the batch is being sent from.</param>
		/// <returns>The generated CommunicationID.</returns>
		public long RecordCommunication(string subject, DateTime sentAt, int recipientCount, string recipient, string sentBy)
		{
			using (WoodClubEntities context = new WoodClubEntities())
			{
				Communication communication = new Communication
				{
					Subject = subject,
					SentAt = sentAt,
					RecipientCount = recipientCount,
					Recipient = recipient,
					SentBy = sentBy
				};

				context.Communications.Add(communication);
				context.SaveChanges();

				return communication.CommunicationID;
			}
		}

		/// <summary>
		/// Links every Communications row from a just-completed batch back to
		/// the SavedEmail "sent copy" row created for it, so dropped/blocked/
		/// bounced events recorded later in CommunicationEvents can be queried
		/// directly by which list send caused them. Called once per batch,
		/// after the SavedEmail row exists - every recipient's row is linked
		/// (including ones whose immediate send attempt failed), since
		/// delivery events for those still arrive later via webhook.
		/// </summary>
		/// <param name="communicationIds">The CommunicationIDs returned by <see cref="RecordCommunication"/> for this batch.</param>
		/// <param name="savedEmailId">The SavedEmailId of the batch's sent copy.</param>
		public void LinkCommunicationsToSavedEmail(List<long> communicationIds, int savedEmailId)
		{
			if (communicationIds == null || communicationIds.Count == 0)
			{
				return;
			}

			using (WoodClubEntities context = new WoodClubEntities())
			{
				List<Communication> rows = context.Communications
					.Where(c => communicationIds.Contains(c.CommunicationID))
					.ToList();

				foreach (Communication row in rows)
				{
					row.SavedEmailId = savedEmailId;
				}

				context.SaveChanges();
			}
		}

		/// <summary>
		/// Sends a single email to one recipient using SendGrid
		/// </summary>
		/// <param name="fromEmail">Sender email address</param>
		/// <param name="toEmail">Recipient email address</param>
		/// <param name="toName">Recipient name</param>
		/// <param name="subject">Email subject</param>
		/// <param name="htmlBody">HTML body content</param>
		/// <param name="emailId">The CommunicationID from <see cref="RecordCommunication"/> for this batch, attached as the "EmailID" custom arg.</param>
		/// <param name="memberId">The recipient's Badge number, attached as the "MemberID" custom arg (empty if the recipient isn't a member).</param>
		/// <param name="plainTextBody">Optional plain text body (default: empty)</param>
		/// <param name="attachments">Optional file attachments (default: none)</param>
		/// <returns>SendGrid Response</returns>
		public async Task<Response> SendSingleEmailAsync(string fromEmail, string toEmail, string toName, string subject, string htmlBody, long emailId, string memberId, string plainTextBody = "", List<EmailAttachment> attachments = null)
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

			//msg.ReplyTo = new EmailAddress("treasurer@scwwoodshop.com", "Finance Committee");

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

			msg.AddGlobalCustomArg("System", "WoodClub");
			msg.AddGlobalCustomArg("EmailID", emailId.ToString());
			msg.AddGlobalCustomArg("MemberID", memberId ?? string.Empty);

			return await client.SendEmailAsync(msg);
		}
	}
}

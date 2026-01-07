using System;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace WoodClub
{
	internal class SendText
	{
		public void CreateText(string message, string phoneNumber)
		{
			// Find your Account SID and Auth Token at twilio.com/console
			// and set the environment variables. See http://twil.io/secure
			string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
			string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

			TwilioClient.Init(accountSid, authToken);

			string number = phoneNumber.Replace("-", string.Empty);
			if (number.Length == 10)
			{
				var msg = MessageResource.Create(
					body: message,
					from: new Twilio.Types.PhoneNumber("+16233049716"),
					to: new Twilio.Types.PhoneNumber("+1" + number)
				);

				System.Threading.Thread.Sleep(1000);
			}
			else
			{
				MessageBox.Show(phoneNumber);
			}
		}

		public void ReadText()
		{
			string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
			string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

			TwilioClient.Init(accountSid, authToken);
			var messages = MessageResource.Read();
			foreach (var message in messages)
			{
				Console.WriteLine(message.Body);
			}
		}
	}
}

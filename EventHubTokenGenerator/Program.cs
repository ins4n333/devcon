using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace EventHubTokenGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			var eventHubNameSpace = new Uri("https://metrics.servicebus.windows.net/");
			var eventHubName = "devconhub";
			var publisherName = "DevConDev.TestWebApp";
			var policy = "MetricsPublishers";
			var key = "";

			var token = GenerateTokenAndCheck(eventHubNameSpace, eventHubName, publisherName, policy, key);

			Console.WriteLine($"Token: {token}");
		}

		private static string GenerateTokenAndCheck(Uri namaSpaceUri, string hubName, string publisher, string policy, string key)
		{
			string token = SharedAccessSignatureTokenProvider.GetPublisherSharedAccessSignature(
				namaSpaceUri,
				hubName, 
				publisher,
				policy,
				key,
				new TimeSpan(365, 0, 0));
			var connStr = ServiceBusConnectionStringBuilder.CreateUsingSharedAccessSignature(
				namaSpaceUri,
				hubName,
				publisher,
				token);
			CheckToken(connStr);

			return token;
		}

		private static void CheckToken(string connStr)
		{
			var sender = EventHubSender.CreateFromConnectionString(connStr);
			sender.Send(new EventData(Encoding.UTF8.GetBytes("Hello Event Hub")));
		}
	}
}
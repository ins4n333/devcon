using Metrics;
using Serilog;
using TestWebApp.CustomMetrics;
using TestWebApp.CustomMetrics.Configuration;
using TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter;

namespace TestWebApp.Monitoring
{
	public class Monitoring
	{
		public static void Configure()
		{
			var settings = Settings.Read();
			var logger = Log.Logger;
			if (settings.Enabled)
			{
				var serviceDescriptor = new ServiceDescriptor(
					systemId: settings.SystemId,
					serviceId: settings.ServiceId,
					deploymentId: "-",
					instanceId: settings.InstanseId);

				var connectionInfo = new EventHubConnectionInfo(
					ns: settings.MetricsHubNs,
					hub: settings.MetricsHub,
					sharedAccessSignature: settings.MetricsClientSasToken);

				var config = Metric.Config
					.WithAspNetCounters()
					.WithSystemCounters()
					.WithWebServerCounters();				

				config.WithReporting(serviceDescriptor, connectionInfo);
				logger.Information("Service {@ServiceDescriptor} metrics  reporting enabled.", serviceDescriptor);
			}
		}
	}
}
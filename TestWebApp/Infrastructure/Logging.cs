using Microsoft.WindowsAzure.Storage;
using Serilog;
using TestWebApp.Common;

namespace TestWebApp.Infrastructure
{
	public class Logging
	{
		public static void Configure()
		{
			var loggingStorage =
				CloudStorageAccount.Parse(SettingsReader.GetFromAppSettings("LoggingStorageConnectionString"));
			var configuration = new LoggerConfiguration()
				.WriteTo.AzureTableStorage(loggingStorage)
				.MinimumLevel.Verbose();

			Log.Logger = configuration.CreateLogger();
		}
	}
}
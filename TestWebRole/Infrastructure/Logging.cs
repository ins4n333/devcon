using Microsoft.WindowsAzure.Storage;
using Serilog;
using TestWebRole.Common;

namespace TestWebApp.Infrastructure
{
	public class Logging
	{
		public static void Configure()
		{
			var loggingStorage =
				CloudStorageAccount.Parse(SettingsReader.GetFromCloud("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString"));
			var configuration = new LoggerConfiguration()
				.WriteTo.AzureTableStorage(loggingStorage)
				.MinimumLevel.Verbose();

			Log.Logger = configuration.CreateLogger();
		}
	}
}
using System;
using System.Configuration;
using TestWebApp.Common;

namespace TestWebApp.Monitoring
{
	public class Settings
	{
		public static Settings Read()
		{
			return new Settings
			{
				InstanseId = SettingsReader.GetFromEnvironmentVariables("WEBSITE_INSTANCE_ID"),
				MetricsHubNs = new Uri(SettingsReader.GetFromAppSettings("MetricsHubNs")),
				MetricsHub = SettingsReader.GetFromAppSettings("MetricsHub"),
				MetricsClientSasToken = SettingsReader.GetFromAppSettings("MetricsClientSasToken"),
				SystemId = SettingsReader.GetFromAppSettings("SystemId"),
				ServiceId = SettingsReader.GetFromAppSettings("ServiceId"),
				Enabled = Convert.ToBoolean(SettingsReader.GetFromAppSettings("Enabled"))
			};
		}

		public string InstanseId { get; private set; }

		public Uri MetricsHubNs { get; private set; }

		public string MetricsHub { get; private set; }

		public string MetricsClientSasToken { get; private set; }

		public string SystemId { get; private set; }

		public string ServiceId { get; private set; }

		public bool Enabled { get; private set; }
	}
}
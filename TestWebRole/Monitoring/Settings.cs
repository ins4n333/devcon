using System;
using System.Configuration;
using Microsoft.WindowsAzure.ServiceRuntime;
using TestWebRole.Common;

namespace TestWebApp.Monitoring
{
	public class Settings
	{
		public static Settings Read()
		{
			return new Settings
			{
				InstanseId = RoleEnvironment.CurrentRoleInstance.Id,
				MetricsHubNs = new Uri(SettingsReader.GetFromCloud("MetricsHubNs")),
				MetricsHub = SettingsReader.GetFromCloud("MetricsHub"),
				MetricsClientSasToken = SettingsReader.GetFromCloud("MetricsClientSasToken"),
				SystemId = SettingsReader.GetFromCloud("SystemId"),
				ServiceId = SettingsReader.GetFromCloud("ServiceId"),
				Enabled = Convert.ToBoolean(SettingsReader.GetFromCloud("Enabled"))
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
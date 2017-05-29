using System;
using System.Configuration;
using Microsoft.Azure;

namespace TestWebRole.Common
{
	public class SettingsReader
	{
		public static string GetFromCloud(string key)
		{
			var value = CloudConfigurationManager.GetSetting(key);

			return value;
		}

		public static string GetFromAppSettings(string key)
		{
			var value = ConfigurationManager.AppSettings[key];

			return value;
		}

		public static string GetFromEnvironmentVariables(string key)
		{
			return Environment.GetEnvironmentVariable(key);
		}
	}
}
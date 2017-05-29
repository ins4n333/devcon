using System;
using System.Configuration;

namespace TestWebApp.Common
{
	public class SettingsReader
	{
		public static string GetFromAppSettings(string key)
		{
			var value = ConfigurationManager.AppSettings.Get(key);

			return value;
		}

		public static string GetFromEnvironmentVariables(string key)
		{
			return Environment.GetEnvironmentVariable(key);
		}
	}
}
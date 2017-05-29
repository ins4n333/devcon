﻿using System.Web.Http;
using TestWebApp.Infrastructure;

namespace TestWebApp
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			Logging.Configure();
			Monitoring.Monitoring.Configure();
		}
	}
}
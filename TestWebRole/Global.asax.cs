using System.Web.Http;

namespace TestWebRole
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			//TelemetryConfiguration.Active.InstrumentationKey = "";
		}
	}
}

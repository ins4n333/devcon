using Microsoft.WindowsAzure.ServiceRuntime;
using TestWebApp.Infrastructure;

namespace TestWebRole
{
	public class WebRole : RoleEntryPoint
	{
		public override bool OnStart()
		{
			// For information on handling configuration changes
			// see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
			Logging.Configure();
			TestWebApp.Monitoring.Monitoring.Configure();

			return base.OnStart();
		}
	}
}
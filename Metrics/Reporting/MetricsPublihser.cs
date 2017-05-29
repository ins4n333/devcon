using Journalist;

namespace TestWebApp.CustomMetrics.Reporting
{
    public class MetricsPublihser
    {
        public static string GetPublisherId(ServiceDescriptor service)
        {
            Require.NotNull(service, nameof(service));

            return service.SystemId + "." + service.ServiceId;
        }
    }
}
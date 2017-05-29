using Metrics.PerfCounters;

namespace TestWebApp.CustomMetrics.ValueProviders
{
    internal class AspNetPerfCounterValueProvider : PerfCounterValueProvider
    {
        public AspNetPerfCounterValueProvider(string counter) : base("ASP.NET", counter)
        {
        }
    }
}
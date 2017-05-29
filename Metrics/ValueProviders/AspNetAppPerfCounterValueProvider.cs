namespace TestWebApp.CustomMetrics.ValueProviders
{
    internal class AspNetAppPerfCounterValueProvider : PerfCounterValueProvider
    {
        public AspNetAppPerfCounterValueProvider(string counter) : base("ASP.NET Applications", counter, "__Total__")
        {
        }
    }
}
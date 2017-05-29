namespace TestWebApp.CustomMetrics.ValueProviders
{
    internal class WebServicePerfCounterValueProvider : PerfCounterValueProvider
    {
        public WebServicePerfCounterValueProvider(string counterName) : base("Web Service", counterName, "_Total")
        {
        }
    }
}
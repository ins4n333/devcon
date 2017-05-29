using Metrics.MetricData;

namespace TestWebApp.CustomMetrics.ValueProviders
{
    internal class MemoryLoadValueProvider : MetricValueProvider<double>
    {
        public double GetValue(bool resetMetric = false)
        {
            return Value;
        }

        public double Value => NariveMethods.GetMemoryLoad();
    }
}
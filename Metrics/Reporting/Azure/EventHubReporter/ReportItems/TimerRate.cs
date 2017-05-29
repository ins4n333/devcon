using Metrics.MetricData;

namespace TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter.ReportItems
{
    public class TimerRate
    {
        public TimerRate(MeterValue meter)
        {
            Mean = meter.MeanRate;
            FifteenMinute = meter.FifteenMinuteRate;
            FiveMinute = meter.FiveMinuteRate;
            OneMinute = meter.OneMinuteRate;
        }

        public double Mean { get; }

        public double FifteenMinute { get; }

        public double FiveMinute { get; }

        public double OneMinute { get; }
    }
}
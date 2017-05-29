using Metrics.MetricData;

namespace TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter.ReportItems
{
    public class TimerReportItem : EventHubReportItem
    {
        public TimerReportItem(ServiceDescriptor service, string name, TimerValue timerValue) : base(service, name)
        {
            Value = timerValue.ActiveSessions;
            Total = new TimerTotals(timerValue.TotalTime, timerValue.Rate.Count);
            Rate = new TimerRate(timerValue.Rate);
            Histogram = new TimerHistogram(timerValue.Histogram);
         }

        public TimerHistogram Histogram { get; }

        public TimerRate Rate { get; }

        public TimerTotals Total { get; }

        public long Value { get; }

        public override MetricEventType MetricType => MetricEventType.Timer;
    }
}
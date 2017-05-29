namespace TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter.ReportItems
{
    public class GaugeEventHubReportItem : EventHubReportItem
    {
        public GaugeEventHubReportItem(ServiceDescriptor service, string name, double value) : base(service, name)
        {
            Value = value;
        }

        public double Value { get; }

        public override MetricEventType MetricType => MetricEventType.Gauge;
    }
}
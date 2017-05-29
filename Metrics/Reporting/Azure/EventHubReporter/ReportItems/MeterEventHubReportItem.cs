namespace TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter.ReportItems
{
    public class MeterEventHubReportItem : EventHubReportItem
    {
        public MeterEventHubReportItem(ServiceDescriptor service, string name, double value) : base(service, name)
        {
            Value = value;
        }

        public double Value { get; }

        public override MetricEventType MetricType => MetricEventType.Meter;
    }
}
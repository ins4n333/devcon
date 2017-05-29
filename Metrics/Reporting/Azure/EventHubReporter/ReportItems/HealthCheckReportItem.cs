namespace TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter.ReportItems
{
    public class HealthCheckReportItem : EventHubReportItem
    {
        public HealthCheckReportItem(ServiceDescriptor service, string name, HealthState healthState, string message) : base(service, name)
        {
            HealthState = healthState;
            Message = message;
        }

        public HealthState HealthState { get; }

        public string Message { get; }

        public override MetricEventType MetricType => MetricEventType.HealthCheck;
    }
}
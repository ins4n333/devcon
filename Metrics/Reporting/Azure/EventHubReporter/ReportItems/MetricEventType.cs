namespace TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter.ReportItems
{
    public enum MetricEventType
    {
        Unknown = 0,
        Gauge = 1,
        Counter = 2,
        Meter = 3,
        Histogram = 4,
        Timer = 5,
        HealthCheck = 6
    }
}
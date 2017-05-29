namespace TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter.ReportItems
{
    public class TimerTotals
    {
        public TimerTotals(long totalTime, long totalCount)
        {
            TotalTime = totalTime;
            TotalCount = totalCount;
        }

        public long TotalTime { get; }

        public long TotalCount { get; }
    }
}
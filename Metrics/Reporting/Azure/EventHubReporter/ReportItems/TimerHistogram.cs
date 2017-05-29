using Metrics.MetricData;

namespace TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter.ReportItems
{
    public class TimerHistogram
    {
        public TimerHistogram(HistogramValue histogram)
        {
            Last = histogram.LastValue;
            Mean = histogram.Mean;
            Median = histogram.Median;
            StdDev = histogram.StdDev;
            Max = histogram.Max;
            Min = histogram.Min;
            P999 = histogram.Percentile999;
            P99 = histogram.Percentile99;
            P98 = histogram.Percentile98;
            P95 = histogram.Percentile95;
            P75 = histogram.Percentile75;
        }

        public double Last { get; }

        public double Mean { get; }

        public double Median { get; }

        public double StdDev { get; }

        public double Max { get; set; }

        public double Min { get; set; }

        public double P999 { get; set; }

        public double P99 { get; set; }

        public double P98 { get; set; }

        public double P95 { get; set; }

        public double P75 { get; set; }
    }
}
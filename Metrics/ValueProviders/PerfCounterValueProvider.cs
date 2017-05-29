using System;
using System.Diagnostics;
using Journalist;
using Journalist.Extensions;
using Metrics.MetricData;
using Serilog;

namespace TestWebApp.CustomMetrics.ValueProviders
{
    internal class PerfCounterValueProvider : MetricValueProvider<double>
    {
        private static readonly ILogger s_logger = Log.ForContext<PerfCounterValueProvider>();
        private readonly object m_lock = new object();
        private readonly string m_categoryName;
        private readonly string m_counterName;
        private readonly string m_instanceName;

        private PerformanceCounter m_counterInstance;

        public PerfCounterValueProvider(string categoryName, string counterName, string instanceName)
        {
            Require.NotEmpty(categoryName, nameof(categoryName));
            Require.NotEmpty(counterName, nameof(counterName));

            m_categoryName = categoryName;
            m_counterName = counterName;
            m_instanceName = instanceName;
        }

        public PerfCounterValueProvider(string categoryName, string counterName) : this(categoryName, counterName, null)
        {
        }

        public double GetValue(bool resetMetric = false)
        {
            return Value;
        }

        public double Value => GetCounterValue();

        private double GetCounterValue()
        {
            if (m_counterInstance == null)
            {
                lock (m_lock)
                {
                    if (m_counterInstance == null)
                    {
                        try
                        {
                            var counterInstance = m_instanceName.IsNullOrEmpty()
                                ? new PerformanceCounter(m_categoryName, m_counterName, true)
                                : new PerformanceCounter(m_categoryName, m_counterName, m_instanceName, true);

                            m_counterInstance = counterInstance;
                        }
                        catch (Exception exception)
                        {
                            s_logger.Error(
                                exception,
                                "Performance counter ({Category, Counter, Instance}) initialization error.",
                                m_categoryName,
                                m_counterName, 
                                m_instanceName);
                        }
                    }
                }
            }

            var result = double.NaN;
            if (m_counterInstance != null)
            {
                try
                {
                    result = m_counterInstance.NextValue();
                }
                catch (Exception exception)
                {
                    s_logger.Error(
                        exception, 
                        "Performance counter ({Category, Counter, Instance}) reading error.",
                        m_categoryName, 
                        m_counterName, 
                        m_instanceName);
                }
            }

            return result;
        }
    }
}
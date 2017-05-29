using System;
using System.Collections.Generic;
using System.Linq;
using Journalist;
using Journalist.Extensions;
using TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter.ReportItems;
using Metrics;
using Metrics.MetricData;
using Metrics.Reporters;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Serilog;

namespace TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter
{
    public class EventHubReporter : BaseReport
    {
        private static readonly ILogger s_logger = Log.ForContext<EventHubReporter>();

        private readonly EventHubSender m_sender;
        private readonly ServiceDescriptor m_service;
        private readonly EventHubConnectionInfo m_eventHubConnection;
        private List<EventHubReportItem> m_items;

        public EventHubReporter(ServiceDescriptor service, EventHubConnectionInfo eventHubConnection)
        {
            Require.NotNull(service, nameof(service));
            Require.NotNull(eventHubConnection, nameof(eventHubConnection));

            var connectionString = ServiceBusConnectionStringBuilder.CreateUsingSharedAccessSignature(
                endpoint: eventHubConnection.Namespace,
                entityPath: eventHubConnection.Hub,
                publisher: MetricsPublihser.GetPublisherId(service),
                sharedAccessSignature: eventHubConnection.SharedAccessSignature);

            m_service = service;
            m_eventHubConnection = eventHubConnection;
            m_sender = EventHubSender.CreateFromConnectionString(connectionString);
        }

        protected override void ReportGauge(string name, double value, Unit unit, MetricTags tags)
        {
            m_items.Add(new GaugeEventHubReportItem(m_service, name, value));
        }

        protected override void ReportCounter(string name, CounterValue value, Unit unit, MetricTags tags)
        {
            m_items.Add(new CounterEventHubReportItem(m_service, name, value.Count));
        }

        protected override void ReportMeter(string name, MeterValue value, Unit unit, TimeUnit rateUnit, MetricTags tags)
        {
            m_items.Add(new MeterEventHubReportItem(m_service, name, value.OneMinuteRate));
        }

        protected override void ReportHistogram(string name, HistogramValue value, Unit unit, MetricTags tags)
        {
        }

        protected override void ReportTimer(string name, TimerValue value, Unit unit, TimeUnit rateUnit, TimeUnit durationUnit, MetricTags tags)
        {
            m_items.Add(new TimerReportItem(m_service, name, value));
        }

        protected override void ReportHealth(HealthStatus status)
        {
            foreach (var statusResult in status.Results)
            {
                if (statusResult.Check.IsHealthy)
                {
                    m_items.Add(new HealthCheckReportItem(
                        m_service,
                        statusResult.Name,
                        HealthState.Ok,
                        statusResult.Check.Message));
                }
                else
                {
                    m_items.Add(new HealthCheckReportItem(
                        m_service,
                        statusResult.Name,
                        statusResult.Check.Message.StartsWith("[WRN]") ? HealthState.Warning : HealthState.Error,
                        statusResult.Check.Message));
                }
            }
        }

        protected override void StartReport(string contextName)
        {
            m_items = new List<EventHubReportItem>();
        }

        protected override void EndReport(string contextName)
        {
            if (m_items.IsEmpty())
            {
                return;
            }

            try
            {
                m_sender.SendBatch(m_items.Select(item => new EventData(item.ToJson())));
            }
            catch (Exception exception)
            {
                s_logger.Error(exception, "Sending metrics to event hub {EventHub} in {Namespace} namespace failed.", m_eventHubConnection.Hub, m_eventHubConnection.Namespace);
            }

            m_items = null;
        }

        protected override string FormatMetricName<T>(string context, MetricValueSource<T> metric)
        {
            return metric.Name;
        }
    }
}
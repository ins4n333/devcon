using System;
using Journalist;
using TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter;
using TestWebApp.CustomMetrics.ValueProviders;
using Metrics;

namespace TestWebApp.CustomMetrics.Configuration
{
    public static class MetricsConfigurationExtensions
    {
        public static MetricsConfig WithReporting(
            this MetricsConfig config,
            ServiceDescriptor service,
            EventHubConnectionInfo eventHubConnection)
        {
            Require.NotNull(config, nameof(config));
            Require.NotNull(service, nameof(service));
            Require.NotNull(eventHubConnection, nameof(eventHubConnection));

            config.WithReporting(reports => reports.WithReport(new EventHubReporter(service, eventHubConnection), TimeSpan.FromSeconds(5)));

            return config;
        }

        public static MetricsConfig WithSystemCounters(this MetricsConfig config)
        {
            Require.NotNull(config, nameof(config));

            config.WithConfigExtension((context, hs) =>
            {
                context.Advanced.Gauge("cpu", () => new PerfCounterValueProvider("Processor", "% Processor Time", "_Total"), Unit.Percent);
                context.Advanced.Gauge("memory", () => new MemoryLoadValueProvider(), Unit.Percent);
            });

            return config;
        }

        public static MetricsConfig WithAspNetCounters(this MetricsConfig config)
        {
            Require.NotNull(config, nameof(config));

            config.WithConfigExtension((context, hs) =>
            {
                context.Advanced.Gauge("aspnet_req_rate", () => new AspNetAppPerfCounterValueProvider(AspNetAppPerfCounters.REQUESTS_PER_SECOND), Unit.Custom("requests/s"));
                context.Advanced.Gauge("aspnet_req_exec_time", () => new AspNetPerfCounterValueProvider(AspNetPerfCounters.REQUEST_EXECUTION_TIME), Unit.Custom("ms"));
                context.Advanced.Gauge("aspnet_req_wait_time", () => new AspNetPerfCounterValueProvider(AspNetPerfCounters.REQUEST_WAIT_TIME), Unit.Custom("ms"));
                context.Advanced.Gauge("aspnet_current_req_cnt", () => new AspNetPerfCounterValueProvider(AspNetPerfCounters.REQUESTS_CURRENT), Unit.Requests);
                context.Advanced.Gauge("aspnet_disc_req_cnt", () => new AspNetPerfCounterValueProvider(AspNetPerfCounters.REQUESTS_DISCONNECTED), Unit.Requests);
                context.Advanced.Gauge("aspnet_queued_req_cnt", () => new AspNetPerfCounterValueProvider(AspNetPerfCounters.REQUESTS_QUEUED), Unit.Requests);
                context.Advanced.Gauge("aspnet_rejected_req_cnt", () => new AspNetPerfCounterValueProvider(AspNetPerfCounters.REQUESTS_REJECTED), Unit.Requests);
            });

            return config;
        }

        public static MetricsConfig WithWebServerCounters(this MetricsConfig config)
        {
            Require.NotNull(config, nameof(config));

            config.WithConfigExtension((context, hs) =>
            {
                context.Advanced.Gauge("websrv_conn_attempts_rate", () => new WebServicePerfCounterValueProvider(WebServicePerfCounters.CONNECTION_ATTEMPTS_PER_SECOND), Unit.Custom("connections/s"));
                context.Advanced.Gauge("websrv_current_conn_cnt", () => new WebServicePerfCounterValueProvider(WebServicePerfCounters.CURRENT_CONNECTIONS), Unit.Custom("connections"));
            });

            return config;
        }
    }
}

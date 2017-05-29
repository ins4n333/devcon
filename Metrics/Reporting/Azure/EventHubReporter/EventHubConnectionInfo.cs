using System;
using Journalist;

namespace TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter
{
    public class EventHubConnectionInfo
    {
        public EventHubConnectionInfo(Uri ns, string hub, string sharedAccessSignature)
        {
            Require.NotNull(ns, nameof(ns));
            Require.NotEmpty(hub, nameof(hub));
            Require.NotEmpty(sharedAccessSignature, nameof(sharedAccessSignature));

            Namespace = ns;
            Hub = hub;
            SharedAccessSignature = sharedAccessSignature;
        }

        public Uri Namespace { get; }

        public string Hub { get; }

        public string SharedAccessSignature { get; }
    }
}
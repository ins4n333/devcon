using System.Text;
using Journalist;
using TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter.ReportItems;
using Newtonsoft.Json;

namespace TestWebApp.CustomMetrics.Reporting.Azure.EventHubReporter
{
    public abstract class EventHubReportItem
    {
        private static readonly JsonSerializerSettings s_settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        private readonly ServiceDescriptor m_service;

        protected EventHubReportItem(ServiceDescriptor service, string name)
        {
            Require.NotNull(service, nameof(service));
            Require.NotEmpty(name, nameof(name));

            m_service = service;

            Name = name;
        }

        public string SystemId => m_service.SystemId;

        public string ServiceId => m_service.ServiceId;

        public string DeploymentId => m_service.DeploymentId;

        public string InstanceId => m_service.InstanceId;

        public string Name { get; }

        public abstract MetricEventType MetricType { get; }

        public virtual byte[] ToJson()
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this, s_settings));
        }  
    }
}
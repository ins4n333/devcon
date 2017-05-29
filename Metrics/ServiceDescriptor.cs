using Journalist;

namespace TestWebApp.CustomMetrics
{
    public class ServiceDescriptor
    {
        public ServiceDescriptor(string systemId, string serviceId, string deploymentId, string instanceId)
        {
            Require.NotEmpty(systemId, nameof(systemId));
            Require.NotEmpty(serviceId, nameof(serviceId));
            Require.NotEmpty(deploymentId, nameof(deploymentId));
            Require.NotEmpty(instanceId, nameof(instanceId));

            SystemId = systemId;
            ServiceId = serviceId;
            DeploymentId = deploymentId;
            InstanceId = instanceId;
        }

        public string SystemId { get; }

        public string ServiceId { get; }

        public string DeploymentId { get; }

        public string InstanceId { get; }
    }
}
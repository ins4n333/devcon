namespace TestWebApp.CustomMetrics.ValueProviders
{

    internal class AspNetPerfCounters
    {
        public const string REQUEST_EXECUTION_TIME = "Request Execution Time";
        public const string REQUEST_WAIT_TIME = "Request Wait Time";
        public const string REQUESTS_CURRENT = "Requests Current";
        public const string REQUESTS_DISCONNECTED = "Requests Disconnected";
        public const string REQUESTS_QUEUED = "Requests Queued";
        public const string REQUESTS_REJECTED = "Requests Rejected";
    }
}
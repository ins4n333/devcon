using System;
using Metrics;
using Metrics.Core;
using Serilog;

namespace TestWebApp.CustomMetrics.HealthChecks
{
    public abstract class UcpHealthCheck : HealthCheck
    {
        protected UcpHealthCheck(string name) : base(name)
        {
            Logger = Log.ForContext(GetType()).ForContext("CheckName", name);
        }

        protected UcpHealthCheck(string name, Action check) : base(name, check)
        {
            Logger = Log.ForContext(GetType()).ForContext("CheckName", name);
        }

        protected UcpHealthCheck(string name, Func<string> check) : base(name, check)
        {
            Logger = Log.ForContext(GetType()).ForContext("CheckName", name);
        }

        protected UcpHealthCheck(string name, Func<HealthCheckResult> check) : base(name, check)
        {
            Logger = Log.ForContext(GetType()).ForContext("CheckName", name);
        }

        protected override HealthCheckResult Check()
        {
            try
            {
                return Run();
            }
            catch (Exception exception)
            {
                Logger.Error(exception, "Check execution failed");

                return Error(exception.Message);
            }
        }

        public abstract HealthCheckResult Run();

        protected ILogger Logger
        {
            get;
        }

        protected HealthCheckResult Warning(string message)
        {
            return HealthCheckResult.Unhealthy("[WRN] " + message);
        }

        protected HealthCheckResult Error(string message)
        {
            return HealthCheckResult.Unhealthy("[ERR] " + message);
        }

        protected HealthCheckResult Ok(string message)
        {
            return HealthCheckResult.Healthy("[OK] " + message);
        }
    }
}
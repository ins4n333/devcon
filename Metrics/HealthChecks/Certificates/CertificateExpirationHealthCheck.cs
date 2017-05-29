using System;
using System.Security.Cryptography.X509Certificates;
using Journalist;
using Metrics;

namespace TestWebApp.CustomMetrics.HealthChecks.Certificates
{
    public class CertificateExpirationHealthCheck : UcpHealthCheck
    {
        private readonly CertificateInfo m_certificate;

        public CertificateExpirationHealthCheck(string name, CertificateInfo certificate) : base(name)
        {
            Require.NotNull(certificate, nameof(certificate));

            m_certificate = certificate;
        }

        public override HealthCheckResult Run()
        {
            var store = new X509Store(m_certificate.StoreName, m_certificate.StoreLocation);
            store.Open(OpenFlags.ReadOnly);
            var certs = store.Certificates.Find(m_certificate.FindType, m_certificate.CertificateIdentity, false);
            var certificate = certs[0];
            var period = certificate.NotAfter.ToUniversalTime() - DateTime.UtcNow;
            var days = period.TotalDays;

            if (days <= 7)
            {
                return Error($"Certificate {certificate.FriendlyName} expires in {days} days");
            }

            if (days <= 30)
            {
                return Warning($"Certificate {certificate.FriendlyName} expires in {days} days");
            }

            return Ok($"Certificate {certificate.FriendlyName} expires in {days} days");
        }
    }
}
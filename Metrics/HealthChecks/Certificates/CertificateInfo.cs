using System.Security.Cryptography.X509Certificates;
using Journalist;

namespace TestWebApp.CustomMetrics.HealthChecks.Certificates
{
    public class CertificateInfo
    {
        public CertificateInfo(string certificateIdentity, string storeName, StoreLocation storeLocation, X509FindType findType)
        {
            Require.NotEmpty(certificateIdentity, nameof(certificateIdentity));
            Require.NotEmpty(storeName, nameof(storeName));

            CertificateIdentity = certificateIdentity;
            StoreName = storeName;
            StoreLocation = storeLocation;
            FindType = findType;
        }

        public CertificateInfo(string certificateIdentity)
        {
            Require.NotEmpty(certificateIdentity, nameof(certificateIdentity));

            CertificateIdentity = certificateIdentity;
            StoreName = "My";
            StoreLocation = StoreLocation.CurrentUser;
            FindType = X509FindType.FindBySubjectName;
        }

        public string CertificateIdentity { get; }

        public string StoreName { get; }

        public StoreLocation StoreLocation { get; }

        public X509FindType FindType { get; }
    }
}
using Microsoft.Extensions.Configuration;

namespace VDCompanyMVC.Models
{
    public class PrivacyOperator
    {
        public PrivacyOperator(IConfiguration configuration)
        {
            Name = configuration["Privacy:OperatorName"];
            Address = configuration["Privacy:OperatorAddress"];
            Requisites = configuration["Privacy:OperatorRequisites"];
            ConsentVersion = configuration["Privacy:ConsentVersion"];
        }

        public string Name { get; }
        public string Address { get; }
        public string Requisites { get; }
        public string ConsentVersion { get; }
        public bool IsIdentified => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Address);
        public bool HasConsentDocument => IsIdentified && !string.IsNullOrWhiteSpace(ConsentVersion);
    }
}

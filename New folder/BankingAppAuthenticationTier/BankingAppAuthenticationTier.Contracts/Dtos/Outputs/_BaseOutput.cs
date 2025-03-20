using BankingAppAuthenticationTier.Contracts.Dtos.Entities;

namespace BankingAppAuthenticationTier.Contracts.Dtos.Outputs
{
    public class BaseOutput
    {
        public BankingAppAuthenticationTierError? Error { get; set; }
    }
}

using BankingAppDataTier.Contracts.Dtos.Entities;

namespace BankingAppDataTier.Contracts.Dtos.Outputs
{
    public class BaseOutput
    {
        public BankingAppDataTierError? Error { get; set; }
    }
}

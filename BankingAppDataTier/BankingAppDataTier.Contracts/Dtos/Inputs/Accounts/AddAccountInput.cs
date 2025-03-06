using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Accounts
{
    public class AddAccountInput
    {
        public required AccountDto Account { get; set; }
    }
}

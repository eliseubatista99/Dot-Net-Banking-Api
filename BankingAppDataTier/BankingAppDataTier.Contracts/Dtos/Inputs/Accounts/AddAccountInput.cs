using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Accounts
{
    public class AddAccountInput : _BaseInput
    {
        public required AccountDto Account { get; set; }
    }
}

using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Accounts
{
    public class GetAccountByIdOutput : _BaseOutput
    {
        public AccountDto? Account { get; set; }
    }
}

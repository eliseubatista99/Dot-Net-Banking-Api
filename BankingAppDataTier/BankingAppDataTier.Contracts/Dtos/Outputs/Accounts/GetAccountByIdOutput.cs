using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Accounts
{
    public class GetAccountByIdOutput : BaseOutput
    {
        public AccountDto? Account { get; set; }
    }
}

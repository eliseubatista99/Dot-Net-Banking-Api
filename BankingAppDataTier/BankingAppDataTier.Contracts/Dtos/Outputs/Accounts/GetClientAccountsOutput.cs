using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Accounts
{
    public class GetClientAccountsOutput: _BaseOutput
    {
        public required List<AccountDto> Accounts {  get; set; }
    }
}

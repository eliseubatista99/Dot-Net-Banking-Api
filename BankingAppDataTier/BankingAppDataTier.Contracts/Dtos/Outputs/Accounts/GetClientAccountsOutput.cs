using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Accounts
{
    public class GetClientAccountsOutput: OperationOutput
    {
        public required List<AccountDto> Accounts {  get; set; }
    }
}

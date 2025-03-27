using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Accounts
{
    public class GetAccountByIdOutput : OperationOutput
    {
        public AccountDto? Account { get; set; }
    }
}

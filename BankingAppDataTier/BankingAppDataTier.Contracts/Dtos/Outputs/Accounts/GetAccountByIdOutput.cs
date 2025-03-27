using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Accounts
{
    public class GetAccountByIdOutput : OperationOutput
    {
        public AccountDto? Account { get; set; }
    }
}

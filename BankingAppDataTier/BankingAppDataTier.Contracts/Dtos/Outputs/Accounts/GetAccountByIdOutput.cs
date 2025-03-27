using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Accounts
{
    [ExcludeFromCodeCoverage]

    public class GetAccountByIdOutput : OperationOutput
    {
        public AccountDto? Account { get; set; }
    }
}

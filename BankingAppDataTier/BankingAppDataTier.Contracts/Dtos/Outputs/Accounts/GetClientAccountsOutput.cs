using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Accounts
{
    [ExcludeFromCodeCoverage]

    public class GetClientAccountsOutput: OperationOutput
    {
        public required List<AccountDto> Accounts {  get; set; }
    }
}

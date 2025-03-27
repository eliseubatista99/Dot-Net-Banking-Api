using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Outputs.Accounts
{
    [ExcludeFromCodeCoverage]

    public class GetClientAccountsOutput : OperationOutput
    {
        public required List<AccountDto> Accounts { get; set; }
    }
}

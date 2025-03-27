using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Accounts
{
    [ExcludeFromCodeCoverage]

    public class GetAccountByIdOutput : OperationOutput
    {
        public AccountDto? Account { get; set; }
    }
}

using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Accounts
{
    [ExcludeFromCodeCoverage]

    public class AddAccountInput : OperationInput
    {
        public required AccountDto Account { get; set; }
    }
}

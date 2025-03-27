using BankingAppDataTier.Contracts.Enums;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Transactions
{
    [ExcludeFromCodeCoverage]

    public class GetTransactionsOfClientInput : OperationInput
    {
        public required string Client { get; set; }

        public List<string>? Accounts { get; set; }

        public List<string>? Cards { get; set; }

        public TransactionRole? Role { get; set; }
    }
}

using BankingAppDataTier.Contracts.Enums;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Transactions
{
    public class GetTransactionsOfClientInput : OperationInput
    {
        public required string Client { get; set; }

        public List<string>? Accounts { get; set; }

        public List<string>? Cards { get; set; }

        public TransactionRole? Role { get; set; }
    }
}

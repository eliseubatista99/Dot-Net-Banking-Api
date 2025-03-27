using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Transactions
{
    public class GetTransactionByIdInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }
    }
}

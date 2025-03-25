using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Loans
{
    public class DeleteLoanInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the loan id.
        /// </summary>
        public required string Id { get; set; }
    }
}

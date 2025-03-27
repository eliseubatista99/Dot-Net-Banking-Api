using ElideusDotNetFramework.Core.Operations;

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

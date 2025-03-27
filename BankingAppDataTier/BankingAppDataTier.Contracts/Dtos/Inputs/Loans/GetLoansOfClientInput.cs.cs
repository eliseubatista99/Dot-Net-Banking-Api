using BankingAppDataTier.Contracts.Enums;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Loans
{
    public class GetLoansOfClientInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public required string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the loan type.
        /// </summary>
        public LoanType? LoanType { get; set; }
    }
}

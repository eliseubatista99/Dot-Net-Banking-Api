using BankingAppDataTier.Contracts.Enums;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Loans
{
    public class GetLoansOfAccountInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        public required string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the loan type.
        /// </summary>
        public LoanType? LoanType { get; set; }
    }
}

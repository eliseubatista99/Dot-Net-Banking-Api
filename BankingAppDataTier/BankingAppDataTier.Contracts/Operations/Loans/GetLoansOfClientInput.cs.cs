using BankingAppDataTier.Contracts.Enums;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Loans
{
    [ExcludeFromCodeCoverage]

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

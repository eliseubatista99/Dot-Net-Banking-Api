using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Inputs.Loans
{
    [ExcludeFromCodeCoverage]

    public class AmortizeLoanInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the loan id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public required decimal Amount { get; set; }
    }
}

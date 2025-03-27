using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Loans
{
    [ExcludeFromCodeCoverage]

    public class GetLoanByIdInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the loan id.
        /// </summary>
        public required string Id { get; set; }
    }
}

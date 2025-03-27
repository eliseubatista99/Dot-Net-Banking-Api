using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Inputs.Loans
{
    [ExcludeFromCodeCoverage]

    public class AddLoanInput : OperationInput
    {
        public required LoanDto Loan { get; set; }
    }
}

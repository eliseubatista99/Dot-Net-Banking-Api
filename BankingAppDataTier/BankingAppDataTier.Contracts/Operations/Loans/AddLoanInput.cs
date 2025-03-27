using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Loans
{
    [ExcludeFromCodeCoverage]

    public class AddLoanInput : OperationInput
    {
        public required LoanDto Loan { get; set; }
    }
}

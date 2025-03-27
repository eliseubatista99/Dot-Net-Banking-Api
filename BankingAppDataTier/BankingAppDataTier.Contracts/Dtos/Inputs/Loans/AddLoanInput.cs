using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Loans
{
    [ExcludeFromCodeCoverage]

    public class AddLoanInput : OperationInput
    {
        public required LoanDto Loan { get; set; }
    }
}

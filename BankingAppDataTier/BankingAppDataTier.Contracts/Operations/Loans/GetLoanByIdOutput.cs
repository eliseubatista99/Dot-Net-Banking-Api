using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Loans
{
    [ExcludeFromCodeCoverage]

    public class GetLoanByIdOutput : OperationOutput
    {
        public LoanDto? Loan { get; set; }
    }
}

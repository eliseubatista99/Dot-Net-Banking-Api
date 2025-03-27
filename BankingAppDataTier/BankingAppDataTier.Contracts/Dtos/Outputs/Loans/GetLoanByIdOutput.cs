using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Loans
{
    [ExcludeFromCodeCoverage]

    public class GetLoanByIdOutput : OperationOutput
    {
        public LoanDto? Loan { get; set; }
    }
}

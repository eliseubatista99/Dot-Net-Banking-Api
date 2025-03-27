using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Loans
{
    public class GetLoanByIdOutput : OperationOutput
    {
        public LoanDto? Loan { get; set; }
    }
}

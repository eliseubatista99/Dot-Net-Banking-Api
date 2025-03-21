using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Loans
{
    public class GetLoanByIdOutput : _BaseOutput
    {
        public LoanDto? Loan { get; set; }
    }
}

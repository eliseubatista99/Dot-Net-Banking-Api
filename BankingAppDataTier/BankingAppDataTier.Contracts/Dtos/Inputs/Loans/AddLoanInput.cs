using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Loans
{
    public class AddLoanInput
    {
        public required LoanDto Loan { get; set; }
    }
}

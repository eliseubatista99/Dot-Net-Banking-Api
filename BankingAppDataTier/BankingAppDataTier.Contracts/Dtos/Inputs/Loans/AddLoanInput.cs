using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Loans
{
    public class AddLoanInput : _BaseInput
    {
        public required LoanDto Loan { get; set; }
    }
}

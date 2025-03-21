using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Loans
{
    public class GetLoansOfAccountOutput : _BaseOutput
    {
        public required List<LoanDto> Loans { get; set; }
    }
}

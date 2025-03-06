using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers
{
    public class GetLoanOffersByTypeOutput : BaseOutput
    {
        public required List<LoanOfferDto> LoanOffers { get; set; }
    }
}

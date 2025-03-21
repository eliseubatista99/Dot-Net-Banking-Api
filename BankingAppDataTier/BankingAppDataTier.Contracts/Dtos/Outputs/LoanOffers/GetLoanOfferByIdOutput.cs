using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers
{
    public class GetLoanOfferByIdOutput : _BaseOutput
    {
        public LoanOfferDto? LoanOffer { get; set; }
    }
}

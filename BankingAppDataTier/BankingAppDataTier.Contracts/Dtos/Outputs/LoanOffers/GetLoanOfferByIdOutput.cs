using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers
{
    public class GetLoanOfferByIdOutput : BaseOutput
    {
        public LoanOfferDto? LoanOffer { get; set; }
    }
}

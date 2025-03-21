using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    public class AddLoanOfferInput : _BaseInput
    {
        public required LoanOfferDto LoanOffer { get; set; }
    }
}

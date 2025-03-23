using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers
{
    public class GetLoanOfferByIdOutput : OperationOutput
    {
        public LoanOfferDto? LoanOffer { get; set; }
    }
}

using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers
{
    public class GetLoanOfferByIdOutput : OperationOutput
    {
        public LoanOfferDto? LoanOffer { get; set; }
    }
}

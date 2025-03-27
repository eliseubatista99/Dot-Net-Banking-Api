using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers
{
    public class GetLoanOffersByTypeOutput : OperationOutput
    {
        public required List<LoanOfferDto> LoanOffers { get; set; }
    }
}

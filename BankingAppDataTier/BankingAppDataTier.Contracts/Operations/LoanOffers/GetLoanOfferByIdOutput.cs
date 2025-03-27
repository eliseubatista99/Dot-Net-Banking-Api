using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.LoanOffers
{
    [ExcludeFromCodeCoverage]

    public class GetLoanOfferByIdOutput : OperationOutput
    {
        public LoanOfferDto? LoanOffer { get; set; }
    }
}

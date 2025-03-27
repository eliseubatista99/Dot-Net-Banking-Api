using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers
{
    [ExcludeFromCodeCoverage]

    public class GetLoanOffersByTypeOutput : OperationOutput
    {
        public required List<LoanOfferDto> LoanOffers { get; set; }
    }
}

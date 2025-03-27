using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    [ExcludeFromCodeCoverage]

    public class AddLoanOfferInput : OperationInput
    {
        public required LoanOfferDto LoanOffer { get; set; }
    }
}

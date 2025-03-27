using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    public class AddLoanOfferInput : OperationInput
    {
        public required LoanOfferDto LoanOffer { get; set; }
    }
}

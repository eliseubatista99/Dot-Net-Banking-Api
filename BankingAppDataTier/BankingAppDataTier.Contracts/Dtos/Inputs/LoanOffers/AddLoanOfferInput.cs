using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    public class AddLoanOfferInput : OperationInput
    {
        public required LoanOfferDto LoanOffer { get; set; }
    }
}

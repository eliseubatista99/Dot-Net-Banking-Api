using ElideusDotNetFramework.Core.Errors;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class LoansErrors
    {
        public static Error InvalidRelatedOffer = new Error { Code = "InvalidRelatedOffer", Message = "Invalid Related Loan Offer" };

        public static Error InvalidRelatedAccount = new Error { Code = "InvalidRelatedAccount", Message = "Invalid Related Account" };

        public static Error CantChangeLoanType = new Error { Code = "CantChangeLoanType", Message = "Cannot change an active loan type. Cancel this loan and contract a new one." };

        public static Error InsufficientFunds = new Error { Code = "InsufficientFunds", Message = "The account balance is not enough." };

    }
}

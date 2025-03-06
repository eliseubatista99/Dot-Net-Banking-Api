using BankingAppDataTier.Contracts.Dtos.Entities;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class LoansErrors
    {
        public static BankingAppDataTierError InvalidRelatedOffer = new BankingAppDataTierError { Code = "InvalidRelatedOffer", Message = "Invalid Related Loan Offer" };

        public static BankingAppDataTierError InvalidRelatedAccount = new BankingAppDataTierError { Code = "InvalidRelatedAccount", Message = "Invalid Related Account" };

        public static BankingAppDataTierError CantChangeLoanType = new BankingAppDataTierError { Code = "CantChangeLoanType", Message = "Cannot change an active loan type. Cancel this loan and contract a new one." };

    }
}

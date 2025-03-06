using BankingAppDataTier.Contracts.Dtos.Entities;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class LoanOffersErrors
    {
        public static BankingAppDataTierError CantDeleteWithRelatedLoans = new BankingAppDataTierError { Code = "CantDeleteWithRelatedLoans", Message = "In order to delete a Loan Offer first delete the related loans." };
    }
}

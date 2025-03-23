using ElideusDotNetFramework.Errors.Contracts;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class LoanOffersErrors
    {
        public static Error CantDeleteWithRelatedLoans = new Error { Code = "CantDeleteWithRelatedLoans", Message = "In order to delete a Loan Offer first delete the related loans." };
    }
}

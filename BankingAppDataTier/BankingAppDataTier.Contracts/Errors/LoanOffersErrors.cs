using ElideusDotNetFramework.Core.Errors;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Errors
{
    [ExcludeFromCodeCoverage]
    public static class LoanOffersErrors
    {
        public static Error CantDeleteWithRelatedLoans = new Error { Code = "CantDeleteWithRelatedLoans", Message = "In order to delete a Loan Offer first delete the related loans." };
    }
}

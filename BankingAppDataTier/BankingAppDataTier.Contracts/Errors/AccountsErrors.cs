using ElideusDotNetFramework.Core.Errors;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Errors
{
    [ExcludeFromCodeCoverage]

    public static class AccountsErrors
    {
        public static Error InvalidOwnerId = new Error { Code = "InvalidOwnerId", Message = "Invalid Owner Id" };

        public static Error InvalidSourceAccount = new Error { Code = "InvalidSourceAccount", Message = "Invalid Source Account" };

        public static Error CantCloseWithRelatedCards = new Error { Code = "CantCloseWithRelatedCards", Message = "In order to close the account first close the related cards" };

        public static Error CantCloseWithActiveLoans = new Error { Code = "CantCloseWithActiveLoans", Message = "In order to close the account first close the related loans" };

        public static Error MissingInvestementsAccountDetails = new Error { Code = "MissingInvestementsAccountDetails", Message = "SourceAccountId, Duration and Interest are Required To Create An Investments Account" };
    }
}

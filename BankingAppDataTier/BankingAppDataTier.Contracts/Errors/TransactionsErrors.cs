using ElideusDotNetFramework.Errors;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class TransactionsErrors
    {
        public static Error InvalidClientId = new Error { Code = "InvalidClientId", Message = "The specified client id is invalid." };

        public static Error NoAccountsFound = new Error { Code = "NoAccountsFound", Message = "No accounts found for the specified request" };

        public static Error NoCardsFound = new Error { Code = "NoCardsFound", Message = "No cards found for the specified request" };

        public static Error InvalidSourceAccount = new Error { Code = "InvalidSourceAccount", Message = "Source Account Is Invalid" };

        public static Error InvalidSourceCard = new Error { Code = "InvalidSourceCard", Message = "Source Card Is Invalid" };


    }
}

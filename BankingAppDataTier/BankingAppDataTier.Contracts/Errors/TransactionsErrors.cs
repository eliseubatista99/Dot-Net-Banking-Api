using BankingAppDataTier.Contracts.Dtos.Entities;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class TransactionsErrors
    {
        public static BankingAppDataTierError InvalidClientId = new BankingAppDataTierError { Code = "InvalidClientId", Message = "The specified client id is invalid." };

        public static BankingAppDataTierError NoAccountsFound = new BankingAppDataTierError { Code = "NoAccountsFound", Message = "No accounts found for the specified request" };

        public static BankingAppDataTierError NoCardsFound = new BankingAppDataTierError { Code = "NoCardsFound", Message = "No cards found for the specified request" };

        public static BankingAppDataTierError InvalidSourceAccount = new BankingAppDataTierError { Code = "InvalidSourceAccount", Message = "Source Account Is Invalid" };

        public static BankingAppDataTierError InvalidSourceCard = new BankingAppDataTierError { Code = "InvalidSourceCard", Message = "Source Card Is Invalid" };


    }
}

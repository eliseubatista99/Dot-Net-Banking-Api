using BankingAppDataTier.Contracts.Dtos.Entities;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class GenericErrors
    {
        public static BankingAppDataTierError Unauthorized = new BankingAppDataTierError { Code = "Unauthorized", Message = "Unauthorized" };

        public static BankingAppDataTierError InternalServerError = new BankingAppDataTierError { Code = "InternalServerError", Message = "Something Went Wrong" };

        public static BankingAppDataTierError FailedToPerformDatabaseOperation = new BankingAppDataTierError { Code = "FailedToPerformDatabaseOperation", Message = "Failed to Perform The Database Operation" };

        public static BankingAppDataTierError InvalidId = new BankingAppDataTierError { Code = "InvalidId", Message = "No Item Found For The Specified Id" };

        public static BankingAppDataTierError IdAlreadyInUse = new BankingAppDataTierError { Code = "IdAlreadyInUse", Message = "Id is already being used" };
    }
}

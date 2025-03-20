using BankingAppDataTier.Contracts.Dtos.Entities;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class AuthenticationErrors
    {
        public static BankingAppDataTierError InvalidClient = new BankingAppDataTierError { Code = "InvalidClient", Message = "The specified client is invalid." };

        public static BankingAppDataTierError InvalidToken = new BankingAppDataTierError { Code = "InvalidToken", Message = "The provided token is invalid." };

        public static BankingAppDataTierError TokenExpired = new BankingAppDataTierError { Code = "TokenExpired", Message = "The provided token is expired." };

        public static BankingAppDataTierError WrongCode = new BankingAppDataTierError { Code = "WrongCode", Message = "The provided code is invalid." };

        public static BankingAppDataTierError FailedToKeepAlive = new BankingAppDataTierError { Code = "FailedToKeepAlive", Message = "Unable to extend token lifetime." };

        public static BankingAppDataTierError FailedToGenerateToken = new BankingAppDataTierError { Code = "FailedToGenerateToken", Message = "Unable to generate token." };
    }
}

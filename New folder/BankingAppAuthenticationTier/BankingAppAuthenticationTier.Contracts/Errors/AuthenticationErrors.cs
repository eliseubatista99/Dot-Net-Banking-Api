using BankingAppAuthenticationTier.Contracts.Dtos.Entities;

namespace BankingAppAuthenticationTier.Contracts.Errors
{
    public static class AuthenticationErrors
    {
        public static BankingAppAuthenticationTierError InvalidToken = new BankingAppAuthenticationTierError { Code = "InvalidToken", Message = "The provided token is invalid." };

        public static BankingAppAuthenticationTierError TokenExpired = new BankingAppAuthenticationTierError { Code = "TokenExpired", Message = "The provided token is expired." };

        public static BankingAppAuthenticationTierError WrongCode = new BankingAppAuthenticationTierError { Code = "WrongCode", Message = "The provided code is invalid." };

        public static BankingAppAuthenticationTierError FailedToKeepAlive = new BankingAppAuthenticationTierError { Code = "FailedToKeepAlive", Message = "Unable to extend token lifetime." };

        public static BankingAppAuthenticationTierError FailedToGenerateToken = new BankingAppAuthenticationTierError { Code = "FailedToGenerateToken", Message = "Unable to generate token." };
    }
}

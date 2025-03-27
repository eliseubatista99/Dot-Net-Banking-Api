
using ElideusDotNetFramework.Core.Errors;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class AuthenticationErrors
    {
        public static Error InvalidClient = new Error { Code = "InvalidClient", Message = "The specified client is invalid." };

        public static Error InvalidToken = new Error { Code = "InvalidToken", Message = "The provided token is invalid." };

        public static Error TokenExpired = new Error { Code = "TokenExpired", Message = "The provided token is expired." };

        public static Error WrongCode = new Error { Code = "WrongCode", Message = "The provided code is invalid." };

        public static Error FailedToKeepAlive = new Error { Code = "FailedToKeepAlive", Message = "Unable to extend token lifetime." };

        public static Error FailedToGenerateToken = new Error { Code = "FailedToGenerateToken", Message = "Unable to generate token." };
    }
}

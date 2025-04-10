using ElideusDotNetFramework.Core.Errors;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Library.Errors
{
    [ExcludeFromCodeCoverage]

    public static class AuthenticationErrors
    {
        public static Error InvalidClient = new Error { Code = "InvalidClient", Message = "The specified client is invalid." };

        public static Error InvalidToken = new Error { Code = "InvalidToken", Message = "The provided token is invalid." };

        public static Error InvalidRefreshToken = new Error { Code = "InvalidRefreshToken", Message = "The provided refresh token is invalid." };

        public static Error TokenExpired = new Error { Code = "TokenExpired", Message = "The provided token is expired." };

        public static Error WrongCode = new Error { Code = "WrongCode", Message = "The provided code is invalid." };

        public static Error FailedToKeepAlive = new Error { Code = "FailedToKeepAlive", Message = "Unable to extend token lifetime." };

        public static Error FailedToGenerateToken = new Error { Code = "FailedToGenerateToken", Message = "Unable to generate token." };
    }
}

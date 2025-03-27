using ElideusDotNetFramework.Core.Errors;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppBusinessTier.Library.Errors
{
    [ExcludeFromCodeCoverage]

    public static class AuthenticationErrors
    {
        public static Error InvalidToken = new Error { Code = "InvalidToken", Message = "The provided token is invalid." };
    }
}

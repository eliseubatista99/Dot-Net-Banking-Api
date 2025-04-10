using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Library.Configs
{
    [ExcludeFromCodeCoverage]
    public static class AuthenticationConfigs
    {
        public static string AuthenticationSection = "Authentication";
        public static string Issuer = "Issuer";
        public static string Audience = "Audience";
        public static string Key = "Key";
        public static string TokenLifeTime = "TokenLifeTime";
        public static string RefreshTokenLifeTime = "RefreshTokenLifeTime";
    }
}

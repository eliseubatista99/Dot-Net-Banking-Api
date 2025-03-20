namespace BankingAppAuthenticationTier.Contracts.Configs
{
    public static class AuthenticationConfigs
    {
        public static string AuthenticationSection = "Authentication";
        public static string Issuer = "Issuer";
        public static string Audience = "Audience";
        public static string Key = "Key";

        public static readonly int TokenLifetime = 30;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Configs
{
    public static class AuthenticationConfigs
    {
        public static string AuthenticationSection = "Authentication";
        public static string Issuer = "Issuer";
        public static string Audience = "Audience";
        public static string Key = "Key";

        public static readonly TimeSpan TokenLifetime = TimeSpan.FromMinutes(30);

    }
}

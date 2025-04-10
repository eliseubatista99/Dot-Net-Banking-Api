﻿using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Library.Configs
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseConfigs
    {
        public static string DatabaseSection = "Database";
        public static string DatabaseName = "Name";
        public static string DatabaseConnection = "ConnectionString";
    }
}

﻿using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Library.Constants.Database
{
    [ExcludeFromCodeCoverage]
    public static class TokensTable
    {
        public static string TABLE_NAME = "TOKENS";

        public static string COLUMN_TOKEN = "TOKEN";

        public static string COLUMN_CLIENT_ID = "CLIENT_ID";

        public static string COLUMN_EXPIRATION_DATE = "EXPIRATION_DATE";
    }
}

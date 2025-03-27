using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Contracts.Constants.Database
{
    [ExcludeFromCodeCoverage]
    public static class ClientsTable
    {
        public static string TABLE_NAME = "CLIENTS";

        public static string COLUMN_ID = "ID";

        public static string COLUMN_PASSWORD = "PASSWORD";

        public static string COLUMN_NAME = "NAME";

        public static string COLUMN_SURNAME = "SURNAME";

        public static string COLUMN_BIRTH_DATE = "BIRTHDATE";

        public static string COLUMN_VAT_NUMBER = "VAT";

        public static string COLUMN_PHONE_NUMBER = "PHONE";

        public static string COLUMN_EMAIL = "EMAIL";
    }
}

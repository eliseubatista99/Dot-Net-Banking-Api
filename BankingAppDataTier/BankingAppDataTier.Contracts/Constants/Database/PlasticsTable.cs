using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Constants.Database
{
    [ExcludeFromCodeCoverage]
    public static class PlasticsTable
    {
        public static string TABLE_NAME = "PLASTICS";

        public static string COLUMN_ID = "ID";

        public static string COLUMN_NAME = "CARD_NAME";

        public static string COLUMN_TYPE = "CARD_TYPE";

        public static string COLUMN_CASHBACK = "CASHBACK";

        public static string COLUMN_COMISSION = "COMISSION";

        public static string COLUMN_IMAGE = "IMAGE";

        public static string COLUMN_IS_ACTIVE = "IS_ACTIVE";

    }
}

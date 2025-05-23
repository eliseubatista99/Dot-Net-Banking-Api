﻿using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Library.Constants.Database
{
    [ExcludeFromCodeCoverage]
    public static class LoansTable
    {
        public static string TABLE_NAME = "LOANS";

        public static string COLUMN_ID = "ID";

        public static string COLUMN_NAME = "LOAN_NAME";

        public static string COLUMN_RELATED_OFFER = "RELATED_OFFER";

        public static string COLUMN_RELATED_ACCOUNT = "RELATED_ACCOUNT";

        public static string COLUMN_START_DATE = "START_DATE";

        public static string COLUMN_DURATION = "DURATION";

        public static string COLUMN_CONTRACTED_AMOUNT = "CONTRACTED_AMOUNT";

        public static string COLUMN_PAID_AMOUNT = "PAID_AMOUNT";

    }
}

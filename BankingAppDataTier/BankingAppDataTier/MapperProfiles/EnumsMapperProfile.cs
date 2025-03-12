using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Enums;

namespace BankingAppDataTier.MapperProfiles
{
    public static class EnumsMapperProfile
    {
        public static AccountType MapAccountTypeFromString(string value)
        {
            return value switch
            {
                BankingAppDataTierConstants.ACCOUNT_TYPE_CURRENT => AccountType.Current,
                BankingAppDataTierConstants.ACCOUNT_TYPE_SAVINGS => AccountType.Savings,
                BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS => AccountType.Investments,
                _ => AccountType.None,
            };
        }

        public static string MapAccountTypeToString(AccountType value)
        {
            return value switch
            {
                AccountType.Current => BankingAppDataTierConstants.ACCOUNT_TYPE_CURRENT,
                AccountType.Savings => BankingAppDataTierConstants.ACCOUNT_TYPE_SAVINGS,
                AccountType.Investments => BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS,
                _ => ""
            };
        }

        public static CardType MapCardTypeFromString(string value)
        {
            return value switch
            {
                BankingAppDataTierConstants.CARD_TYPE_DEBIT => CardType.Debit,
                BankingAppDataTierConstants.CARD_TYPE_CREDIT => CardType.Credit,
                BankingAppDataTierConstants.CARD_TYPE_PRE_PAID => CardType.PrePaid,
                BankingAppDataTierConstants.CARD_TYPE_MEAL => CardType.Meal,
                _ => CardType.None,
            };
        }

        public static string MapCardTypeToString(CardType value)
        {
            return value switch
            {
                CardType.Debit => BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                CardType.Credit => BankingAppDataTierConstants.CARD_TYPE_CREDIT,
                CardType.PrePaid => BankingAppDataTierConstants.CARD_TYPE_PRE_PAID,
                CardType.Meal => BankingAppDataTierConstants.CARD_TYPE_MEAL,
                _ => ""
            };
        }

        public static LoanType MapLoanTypeFromString(string value)
        {
            return value switch
            {
                BankingAppDataTierConstants.LOAN_TYPE_AUTO => LoanType.Auto,
                BankingAppDataTierConstants.LOAN_TYPE_MORTAGAGE => LoanType.Mortgage,
                BankingAppDataTierConstants.LOAN_TYPE_PERSONAL => LoanType.Personal,
                _ => LoanType.None,
            };
        }

        public static string MapLoanTypeToString(LoanType value)
        {
            return value switch
            {
                LoanType.Auto => BankingAppDataTierConstants.LOAN_TYPE_AUTO,
                LoanType.Mortgage => BankingAppDataTierConstants.LOAN_TYPE_MORTAGAGE,
                LoanType.Personal => BankingAppDataTierConstants.LOAN_TYPE_PERSONAL,
                _ => ""
            };
        }
    }
}

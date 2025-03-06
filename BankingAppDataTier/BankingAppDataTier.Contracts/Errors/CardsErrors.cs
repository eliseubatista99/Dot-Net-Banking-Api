using BankingAppDataTier.Contracts.Dtos.Entities;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class CardsErrors
    {
        public static BankingAppDataTierError InvalidPlastic = new BankingAppDataTierError { Code = "InvalidPlastic", Message = "No plastic was foudn for the specified plastic id" };

        public static BankingAppDataTierError MissingCreditCardDetails = new BankingAppDataTierError { Code = "MissingCreditCardDetails", Message = "PaymentDay and Balance are Required To Create A Credit Card" };

        public static BankingAppDataTierError MissingPrePaidCardDetails = new BankingAppDataTierError { Code = "MissingPrePaidCardDetails", Message = "Balance is Required To Create A PrePaid Card" };
    }
}

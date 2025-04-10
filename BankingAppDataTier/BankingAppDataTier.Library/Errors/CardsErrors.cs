using ElideusDotNetFramework.Core.Errors;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Library.Errors
{
    [ExcludeFromCodeCoverage]
    public static class CardsErrors
    {
        public static Error InvalidAccount = new Error { Code = "InvalidAccount", Message = "No accoount was found for the specified plastic id" };

        public static Error InvalidPlastic = new Error { Code = "InvalidPlastic", Message = "No plastic was found for the specified plastic id" };

        public static Error MissingCreditCardDetails = new Error { Code = "MissingCreditCardDetails", Message = "PaymentDay and Balance are Required To Create A Credit Card" };

        public static Error MissingPrePaidCardDetails = new Error { Code = "MissingPrePaidCardDetails", Message = "Balance is Required To Create A PrePaid Card" };
    }
}

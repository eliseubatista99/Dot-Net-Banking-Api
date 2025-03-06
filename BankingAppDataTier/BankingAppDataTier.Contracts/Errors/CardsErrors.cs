﻿using BankingAppDataTier.Contracts.Dtos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class CardsErrors
    {
        public static BankingAppDataTierError MissingCreditCardDetails = new BankingAppDataTierError { Code = "MissingCreditCardDetails", Message = "PaymentDay and Balance are Required To Create A Credit Card" };

        public static BankingAppDataTierError MissingPrePaidCardDetails = new BankingAppDataTierError { Code = "MissingPrePaidCardDetails", Message = "Balance is Required To Create A PrePaid Card" };
    }
}

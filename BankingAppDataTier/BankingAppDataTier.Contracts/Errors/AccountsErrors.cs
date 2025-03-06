using BankingAppDataTier.Contracts.Dtos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class AccountsErrors
    {
        public static BankingAppDataTierError InvalidOwnerId = new BankingAppDataTierError { Code = "InvalidOwnerId", Message = "Invalid Owner Id" };

        public static BankingAppDataTierError InvalidSourceAccount = new BankingAppDataTierError { Code = "InvalidSourceAccount", Message = "Invalid Source Account" };

        public static BankingAppDataTierError CantCloseWithRelatedCards = new BankingAppDataTierError { Code = "CantCloseWithRelatedCards", Message = "In order to close the account first close the related cards" };

        public static BankingAppDataTierError CantCloseWithActiveLoans = new BankingAppDataTierError { Code = "CantCloseWithActiveLoans", Message = "In order to close the account first close the related loans" };

        public static BankingAppDataTierError MissingInvestementsAccountDetails = new BankingAppDataTierError { Code = "MissingInvestementsAccountDetails", Message = "SourceAccountId, Duration and Interest are Required To Create An Investments Account" };
    }
}

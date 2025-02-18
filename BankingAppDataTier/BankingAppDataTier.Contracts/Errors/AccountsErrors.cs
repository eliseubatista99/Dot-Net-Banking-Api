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
        public static BankingAppDataTierError InvalidAccountId = new BankingAppDataTierError { Code = "InvalidAccountId", Message = "No Account Found For The Specified Account Id" }; 
    
        public static BankingAppDataTierError MissingInvestementsAccountDetails = new BankingAppDataTierError { Code = "MissingInvestementsAccountDetails", Message = "SourceAccountId, Duration and Interest are Required To Create An Investments Account" };

        public static BankingAppDataTierError FailedToCreateNewAccount = new BankingAppDataTierError { Code = "FailedToCreateNewAccount", Message = "Failed to Create New Account" };

        public static BankingAppDataTierError FailedToUpdateAccount = new BankingAppDataTierError { Code = "FailedToUpdateAccount", Message = "Failed to Update Account" };

        public static BankingAppDataTierError FailedToDeleteAccount = new BankingAppDataTierError { Code = "FailedToDeleteAccount", Message = "Failed to Delete Account" };
    }
}

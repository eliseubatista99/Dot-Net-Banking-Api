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

        public static BankingAppDataTierError IdAlreadyInUse = new BankingAppDataTierError { Code = "IdAlreadyInUse", Message = "An Account With This Id Already Exists" };

        public static BankingAppDataTierError MissingInvestementsAccountDetails = new BankingAppDataTierError { Code = "MissingInvestementsAccountDetails", Message = "SourceAccountId, Duration and Interest are Required To Create An Investments Account" };
    }
}

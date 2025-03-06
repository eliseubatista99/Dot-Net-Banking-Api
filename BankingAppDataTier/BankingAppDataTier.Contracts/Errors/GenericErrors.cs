using BankingAppDataTier.Contracts.Dtos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class GenericErrors
    {
        public static BankingAppDataTierError InternalServerError = new BankingAppDataTierError { Code = "InternalServerError", Message = "Something Went Wrong" };

        public static BankingAppDataTierError FailedToPerformDatabaseOperation = new BankingAppDataTierError { Code = "FailedToPerformDatabaseOperation", Message = "Failed to Perform The Database Operation" };

        public static BankingAppDataTierError InvalidId = new BankingAppDataTierError { Code = "InvalidId", Message = "No Item Found For The Specified Id" };

        public static BankingAppDataTierError IdAlreadyInUse = new BankingAppDataTierError { Code = "IdAlreadyInUse", Message = "No item found for the specified id" };
    }
}

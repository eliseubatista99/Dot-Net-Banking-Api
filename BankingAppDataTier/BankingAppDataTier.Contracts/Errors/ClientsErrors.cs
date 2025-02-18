using BankingAppDataTier.Contracts.Dtos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class ClientsErrors
    {
        public static BankingAppDataTierError InvalidClientId = new BankingAppDataTierError { Code = "InvalidClientId", Message = "No Client Found For The Specified Client Id" };
    }
}

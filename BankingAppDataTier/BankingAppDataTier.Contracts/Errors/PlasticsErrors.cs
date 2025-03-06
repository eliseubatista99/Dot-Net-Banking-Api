using BankingAppDataTier.Contracts.Dtos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Errors
{
    public static class PlasticsErrors
    {
        public static BankingAppDataTierError InvalidCardType = new BankingAppDataTierError { Code = "InvalidCardType", Message = "No cards found for the specified type" };
    }
}

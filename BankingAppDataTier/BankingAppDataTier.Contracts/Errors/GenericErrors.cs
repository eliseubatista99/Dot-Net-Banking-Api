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
    }
}

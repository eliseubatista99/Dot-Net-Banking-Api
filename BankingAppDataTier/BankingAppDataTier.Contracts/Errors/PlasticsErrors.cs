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
        public static BankingAppDataTierError CantDeleteWithRelatedCards = new BankingAppDataTierError { Code = "CantDeleteWithRelatedCards", Message = "In order to delete a plastic first delete the related cards." };
    }
}

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
        public static BankingAppDataTierError CantCloseWithActiveAccounts = new BankingAppDataTierError { Code = "CantCloseWithActiveAccounts", Message = "In order to delete the client first close the related accounts" };
    }
}

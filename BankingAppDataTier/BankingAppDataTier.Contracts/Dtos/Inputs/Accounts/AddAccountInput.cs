using BankingAppDataTier.Contracts.Dtos.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Accounts
{
    public class AddAccountInput
    {
        public required AccountDto Account { get; set; }
    }
}

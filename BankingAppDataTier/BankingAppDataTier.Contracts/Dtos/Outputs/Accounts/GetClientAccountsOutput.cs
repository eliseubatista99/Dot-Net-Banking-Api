﻿using BankingAppDataTier.Contracts.Dtos.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Accounts
{
    public class GetClientAccountsOutput: BaseOutput
    {
        public required List<AccountDto> Accounts {  get; set; }
    }
}

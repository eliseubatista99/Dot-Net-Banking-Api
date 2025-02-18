﻿using BankingAppDataTier.Contracts.Dtos.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Outputs
{
    public class GetAccountByIdOutput : BaseOutput
    {
        public AccountDto? Account { get; set; }
    }
}

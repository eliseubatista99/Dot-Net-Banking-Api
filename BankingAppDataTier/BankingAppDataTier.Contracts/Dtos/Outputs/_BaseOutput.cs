﻿using BankingAppDataTier.Contracts.Dtos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Outputs
{
    public class BaseOutput
    {
        public BankingAppDataTierError? Error { get; set; }
    }
}

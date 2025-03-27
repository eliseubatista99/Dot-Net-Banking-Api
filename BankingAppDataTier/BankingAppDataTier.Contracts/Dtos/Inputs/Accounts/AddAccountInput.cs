﻿using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Accounts
{
    public class AddAccountInput : OperationInput
    {
        public required AccountDto Account { get; set; }
    }
}

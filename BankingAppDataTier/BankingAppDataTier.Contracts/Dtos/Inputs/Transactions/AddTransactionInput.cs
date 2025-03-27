﻿using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Transactions
{
    public class AddTransactionInput : OperationInput
    {
        public required TransactionDto Transaction { get; set; }
    }
}

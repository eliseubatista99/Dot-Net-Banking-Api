﻿using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Transactions
{
    public class GetUrgentTransactionsInput
    {
        public required bool Urgent { get; set; }

        public required string Client { get; set; }

        public List<string>? Accounts { get; set; }

        public TransactionRole? Role { get; set; }
    }
}

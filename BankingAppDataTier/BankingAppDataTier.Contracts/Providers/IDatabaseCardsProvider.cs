﻿using BankingAppDataTier.Contracts.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseCardsProvider : INpgsqlDatabaseProvider<CardsTableEntry>
    {
        public List<CardsTableEntry> GetCardsOfAccount(string accountId);

        public List<CardsTableEntry> GetCardsWithPlastic(string plasticId);
    }
}

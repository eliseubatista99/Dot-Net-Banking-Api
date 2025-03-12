﻿using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Tests.Mocks.Database
{
    public static class TransactionsEntriesMock
    {
        private static List<TransactionTableEntry> Entries =
        [
            new TransactionTableEntry
            {
                Id = "Permanent_Transaction_01",
                TransactionDate = new DateTime(2025, 02, 10),
                Description = "Test transfer",
                Amount = 100.35M,
                Fees = 1.25M,
                Urgent = true,
                SourceAccount = "Permanent_Current_01",
                DestinationName = "Eletricity Company",
            },
            new TransactionTableEntry
            {
                Id = "Permanent_Transaction_02",
                TransactionDate = new DateTime(2025, 03, 10),
                Description = "Test transfer from card",
                Amount = 100.35M,
                Fees = 1.25M,
                Urgent = false,
                SourceCard = "Permanent_Current_01",
                DestinationName = "Water Company",
            },
            new TransactionTableEntry
            {
                Id = "Permanent_Transaction_03",
                TransactionDate = new DateTime(2025, 03, 10),
                Description = "Test transfer from card",
                Amount = 100.35M,
                Fees = 1.25M,
                Urgent = false,
                SourceAccount = "Permanent_Current_01",
                DestinationName = "Water Company",
            },
            new TransactionTableEntry
            {
                Id = "To_Edit_Transaction_01",
                TransactionDate = new DateTime(2025, 03, 10),
                Description = "Test transfer from card",
                Amount = 100.35M,
                Fees = 1.25M,
                Urgent = false,
                SourceAccount = "ACJW000000",
                DestinationName = "Water Company",
            },
            new TransactionTableEntry
            {
                Id = "To_Delete_Transaction_01",
                TransactionDate = new DateTime(2025, 03, 10),
                Description = "Test transfer from card",
                Amount = 100.35M,
                Fees = 1.25M,
                Urgent = false,
                SourceAccount = "ACJW000000",
                DestinationName = "Water Company",
            }
        ];

        public static void Mock(IDatabaseTransactionsProvider dbProvider)
        {
            dbProvider.CreateTableIfNotExists();

            var elementsInDb = dbProvider.GetAll();

            //If the database already has entries, don't add anything
            if (elementsInDb.Count > 0)
            {
                return;
            }

            foreach (var entry in Entries)
            {
                dbProvider.Add(entry);
            }
        }
    }
}


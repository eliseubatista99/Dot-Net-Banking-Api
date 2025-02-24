using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Enums;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseTransactionsProvider
    {
        public bool CreateTableIfNotExists();

        public List<TransactionTableEntry> GetAll();

        public TransactionTableEntry? GetById(string id);

        public List<TransactionTableEntry> GetBySourceAccount(string account);

        public List<TransactionTableEntry> GetByDestinationAccount(string account);

        public List<TransactionTableEntry> GetBySourceCard(string card);

        public bool Add(TransactionTableEntry entry);

        public bool Edit(TransactionTableEntry entry);

        public bool Delete(string id);
    }
}

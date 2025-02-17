using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseClientAccountBridgeProvider
    {
        public bool CreateTableIfNotExists();

        public List<ClientAccountBridgeTableEntry> GetAll();

        public ClientAccountBridgeTableEntry? GetByAccountId(string accountId);

        public List<ClientAccountBridgeTableEntry> GetAccountsOfClient(string clientID);

        public string? GetClientOfAccount(string accountId);

        public bool Add(ClientAccountBridgeTableEntry entry);

        public bool Edit(ClientAccountBridgeTableEntry entry);

        public bool Delete(string id);

    }
}

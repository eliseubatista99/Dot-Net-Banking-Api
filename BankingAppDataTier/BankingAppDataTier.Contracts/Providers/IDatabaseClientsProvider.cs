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
    public interface IDatabaseClientsProvider
    {
        public bool CreateTableIfNotExists();

        public List<ClientsTableEntry> GetAll();

        public ClientsTableEntry? GetById(string id);

        public bool Add(ClientsTableEntry entry);

        public bool Edit(ClientsTableEntry entry);

        public bool ChangePassword(string id, string password);

        public bool Delete(string id);
    }
}

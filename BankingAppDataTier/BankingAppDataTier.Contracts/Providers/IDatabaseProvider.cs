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
    public interface IDatabaseProvider
    {
        public bool CreateClientsTableIfNotExists();

        public List<ClientsTableEntry> GetAllClients();

        public ClientsTableEntry? GetClientById(string id);

        public bool AddClient(ClientsTableEntry client);

    }
}

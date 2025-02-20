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
    public interface IDatabaseAccountsProvider
    {
        public bool CreateTableIfNotExists();

        public List<AccountsTableEntry> GetAll();

        public List<AccountsTableEntry> GetAccountsOfClient(string clientId);

        public AccountsTableEntry? GetById(string id);

        public bool Add(AccountsTableEntry entry);

        public bool Edit(AccountsTableEntry entry);

        public bool Delete(string id);
    }
}

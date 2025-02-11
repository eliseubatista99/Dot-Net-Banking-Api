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
        public bool CreateAccountsTableIfNotExists();

        public List<AccountsTableEntry> GetAll();

        public AccountsTableEntry? GetById(string id);

        public bool Add(AccountsTableEntry entry);
    }
}

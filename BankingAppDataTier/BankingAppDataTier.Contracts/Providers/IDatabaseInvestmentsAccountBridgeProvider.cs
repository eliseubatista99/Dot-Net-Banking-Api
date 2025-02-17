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
    public interface IDatabaseInvestmentsAccountBridgeProvider
    {
        public bool CreateTableIfNotExists();

        public List<InvestmentsAccountBridgeTableEntry> GetAll();

        public InvestmentsAccountBridgeTableEntry? GetByInvestmentsAccountId(string investmentsAccountId);

        public List<InvestmentsAccountBridgeTableEntry> GetInvestmentsAccountsOfAccount(string accountId);

        public bool Add(InvestmentsAccountBridgeTableEntry entry);

        public bool Edit(InvestmentsAccountBridgeTableEntry entry);

        public bool Delete(string id);
    }
}

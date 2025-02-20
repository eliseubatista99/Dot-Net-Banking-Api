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
    public interface IDatabasePlasticsProvider
    {
        public bool CreateTableIfNotExists();

        public List<PlasticTableEntry> GetAll();

        public PlasticTableEntry? GetById(string id);

        public List<PlasticTableEntry> GetPlasticsOfCardType(CardType type);

        public bool Add(PlasticTableEntry entry);

        public bool Edit(PlasticTableEntry entry);

        public bool Delete(string id);
    }
}

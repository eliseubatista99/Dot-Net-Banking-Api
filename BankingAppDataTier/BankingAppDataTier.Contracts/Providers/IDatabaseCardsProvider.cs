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
    public interface IDatabaseCardsProvider
    {
        public bool CreateTableIfNotExists();

        public List<CardsTableEntry> GetAll();

        public List<CardsTableEntry> GetCardsOfAccount(string accountId);

        public CardsTableEntry? GetCardById(string id);

        public bool Add(CardsTableEntry entry, string clientId);

        public bool Edit(CardsTableEntry entry);

        public bool Delete(string id);
    }
}

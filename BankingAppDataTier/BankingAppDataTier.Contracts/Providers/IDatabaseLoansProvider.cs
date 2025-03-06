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
    public interface IDatabaseLoansProvider
    {
        public bool CreateTableIfNotExists();

        public List<LoanTableEntry> GetAll();

        public LoanTableEntry? GetById(string id);

        public List<LoanTableEntry> GetByAccount(string relatedAccount);

        public bool Add(LoanTableEntry entry);

        public bool Edit(LoanTableEntry entry);

        public bool Delete(string id);
    }
}

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
    public interface IDatabaseLoanOfferProvider
    {
        public bool CreateTableIfNotExists();

        public List<LoanOfferTableEntry> GetAll();

        public LoanOfferTableEntry? GetById(string id);

        public List<LoanOfferTableEntry> GetByType(string loanType, bool onlyActive = false);

        public bool Add(LoanOfferTableEntry entry);

        public bool Edit(LoanOfferTableEntry entry);

        public bool Delete(string id);
    }
}

using BankingAppDataTier.Contracts.Database;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseLoansProvider
    {
        public bool CreateTableIfNotExists();

        public List<LoanTableEntry> GetAll();

        public LoanTableEntry? GetById(string id);

        public List<LoanTableEntry> GetByAccount(string relatedAccount);

        public List<LoanTableEntry> GetByOffer(string loanOffer);


        public bool Add(LoanTableEntry entry);

        public bool Edit(LoanTableEntry entry);

        public bool Delete(string id);
    }
}

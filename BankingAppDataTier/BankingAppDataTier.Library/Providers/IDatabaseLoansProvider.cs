using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabaseLoansProvider : IDatabaseProvider<LoanTableEntry>
    {
        public List<LoanTableEntry> GetByAccount(string relatedAccount);

        public List<LoanTableEntry> GetByOffer(string loanOffer);
    }
}

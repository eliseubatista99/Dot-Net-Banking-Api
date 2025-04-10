using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabaseLoanOfferProvider : IDatabaseProvider<LoanOfferTableEntry>
    {
        public List<LoanOfferTableEntry> GetByType(string loanType, bool onlyActive = false);
    }
}

using BankingAppDataTier.Contracts.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseLoanOfferProvider : INpgsqlDatabaseProvider<LoanOfferTableEntry>
    {
        public List<LoanOfferTableEntry> GetByType(string loanType, bool onlyActive = false);
    }
}

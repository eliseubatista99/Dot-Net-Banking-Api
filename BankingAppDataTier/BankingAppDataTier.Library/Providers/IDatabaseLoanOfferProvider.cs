using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabaseLoanOfferProvider : INpgsqlDatabaseProvider<LoanOfferTableEntry>
    {
        public List<LoanOfferTableEntry> GetByType(string loanType, bool onlyActive = false);
    }
}

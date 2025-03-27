using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabaseLoansProvider : INpgsqlDatabaseProvider<LoanTableEntry>
    {
        public List<LoanTableEntry> GetByAccount(string relatedAccount);

        public List<LoanTableEntry> GetByOffer(string loanOffer);
    }
}

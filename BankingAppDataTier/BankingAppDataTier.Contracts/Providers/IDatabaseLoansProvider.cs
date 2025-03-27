using BankingAppDataTier.Contracts.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseLoansProvider : INpgsqlDatabaseProvider<LoanTableEntry>
    {
        public List<LoanTableEntry> GetByAccount(string relatedAccount);

        public List<LoanTableEntry> GetByOffer(string loanOffer);
    }
}

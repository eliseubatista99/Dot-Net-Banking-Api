using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class DatabaseLoanProviderMock
    {
        private static readonly object _lock = new object();

        private static DatabaseLoanProvider _dbProvider;

        public static IDatabaseLoansProvider Mock()
        {
            lock (_lock)
            {
                if (_dbProvider != null)
                {
                    return _dbProvider;
                }

                var _configuration = ConfigurationMock.Mock();
                _dbProvider = new DatabaseLoanProvider(_configuration);

                return _dbProvider;
            }
        }
    }
}

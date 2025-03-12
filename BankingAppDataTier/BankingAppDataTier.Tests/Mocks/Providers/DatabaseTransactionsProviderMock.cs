using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class DatabaseTransactionsProviderMock
    {
        private static readonly object _lock = new object();

        private static DatabaseTransactionsProvider _dbProvider;

        public static IDatabaseTransactionsProvider Mock()
        {
            lock (_lock)
            {
                if (_dbProvider != null)
                {
                    return _dbProvider;
                }

                var _configuration = ConfigurationMock.Mock();
                _dbProvider = new DatabaseTransactionsProvider(_configuration);

                return _dbProvider;
            }
        }
    }
}

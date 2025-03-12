using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class DatabaseAccountsProviderMock
    {
        private static readonly object _lock = new object();

        private static DatabaseAccountsProvider _dbProvider;

        public static IDatabaseAccountsProvider Mock()
        {
            lock (_lock)
            {
                if (_dbProvider != null)
                {
                    return _dbProvider;
                }

                var _configuration = ConfigurationMock.Mock();
                _dbProvider = new DatabaseAccountsProvider(_configuration);

                return _dbProvider;
            }
        }
    }
}

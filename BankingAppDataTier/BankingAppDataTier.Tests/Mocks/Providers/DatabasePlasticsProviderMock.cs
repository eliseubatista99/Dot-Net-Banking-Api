using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class DatabasePlasticsProviderMock
    {
        private static readonly object _lock = new object();

        private static DatabasePlasticsProvider _dbProvider;

        public static IDatabasePlasticsProvider Mock()
        {
            lock (_lock)
            {
                if (_dbProvider != null)
                {
                    return _dbProvider;
                }

                var _configuration = ConfigurationMock.Mock();
                _dbProvider = new DatabasePlasticsProvider(_configuration);

                return _dbProvider;
            }
        }
    }
}

using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class DatabaseCardsProviderMock
    {
        private static readonly object _lock = new object();

        private static DatabaseCardsProvider _dbProvider;

        public static IDatabaseCardsProvider Mock()
        {
            lock (_lock)
            {
                if (_dbProvider != null)
                {
                    return _dbProvider;
                }

                var _configuration = ConfigurationMock.Mock();
                var _mapper = MapperProviderMock.Mock();
                _dbProvider = new DatabaseCardsProvider(_configuration, _mapper);

                return _dbProvider;
            }
        }
    }
}

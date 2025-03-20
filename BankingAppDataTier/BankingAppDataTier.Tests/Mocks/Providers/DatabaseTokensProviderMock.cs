using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class DatabaseTokensProviderMock
    {
        private static readonly object _lock = new object();

        private static DatabaseTokenProvider _dbProvider;

        public static IDatabaseTokenProvider Mock()
        {
            lock (_lock)
            {
                if (_dbProvider != null)
                {
                    return _dbProvider;
                }

                var _configuration = ConfigurationMock.Mock();
                var _mapper = MapperProviderMock.Mock();
                _dbProvider = new DatabaseTokenProvider(_configuration, _mapper);

                return _dbProvider;
            }
        }
    }
}

using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class DatabaseClientsProviderMock
    {
        private static readonly object _lock = new object();

        private static DatabaseClientsProvider _dbProvider;

        public static IDatabaseClientsProvider Mock()
        {
            lock (_lock)
            {
                if (_dbProvider == null)
                {


                    var _configuration = ConfigurationMock.Mock();
                    var _mapper = MapperProviderMock.Mock();
                    _dbProvider = new DatabaseClientsProvider(_configuration, _mapper);
                }


                return _dbProvider;
            }
        }
    }
}

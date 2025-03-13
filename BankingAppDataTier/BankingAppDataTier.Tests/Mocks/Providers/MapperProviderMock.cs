using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class MapperProviderMock
    {
        private static readonly object _lock = new object();

        private static MapperProvider _mapperProvider;

        public static IMapperProvider Mock()
        {
            lock (_lock)
            {
                if (_mapperProvider != null)
                {
                    return _mapperProvider;
                }

                _mapperProvider = new MapperProvider();

                return _mapperProvider;
            }
        }
    }
}

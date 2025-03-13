using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class DatabaseLoanOffersProviderMock
    {
        private static readonly object _lock = new object();

        private static DatabaseLoanOffersProvider _dbProvider;

        public static IDatabaseLoanOfferProvider Mock()
        {
            lock (_lock)
            {
                if (_dbProvider != null)
                {
                    return _dbProvider;
                }

                var _configuration = ConfigurationMock.Mock();
                var _mapper = MapperProviderMock.Mock();
                _dbProvider = new DatabaseLoanOffersProvider(_configuration, _mapper);
                return _dbProvider;
            }
        }
    }
}

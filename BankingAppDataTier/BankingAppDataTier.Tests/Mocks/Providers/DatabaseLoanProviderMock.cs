using BankingAppDataTier.Contracts.Providers;
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
                var _mapper = MapperProviderMock.Mock();
                _dbProvider = new DatabaseLoanProvider(_configuration, _mapper);

                return _dbProvider;
            }
        }
    }
}

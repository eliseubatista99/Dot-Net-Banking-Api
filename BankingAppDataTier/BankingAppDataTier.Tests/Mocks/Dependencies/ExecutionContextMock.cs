using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class ExecutionContextMock
    {
        private static IExecutionContext _executionContext;

        public static IExecutionContext Mock()
        {
            if (_executionContext != null)
            {
                return _executionContext;
            }

            var configuration = ConfigurationMock.Mock();
            var logger = LoggerMock<Providers.ExecutionContext>.Mock();
            var mapperProvider = new MapperProvider();

            var authProvider = new AuthenticationProvider(configuration);
            var dbAccountsProvider = new DatabaseAccountsProvider(configuration, mapperProvider);
            var dbCardsProvider = new DatabaseCardsProvider(configuration, mapperProvider);
            var dbClientsProvider = new DatabaseClientsProvider(configuration, mapperProvider);
            var dbLoanOffersProvider = new DatabaseLoanOffersProvider(configuration, mapperProvider);
            var dbLoansProvider = new DatabaseLoanProvider(configuration, mapperProvider);
            var dbPlasticsProvider = new DatabasePlasticsProvider(configuration, mapperProvider);
            var dbTokensProvider = new DatabaseTokenProvider(configuration, mapperProvider);
            var dbTransactionsProvider = new DatabaseTransactionsProvider(configuration, mapperProvider);

            //var executionContext = ExecutionContextMock.Mock();
            _executionContext = new Providers.ExecutionContext(
                logger,
                mapperProvider,
                authProvider,
                dbClientsProvider,
                dbTokensProvider,
                dbAccountsProvider,
                dbPlasticsProvider,
                dbCardsProvider,
                dbTransactionsProvider,
                dbLoanOffersProvider,
                dbLoansProvider);

            return _executionContext;
        }
    }
}

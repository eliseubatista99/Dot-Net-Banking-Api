using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Providers
{
    public class ExecutionContext : IExecutionContext
    {
        private List<object> dependencies = new List<object>();

        public ExecutionContext(
            ILogger<ExecutionContext> _logger,
            IMapperProvider _mapperProvider,
            IAuthenticationProvider _authProvider,
            IDatabaseClientsProvider _dbClientsProvider,
            IDatabaseTokenProvider _dbTokensProvider,
            IDatabaseAccountsProvider _dbAccountsProvider,
            IDatabasePlasticsProvider _dbPlasticsProvider,
            IDatabaseCardsProvider _dbCardsProvider,
            IDatabaseTransactionsProvider _dbTransactionsProvider,
            IDatabaseLoanOfferProvider _dbLoanOffersProvider,
            IDatabaseLoansProvider _dbLoansProvider)
        {
            dependencies.AddRange(new List<object>
            {
                _logger,
                _mapperProvider,
                _authProvider,
                _dbClientsProvider,
                _dbTokensProvider,
                _dbAccountsProvider,
                _dbPlasticsProvider,
                _dbCardsProvider,
                _dbTransactionsProvider,
                _dbLoanOffersProvider,
                _dbLoansProvider
            });
        }

        public T? GetDependency<T>() where T : class
        {
            foreach (var dependency in dependencies)
            {
                if (dependency is T)
                {
                    return (T) dependency;
                }
            }

            return default(T);
        }
    }
}

using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Tests.Mocks;
using ElideusDotNetFramework.Providers.Contracts;
using Microsoft.AspNetCore.Builder;
using BankingAppDataTier.Providers;
using ElideusDotNetFramework.Providers;
using AutoMapper;
using BankingAppDataTier.MapperProfiles;

namespace BankingAppDataTier.Tests
{
    public class TestsApplicationContext : IApplicationContext
    {
        private static bool _initialized = false;
        private static readonly object _lock = new object();
        
        private List<object> dependencies = new List<object>();

        private void Mock()
        {
            lock (_lock)
            {
                if (_initialized)
                {
                    return;
                }

                var configuration = ConfigurationMock.Mock();
                var logger = LoggerMock<ApplicationContext>.Mock();
                var mapperProvider = new MapperProvider();

                mapperProvider.CreateMapper(
                    new List<AutoMapper.Profile>
                    {
                        new TokensMapperProfile(),
                        new ClientsMapperProfile(),
                        new AccountsMapperProfile(),
                        new PlasticsMapperProfile(),
                        new CardsMapperProfile(),
                        new LoanOffersMapperProfile(),
                        new LoansMapperProfile(),
                        new TransactionsMapperProfile(),
                    });

                dependencies.Add(configuration);
                dependencies.Add(logger);
                dependencies.Add(mapperProvider);
                dependencies.Add(new AuthenticationProvider(this));
                dependencies.Add(new DatabaseAccountsProvider(this));
                dependencies.Add(new DatabaseCardsProvider(this));
                dependencies.Add(new DatabaseClientsProvider(this));
                dependencies.Add(new DatabaseLoanOffersProvider(this));
                dependencies.Add(new DatabaseLoanProvider(this));
                dependencies.Add(new DatabasePlasticsProvider(this));
                dependencies.Add(new DatabaseTokenProvider(this));
                dependencies.Add(new DatabaseTransactionsProvider(this));

                var _AuthenticationProviderMock = new AuthenticationProvider(this);
                var _DatabaseClientsProviderMock = new DatabaseClientsProvider(this);
                var _DatabaseTokensProviderMock = new DatabaseTokenProvider(this);
                var _DatabaseAccountsProviderMock = new DatabaseAccountsProvider(this);
                var _DatabasePlasticsProviderMock = new DatabasePlasticsProvider(this);
                var _DatabaseCardsProviderMock = new DatabaseCardsProvider(this);
                var _DatabaseLoanOffersProviderMock = new DatabaseLoanOffersProvider(this);
                var _DatabaseLoanProviderMock = new DatabaseLoanProvider(this);
                var _DatabaseTransactionsProviderMock = new DatabaseTransactionsProvider(this);

                _initialized = true;
            }
        }

        public void AddDependency<TService, TImplementation>(ref WebApplicationBuilder builder) where TService : class
            where TImplementation : class, TService
        {
            //Nothing  
        }

        void IApplicationContext.AddDependency<TService, TImplementation>(ref WebApplicationBuilder builder, TImplementation implementation)
        {
            //Nothing  
        }

        public T? GetDependency<T>() where T : class
        {
            if (dependencies.Count == 0)
            {
                Mock();
            }

            foreach (var dependency in dependencies)
            {
                if (dependency is T)
                {
                    return (T)dependency;
                }
            }

            return default(T);
        }
    }
}

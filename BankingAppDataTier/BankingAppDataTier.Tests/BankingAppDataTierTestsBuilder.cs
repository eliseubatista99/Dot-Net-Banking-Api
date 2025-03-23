using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.MapperProfiles;
using BankingAppDataTier.Providers;
using BankingAppDataTier.Tests.Mocks;
using BankingAppDataTier.Tests.Mocks.Database;
using ElideusDotNetFramework.Providers;
using ElideusDotNetFramework.Providers.Contracts;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests
{
    class BankingAppDataTierTestsBuilder : ElideusDotNetFrameworkTestsBuilder
    {
        // CONTROLLERS
        public static AuthenticationController _AuthenticationControllerMock { get; set; }
        public static CardsController _CardsControllerMock { get; set; }
        public static ClientsController _ClientsControllerMock { get; set; }
        public static LoanOffersController _LoanOffersControllerMock { get; set; }
        public static LoansController _LoansControllerMock { get; set; }
        public static PlasticsController _PlasticsControllerMock { get; set; }
        public static TransactionsController _TransactionsControllerMock { get; set; }

        protected override IMapperProvider MockAutoMapper()
        {
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

            return mapperProvider;
        }

        protected override IApplicationContext MockApplicationContext()
        {
            return new BakingAppDataTierApplicationContextMock().Mock()!;
        }

        private void MockDatabase()
        {
            var _AuthenticationProviderMock = ApplicationContextMock!.GetDependency<IAuthenticationProvider>();
            var _DatabaseClientsProviderMock = ApplicationContextMock!.GetDependency<IDatabaseClientsProvider>();
            var _DatabaseTokensProviderMock = ApplicationContextMock!.GetDependency<IDatabaseTokenProvider>();
            var _DatabaseAccountsProviderMock = ApplicationContextMock!.GetDependency<IDatabaseAccountsProvider>();
            var _DatabasePlasticsProviderMock = ApplicationContextMock!.GetDependency<IDatabasePlasticsProvider>();
            var _DatabaseCardsProviderMock = ApplicationContextMock!.GetDependency<IDatabaseCardsProvider>();
            var _DatabaseLoanOffersProviderMock = ApplicationContextMock!.GetDependency<IDatabaseLoanOfferProvider>();
            var _DatabaseLoanProviderMock = ApplicationContextMock!.GetDependency<IDatabaseLoansProvider>();
            var _DatabaseTransactionsProviderMock = ApplicationContextMock!.GetDependency<IDatabaseTransactionsProvider>();

            // Create databases if not exists
            _DatabaseClientsProviderMock!.CreateTableIfNotExists();
            _DatabaseTokensProviderMock!.CreateTableIfNotExists();
            _DatabaseAccountsProviderMock!.CreateTableIfNotExists();
            _DatabasePlasticsProviderMock!.CreateTableIfNotExists();
            _DatabaseCardsProviderMock!.CreateTableIfNotExists();
            _DatabaseLoanOffersProviderMock!.CreateTableIfNotExists();
            _DatabaseLoanProviderMock!.CreateTableIfNotExists();
            _DatabaseTransactionsProviderMock!.CreateTableIfNotExists();

            // Clean database values
            _DatabaseTransactionsProviderMock.DeleteAll();
            _DatabaseLoanProviderMock.DeleteAll();
            _DatabaseLoanOffersProviderMock.DeleteAll();
            _DatabaseCardsProviderMock.DeleteAll();
            _DatabasePlasticsProviderMock.DeleteAll();
            _DatabaseAccountsProviderMock.DeleteAll();
            _DatabaseTokensProviderMock.DeleteAll();
            _DatabaseClientsProviderMock.DeleteAll();

            // Mock database values
            ClientsEntriesMock.Mock(_DatabaseClientsProviderMock);
            TokensEntriesMock.Mock(_DatabaseTokensProviderMock);
            AccountsEntriesMock.Mock(_DatabaseAccountsProviderMock);
            PlasticsEntriesMock.Mock(_DatabasePlasticsProviderMock);
            CardsEntriesMock.Mock(_DatabaseCardsProviderMock);
            LoanOffersEntriesMock.Mock(_DatabaseLoanOffersProviderMock);
            LoansEntriesMock.Mock(_DatabaseLoanProviderMock);
            TransactionsEntriesMock.Mock(_DatabaseTransactionsProviderMock);

            // Mock controllers

            _AuthenticationControllerMock = new AuthenticationController(ApplicationContextMock!);
            _CardsControllerMock = new CardsController(ApplicationContextMock!);
            _ClientsControllerMock = new ClientsController(ApplicationContextMock!);
            _LoanOffersControllerMock = new LoanOffersController(ApplicationContextMock!);
            _LoansControllerMock = new LoansController(ApplicationContextMock!);
            _PlasticsControllerMock = new PlasticsController(ApplicationContextMock!);
            _TransactionsControllerMock = new TransactionsController(ApplicationContextMock!);
        }

        protected override void ConfigureMocks()
        {
            base.ConfigureMocks();

            ApplicationContextMock!.AddTestDependency(new AuthenticationProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseAccountsProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseCardsProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseClientsProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseLoanOffersProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseLoanProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabasePlasticsProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseTokenProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseTransactionsProvider(ApplicationContextMock));

            MockDatabase();
        }
    }
}

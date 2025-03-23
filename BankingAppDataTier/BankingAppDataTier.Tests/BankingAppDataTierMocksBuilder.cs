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
    class BankingAppDataTierMocksBuilder : ElideusDotNetFrameworkMocksBuilder
    {
        // CONTROLLERS
        public static AuthenticationController _AuthenticationControllerMock { get; set; }
        public static CardsController _CardsControllerMock { get; set; }
        public static ClientsController _ClientsControllerMock { get; set; }
        public static LoanOffersController _LoanOffersControllerMock { get; set; }
        public static LoansController _LoansControllerMock { get; set; }
        public static PlasticsController _PlasticsControllerMock { get; set; }
        public static TransactionsController _TransactionsControllerMock { get; set; }

        public override IMapperProvider MockAutoMapper()
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

        private void MockDatabase(IApplicationContext applicationContext)
        {
            var _AuthenticationProviderMock = applicationContext!.GetDependency<IAuthenticationProvider>();
            var _DatabaseClientsProviderMock = applicationContext!.GetDependency<IDatabaseClientsProvider>();
            var _DatabaseTokensProviderMock = applicationContext!.GetDependency<IDatabaseTokenProvider>();
            var _DatabaseAccountsProviderMock = applicationContext!.GetDependency<IDatabaseAccountsProvider>();
            var _DatabasePlasticsProviderMock = applicationContext!.GetDependency<IDatabasePlasticsProvider>();
            var _DatabaseCardsProviderMock = applicationContext!.GetDependency<IDatabaseCardsProvider>();
            var _DatabaseLoanOffersProviderMock = applicationContext!.GetDependency<IDatabaseLoanOfferProvider>();
            var _DatabaseLoanProviderMock = applicationContext!.GetDependency<IDatabaseLoansProvider>();
            var _DatabaseTransactionsProviderMock = applicationContext!.GetDependency<IDatabaseTransactionsProvider>();

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

            _AuthenticationControllerMock = new AuthenticationController(applicationContext!);
            _CardsControllerMock = new CardsController(applicationContext!);
            _ClientsControllerMock = new ClientsController(applicationContext!);
            _LoanOffersControllerMock = new LoanOffersController(applicationContext!);
            _LoansControllerMock = new LoansController(applicationContext!);
            _PlasticsControllerMock = new PlasticsController(applicationContext!);
            _TransactionsControllerMock = new TransactionsController(applicationContext!);
        }

        public override void ConfigureRemainingMocks(IApplicationContext applicationContext)
        {
            base.ConfigureRemainingMocks(applicationContext);

            applicationContext!.AddTestDependency(new AuthenticationProvider(applicationContext));
            applicationContext!.AddTestDependency(new DatabaseAccountsProvider(applicationContext));
            applicationContext!.AddTestDependency(new DatabaseCardsProvider(applicationContext));
            applicationContext!.AddTestDependency(new DatabaseClientsProvider(applicationContext));
            applicationContext!.AddTestDependency(new DatabaseLoanOffersProvider(applicationContext));
            applicationContext!.AddTestDependency(new DatabaseLoanProvider(applicationContext));
            applicationContext!.AddTestDependency(new DatabasePlasticsProvider(applicationContext));
            applicationContext!.AddTestDependency(new DatabaseTokenProvider(applicationContext));
            applicationContext!.AddTestDependency(new DatabaseTransactionsProvider(applicationContext));

            MockDatabase(applicationContext);
        }
    }
}

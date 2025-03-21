using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks.Database;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class TestMocksBuilder
    {
        private static bool _initialized = false;
        private static readonly object _lock = new object();

        //PROVIDERS
        public static IExecutionContext _ExecutionContextMock { get; set; }

        // CONTROLLERS
        public static AccountsController _AccountsControllerMock { get; set; }
        public static AuthenticationController _AuthenticationControllerMock { get; set; }
        public static CardsController _CardsControllerMock { get; set; }
        public static ClientsController _ClientsControllerMock { get; set; }
        public static LoanOffersController _LoanOffersControllerMock { get; set; }
        public static LoansController _LoansControllerMock { get; set; }
        public static PlasticsController _PlasticsControllerMock { get; set; }
        public static TransactionsController _TransactionsControllerMock { get; set; }

        public static void Mock()
        {
            lock (_lock)
            {
                if (_initialized)
                {
                    return;
                }

                // Mock the providers
                _ExecutionContextMock = ExecutionContextMock.Mock();
                var _AuthenticationProviderMock = _ExecutionContextMock.GetDependency<IAuthenticationProvider>();
                var _DatabaseClientsProviderMock = _ExecutionContextMock.GetDependency<IDatabaseClientsProvider>();
                var _DatabaseTokensProviderMock = _ExecutionContextMock.GetDependency<IDatabaseTokenProvider>();
                var _DatabaseAccountsProviderMock = _ExecutionContextMock.GetDependency<IDatabaseAccountsProvider>();
                var _DatabasePlasticsProviderMock = _ExecutionContextMock.GetDependency<IDatabasePlasticsProvider>();
                var _DatabaseCardsProviderMock = _ExecutionContextMock.GetDependency<IDatabaseCardsProvider>();
                var _DatabaseLoanOffersProviderMock = _ExecutionContextMock.GetDependency<IDatabaseLoanOfferProvider>();
                var _DatabaseLoanProviderMock = _ExecutionContextMock.GetDependency<IDatabaseLoansProvider>();
                var _DatabaseTransactionsProviderMock = _ExecutionContextMock.GetDependency<IDatabaseTransactionsProvider>();

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

                _AuthenticationControllerMock = new AuthenticationController(_ExecutionContextMock);
                _AccountsControllerMock = new AccountsController(_ExecutionContextMock);
                _CardsControllerMock = new CardsController(_ExecutionContextMock);
                _ClientsControllerMock = new ClientsController(_ExecutionContextMock);
                _LoanOffersControllerMock = new LoanOffersController(_ExecutionContextMock);
                _LoansControllerMock = new LoansController(_ExecutionContextMock);
                _PlasticsControllerMock = new PlasticsController(_ExecutionContextMock);
                _TransactionsControllerMock = new TransactionsController(_ExecutionContextMock);

                _initialized = true;
            }
        }
    }
}

using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.DatabaseInitializers;
using BankingAppDataTier.Tests.Mocks.Database;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class TestMocksBuilder
    {
        private static bool _initialized = false;
        private static readonly object _lock = new object();

        //PROVIDERS
        public static IAuthenticationProvider _AuthenticationProviderMock { get; set; }
        public static IDatabaseAccountsProvider _DatabaseAccountsProviderMock { get; set; }
        public static IDatabaseCardsProvider _DatabaseCardsProviderMock { get; set; }
        public static IDatabaseClientsProvider _DatabaseClientsProviderMock { get; set; }
        public static IDatabaseLoanOfferProvider _DatabaseLoanOffersProviderMock { get; set; }
        public static IDatabaseLoansProvider _DatabaseLoanProviderMock { get; set; }
        public static IDatabasePlasticsProvider _DatabasePlasticsProviderMock { get; set; }
        public static IDatabaseTransactionsProvider _DatabaseTransactionsProviderMock { get; set; }


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
                _AuthenticationProviderMock = AuthenticationProviderMock.Mock();
                _DatabaseAccountsProviderMock = DatabaseAccountsProviderMock.Mock();
                _DatabaseCardsProviderMock = DatabaseCardsProviderMock.Mock();
                _DatabaseClientsProviderMock = DatabaseClientsProviderMock.Mock();
                _DatabaseLoanOffersProviderMock = DatabaseLoanOffersProviderMock.Mock();
                _DatabaseLoanProviderMock = DatabaseLoanProviderMock.Mock();
                _DatabasePlasticsProviderMock = DatabasePlasticsProviderMock.Mock();
                _DatabaseTransactionsProviderMock = DatabaseTransactionsProviderMock.Mock();

                // Clean database values
                _DatabaseTransactionsProviderMock.DeleteAll();
                _DatabaseLoanProviderMock.DeleteAll();
                _DatabaseLoanOffersProviderMock.DeleteAll();
                _DatabaseCardsProviderMock.DeleteAll();
                _DatabasePlasticsProviderMock.DeleteAll();
                _DatabaseAccountsProviderMock.DeleteAll();
                _DatabaseClientsProviderMock.DeleteAll();

                // Mock database values
                ClientsEntriesMock.Mock(_DatabaseClientsProviderMock);
                AccountsEntriesMock.Mock(_DatabaseAccountsProviderMock);
                PlasticsEntriesMock.Mock(_DatabasePlasticsProviderMock);
                CardsEntriesMock.Mock(_DatabaseCardsProviderMock);
                LoanOffersEntriesMock.Mock(_DatabaseLoanOffersProviderMock);
                LoansEntriesMock.Mock(_DatabaseLoanProviderMock);
                TransactionsEntriesMock.Mock(_DatabaseTransactionsProviderMock);

                // Mock controllers
                _AccountsControllerMock = AccountsControllerMock.Mock();
                _AuthenticationControllerMock = AuthenticationControllerMock.Mock();
                _CardsControllerMock = CardsControllerMock.Mock();
                _ClientsControllerMock = ClientsControllerMock.Mock();
                _LoanOffersControllerMock = LoanOffersControllerMock.Mock();
                _LoansControllerMock = LoansControllerMock.Mock();
                _PlasticsControllerMock = PlasticsControllerMock.Mock();
                _TransactionsControllerMock = TransactionsControllerMock.Mock();

                _initialized = true;
            }
        }
    }
}

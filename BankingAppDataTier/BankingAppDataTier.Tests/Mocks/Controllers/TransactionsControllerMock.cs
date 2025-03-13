namespace BankingAppDataTier.Tests.Mocks
{
    public static class TransactionsControllerMock
    {
        private static BankingAppDataTier.Controllers.TransactionsController _controller;

        public static BankingAppDataTier.Controllers.TransactionsController Mock()
        {
            if (_controller == null)
            {
                var logger = LoggerMock<BankingAppDataTier.Controllers.ClientsController>.Mock();
                var databaseTransactionsProvider = DatabaseTransactionsProviderMock.Mock();
                var databaseClientsProvider = DatabaseClientsProviderMock.Mock();
                var databaseAccountsProvider = DatabaseAccountsProviderMock.Mock();
                var databaseCardsProvider = DatabaseCardsProviderMock.Mock();
                var _mapper = MapperProviderMock.Mock();

                _controller = new BankingAppDataTier.Controllers.TransactionsController(
                    logger,
                    _mapper,
                    databaseTransactionsProvider,
                    databaseClientsProvider,
                    databaseAccountsProvider,
                    databaseCardsProvider);
            }

            return _controller;
        }
    }
}

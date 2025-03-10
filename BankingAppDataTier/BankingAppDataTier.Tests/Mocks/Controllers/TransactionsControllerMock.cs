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

                _controller = new BankingAppDataTier.Controllers.TransactionsController(
                    logger,
                    databaseTransactionsProvider,
                    databaseClientsProvider,
                    databaseAccountsProvider);
            }

            return _controller;
        }
    }
}

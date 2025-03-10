namespace BankingAppDataTier.Tests.Mocks
{
    public static class AccountsControllerMock
    {
        private static BankingAppDataTier.Controllers.AccountsController _controller;

        public static BankingAppDataTier.Controllers.AccountsController Mock()
        {
            if (_controller == null)
            {
                var logger = LoggerMock<BankingAppDataTier.Controllers.ClientsController>.Mock();
                var databaseClientsProvider = DatabaseClientsProviderMock.Mock();
                var databaseAccountsProvider = DatabaseAccountsProviderMock.Mock();
                var databaseCardsProvider = DatabaseCardsProviderMock.Mock();
                var databaseLoansProvider = DatabaseLoanProviderMock.Mock();

                _controller = new BankingAppDataTier.Controllers.AccountsController(
                    logger,
                    databaseClientsProvider,
                    databaseAccountsProvider,
                    databaseCardsProvider,
                    databaseLoansProvider);
            }

            return _controller;
        }
    }
}

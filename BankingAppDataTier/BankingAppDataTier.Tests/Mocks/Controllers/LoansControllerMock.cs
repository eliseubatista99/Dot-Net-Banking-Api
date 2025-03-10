namespace BankingAppDataTier.Tests.Mocks
{
    public static class LoansControllerMock
    {
        private static BankingAppDataTier.Controllers.LoansController _controller;

        public static BankingAppDataTier.Controllers.LoansController Mock()
        {
            if (_controller == null)
            {
                var logger = LoggerMock<BankingAppDataTier.Controllers.ClientsController>.Mock();
                var databaseLoanOffersProvider = DatabaseLoanOffersProviderMock.Mock();
                var databaseAccountsProvider = DatabaseAccountsProviderMock.Mock();
                var databasLoansProvider = DatabaseLoanProviderMock.Mock();

                _controller = new BankingAppDataTier.Controllers.LoansController(logger, databasLoansProvider, databaseAccountsProvider, databaseLoanOffersProvider);
            }

            return _controller;
        }
    }
}

namespace BankingAppDataTier.Tests.Mocks
{
    public static class LoanOffersControllerMock
    {
        private static BankingAppDataTier.Controllers.LoanOffersController _controller;

        public static BankingAppDataTier.Controllers.LoanOffersController Mock()
        {
            if (_controller == null)
            {
                var logger = LoggerMock<BankingAppDataTier.Controllers.ClientsController>.Mock();
                var databaseLoanOffersProvider = DatabaseLoanOffersProviderMock.Mock();
                var databasLoansProvider = DatabaseLoanProviderMock.Mock();

                _controller = new BankingAppDataTier.Controllers.LoanOffersController(logger, databaseLoanOffersProvider, databasLoansProvider);
            }

            return _controller;
        }
    }
}

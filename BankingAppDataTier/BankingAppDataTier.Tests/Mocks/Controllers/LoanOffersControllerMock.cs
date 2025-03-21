namespace BankingAppDataTier.Tests.Mocks
{
    public static class LoanOffersControllerMock
    {
        private static BankingAppDataTier.Controllers.LoanOffersController _controller;

        public static BankingAppDataTier.Controllers.LoanOffersController Mock()
        {
            if (_controller == null)
            {
                var executionContextMock = ExecutionContextMock.Mock();

                _controller = new Controllers.LoanOffersController(executionContextMock);
            }

            return _controller;
        }
    }
}

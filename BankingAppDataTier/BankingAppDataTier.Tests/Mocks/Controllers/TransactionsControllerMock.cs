namespace BankingAppDataTier.Tests.Mocks
{
    public static class TransactionsControllerMock
    {
        private static BankingAppDataTier.Controllers.TransactionsController _controller;

        public static BankingAppDataTier.Controllers.TransactionsController Mock()
        {
            if (_controller == null)
            {
                var executionContextMock = ExecutionContextMock.Mock();

                _controller = new Controllers.TransactionsController(executionContextMock);
            }

            return _controller;
        }
    }
}

namespace BankingAppDataTier.Tests.Mocks
{
    public static class LoansControllerMock
    {
        private static BankingAppDataTier.Controllers.LoansController _controller;

        public static BankingAppDataTier.Controllers.LoansController Mock()
        {
            if (_controller == null)
            {
                var executionContextMock = ExecutionContextMock.Mock();

                _controller = new Controllers.LoansController(executionContextMock);
            }

            return _controller;
        }
    }
}

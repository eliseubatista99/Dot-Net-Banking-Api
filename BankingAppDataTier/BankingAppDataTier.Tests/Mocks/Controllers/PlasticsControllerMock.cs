namespace BankingAppDataTier.Tests.Mocks
{
    public static class PlasticsControllerMock
    {
        private static BankingAppDataTier.Controllers.PlasticsController _controller;

        public static BankingAppDataTier.Controllers.PlasticsController Mock()
        {
            if (_controller == null)
            {
                var executionContextMock = ExecutionContextMock.Mock();

                _controller = new Controllers.PlasticsController(executionContextMock);
            }

            return _controller;
        }
    }
}

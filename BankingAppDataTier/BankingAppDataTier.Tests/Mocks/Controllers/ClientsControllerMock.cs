namespace BankingAppDataTier.Tests.Mocks
{
    public static class ClientsControllerMock
    {
        private static BankingAppDataTier.Controllers.ClientsController _controller;

        public static BankingAppDataTier.Controllers.ClientsController Mock()
        {
            if (_controller == null)
            {
                var executionContextMock = ExecutionContextMock.Mock();

                _controller = new Controllers.ClientsController(executionContextMock);
            }

            return _controller;
        }
    }
}

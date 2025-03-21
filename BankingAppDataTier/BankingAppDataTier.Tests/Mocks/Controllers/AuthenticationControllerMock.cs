namespace BankingAppDataTier.Tests.Mocks
{
    public static class AuthenticationControllerMock
    {
        private static BankingAppDataTier.Controllers.AuthenticationController _controller;

        public static BankingAppDataTier.Controllers.AuthenticationController Mock()
        {
            if (_controller == null)
            {
                var executionContextMock = ExecutionContextMock.Mock();

                _controller = new Controllers.AuthenticationController(executionContextMock);
            }

            return _controller;
        }
    }
}

namespace BankingAppDataTier.Tests.Mocks
{
    public static class AuthenticationControllerMock
    {
        private static BankingAppDataTier.Controllers.AuthenticationController _controller;

        public static BankingAppDataTier.Controllers.AuthenticationController Mock()
        {
            if (_controller == null)
            {
                var logger = LoggerMock<BankingAppDataTier.Controllers.ClientsController>.Mock();
                var configuration = ConfigurationMock.Mock();
                var authProvider = AuthenticationProviderMock.Mock();

                _controller = new BankingAppDataTier.Controllers.AuthenticationController(
                    logger,
                    configuration, 
                    authProvider);
            }

            return _controller;
        }
    }
}

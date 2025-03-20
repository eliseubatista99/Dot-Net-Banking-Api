namespace BankingAppDataTier.Tests.Mocks
{
    public static class AuthenticationControllerMock
    {
        private static BankingAppDataTier.Controllers.AuthenticationController _controller;

        public static BankingAppDataTier.Controllers.AuthenticationController Mock()
        {
            if (_controller == null)
            {
                var logger = LoggerMock<BankingAppDataTier.Controllers.AuthenticationController>.Mock();
                var authProvider = AuthenticationProviderMock.Mock();
                var databaseClientsProvider = DatabaseClientsProviderMock.Mock();
                var databaseTokensProvider = DatabaseTokensProviderMock.Mock();

                _controller = new BankingAppDataTier.Controllers.AuthenticationController(
                    logger,
                    authProvider,
                    databaseClientsProvider,
                    databaseTokensProvider);
            }

            return _controller;
        }
    }
}

namespace BankingAppDataTier.Tests.Mocks
{
    public static class ClientsControllerMock
    {
        private static BankingAppDataTier.Controllers.ClientsController _controller;

        public static BankingAppDataTier.Controllers.ClientsController Mock()
        {
            if (_controller == null)
            {
                var logger = LoggerMock< BankingAppDataTier.Controllers.ClientsController >.Mock();
                var databaseClientsProvider = DatabaseClientsProviderMock.Mock();
                var databaseAccountsProvider = DatabaseAccountsProviderMock.Mock();
                var _mapper = MapperProviderMock.Mock();

                _controller = new BankingAppDataTier.Controllers.ClientsController(
                    logger,
                    _mapper,
                    databaseClientsProvider,
                    databaseAccountsProvider);
            }

            return _controller;
        }
    }
}

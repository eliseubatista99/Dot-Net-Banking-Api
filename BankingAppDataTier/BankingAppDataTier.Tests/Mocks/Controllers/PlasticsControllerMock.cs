namespace BankingAppDataTier.Tests.Mocks
{
    public static class PlasticsControllerMock
    {
        private static BankingAppDataTier.Controllers.PlasticsController _controller;

        public static BankingAppDataTier.Controllers.PlasticsController Mock()
        {
            if (_controller == null)
            {
                var logger = LoggerMock<BankingAppDataTier.Controllers.ClientsController>.Mock();
                var databaseCardsProvider = DatabaseCardsProviderMock.Mock();
                var databasePlasticsProvider = DatabasePlasticsProviderMock.Mock();

                _controller = new BankingAppDataTier.Controllers.PlasticsController(logger, databasePlasticsProvider, databaseCardsProvider);
            }

            return _controller;
        }
    }
}

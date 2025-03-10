namespace BankingAppDataTier.Tests.Mocks
{
    public static class CardsControllerMock
    {
        private static BankingAppDataTier.Controllers.CardsController _controller;

        public static BankingAppDataTier.Controllers.CardsController Mock()
        {
            if (_controller == null)
            {
                var logger = LoggerMock<BankingAppDataTier.Controllers.ClientsController>.Mock();
                var databaseCardsProvider = DatabaseCardsProviderMock.Mock();
                var databasePlasticsProvider = DatabasePlasticsProviderMock.Mock();

                _controller = new BankingAppDataTier.Controllers.CardsController(logger, databaseCardsProvider, databasePlasticsProvider);
            }

            return _controller;
        }
    }
}

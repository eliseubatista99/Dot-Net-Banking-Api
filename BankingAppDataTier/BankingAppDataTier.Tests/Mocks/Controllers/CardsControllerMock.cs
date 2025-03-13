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
                var databaseAccountsProvider = DatabaseAccountsProviderMock.Mock();
                var _mapper = MapperProviderMock.Mock();

                _controller = new BankingAppDataTier.Controllers.CardsController(
                    logger,
                    _mapper,
                    databaseCardsProvider,
                    databasePlasticsProvider,
                    databaseAccountsProvider);
            }

            return _controller;
        }
    }
}

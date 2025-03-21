namespace BankingAppDataTier.Tests.Mocks
{
    public static class CardsControllerMock
    {
        private static BankingAppDataTier.Controllers.CardsController _controller;

        public static BankingAppDataTier.Controllers.CardsController Mock()
        {
            if (_controller == null)
            {
                var executionContextMock = ExecutionContextMock.Mock();

                _controller = new Controllers.CardsController(executionContextMock);
            }

            return _controller;
        }
    }
}

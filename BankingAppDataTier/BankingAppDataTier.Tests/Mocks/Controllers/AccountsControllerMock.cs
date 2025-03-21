namespace BankingAppDataTier.Tests.Mocks
{
    public class AccountsControllerMock
    {
        private static BankingAppDataTier.Controllers.AccountsController _controller;

        public static BankingAppDataTier.Controllers.AccountsController Mock()
        {
            if (_controller == null)
            {
                var executionContextMock = ExecutionContextMock.Mock();

                _controller = new Controllers.AccountsController(executionContextMock);
            }

            return _controller;
        }
    }
}

namespace BankingAppDataTier.Tests.Mocks
{
    public class AccountsControllerMock
    {
        private static BankingAppDataTier.Controllers.AccountsController _controller;

        public static BankingAppDataTier.Controllers.AccountsController Mock()
        {
            if (_controller == null)
            {
                var logger = LoggerMock<BankingAppDataTier.Controllers.ClientsController>.Mock();
                var databaseClientsProvider = DatabaseClientsProviderMock.Mock();
                var databaseAccountsProvider = DatabaseAccountsProviderMock.Mock();
                var databaseCardsProvider = DatabaseCardsProviderMock.Mock();
                var databaseLoansProvider = DatabaseLoanProviderMock.Mock();
                var _mapper = MapperProviderMock.Mock();

                _controller = new BankingAppDataTier.Controllers.AccountsController(
                    logger,
                    _mapper,
                    databaseClientsProvider,
                    databaseAccountsProvider,
                    databaseCardsProvider,
                    databaseLoansProvider);
            }

            return _controller;
        }
    }
}

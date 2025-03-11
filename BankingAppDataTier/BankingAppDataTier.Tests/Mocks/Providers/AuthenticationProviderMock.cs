using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class AuthenticationProviderMock
    {
        private static readonly object _lock = new object();

        private static AuthenticationProvider _authProvider;

        public static IAuthenticationProvider Mock()
        {
            lock (_lock)
            {
                if (_authProvider != null)
                {
                    return _authProvider;
                }

                var _configuration = ConfigurationMock.Mock();
                _authProvider = new AuthenticationProvider(_configuration);

                return _authProvider;
            }
        }
    }
}

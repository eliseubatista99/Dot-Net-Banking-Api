using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class AuthMock
    {
        private static IAuthenticationProvider _auth;

        public static IAuthenticationProvider Mock(Dictionary<string, string?> mock)
        {
            var configuration = ConfigurationMock.Mock();

            _auth = new AuthenticationProvider(configuration);

            return _auth;
        }
    }
}

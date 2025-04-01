using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;

namespace BankingAppDataTier.Tests.Mocks.Providers
{
    public class AuthenticationTierProviderMock : IAuthenticationTierProvider
    {
        public async Task<AuthenticateOutput> Authenticate(AuthenticateInput input)
        {
            await Task.Delay(1);

            return new AuthenticateOutput
            {
                Token = new ElideusDotNetFramework.Core.TokenData
                {
                    Token = "token",
                    ExpirationDateTime = DateTime.UtcNow.AddMinutes(30),
                },
                RefreshToken = new ElideusDotNetFramework.Core.TokenData
                {
                    Token = "token",
                    ExpirationDateTime = DateTime.UtcNow.AddMinutes(30),
                }
            };
        }

        public async Task<IsValidTokenOutput> IsValidToken(IsValidTokenInput input)
        {
            await Task.Delay(1);

            return new IsValidTokenOutput
            {
                IsValid = true,
                ExpirationDateTime = DateTime.Now.AddMinutes(30),
            };
        }
    }
}

using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;

namespace BankingAppDataTier.Tests.Mocks.Providers
{
    public class AuthenticationTierProviderMock : IAuthenticationTierProvider
    {
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

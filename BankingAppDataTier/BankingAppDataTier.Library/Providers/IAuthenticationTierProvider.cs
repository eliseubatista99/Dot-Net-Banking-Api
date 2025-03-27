using BankingAppAuthenticationTier.Contracts.Operations;
using ElideusDotNetFramework.ExternalServices;

namespace BankingAppDataTier.Library.Providers
{
    public interface IAuthenticationTierProvider : IExternalServiceProvider
    {
        public Task<IsValidTokenOutput> IsValidToken(IsValidTokenInput input);
    }
}

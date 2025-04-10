using BankingAppAuthenticationTier.Contracts.Operations;
using ElideusDotNetFramework.ExternalServices;

namespace BankingAppBusinessTier.Library.Providers
{
    public interface IAuthenticationTierProvider : IExternalServiceProvider
    {
        public Task<IsValidTokenOutput> IsValidToken(IsValidTokenInput input);
    }
}

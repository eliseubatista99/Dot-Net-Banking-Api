using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppDataTier.Library.Configs;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.ExternalServices;

namespace BankingAppDataTier.Providers
{
    public class AuthenticationTierProvider : ExternalServiceProvider, IAuthenticationTierProvider
    {
        protected IConfiguration configuration;

        public AuthenticationTierProvider(IApplicationContext _applicationContext) : base(_applicationContext)
        {
            this.configuration = applicationContext.GetDependency<IConfiguration>()!;
        }

        protected override string GetServiceUrl()
        {
            return configuration.GetSection(AuthenticationTierConfigs.Section).GetValue<string>(AuthenticationTierConfigs.Url)!;
        }

        public Task<IsValidTokenOutput> IsValidToken(IsValidTokenInput input)
        {
            return CallExternalPostOperation<IsValidTokenInput, IsValidTokenOutput>("IsValidToken", input);
        }
    }
}

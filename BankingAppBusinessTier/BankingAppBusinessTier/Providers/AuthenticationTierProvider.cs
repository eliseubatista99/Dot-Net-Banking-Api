using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppBusinessTier.Library.Configs;
using BankingAppBusinessTier.Library.Providers;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.ExternalServices;

namespace BankingAppBusinessTier.Providers
{
    public class AuthenticationTierProvider : ExternalServiceProvider, IAuthenticationTierProvider
    {
        protected IConfiguration configuration;

        public AuthenticationTierProvider(IApplicationContext _applicationContext) : base(_applicationContext)
        {
            this.configuration = applicationContext.GetDependency<IConfiguration>()!;
            httpClient = new HttpClient();
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

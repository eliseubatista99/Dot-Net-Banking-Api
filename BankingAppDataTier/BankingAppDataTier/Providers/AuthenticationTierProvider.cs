using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppDataTier.Library.Configs;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.ExternalServices;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BankingAppDataTier.Providers
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

        protected override string GetToken()
        {
            return "tokenzinho";
        }

        public Task<AuthenticateOutput> Authenticate(AuthenticateInput input)
        {
            return CallExternalPostOperation<AuthenticateInput, AuthenticateOutput>("Authenticate", input);
        }

        public Task<IsValidTokenOutput> IsValidToken(IsValidTokenInput input)
        {
            return CallExternalPostOperation<IsValidTokenInput, IsValidTokenOutput>("IsValidToken", input);
        }
    }
}

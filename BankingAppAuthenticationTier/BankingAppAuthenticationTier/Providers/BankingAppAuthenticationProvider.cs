using BankingAppAuthenticationTier.Library.Configs;
using BankingAppAuthenticationTier.Library.Providers;
using ElideusDotNetFramework.Authentication;
using ElideusDotNetFramework.Core;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace BankingAppAuthenticationTier.Providers
{
    [ExcludeFromCodeCoverage]
    public class BankingAppAuthenticationProvider : AuthenticationProvider, IAuthenticationProvider
    {
        private IConfiguration Configuration;

        public BankingAppAuthenticationProvider(IApplicationContext applicationContext) : base(applicationContext)
        {
            this.Configuration = applicationContext.GetDependency<IConfiguration>()!;
        }

        protected override (SymmetricSecurityKey key, string issuer, string audience, DateTime expireDateTime, int lifeTime) GetConfiguration()
        {
            var authConfigs = this.Configuration.GetSection(AuthenticationConfigs.AuthenticationSection);

            var tokenHandler = new JwtSecurityTokenHandler();

            var issuer = authConfigs.GetSection(AuthenticationConfigs.Issuer).Value!;
            var audience = authConfigs.GetSection(AuthenticationConfigs.Audience).Value!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfigs[AuthenticationConfigs.Key]!));
            var lifetime = AuthenticationConfigs.TokenLifetime;
            DateTime expireDateTime = DateTime.UtcNow.AddMinutes(lifetime);

            return (key, issuer, audience, expireDateTime, lifetime);
        }
    }
}

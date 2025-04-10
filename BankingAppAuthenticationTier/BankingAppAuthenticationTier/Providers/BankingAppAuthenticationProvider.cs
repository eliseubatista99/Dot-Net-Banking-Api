using BankingAppAuthenticationTier.Library.Configs;
using ElideusDotNetFramework.Core;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
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

        protected override TokenConfiguration GetTokenConfiguration()
        {
            var authConfigs = this.Configuration.GetSection(AuthenticationConfigs.AuthenticationSection);

            return new TokenConfiguration
            {
                Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfigs[AuthenticationConfigs.Key]!)),
                Audience = authConfigs.GetSection(AuthenticationConfigs.Audience).Value!,
                Issuer = authConfigs.GetSection(AuthenticationConfigs.Issuer).Value!,
                LifeTime = Convert.ToInt16(authConfigs.GetSection(AuthenticationConfigs.TokenLifeTime).Value)!,
            };
        }

        protected override TokenConfiguration GetRefreshTokenConfiguration()
        {
            var authConfigs = this.Configuration.GetSection(AuthenticationConfigs.AuthenticationSection);

            return new TokenConfiguration
            {
                Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfigs[AuthenticationConfigs.Key]!)),
                Audience = authConfigs.GetSection(AuthenticationConfigs.Audience).Value!,
                Issuer = authConfigs.GetSection(AuthenticationConfigs.Issuer).Value!,
                LifeTime = Convert.ToInt16(authConfigs.GetSection(AuthenticationConfigs.RefreshTokenLifeTime).Value)!,
            };
        }

        protected override (List<Claim> claims, string securityAlgorithm) GetGenerationConfigs(string id)
        {
            var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Name, id),
                };

            return (claims, SecurityAlgorithms.HmacSha256);
        }
    }
}

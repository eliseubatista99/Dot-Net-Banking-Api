using Microsoft.AspNetCore.Builder;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IAuthenticationProvider
    {
        public void AddAuthenticationToApplicationBuilder(ref WebApplicationBuilder builder);

        public void AddAuthorizationToSwaggerGen(ref WebApplicationBuilder builder);

        public string GenerateToken(string audience);
    }
}

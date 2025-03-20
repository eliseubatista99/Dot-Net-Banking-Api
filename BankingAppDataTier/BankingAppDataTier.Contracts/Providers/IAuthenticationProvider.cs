using Microsoft.AspNetCore.Builder;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IAuthenticationProvider
    {
        public void AddAuthenticationToApplicationBuilder(ref WebApplicationBuilder builder);

        public void AddAuthorizationToSwaggerGen(ref WebApplicationBuilder builder);

        public (string token, DateTime expirationTime) GenerateToken(string clientId);

        public (bool isValid, DateTime expirationTime) IsValidToken(string token);

        public int GetTokenLifeTime();
    }
}

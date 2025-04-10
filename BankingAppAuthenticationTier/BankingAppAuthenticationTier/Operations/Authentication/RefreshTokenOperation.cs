using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppAuthenticationTier.Library.Errors;
using System.IdentityModel.Tokens.Jwt;

namespace BankingAppAuthenticationTier.Operations
{
    public class RefreshTokenOperation(IApplicationContext context, string endpoint)
        : BankingAppAuthenticationTierOperation<RefreshTokenInput, RefreshTokenOutput>(context, endpoint)
    {
        protected override async Task<RefreshTokenOutput> ExecuteAsync(RefreshTokenInput input)
        {
            var (isValid, _, claims) = authProvider.IsValidToken(input.RefreshToken);

            if (!isValid)
            {
                return new RefreshTokenOutput()
                {
                    Error = AuthenticationErrors.InvalidRefreshToken,
                };
            }

            var nameClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;

            if(nameClaim == null)
            {
                return new RefreshTokenOutput()
                {
                    Error = AuthenticationErrors.InvalidRefreshToken,
                };
            }

            var (token, refreshToken) = authProvider.GenerateTokens(nameClaim);

            //var result = databaseTokensProvider.Edit(token);

            if (token == null || refreshToken == null)
            {
                return new RefreshTokenOutput()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = AuthenticationErrors.FailedToGenerateToken,
                };
            }

            return new RefreshTokenOutput()
            {
                Token = token,
                RefreshToken = refreshToken,
            };
        }
    }
}

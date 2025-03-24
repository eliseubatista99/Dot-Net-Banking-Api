using BankingAppDataTier.Contracts.Dtos.Inputs.Authentication;
using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
using BankingAppDataTier.Contracts.Errors;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

namespace BankingAppDataTier.Operations.Authentication
{
    public class KeepAliveOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<KeepAliveInput, KeepAliveOutput>(context, endpoint)
    {
        protected override async Task<KeepAliveOutput> ExecuteAsync(KeepAliveInput input)
        {            
            var lifeTime = authProvider.GetTokenLifeTime();

            var newExpirationTime = DateTime.Now.AddMinutes(lifeTime);

            var result = databaseTokensProvider.SetExpirationDateTime(input.Token, newExpirationTime);

            if (!result)
            {
                return new KeepAliveOutput()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = AuthenticationErrors.FailedToKeepAlive,
                };
            }

            return new KeepAliveOutput()
            {
                ExpirationDateTime = newExpirationTime,
            };
        }
    }
}

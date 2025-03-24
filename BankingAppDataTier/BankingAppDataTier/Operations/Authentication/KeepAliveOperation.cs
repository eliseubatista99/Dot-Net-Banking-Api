using BankingAppDataTier.Contracts.Dtos.Inputs.Authentication;
using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
using BankingAppDataTier.Contracts.Errors;
using ElideusDotNetFramework.Errors.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

namespace BankingAppDataTier.Operations.Authentication
{
    public class KeepAliveOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<KeepAliveInput, KeepAliveOutput>(context, endpoint)
    {
        protected override async Task<(HttpStatusCode? StatusCode, Error? Error)> ValidateInput(KeepAliveInput input)
        {
            var a = await base.ValidateInput(input);

            return a;
        }

        protected override async Task<KeepAliveOutput> ExecuteAsync(KeepAliveInput input)
        {
            var lifeTime = authProvider.GetTokenLifeTime();

            var newExpirationTime = DateTime.Now.AddMinutes(lifeTime);

            var result = databaseTokensProvider.SetExpirationDateTime(input.Metadata!.Token!, newExpirationTime);

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

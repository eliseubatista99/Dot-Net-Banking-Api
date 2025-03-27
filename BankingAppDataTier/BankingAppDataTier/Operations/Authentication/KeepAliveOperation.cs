﻿using BankingAppDataTier.Contracts.Errors;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Operations.Authentication;

namespace BankingAppDataTier.Operations.Authentication
{
    public class KeepAliveOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<VoidOperationInput, KeepAliveOutput>(context, endpoint)
    {
        protected override async Task<KeepAliveOutput> ExecuteAsync(VoidOperationInput input)
        {
            var (token, tokenValidationError) = ValidateToken(input.Metadata!.Token!);

            var lifeTime = authProvider.GetTokenLifeTime();

            var newExpirationTime = DateTime.Now.AddMinutes(lifeTime);
            token!.ExpirationDate = newExpirationTime;

            var result = databaseTokensProvider.Edit(token);

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

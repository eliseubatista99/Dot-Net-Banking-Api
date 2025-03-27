﻿using ElideusDotNetFramework.Core.Errors;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppAuthenticationTier.Library.Providers;
using BankingAppAuthenticationTier.Library.Database;
using BankingAppAuthenticationTier.Library.Errors;
using ElideusDotNetFramework.Authentication;

namespace BankingAppAuthenticationTier.Operations
{
    public class BankingAppAuthenticationTierOperation<TIn, TOut>(IApplicationContext context, string endpoint) :
        BaseOperation<TIn, TOut>(context, endpoint) where TIn : OperationInput where TOut : OperationOutput
    {
        protected virtual bool UseAuthentication { get; set; } = true;

        protected IMapperProvider mapperProvider;
        protected IAuthenticationProvider authProvider;
        protected IDatabaseTokenProvider databaseTokensProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            mapperProvider = executionContext.GetDependency<IMapperProvider>()!;
            authProvider = executionContext.GetDependency<IAuthenticationProvider>()!;
            databaseTokensProvider = executionContext.GetDependency<IDatabaseTokenProvider>()!;
        }

        protected virtual (TokenTableEntry? token, Error? error) ValidateToken(string token)
        {
            var isValidResult = authProvider.IsValidToken(token);

            if (!isValidResult.isValid)
            {
                return (null, AuthenticationErrors.InvalidToken);
            }

            var tokenInDb = databaseTokensProvider.GetById(token);

            if (tokenInDb == null)
            {
                return (null, AuthenticationErrors.InvalidToken);
            }

            var today = DateTime.Now;

            if (tokenInDb.ExpirationDate.Ticks <= today.Ticks)
            {
                return (tokenInDb, AuthenticationErrors.TokenExpired);
            }

            return (tokenInDb, null);
        } 


        protected override async Task<(HttpStatusCode? StatusCode, Error? Error)> ValidateInput(TIn input)
        {
            if (UseAuthentication)
            {
                if (input.Metadata?.Token == null || input.Metadata.Token == String.Empty)
                {
                    var invalidInputError = InputErrors.InvalidInputField(nameof(input.Metadata.Token));
                    return (HttpStatusCode.BadRequest, invalidInputError);
                }

                var (_, validationError) = ValidateToken(input.Metadata.Token);

                if (validationError != null)
                {
                    return (HttpStatusCode.Unauthorized, validationError);
                }
            }

            return await base.ValidateInput(input);
        }
    }
}

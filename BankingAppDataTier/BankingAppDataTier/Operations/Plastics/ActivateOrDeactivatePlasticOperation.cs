﻿using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Operations;

namespace BankingAppDataTier.Operations
{
    public class ActivateOrDeactivatePlasticOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<ActivateOrDeactivatePlasticInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabasePlasticsProvider databasePlasticsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databasePlasticsProvider = executionContext.GetDependency<IDatabasePlasticsProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(ActivateOrDeactivatePlasticInput input)
        {
            var entryInDb = databasePlasticsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            if (input.Active == entryInDb.IsActive)
            {
                return new VoidOperationOutput();
            }

            entryInDb.IsActive = input.Active;

            var result = databasePlasticsProvider.Edit(entryInDb);

            if (!result)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }

            return new VoidOperationOutput();
        }
    }
}

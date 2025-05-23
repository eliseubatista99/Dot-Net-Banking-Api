﻿using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations
{
    public class GetPlasticByIdOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetPlasticByIdInput, GetPlasticByIdOutput>(context, endpoint)
    {
        private IDatabasePlasticsProvider databasePlasticsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databasePlasticsProvider = executionContext.GetDependency<IDatabasePlasticsProvider>()!;
        }

        protected override async Task<GetPlasticByIdOutput> ExecuteAsync(GetPlasticByIdInput input)
        {
            var itemInDb = databasePlasticsProvider.GetById(input.Id);

            if (itemInDb == null)
            {
                return new GetPlasticByIdOutput()
                {
                    Plastic = null,
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.NotFound,
                };
            }

            return new GetPlasticByIdOutput()
            {
                Plastic = mapperProvider.Map<PlasticTableEntry, PlasticDto>(itemInDb),
            };
        }
    }
}

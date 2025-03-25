﻿using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Providers.Contracts;

namespace BankingAppDataTier.Operations.Plastics
{
    public class GetPlasticsOfTypeOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetPlasticOfTypeInput, GetPlasticsOfTypeOutput>(context, endpoint)
    {
        private IDatabasePlasticsProvider databasePlasticsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databasePlasticsProvider = executionContext.GetDependency<IDatabasePlasticsProvider>()!;
        }

        protected override async Task<GetPlasticsOfTypeOutput> ExecuteAsync(GetPlasticOfTypeInput input)
        {
            var result = new List<PlasticDto>();

            var typeAsString = mapperProvider.Map<CardType, string>(input.PlasticType);

            var plasticsInDb = databasePlasticsProvider.GetPlasticsOfCardType(typeAsString, input.IncludeInactive != true);

            if (plasticsInDb == null || plasticsInDb.Count == 0)
            {
                return new GetPlasticsOfTypeOutput()
                {
                    Plastics = new List<PlasticDto>(),
                };
            }

            result = plasticsInDb.Select(acc => mapperProvider.Map<PlasticTableEntry, PlasticDto>(acc)).ToList();

            return new GetPlasticsOfTypeOutput()
            {
                Plastics = result,
            };
        }
    }
}

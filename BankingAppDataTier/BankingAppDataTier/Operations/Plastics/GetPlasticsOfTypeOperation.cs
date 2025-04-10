using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core;

namespace BankingAppDataTier.Operations
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

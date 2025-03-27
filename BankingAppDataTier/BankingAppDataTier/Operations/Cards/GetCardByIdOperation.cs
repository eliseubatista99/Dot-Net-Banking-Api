using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations.Cards;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations.Cards
{
    public class GetCardByIdOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetCardByIdInput, GetCardByIdOutput>(context, endpoint)
    {
        private IDatabaseCardsProvider databaseCardsProvider;
        private IDatabasePlasticsProvider databasePlasticsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseCardsProvider = executionContext.GetDependency<IDatabaseCardsProvider>()!;
            databasePlasticsProvider = executionContext.GetDependency<IDatabasePlasticsProvider>()!;
        }

        protected override async Task<GetCardByIdOutput> ExecuteAsync(GetCardByIdInput input)
        {
            var itemInDb = databaseCardsProvider.GetById(input.Id);

            if (itemInDb == null)
            {
                return new GetCardByIdOutput()
                {
                    Card = null,
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.NotFound,
                };
            }

            return new GetCardByIdOutput()
            {
                Card = this.BuildCardDto(itemInDb),
            };
        }

        private CardDto BuildCardDto(CardsTableEntry entry)
        {
            var card = mapperProvider.Map<CardsTableEntry, CardDto>(entry);

            var plasticData = databasePlasticsProvider.GetById(card.PlasticId);

            if (plasticData == null)
            {
                return card;
            }

            card.CardType = mapperProvider.Map<string, CardType>(plasticData.CardType);
            card.Image = plasticData.Image;

            return card;
        }
    }
}

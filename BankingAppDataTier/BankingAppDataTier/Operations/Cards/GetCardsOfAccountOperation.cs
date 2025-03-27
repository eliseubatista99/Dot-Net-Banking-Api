using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations.Cards
{
    public class GetCardsOfAccountOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetCardsOfAccountInput, GetCardsOfAccountOutput>(context, endpoint)
    {
        private IDatabaseCardsProvider databaseCardsProvider;
        private IDatabasePlasticsProvider databasePlasticsProvider;
        private IDatabaseAccountsProvider databaseAccountsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseCardsProvider = executionContext.GetDependency<IDatabaseCardsProvider>()!;
            databasePlasticsProvider = executionContext.GetDependency<IDatabasePlasticsProvider>()!;
            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
        }

        protected override async Task<GetCardsOfAccountOutput> ExecuteAsync(GetCardsOfAccountInput input)
        {
            var result = new List<CardDto>();

            var accountInDb = databaseAccountsProvider.GetById(input.AccountId);

            if (accountInDb == null)
            {
                return new GetCardsOfAccountOutput()
                {
                    Cards = null,
                    Error = CardsErrors.InvalidAccount,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
            var cardsInDb = databaseCardsProvider.GetCardsOfAccount(input.AccountId);

            if (cardsInDb == null || cardsInDb.Count == 0)
            {
                return new GetCardsOfAccountOutput()
                {
                    Cards = new List<CardDto>(),
                };
            }

            result = cardsInDb.Select(acc => this.BuildCardDto(acc)).ToList();

            return new GetCardsOfAccountOutput()
            {
                Cards = result,
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

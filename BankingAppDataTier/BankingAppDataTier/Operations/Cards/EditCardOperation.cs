using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations.Cards
{
    public class EditCardOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<EditCardInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseCardsProvider databaseCardsProvider;
        private IDatabasePlasticsProvider databasePlasticsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseCardsProvider = executionContext.GetDependency<IDatabaseCardsProvider>()!;
            databasePlasticsProvider = executionContext.GetDependency<IDatabasePlasticsProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(EditCardInput input)
        {
            var entryInDb = databaseCardsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var card = this.BuildCardDto(entryInDb);

            if (card.CardType == CardType.Credit)
            {
                entryInDb.PaymentDay = input.PaymentDay != null ? input.PaymentDay : entryInDb.PaymentDay;
                entryInDb.Balance = input.Balance != null ? input.Balance : entryInDb.Balance;
            }
            else if (card.CardType == CardType.PrePaid)
            {
                entryInDb.Balance = input.Balance != null ? input.Balance : entryInDb.Balance;
            }

            entryInDb.Name = input.Name != null ? input.Name : entryInDb.Name;
            entryInDb.RequestDate = input.RequestDate != null ? input.RequestDate.GetValueOrDefault() : entryInDb.RequestDate;
            entryInDb.ActivationDate = input.ActivationDate != null ? input.ActivationDate : entryInDb.ActivationDate;
            entryInDb.ExpirationDate = input.ExpirationDate != null ? input.ExpirationDate.GetValueOrDefault() : entryInDb.ExpirationDate;

            var result = databaseCardsProvider.Edit(entryInDb);

            if (!result)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }


            return new VoidOperationOutput()
            {
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

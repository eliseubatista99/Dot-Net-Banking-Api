﻿using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core.Errors;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Operations;

namespace BankingAppDataTier.Operations
{
    public class AddCardOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<AddCardInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseCardsProvider databaseCardsProvider;
        private IDatabasePlasticsProvider databasePlasticsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseCardsProvider = executionContext.GetDependency<IDatabaseCardsProvider>()!;
            databasePlasticsProvider = executionContext.GetDependency<IDatabasePlasticsProvider>()!;
        }

        protected override async Task<(HttpStatusCode? StatusCode, Error? Error)> ValidateInput(HttpRequest request, AddCardInput input)
        {
            var baseValidation = await base.ValidateInput(request, input);

            if (baseValidation.Error == null)
            {
                if (input.Card.CardType == CardType.Credit && (input.Card.PaymentDay == null || input.Card.Balance == null))
                {
                    return (HttpStatusCode.BadRequest, CardsErrors.MissingCreditCardDetails);
                }
                else if (input.Card.CardType == CardType.PrePaid && input.Card.Balance == null)
                {
                    return (HttpStatusCode.BadRequest, CardsErrors.MissingPrePaidCardDetails);
                }
            }

            return baseValidation;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(AddCardInput input)
        {
            var cardInDb = databaseCardsProvider.GetById(input.Card.Id);

            if (cardInDb != null)
            {
                return new VoidOperationOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var relatedPlastic = databasePlasticsProvider.GetById(input.Card.PlasticId);

            if (relatedPlastic == null)
            {
                return new VoidOperationOutput()
                {
                    Error = CardsErrors.InvalidPlastic,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var entry = mapperProvider.Map<CardDto, CardsTableEntry>(input.Card);

            var result = databaseCardsProvider.Add(entry);

            if (!result)
            {
                return new VoidOperationOutput()
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }

            return new VoidOperationOutput()
            {
            };
        }
    }
}

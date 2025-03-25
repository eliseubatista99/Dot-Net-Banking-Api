using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

namespace BankingAppDataTier.Operations.Plastics
{
    public class DeletePlasticOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<DeletePlasticInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabasePlasticsProvider databasePlasticsProvider;
        private IDatabaseCardsProvider databaseCardsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databasePlasticsProvider = executionContext.GetDependency<IDatabasePlasticsProvider>()!;
            databaseCardsProvider = executionContext.GetDependency<IDatabaseCardsProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(DeletePlasticInput input)
        {
            var result = false;
            var entryInDb = databasePlasticsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var cardsWithThisPlastic = databaseCardsProvider.GetCardsWithPlastic(input.Id);

            if (cardsWithThisPlastic != null && cardsWithThisPlastic.Count > 0)
            {
                return new VoidOperationOutput
                {
                    Error = PlasticsErrors.CantDeleteWithRelatedCards,
                    StatusCode = HttpStatusCode.Forbidden,
                };
            }

            result = databasePlasticsProvider.Delete(input.Id);

            if (!result)
            {
                return (new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                    StatusCode = HttpStatusCode.InternalServerError,
                });
            }

            return new VoidOperationOutput();
        }
    }
}

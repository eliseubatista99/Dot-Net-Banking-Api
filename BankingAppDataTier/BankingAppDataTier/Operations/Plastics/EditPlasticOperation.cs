using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Operations;
using ElideusDotNetFramework.Application;
using System.Net;

namespace BankingAppDataTier.Operations.Plastics
{
    public class EditPlasticOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<EditPlasticInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabasePlasticsProvider databasePlasticsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databasePlasticsProvider = executionContext.GetDependency<IDatabasePlasticsProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(EditPlasticInput input)
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

            entryInDb.Name = input.Name != null ? input.Name : entryInDb.Name;
            entryInDb.Cashback = input.Cashback != null ? input.Cashback : entryInDb.Cashback;
            entryInDb.Commission = input.Commission != null ? input.Commission : entryInDb.Commission;
            entryInDb.Image = input.Image != null ? input.Image : entryInDb.Image;


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

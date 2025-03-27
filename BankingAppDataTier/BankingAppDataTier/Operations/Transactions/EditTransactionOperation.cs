using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Operations.Inputs.Transactions;

namespace BankingAppDataTier.Operations.Transactions
{
    public class EditTransactionOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<EditTransactionInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseTransactionsProvider databaseTransactionsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseTransactionsProvider = executionContext.GetDependency<IDatabaseTransactionsProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(EditTransactionInput input)
        {
            var entryInDb = databaseTransactionsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            entryInDb.Description = input.Description != null ? input.Description : entryInDb.Description;
            entryInDb.DestinationName = input.DestinationName != null ? input.DestinationName : entryInDb.DestinationName;

            var result = databaseTransactionsProvider.Edit(entryInDb);

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

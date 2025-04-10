using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Operations;

namespace BankingAppDataTier.Operations
{
    public class DeleteTransactionOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<DeleteTransactionInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseTransactionsProvider databaseTransactionsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseTransactionsProvider = executionContext.GetDependency<IDatabaseTransactionsProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(DeleteTransactionInput input)
        {
            var result = false;
            var entryInDb = databaseTransactionsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }


            result = databaseTransactionsProvider.Delete(input.Id);

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

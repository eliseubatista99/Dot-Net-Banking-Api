using BankingAppDataTier.Contracts.Dtos.Inputs.Transactions;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Operations;
using ElideusDotNetFramework.Application;
using System.Net;

namespace BankingAppDataTier.Operations.Transactions
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

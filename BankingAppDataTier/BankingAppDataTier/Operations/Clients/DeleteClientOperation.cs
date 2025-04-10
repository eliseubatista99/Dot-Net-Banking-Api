using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Operations;

namespace BankingAppDataTier.Operations
{
    public class DeleteClientOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<DeleteClientInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseClientsProvider databaseClientsProvider;
        private IDatabaseAccountsProvider databaseAccountsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseClientsProvider = executionContext.GetDependency<IDatabaseClientsProvider>()!;
            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(DeleteClientInput input)
        {
            var result = false;
            var entryInDb = databaseClientsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var accountsOfClient = databaseAccountsProvider.GetAccountsOfClient(entryInDb.Id);

            if (accountsOfClient != null && accountsOfClient.Count > 0)
            {
                return new VoidOperationOutput
                {
                    Error = ClientsErrors.CantCloseWithActiveAccounts,
                    StatusCode = HttpStatusCode.NotFound,
                };
            }

            result = databaseClientsProvider.Delete(input.Id);

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

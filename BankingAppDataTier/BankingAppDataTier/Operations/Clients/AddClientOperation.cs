using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

namespace BankingAppDataTier.Operations.Clients
{
    public class AddClientOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<AddClientInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseClientsProvider databaseClientsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseClientsProvider = executionContext.GetDependency<IDatabaseClientsProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(AddClientInput input)
        {
            var clientInDb = databaseClientsProvider.GetById(input.Client.Id);

            if (clientInDb != null)
            {
                return new VoidOperationOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var entry = mapperProvider.Map<ClientDto, ClientsTableEntry>(input.Client);
            entry.Password = input.PassWord;

            var result = databaseClientsProvider.Add(entry);

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

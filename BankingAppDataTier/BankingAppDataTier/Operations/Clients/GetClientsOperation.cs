using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Operations;
using ElideusDotNetFramework.Application;

namespace BankingAppDataTier.Operations.Clients
{
    public class GetClientsOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<VoidOperationInput, GetClientsOutput>(context, endpoint)
    {
        private IDatabaseClientsProvider databaseClientsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseClientsProvider = executionContext.GetDependency<IDatabaseClientsProvider>()!;
        }

        protected override async Task<GetClientsOutput> ExecuteAsync(VoidOperationInput input)
        {
            List<ClientDto> result = new List<ClientDto>();

            var itemsInDb = databaseClientsProvider.GetAll();

            return new GetClientsOutput
            {
                Clients = itemsInDb.Select(client => mapperProvider.Map<ClientsTableEntry, ClientDto>(client)).ToList()
            };
        }
    }
}

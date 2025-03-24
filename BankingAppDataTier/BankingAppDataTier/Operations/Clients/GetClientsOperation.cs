using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;
using ElideusDotNetFramework.Errors.Contracts;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

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

using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations
{
    public class GetClientByIdOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetClientByIdInput, GetClientByIdOutput>(context, endpoint)
    {
        private IDatabaseClientsProvider databaseClientsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseClientsProvider = executionContext.GetDependency<IDatabaseClientsProvider>()!;
        }

        protected override async Task<GetClientByIdOutput> ExecuteAsync(GetClientByIdInput input)
        {
            List<ClientDto> result = new List<ClientDto>();

            var itemInDb = databaseClientsProvider.GetById(input.Id);

            if (itemInDb == null)
            {
                return new GetClientByIdOutput()
                {
                    Client = null,
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.NotFound,
                };
            }

            return new GetClientByIdOutput
            {
                Client = mapperProvider.Map<ClientsTableEntry, ClientDto>(itemInDb)
            };
        }
    }
}

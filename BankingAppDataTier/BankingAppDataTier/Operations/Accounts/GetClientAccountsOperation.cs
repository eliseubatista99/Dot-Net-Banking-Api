using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations;
using System.Net;

namespace BankingAppDataTier.Controllers.Accounts
{
    public class GetClientAccountsOperation(IExecutionContext context) : _BankingAppDataTierOperation<GetClientAccountsInput, GetClientAccountsOutput>(context)
    {
        private IMapperProvider mapperProvider;
        private IDatabaseClientsProvider databaseClientsProvider;
        private IDatabaseAccountsProvider databaseAccountsProvider;

        //protected override bool NeedsAuthorization { get; set; } = false;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            mapperProvider = executionContext.GetDependency<IMapperProvider>()!;
            databaseClientsProvider = executionContext.GetDependency<IDatabaseClientsProvider>()!;
            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
        }
        protected override async Task<GetClientAccountsOutput> ExecuteAsync(GetClientAccountsInput input)
        {
            var result = new List<AccountDto>();

            var clientInDb = databaseClientsProvider.GetById(input.ClientId);

            if (clientInDb == null)
            {
                return new GetClientAccountsOutput()
                {
                    Accounts = new List<AccountDto>(),
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var clientAccountsInDb = databaseAccountsProvider.GetAccountsOfClient(input.ClientId);

            if (clientAccountsInDb == null || clientAccountsInDb.Count == 0)
            {
                return new GetClientAccountsOutput()
                {
                    Accounts = new List<AccountDto>(),
                    StatusCode = HttpStatusCode.NoContent,
                };
            }

            result = clientAccountsInDb.Select(acc => mapperProvider.Map<AccountsTableEntry, AccountDto>(acc)).ToList();

            return new GetClientAccountsOutput()
            {
                Accounts = result,
            };
        }
    }
}

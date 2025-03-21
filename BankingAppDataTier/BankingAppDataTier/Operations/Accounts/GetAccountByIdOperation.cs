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
    public class GetAccountByIdOperation(IExecutionContext context) : _BankingAppDataTierOperation<GetAccountByIdInput, GetAccountByIdOutput>(context)
    {
        private IMapperProvider mapperProvider;
        private IDatabaseAccountsProvider databaseAccountsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            mapperProvider = executionContext.GetDependency<IMapperProvider>()!;
            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
        }
        protected override async Task<GetAccountByIdOutput> ExecuteAsync(GetAccountByIdInput input)
        {
            var itemInDb = databaseAccountsProvider.GetById(input.Id);

            if (itemInDb == null)
            {
                return new GetAccountByIdOutput()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Account = null,
                    Error = GenericErrors.InvalidId,
                };
            }

            return new GetAccountByIdOutput()
            {
                Account = mapperProvider.Map<AccountsTableEntry, AccountDto>(itemInDb),
            };
        }
    }
}

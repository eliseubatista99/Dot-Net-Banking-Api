using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Library.Providers;
using System.Net;
using ElideusDotNetFramework.Core;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Operations;

namespace BankingAppDataTier.Operations
{
    public class GetAccountByIdOperation(IApplicationContext context, string endpoint)
            : BankingAppDataTierOperation<GetAccountByIdInput, GetAccountByIdOutput>(context, endpoint)
    {
        private IDatabaseAccountsProvider databaseAccountsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

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

using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using System.Net;
using ElideusDotNetFramework.Core;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Operations.Accounts
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

using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using System.Net;
using ElideusDotNetFramework.Core;
using System.Diagnostics.CodeAnalysis;
using BankingAppDataTier.Contracts.Operations.Outputs.Accounts;
using BankingAppDataTier.Contracts.Operations.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos;

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

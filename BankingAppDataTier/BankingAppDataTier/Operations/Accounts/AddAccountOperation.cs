using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Operations;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

namespace BankingAppDataTier.Controllers.Accounts
{
    public class AddAccountOperation(IApplicationContext context, string endpoint)
        : BaseOperation<AddAccountInput, VoidOperationOutput>(context, endpoint)
    {
        private IMapperProvider mapperProvider;
        private IDatabaseClientsProvider databaseClientsProvider;
        private IDatabaseAccountsProvider databaseAccountsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            mapperProvider = executionContext.GetDependency<IMapperProvider>()!;
            databaseClientsProvider = executionContext.GetDependency<IDatabaseClientsProvider>()!;
            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
        }
        protected override async Task<VoidOperationOutput> ExecuteAsync(AddAccountInput input)
        {
            if (input.Account.AccountType == AccountType.Investments)
            {
                if (input.Account.SourceAccountId == null || input.Account.Duration == null || input.Account.Interest == null)
                {
                    return new VoidOperationOutput
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Error = AccountsErrors.MissingInvestementsAccountDetails,
                    };
                }
            }

            var clientInDb = databaseClientsProvider.GetById(input.Account.OwnerCliendId);

            if (clientInDb == null)
            {
                return new VoidOperationOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = AccountsErrors.InvalidOwnerId,
                };
            }
        

            var accountInDb = databaseAccountsProvider.GetById(input.Account.Id);

            if (accountInDb != null)
            {
                return new VoidOperationOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = GenericErrors.IdAlreadyInUse,
                };
            }

            var entry = mapperProvider.Map<AccountDto, AccountsTableEntry>(input.Account);

            var result = databaseAccountsProvider.Add(entry);

            if (!result)
            {
                return new VoidOperationOutput
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                };
            }

            return new VoidOperationOutput
            {
            };
        }
    }
}

using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations;
using System.Net;

namespace BankingAppDataTier.Controllers.Accounts
{
    public class AddAccountOperation(IExecutionContext context) : _BankingAppDataTierOperation<AddAccountInput, VoidOutput>(context)
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
        protected override async Task<VoidOutput> ExecuteAsync(AddAccountInput input)
        {
            if (input.Account.AccountType == AccountType.Investments)
            {
                if (input.Account.SourceAccountId == null || input.Account.Duration == null || input.Account.Interest == null)
                {
                    return new VoidOutput
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Error = AccountsErrors.MissingInvestementsAccountDetails,
                    };
                }
            }

            var clientInDb = databaseClientsProvider.GetById(input.Account.OwnerCliendId);

            if (clientInDb == null)
            {
                return new VoidOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = AccountsErrors.InvalidOwnerId,
                };
            }
        

            var accountInDb = databaseAccountsProvider.GetById(input.Account.Id);

            if (accountInDb != null)
            {
                return new VoidOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = GenericErrors.IdAlreadyInUse,
                };
            }

            var entry = mapperProvider.Map<AccountDto, AccountsTableEntry>(input.Account);

            var result = databaseAccountsProvider.Add(entry);

            if (!result)
            {
                return new VoidOutput
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                };
            }

            return new VoidOutput
            {
            };
        }
    }
}

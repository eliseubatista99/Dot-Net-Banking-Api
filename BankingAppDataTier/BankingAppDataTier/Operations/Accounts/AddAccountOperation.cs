using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core.Errors;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Operations.Accounts
{
    public class AddAccountOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<AddAccountInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseClientsProvider databaseClientsProvider;
        private IDatabaseAccountsProvider databaseAccountsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseClientsProvider = executionContext.GetDependency<IDatabaseClientsProvider>()!;
            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
        }

        protected override async Task<(HttpStatusCode? StatusCode, Error? Error)> ValidateInput(AddAccountInput input)
        {
            var baseValidation = await base.ValidateInput(input);

            if(baseValidation.Error == null)
            {
                if (input.Account.AccountType == AccountType.Investments)
                {
                    if (input.Account.SourceAccountId == null || input.Account.Duration == null || input.Account.Interest == null)
                    {
                        return (HttpStatusCode.BadRequest, AccountsErrors.MissingInvestementsAccountDetails);
                    }
                }
            }

            return baseValidation;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(AddAccountInput input)
        {
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

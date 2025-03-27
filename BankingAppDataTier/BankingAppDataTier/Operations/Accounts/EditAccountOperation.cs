using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Operations;
using ElideusDotNetFramework.Application;
using System.Net;

namespace BankingAppDataTier.Operations.Accounts
{
    public class EditAccountOperation(IApplicationContext context, string endpoint)
            : BankingAppDataTierOperation<EditAccountInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseAccountsProvider databaseAccountsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
        }
        protected override async Task<VoidOperationOutput> ExecuteAsync(EditAccountInput input)
        {
            var entryInDb = databaseAccountsProvider.GetById(input.AccountId);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = GenericErrors.InvalidId
                };
            }

            if (entryInDb.AccountType == BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS)
            {
                if (input.SourceAccountId != null)
                {
                    var sourceAccountInDb = databaseAccountsProvider.GetById(input.SourceAccountId);

                    if (sourceAccountInDb == null)
                    {
                        return new VoidOperationOutput
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Error = AccountsErrors.InvalidSourceAccount,
                        };
                    }
                }

                entryInDb.SourceAccountId = input.SourceAccountId != null ? input.SourceAccountId : entryInDb.SourceAccountId;
                entryInDb.Duration = input.Duration != null ? input.Duration : entryInDb.Duration;
                entryInDb.Interest = input.Interest != null ? input.Interest : entryInDb.Interest;
            }

            entryInDb.Balance = input.Balance != null ? input.Balance.GetValueOrDefault() : entryInDb.Balance;
            entryInDb.Image = input.Image != null ? input.Image : entryInDb.Image;
            entryInDb.Name = input.Name != null ? input.Name : entryInDb.Name;

            var result = databaseAccountsProvider.Edit(entryInDb);

            if (!result)
            {
                return new VoidOperationOutput
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                };
            }

            return new VoidOperationOutput();
        }
    }
}

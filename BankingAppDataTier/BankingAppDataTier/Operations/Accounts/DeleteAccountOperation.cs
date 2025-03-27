using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core.Operations;
using System.Net;
using ElideusDotNetFramework.Core;
using System.Diagnostics.CodeAnalysis;
using BankingAppDataTier.Contracts.Operations.Inputs.Accounts;

namespace BankingAppDataTier.Operations.Accounts
{
    public class DeleteAccountOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<DeleteAccountInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseAccountsProvider databaseAccountsProvider;
        private IDatabaseCardsProvider databaseCardsProvider;
        private IDatabaseLoansProvider databaseLoansProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
            databaseCardsProvider = executionContext.GetDependency<IDatabaseCardsProvider>()!;
            databaseLoansProvider = executionContext.GetDependency<IDatabaseLoansProvider>()!;
        }
        protected override async Task<VoidOperationOutput> ExecuteAsync(DeleteAccountInput input)
        {
            var result = false;
            var entryInDb = databaseAccountsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = GenericErrors.InvalidId,
                };
            }

            var cardsOfAccount = databaseCardsProvider.GetCardsOfAccount(entryInDb.AccountId);

            if (cardsOfAccount != null && cardsOfAccount.Count > 0)
            {
                return new VoidOperationOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = AccountsErrors.CantCloseWithRelatedCards,
                };
            }

            var loansOfAccount = databaseLoansProvider.GetByAccount(entryInDb.AccountId);

            if (loansOfAccount != null && loansOfAccount.Count > 0)
            {
                return new VoidOperationOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = AccountsErrors.CantCloseWithActiveLoans,
                };
            }

            result = databaseAccountsProvider.Delete(input.Id);

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

using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations;
using System.Net;

namespace BankingAppDataTier.Controllers.Accounts
{
    public class DeleteAccountOperation(IExecutionContext context) : _BankingAppDataTierOperation<DeleteAccountInput, VoidOutput>(context)
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
        protected override async Task<VoidOutput> ExecuteAsync(DeleteAccountInput input)
        {
            var result = false;
            var entryInDb = databaseAccountsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = GenericErrors.InvalidId,
                };
            }

            var cardsOfAccount = databaseCardsProvider.GetCardsOfAccount(entryInDb.AccountId);

            if (cardsOfAccount != null && cardsOfAccount.Count > 0)
            {
                return new VoidOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = AccountsErrors.CantCloseWithRelatedCards,
                };
            }

            var loansOfAccount = databaseLoansProvider.GetByAccount(entryInDb.AccountId);

            if (loansOfAccount != null && loansOfAccount.Count > 0)
            {
                return new VoidOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = AccountsErrors.CantCloseWithActiveLoans,
                };
            }

            result = databaseAccountsProvider.Delete(input.Id);

            if (!result)
            {
                return new VoidOutput
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                };
            }

            return new VoidOutput();
        }
    }
}

using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Transactions;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations.Transactions
{
    public class AddTransactionOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<AddTransactionInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseTransactionsProvider databaseTransactionsProvider;
        private IDatabaseAccountsProvider databaseAccountsProvider;
        private IDatabaseCardsProvider databaseCardsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseTransactionsProvider = executionContext.GetDependency<IDatabaseTransactionsProvider>()!;
            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
            databaseCardsProvider = executionContext.GetDependency<IDatabaseCardsProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(AddTransactionInput input)
        {
            var transactionInDb = databaseTransactionsProvider.GetById(input.Transaction.Id);

            if (transactionInDb != null)
            {
                return new VoidOperationOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            if (input.Transaction.SourceAccount != null)
            {
                var accountInDb = databaseAccountsProvider.GetById(input.Transaction.SourceAccount);

                if (accountInDb == null)
                {
                    return new VoidOperationOutput()
                    {
                        Error = TransactionsErrors.InvalidSourceAccount,
                        StatusCode = HttpStatusCode.BadRequest,
                    };
                }
            }

            if (input.Transaction.SourceCard != null)
            {
                var cardInDb = databaseCardsProvider.GetById(input.Transaction.SourceCard);

                if (cardInDb == null)
                {
                    return new VoidOperationOutput()
                    {
                        Error = TransactionsErrors.InvalidSourceCard,
                        StatusCode = HttpStatusCode.BadRequest,
                    };
                }
            }

            var entry = mapperProvider.Map<TransactionDto, TransactionTableEntry>(input.Transaction);

            var result = databaseTransactionsProvider.Add(entry);

            if (!result)
            {
                return new VoidOperationOutput()
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
            return new VoidOperationOutput();
        }
    }
}

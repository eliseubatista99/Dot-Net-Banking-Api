using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations.Inputs.Transactions;
using BankingAppDataTier.Contracts.Operations.Outputs.Transactions;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations.Transactions
{
    public class GetTransactionByIdOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetTransactionByIdInput, GetTransactionByIdOutput>(context, endpoint)
    {
        private IDatabaseTransactionsProvider databaseTransactionsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseTransactionsProvider = executionContext.GetDependency<IDatabaseTransactionsProvider>()!;
        }

        protected override async Task<GetTransactionByIdOutput> ExecuteAsync(GetTransactionByIdInput input)
        {
            var itemInDb = databaseTransactionsProvider.GetById(input.Id);

            if (itemInDb == null)
            {
                return new GetTransactionByIdOutput()
                {
                    Transaction = null,
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.NotFound,
                };
            }

            return new GetTransactionByIdOutput()
            {
                Transaction = mapperProvider.Map<TransactionTableEntry, TransactionDto>(itemInDb),
            };
        }
    }
}

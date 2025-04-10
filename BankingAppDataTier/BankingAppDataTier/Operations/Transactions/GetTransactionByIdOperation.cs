using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations
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

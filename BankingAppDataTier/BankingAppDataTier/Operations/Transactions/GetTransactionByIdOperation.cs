using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
using BankingAppDataTier.Contracts.Dtos.Inputs.Transactions;
using BankingAppDataTier.Contracts.Dtos.Outputs.Transactions;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Providers;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
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

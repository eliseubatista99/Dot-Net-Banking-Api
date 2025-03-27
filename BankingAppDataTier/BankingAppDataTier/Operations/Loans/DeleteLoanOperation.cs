using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations.Loans
{
    public class DeleteLoanOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<DeleteLoanInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseLoansProvider databaseLoansProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseLoansProvider = executionContext.GetDependency<IDatabaseLoansProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(DeleteLoanInput input)
        {
            var result = false;
            var entryInDb = databaseLoansProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            result = databaseLoansProvider.Delete(input.Id);

            if (!result)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }

            return new VoidOperationOutput();
        }
    }
}

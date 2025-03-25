using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

namespace BankingAppDataTier.Operations.Loans
{
    public class AmortizeLoanOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<AmortizeLoanInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseLoansProvider databaseLoansProvider;
        private IDatabaseAccountsProvider databaseAccountsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseLoansProvider = executionContext.GetDependency<IDatabaseLoansProvider>()!;
            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(AmortizeLoanInput input)
        {
            var entryInDb = databaseLoansProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var relatedAccountInDb = databaseAccountsProvider.GetById(entryInDb.RelatedAccount);

            if (relatedAccountInDb == null)
            {
                return new VoidOperationOutput
                {
                    Error = LoansErrors.InvalidRelatedAccount,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            if (relatedAccountInDb.Balance < input.Amount)
            {
                return new VoidOperationOutput
                {
                    Error = LoansErrors.InsufficientFunds,
                    StatusCode = HttpStatusCode.Forbidden,
                };
            }

            entryInDb.PaidAmount = entryInDb.PaidAmount + input.Amount;

            var result = databaseLoansProvider.Edit(entryInDb);

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

using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Operations.Inputs.LoanOffers;

namespace BankingAppDataTier.Operations.LoanOffers
{
    public class DeleteLoanOfferOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<DeleteLoanOfferInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseLoanOfferProvider databaseLoanOffersProvider;
        private IDatabaseLoansProvider databaseLoansProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseLoanOffersProvider = executionContext.GetDependency<IDatabaseLoanOfferProvider>()!;
            databaseLoansProvider = executionContext.GetDependency<IDatabaseLoansProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(DeleteLoanOfferInput input)
        {
            var entryInDb = databaseLoanOffersProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var loansWithThisOffer = databaseLoansProvider.GetByOffer(entryInDb.Id);

            if (loansWithThisOffer?.Count > 0)
            {
                return new VoidOperationOutput
                {
                    Error = LoanOffersErrors.CantDeleteWithRelatedLoans,
                    StatusCode = HttpStatusCode.Forbidden,
                };
            }

            var result = databaseLoanOffersProvider.Delete(input.Id);

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

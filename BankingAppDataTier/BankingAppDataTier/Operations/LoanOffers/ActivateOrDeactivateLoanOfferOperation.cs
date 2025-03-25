using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

namespace BankingAppDataTier.Operations.LoanOffers
{
    public class ActivateOrDeactivateLoanOfferOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<ActivateOrDeactivateLoanOfferInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseLoanOfferProvider databaseLoanOffersProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseLoanOffersProvider = executionContext.GetDependency<IDatabaseLoanOfferProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(ActivateOrDeactivateLoanOfferInput input)
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

            if (input.Active == entryInDb.IsActive)
            {
                return new VoidOperationOutput();
            }

            entryInDb.IsActive = input.Active;

            var result = databaseLoanOffersProvider.Edit(entryInDb);

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

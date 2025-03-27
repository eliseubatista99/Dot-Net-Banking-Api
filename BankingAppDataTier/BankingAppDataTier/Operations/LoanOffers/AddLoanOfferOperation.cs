using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Operations.LoanOffers;

namespace BankingAppDataTier.Operations.LoanOffers
{
    public class AddLoanOfferOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<AddLoanOfferInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseLoanOfferProvider databaseLoanOffersProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseLoanOffersProvider = executionContext.GetDependency<IDatabaseLoanOfferProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(AddLoanOfferInput input)
        {
            var itemInDb = databaseLoanOffersProvider.GetById(input.LoanOffer.Id);

            if (itemInDb != null)
            {
                return new VoidOperationOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var entry = mapperProvider.Map<LoanOfferDto, LoanOfferTableEntry>(input.LoanOffer);


            var result = databaseLoanOffersProvider.Add(entry);

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

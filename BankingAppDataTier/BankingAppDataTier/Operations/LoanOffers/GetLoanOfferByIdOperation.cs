using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

namespace BankingAppDataTier.Operations.LoanOffers
{
    public class GetLoanOfferByIdOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetLoanOfferByIdInput, GetLoanOfferByIdOutput>(context, endpoint)
    {
        private IDatabaseLoanOfferProvider databaseLoanOffersProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseLoanOffersProvider = executionContext.GetDependency<IDatabaseLoanOfferProvider>()!;
        }

        protected override async Task<GetLoanOfferByIdOutput> ExecuteAsync(GetLoanOfferByIdInput input)
        {
            var itemInDb = databaseLoanOffersProvider.GetById(input.Id);

            if (itemInDb == null)
            {
                return new GetLoanOfferByIdOutput()
                {
                    LoanOffer = null,
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.NotFound,
                };
            }

            return new GetLoanOfferByIdOutput()
            {
                LoanOffer = mapperProvider.Map<LoanOfferTableEntry, LoanOfferDto>(itemInDb)
            };
        }
    }
}

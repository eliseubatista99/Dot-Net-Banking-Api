using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations
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

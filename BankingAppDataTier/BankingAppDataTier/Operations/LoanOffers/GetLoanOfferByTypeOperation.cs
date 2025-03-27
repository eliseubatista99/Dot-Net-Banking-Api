using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;

namespace BankingAppDataTier.Operations.LoanOffers
{
    public class GetLoanOfferByTypeOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetLoanOfferByTypeInput, GetLoanOffersByTypeOutput>(context, endpoint)
    {
        private IDatabaseLoanOfferProvider databaseLoanOffersProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseLoanOffersProvider = executionContext.GetDependency<IDatabaseLoanOfferProvider>()!;
        }

        protected override async Task<GetLoanOffersByTypeOutput> ExecuteAsync(GetLoanOfferByTypeInput input)
        {
            var result = new List<LoanOfferDto>();

            var typeAsString = mapperProvider.Map<LoanType, string>(input.OfferType);

            var loanOffersInDb = databaseLoanOffersProvider.GetByType(typeAsString, input.IncludeInactive != true);

            if (loanOffersInDb == null || loanOffersInDb.Count == 0)
            {
                return new GetLoanOffersByTypeOutput()
                {
                    LoanOffers = new List<LoanOfferDto>(),
                };
            }

            result = loanOffersInDb.Select(i => mapperProvider.Map<LoanOfferTableEntry, LoanOfferDto>(i)).ToList();

            return new GetLoanOffersByTypeOutput()
            {
                LoanOffers = result,
            };
        }
    }
}

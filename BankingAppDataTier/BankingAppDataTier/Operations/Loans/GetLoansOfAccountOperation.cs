using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Outputs.Loans;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Application;

namespace BankingAppDataTier.Operations.Loans
{
    public class GetLoansOfAccountOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetLoansOfAccountInput, GetLoansOfAccountOutput>(context, endpoint)
    {
        private IDatabaseLoansProvider databaseLoansProvider;
        private IDatabaseLoanOfferProvider databaseLoanOffersProvider;


        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseLoansProvider = executionContext.GetDependency<IDatabaseLoansProvider>()!;
            databaseLoanOffersProvider = executionContext.GetDependency<IDatabaseLoanOfferProvider>()!;
        }

        protected override async Task<GetLoansOfAccountOutput> ExecuteAsync(GetLoansOfAccountInput input)
        {
            var result = new List<LoanDto>();

            var itemsInDb = databaseLoansProvider.GetByAccount(input.AccountId);

            if (itemsInDb == null || itemsInDb.Count == 0)
            {
                return new GetLoansOfAccountOutput()
                {
                    Loans = new List<LoanDto>(),
                };
            }

            result = itemsInDb.Select(i => this.BuildLoanDto(i)).ToList();

            if (input.LoanType != null)
            {
                result = result.Where(l => l.LoanType == input.LoanType).ToList();
            }

            return new GetLoansOfAccountOutput()
            {
                Loans = result,
            };
        }

        private LoanDto BuildLoanDto(LoanTableEntry entry)
        {
            var loan = mapperProvider.Map<LoanTableEntry, LoanDto>(entry);
            var offerData = databaseLoanOffersProvider.GetById(loan.RelatedOffer);

            if (offerData == null)
            {
                return loan;
            }

            loan.LoanType = mapperProvider.Map<string, LoanType>(offerData.LoanType);
            loan.Interest = offerData.Interest;

            return loan;
        }
    }
}

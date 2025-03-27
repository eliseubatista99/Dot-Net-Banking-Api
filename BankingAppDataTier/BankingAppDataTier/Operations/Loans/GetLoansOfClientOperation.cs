using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;

namespace BankingAppDataTier.Operations
{
    public class GetLoansOfClientOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetLoansOfClientInput, GetLoansOfClientOutput>(context, endpoint)
    {
        private IDatabaseLoansProvider databaseLoansProvider;
        private IDatabaseLoanOfferProvider databaseLoanOffersProvider;
        private IDatabaseAccountsProvider databaseAccountsProvider;


        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseLoansProvider = executionContext.GetDependency<IDatabaseLoansProvider>()!;
            databaseLoanOffersProvider = executionContext.GetDependency<IDatabaseLoanOfferProvider>()!;
            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
        }

        protected override async Task<GetLoansOfClientOutput> ExecuteAsync(GetLoansOfClientInput input)
        {
            var clientAccounts = databaseAccountsProvider.GetAccountsOfClient(input.ClientId);

            if (clientAccounts == null || clientAccounts.Count == 0)
            {
                return new GetLoansOfClientOutput()
                {
                    Loans = new List<LoanDto>(),
                };
            }

            var result = new List<LoanDto>();


            foreach (var account in clientAccounts)
            {
                var itemsInDb = databaseLoansProvider.GetByAccount(account.AccountId);

                var loansOfAccount = itemsInDb.Select(i => this.BuildLoanDto(i)).ToList();

                result.AddRange(loansOfAccount);
            }

            if (input.LoanType != null)
            {
                result = result.Where(l => l.LoanType == input.LoanType).ToList();
            }

            return new GetLoansOfClientOutput()
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

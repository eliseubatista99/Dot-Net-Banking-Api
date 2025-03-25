using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Outputs.Loans;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

namespace BankingAppDataTier.Operations.Loans
{
    public class GetLoanByIdOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetLoanByIdInput, GetLoanByIdOutput>(context, endpoint)
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

        protected override async Task<GetLoanByIdOutput> ExecuteAsync(GetLoanByIdInput input)
        {
            var itemInDb = databaseLoansProvider.GetById(input.Id);

            if (itemInDb == null)
            {
                return new GetLoanByIdOutput()
                {
                    Loan = null,
                    Error = GenericErrors.InvalidId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            return new GetLoanByIdOutput()
            {
                Loan = this.BuildLoanDto(itemInDb),
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

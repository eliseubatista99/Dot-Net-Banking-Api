using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Operations.Inputs.Loans;
using BankingAppDataTier.Contracts.Dtos;

namespace BankingAppDataTier.Operations.Loans
{
    public class EditLoanOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<EditLoanInput, VoidOperationOutput>(context, endpoint)
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

        protected override async Task<VoidOperationOutput> ExecuteAsync(EditLoanInput input)
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

            var loan = this.BuildLoanDto(entryInDb);

            if (input.RelatedOffer != null)
            {
                var relatedOffer = databaseLoanOffersProvider.GetById(input.RelatedOffer);

                if (relatedOffer == null)
                {
                    return new VoidOperationOutput()
                    {
                        Error = LoansErrors.InvalidRelatedOffer,
                        StatusCode = HttpStatusCode.BadRequest,
                    };
                }

                var relatedOfferLoanType = mapperProvider.Map<string, LoanType>(relatedOffer.LoanType);

                if (relatedOfferLoanType != loan.LoanType)
                {
                    return new VoidOperationOutput()
                    {
                        Error = LoansErrors.CantChangeLoanType,
                        StatusCode = HttpStatusCode.Forbidden,
                    };
                }
            }

            if (input.RelatedAccount != null)
            {
                var relatedAccount = databaseAccountsProvider.GetById(input.RelatedAccount);

                if (relatedAccount == null)
                {
                    return new VoidOperationOutput()
                    {
                        Error = LoansErrors.InvalidRelatedAccount,
                        StatusCode = HttpStatusCode.BadRequest,
                    };
                }
            }


            entryInDb.Name = input.Name != null ? input.Name : entryInDb.Name;
            entryInDb.RelatedAccount = input.RelatedAccount != null ? input.RelatedAccount : entryInDb.RelatedAccount;
            entryInDb.RelatedOffer = input.RelatedOffer != null ? input.RelatedOffer : entryInDb.RelatedOffer;
            entryInDb.Duration = input.Duration != null ? input.Duration.GetValueOrDefault() : entryInDb.Duration;
            entryInDb.PaidAmount = input.PaidAmount != null ? input.PaidAmount.GetValueOrDefault() : entryInDb.PaidAmount;

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

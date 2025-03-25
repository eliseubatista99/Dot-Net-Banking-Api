﻿using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

namespace BankingAppDataTier.Operations.Loans
{
    public class AddLoanOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<AddLoanInput, VoidOperationOutput>(context, endpoint)
    {
        private IDatabaseLoansProvider databaseLoansProvider;
        private IDatabaseLoanOfferProvider databaseLoanOffersProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseLoansProvider = executionContext.GetDependency<IDatabaseLoansProvider>()!;
            databaseLoanOffersProvider = executionContext.GetDependency<IDatabaseLoanOfferProvider>()!;
        }

        protected override async Task<VoidOperationOutput> ExecuteAsync(AddLoanInput input)
        {
            var itemInDb = databaseLoansProvider.GetById(input.Loan.Id);

            if (itemInDb != null)
            {
                return new VoidOperationOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var relatedOffer = databaseLoanOffersProvider.GetById(input.Loan.RelatedOffer);

            if (relatedOffer == null)
            {
                return new VoidOperationOutput()
                {
                    Error = LoansErrors.InvalidRelatedOffer,
                    StatusCode = HttpStatusCode.Forbidden,
                };
            }

            var entry = mapperProvider.Map<LoanDto, LoanTableEntry>(input.Loan);

            var result = databaseLoansProvider.Add(entry);

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

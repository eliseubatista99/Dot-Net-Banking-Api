﻿using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Operations;

namespace BankingAppDataTier.Operations
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

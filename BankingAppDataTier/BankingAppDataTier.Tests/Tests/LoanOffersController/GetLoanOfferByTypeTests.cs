﻿using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.LoanOffers;

public class GetLoanOfferByTypeTests : OperationTest<GetLoanOfferByTypeOperation, GetLoanOfferByTypeInput, GetLoanOffersByTypeOutput>
{
    private IDatabaseLoanOfferProvider databaseLoanOfferProvider { get; set; }

    public GetLoanOfferByTypeTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetLoanOfferByTypeOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoanOfferProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoanOfferProvider>()!;
    }

    [Theory]
    [InlineData(LoanType.Personal)]
    [InlineData(LoanType.Auto)]
    [InlineData(LoanType.Mortgage)]
    public async Task ShouldBe_Success(LoanType loanType)
    {
        var response = await SimulateOperationToTestCall(new GetLoanOfferByTypeInput
        {
            OfferType = loanType,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.LoanOffers.Count > 0);
    }
}

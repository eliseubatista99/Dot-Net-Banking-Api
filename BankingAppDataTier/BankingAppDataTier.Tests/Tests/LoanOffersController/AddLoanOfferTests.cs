﻿using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.LoanOffers;

public class AddLoanOfferTests : OperationTest<AddLoanOfferOperation, AddLoanOfferInput, VoidOperationOutput>
{
    private IDatabaseLoanOfferProvider databaseLoanOfferProvider { get; set; }

    public AddLoanOfferTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new AddLoanOfferOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseLoanOfferProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseLoanOfferProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var addResponse = await SimulateOperationToTestCall(new AddLoanOfferInput
        {
            LoanOffer = new LoanOfferDto
            {
                Id = "Test01",
                Name = "Loan Name",
                Description = "Desc",
                LoanType = Contracts.Enums.LoanType.Auto,
                MaxEffort = 30,
                Interest = 7.0M,
                IsActive = true,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);

        var getByIdResponse = databaseLoanOfferProvider.GetById("Test01");

        Assert.True(getByIdResponse != null);
    }


    [Fact]
    public async Task ShouldReturnError_IdAlreadyInUse()
    {
        var response = await SimulateOperationToTestCall(new AddLoanOfferInput
        {
            LoanOffer = new LoanOfferDto
            {
                Id = "Permanent_AU_01",
                Description = "Desc",
                Name = "Loan Name",
                LoanType = Contracts.Enums.LoanType.Auto,
                MaxEffort = 30,
                Interest = 7.0M,
                IsActive = true,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
    }
}

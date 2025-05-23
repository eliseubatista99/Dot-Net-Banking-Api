﻿using BankingAppDataTier.Library.Constants;
using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Library.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Plastics;

public class ActivateOrDeactivatePlasticTests : OperationTest<ActivateOrDeactivatePlasticOperation, ActivateOrDeactivatePlasticInput, VoidOperationOutput>
{
    private IDatabasePlasticsProvider databasePlasticsProvider { get; set; }

    public ActivateOrDeactivatePlasticTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new ActivateOrDeactivatePlasticOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databasePlasticsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabasePlasticsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var activateResponse = await SimulateOperationToTestCall(new ActivateOrDeactivatePlasticInput
        {
            Id = "Permanent_Debit_01",
            Active = false,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(activateResponse.Error == null);

        var getByTypeResult = databasePlasticsProvider.GetPlasticsOfCardType(BankingAppDataTierConstants.CARD_TYPE_DEBIT, true);

        Assert.True(getByTypeResult.Find(l => l.Id == "Permanent_Debit_01") == null);

        activateResponse = await SimulateOperationToTestCall(new ActivateOrDeactivatePlasticInput
        {
            Id = "Permanent_Debit_01",
            Active = true,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(activateResponse.Error == null);

        getByTypeResult = databasePlasticsProvider.GetPlasticsOfCardType(BankingAppDataTierConstants.CARD_TYPE_DEBIT, true);

        Assert.True(getByTypeResult.Find(l => l.Id == "Permanent_Debit_01") != null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new ActivateOrDeactivatePlasticInput
        {
            Id = "invalidId",
            Active = false,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

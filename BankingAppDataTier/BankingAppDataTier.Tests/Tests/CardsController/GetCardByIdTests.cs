using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Operations.Cards;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests;
using ElideusDotNetFramework.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Cards;

public class GetCardByIdTests : OperationTest<GetCardByIdOperation, GetCardByIdInput, GetCardByIdOutput>
{
    public GetCardByIdTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetCardByIdOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Theory]
    [InlineData("Permanent_Debit_01")]
    [InlineData("Permanent_PrePaid_01")]
    [InlineData("Permanent_Credit_01")]
    public async Task ShouldBe_Success(string id)
    {
        var response = await SimulateOperationToTestCall(new GetCardByIdInput
        {
            Id = id,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Card != null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new GetCardByIdInput
        {
            Id = "invalid",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Card == null);
        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

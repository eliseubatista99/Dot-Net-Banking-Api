using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Clients;

public class GetClientByIdTests : OperationTest<GetClientByIdOperation, GetClientByIdInput, GetClientByIdOutput>
{
    public GetClientByIdTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetClientByIdOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Theory]
    [InlineData("Permanent_Client_01")]
    [InlineData("Permanent_Client_02")]
    public async Task ShouldBe_Success(string id)
    {
        var response = await SimulateOperationToTestCall(new GetClientByIdInput
        {
            Id = id,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Client != null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new GetClientByIdInput
        {
            Id = "invalid",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Client == null);
        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}


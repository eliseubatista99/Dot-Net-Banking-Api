using BankingAppAuthenticationTier.Tests.Constants;
using ElideusDotNetFramework.Tests;
using BankingAppAuthenticationTier.Operations;
using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppAuthenticationTier.Library.Constants;
using BankingAppAuthenticationTier.Library.Errors;

namespace BankingAppAuthenticationTier.Tests.Authentication;

public class GetAuthenticationPositionsTests : OperationTest<GetAuthenticationPositionsOperation, GetAuthenticationPositionsInput, GetAuthenticationPositionsOutput>
{
    public GetAuthenticationPositionsTests(BankingAppAuthenticationTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetAuthenticationPositionsOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var response = await SimulateOperationToTestCall(new GetAuthenticationPositionsInput
        {
            ClientId = "Permanent_Client_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error == null);
        Assert.True(response.Positions.Count == BankingAppAuthenticationTierConstants.DEDFAULT_NUMBER_OF_POSITIONS);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    [InlineData(10)]
    public async Task ShouldBe_Success_ForSpecificNumberOfPositions(int numberOfPositions)
    {
        // This client password has only 8 chars, so we should expect the final count of positions to be 8 at max
        var finalNumberOfPositions = numberOfPositions < 8 ? numberOfPositions : 8;

        var response = await SimulateOperationToTestCall(new GetAuthenticationPositionsInput
        {
            ClientId = "Permanent_Client_01",
            NumberOfPositions = numberOfPositions,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error == null);
        Assert.True(response.Positions.Count == finalNumberOfPositions);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidClient()
    {
        var response = await SimulateOperationToTestCall(new GetAuthenticationPositionsInput
        {
            ClientId = "InvalidClient",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == AuthenticationErrors.InvalidClient.Code);
    }
}


using BankingAppAuthenticationTier.Tests.Constants;
using ElideusDotNetFramework.Tests;
using BankingAppAuthenticationTier.Operations;
using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppAuthenticationTier.Library.Errors;

namespace BankingAppAuthenticationTier.Tests.Authentication;

public class KeepAliveTests : OperationTest<RefreshTokenOperation, RefreshTokenInput, RefreshTokenOutput>
{
    private IsValidTokenOperation isValidTokenOperation { get; set; }

    public KeepAliveTests(BankingAppAuthenticationTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new RefreshTokenOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        isValidTokenOperation = new IsValidTokenOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var isValidResponse = await TestsHelper.SimulateCall<IsValidTokenOperation, IsValidTokenInput, IsValidTokenOutput>(isValidTokenOperation!, new IsValidTokenInput
        {
            Token = TestsConstants.PermanentToken,
            Metadata = TestsConstants.TestsMetadata,
        });

        var originalExpirationTime = isValidResponse.ExpirationDateTime.GetValueOrDefault();

        var keepAliveResponse = await SimulateOperationToTestCall(new RefreshTokenInput
        {
            RefreshToken = TestsConstants.PermanentToken,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(keepAliveResponse.Error == null);
        Assert.True(keepAliveResponse.Token.ExpirationDateTime.Ticks > originalExpirationTime.Ticks);
    }

    [Fact]
    public async Task ShouldBeError_InvalidToken()
    {
        var response = await SimulateOperationToTestCall(new RefreshTokenInput
        {
            RefreshToken = "invalid-token",
        });

        Assert.True(response.Error?.Code == AuthenticationErrors.InvalidToken.Code);
    }

    [Fact]
    public async Task ShouldBeError_TokenExpired()
    {
        var response = await SimulateOperationToTestCall(new RefreshTokenInput
        {
            RefreshToken = TestsConstants.ExpiredToken,

        });

        Assert.True(response.Error?.Code == AuthenticationErrors.TokenExpired.Code);
    }
}


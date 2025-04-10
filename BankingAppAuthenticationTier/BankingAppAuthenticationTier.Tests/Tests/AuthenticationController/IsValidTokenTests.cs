using BankingAppAuthenticationTier.Tests.Constants;
using ElideusDotNetFramework.Tests;
using BankingAppAuthenticationTier.Operations;
using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppAuthenticationTier.Library.Errors;

namespace BankingAppAuthenticationTier.Tests.Authentication;

public class IsValidTokenTests : OperationTest<IsValidTokenOperation, IsValidTokenInput, IsValidTokenOutput>
{
    public IsValidTokenTests(BankingAppAuthenticationTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new IsValidTokenOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var response = await SimulateOperationToTestCall(new IsValidTokenInput
        {
            Token = TestsConstants.PermanentToken,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error == null);
        Assert.True(response.IsValid);
        Assert.True(response.ExpirationDateTime != null);
    }

    [Fact]
    public async Task ShouldBe_InvalidForExpiredToken()
    {
        var response = await SimulateOperationToTestCall(new IsValidTokenInput
        {
            Token = TestsConstants.ExpiredToken,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error == null);
        Assert.True(!response.IsValid);
        Assert.True(response.Reason == AuthenticationErrors.TokenExpired.Code);
    }

    [Fact]
    public async Task ShouldBe_InvalidForInvalidToken()
    {
        var response = await SimulateOperationToTestCall(new IsValidTokenInput
        {
            Token = "invalid-token",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error == null);
        Assert.True(!response.IsValid);
        Assert.True(response.Reason == AuthenticationErrors.InvalidToken.Code);
    }
}


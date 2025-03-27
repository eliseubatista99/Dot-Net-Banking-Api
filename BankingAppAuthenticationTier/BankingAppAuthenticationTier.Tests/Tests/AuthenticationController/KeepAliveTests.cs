﻿using BankingAppAuthenticationTier.Contracts.Errors;
using BankingAppAuthenticationTier.Tests.Constants;
using ElideusDotNetFramework.Tests;
using BankingAppAuthenticationTier.Operations;
using ElideusDotNetFramework.Core.Operations;
using BankingAppAuthenticationTier.Contracts.Operations;

namespace BankingAppAuthenticationTier.Tests.Authentication;

public class KeepAliveTests : OperationTest<KeepAliveOperation, VoidOperationInput, KeepAliveOutput>
{
    private IsValidTokenOperation isValidTokenOperation { get; set; }

    public KeepAliveTests(BankingAppAuthenticationTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new KeepAliveOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        isValidTokenOperation = new IsValidTokenOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var metadata = new InputMetadata
        {
            Token = TestsConstants.PermanentToken,
        };

        var isValidResponse = await TestsHelper.SimulateCall<IsValidTokenOperation, IsValidTokenInput, IsValidTokenOutput>(isValidTokenOperation!, new IsValidTokenInput
        {
            Token = TestsConstants.PermanentToken,
            Metadata = TestsConstants.TestsMetadata,
        });

        var originalExpirationTime = isValidResponse.ExpirationDateTime.GetValueOrDefault();

        var keepAliveResponse = await SimulateOperationToTestCall(new VoidOperationInput
        {
            Metadata = metadata,
        });

        Assert.True(keepAliveResponse.Error == null);
        Assert.True(keepAliveResponse.ExpirationDateTime.GetValueOrDefault().Ticks > originalExpirationTime.Ticks);
    }

    [Fact]
    public async Task ShouldBeError_InvalidToken()
    {
        var response = await SimulateOperationToTestCall(new VoidOperationInput
        {
            Metadata = new InputMetadata
            {
                Token = "invalid-token",
            },
        });

        Assert.True(response.Error?.Code == AuthenticationErrors.InvalidToken.Code);
    }

    [Fact]
    public async Task ShouldBeError_TokenExpired()
    {
        var response = await SimulateOperationToTestCall(new VoidOperationInput
        {
            Metadata = new InputMetadata
            {
                Token = TestsConstants.ExpiredToken,
            },
        });

        Assert.True(response.Error?.Code == AuthenticationErrors.TokenExpired.Code);
    }
}


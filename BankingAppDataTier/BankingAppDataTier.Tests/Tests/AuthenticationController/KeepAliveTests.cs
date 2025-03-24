using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Operations.Accounts;
using BankingAppDataTier.Tests.Constants;
using BankingAppDataTier.Tests;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests.Helpers;
using ElideusDotNetFramework.Tests;
using System.Diagnostics.Contracts;
using BankingAppDataTier.Operations.Authentication;
using BankingAppDataTier.Contracts.Dtos.Inputs.Authentication;
using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Constants;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Authentication;

public class KeepAliveTests : OperationTest<KeepAliveOperation, KeepAliveInput, KeepAliveOutput>
{
    private IsValidTokenOperation isValidTokenOperation { get; set; }

    public KeepAliveTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new KeepAliveOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        isValidTokenOperation = new IsValidTokenOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var metadata = TestsConstants.TestsMetadata;
        metadata.Token = TestsConstants.PermanentToken;

        var isValidResponse = await TestsHelper.SimulateCall<IsValidTokenOperation, IsValidTokenInput, IsValidTokenOutput>(isValidTokenOperation!, new IsValidTokenInput
        {
            Token = TestsConstants.PermanentToken,
            Metadata = TestsConstants.TestsMetadata,
        });

        var originalExpirationTime = isValidResponse.ExpirationDateTime.GetValueOrDefault();

        var keepAliveResponse = await TestsHelper.SimulateCall<KeepAliveOperation, KeepAliveInput, KeepAliveOutput>(OperationToTest!, new KeepAliveInput
        {
            Metadata = metadata,
        });

        Assert.True(keepAliveResponse.Error == null);
        Assert.True(keepAliveResponse.ExpirationDateTime.GetValueOrDefault().Ticks > originalExpirationTime.Ticks);
    }

    [Fact]
    public async Task ShouldBeError_InvalidToken()
    {
        var metadata = TestsConstants.TestsMetadata;
        metadata.Token = "invalid-token";

        var response = await TestsHelper.SimulateCall<KeepAliveOperation, KeepAliveInput, KeepAliveOutput>(OperationToTest!, new KeepAliveInput
        {
            Metadata = metadata,
        });

        Assert.True(response.Error?.Code == AuthenticationErrors.InvalidToken.Code);
    }

    [Fact]
    public async Task ShouldBeError_TokenExpired()
    {
        var metadata = TestsConstants.TestsMetadata;
        metadata.Token = TestsConstants.ExpiredToken;

        var response = await TestsHelper.SimulateCall<KeepAliveOperation, KeepAliveInput, KeepAliveOutput>(OperationToTest!, new KeepAliveInput
        {
            Metadata = metadata,
        });

        Assert.True(response.Error?.Code == AuthenticationErrors.TokenExpired.Code);
    }
}


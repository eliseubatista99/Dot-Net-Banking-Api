using BankingAppAuthenticationTier.Tests.Constants;
using ElideusDotNetFramework.Tests;
using BankingAppAuthenticationTier.Operations;
using BankingAppAuthenticationTier.Contracts.Dtos;
using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppAuthenticationTier.Library.Errors;

namespace BankingAppAuthenticationTier.Tests.Authentication;

public class AuthenticateTests : OperationTest<AuthenticateOperation, AuthenticateInput, AuthenticateOutput>
{
    public AuthenticateTests(BankingAppAuthenticationTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new AuthenticateOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var response = await SimulateOperationToTestCall(new AuthenticateInput
        {
            ClientId = "Permanent_Client_01",
            AuthenticationCode = new List<AuthenticationCodeItemDto>
                {
                    new AuthenticationCodeItemDto
                    {
                        Position = 0,
                        Value = 'p',
                    },
                    new AuthenticationCodeItemDto
                    {
                        Position = 1,
                        Value = 'a',
                    },
                    new AuthenticationCodeItemDto
                    {
                        Position = 2,
                        Value = 's',
                    }
                },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error == null);
        Assert.True(response.Token != string.Empty);
        Assert.True(response.ExpirationDateTime.GetValueOrDefault().Ticks > DateTime.Now.Ticks);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidClient()
    {
        var response = await SimulateOperationToTestCall(new AuthenticateInput
        {
            ClientId = "Invalid_Client",
            AuthenticationCode = new List<AuthenticationCodeItemDto>(),
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == AuthenticationErrors.InvalidClient.Code);
    }

    [Fact]
    public async Task ShouldReturnError_WrongCode()
    {
        var response = await SimulateOperationToTestCall(new AuthenticateInput
        {
            ClientId = "Permanent_Client_01",
            AuthenticationCode = new List<AuthenticationCodeItemDto>
                {
                    new AuthenticationCodeItemDto
                    {
                        Position = 0,
                        Value = 'x',
                    },
                },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == AuthenticationErrors.WrongCode.Code);
    }
}


using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Authentication;
using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Authentication;

public class AuthenticateTests
{
    private AuthenticationController _authController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _authController = TestMocksBuilder._AuthenticationControllerMock;
    }

    [Fact]
    public void ShouldBe_Success()
    {
        Setup();

        var result = (ObjectResult)_authController.Authenticate(new AuthenticateInput
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
            }
        }).Result!;

        var response = (AuthenticateOutput)result.Value!;

        Assert.True(response.Error == null);
        Assert.True(response.Token != string.Empty);
        Assert.True(response.ExpirationDateTime.GetValueOrDefault().Ticks > DateTime.Now.Ticks);
    }

    [Fact]
    public void ShouldReturnError_InvalidClient()
    {
        Setup();

        var result = (ObjectResult)_authController.Authenticate(new AuthenticateInput
        {
            ClientId = "Invalid_Client",
            AuthenticationCode = new List<AuthenticationCodeItemDto>
            {           
            }
        }).Result!;

        var response = (AuthenticateOutput)result.Value!;

        Assert.True(response.Error?.Code == AuthenticationErrors.InvalidClient.Code);
    }

    [Fact]
    public void ShouldReturnError_WrongCode()
    {
        Setup();

        var result = (ObjectResult)_authController.Authenticate(new AuthenticateInput
        {
            ClientId = "Permanent_Client_01",
            AuthenticationCode = new List<AuthenticationCodeItemDto>
            {
                new AuthenticationCodeItemDto
                {
                    Position = 0,
                    Value = 'x',
                },
            }
        }).Result!;

        var response = (AuthenticateOutput)result.Value!;

        Assert.True(response.Error?.Code == AuthenticationErrors.WrongCode.Code);
    }
}

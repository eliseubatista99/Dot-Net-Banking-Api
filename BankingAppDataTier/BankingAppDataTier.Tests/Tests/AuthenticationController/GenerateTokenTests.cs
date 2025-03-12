using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Authentication;

public class GenerateTokenTests
{
    private AuthenticationController _authenticationController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _authenticationController = TestMocksBuilder._AuthenticationControllerMock;
    }

    [Fact]
    public void ShouldBe_Success()
    {
        Setup();

        var result = (ObjectResult)_authenticationController.GenerateToken(new AuthenticationInputDto
        {
            AppId = "",
        }).Result!;

        var response = (GenerateTokenOutput)result.Value!;

        Assert.True(response.Token != null);
    }
}

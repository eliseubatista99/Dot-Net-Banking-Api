using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using BankingAppDataTier.Controllers;

namespace BankingAppDataTier.Tests;

public class ChangePasswordTests
{
    private ClientsController _clientsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _clientsController = TestMocksBuilder._ClientsControllerMock;
    }

    [Fact]
    public void ShouldBe_Success()
    {
        const string newPassword = "NewPassword";
        Setup();

        var result = (ObjectResult)_clientsController.ChangePassword(new ChangeClientPasswordInput
        {
            Id = "JW0000000",
            PassWord = newPassword,
        }).Result!;

        var response = (VoidOutput)result.Value!;

        result = (ObjectResult)_clientsController.HasValidPassword(new HasValidPasswordInput
        {
            Id = "JW0000000",
            PassWord = newPassword,
        }).Result!;
        var response2 = (HasValidPasswordOutput)result.Value!;


        Assert.True(response2.Result == true);
    }


    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_clientsController.ChangePassword(new ChangeClientPasswordInput
        {
            Id = "invalid Id",
            PassWord = "password",
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

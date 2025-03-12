using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using BankingAppDataTier.Controllers;

namespace BankingAppDataTier.Tests.Clients;

public class HasValidPasswordTests
{
    private ClientsController _clientsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _clientsController = TestMocksBuilder._ClientsControllerMock;
    }

    [Fact]
    public void ShouldBe_Success_ValidPassword()
    {
        Setup();

        var result = (ObjectResult)_clientsController.HasValidPassword(new HasValidPasswordInput
        {
            Id = "Permanent_Client_01",
            PassWord = "password",
        }).Result!;

        var response = (HasValidPasswordOutput)result.Value!;

        Assert.True(response.Result == true);
    }

    [Fact]
    public void ShouldBe_Success_WrongPassword()
    {
        Setup();

        var result = (ObjectResult)_clientsController.HasValidPassword(new HasValidPasswordInput
        {
            Id = "Permanent_Client_01",
            PassWord = "wrong password",
        }).Result!;

        var response = (HasValidPasswordOutput)result.Value!;

        Assert.True(response.Result == false);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_clientsController.HasValidPassword(new HasValidPasswordInput
        {
            Id = "invalid id",
            PassWord = "wrong password",
        }).Result!;

        var response = (HasValidPasswordOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

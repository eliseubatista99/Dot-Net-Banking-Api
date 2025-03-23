using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using BankingAppDataTier.Controllers;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Tests.Clients;

public class DeleteClientTests
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
        const string newName = "New Name";
        Setup();

        var result = (ObjectResult)_clientsController.GetClientById("To_Delete_Client_01").Result!;
        var response = (GetClientByIdOutput)result.Value!;

        Assert.True(response.Client != null);

        result = (ObjectResult)_clientsController.DeleteClient("To_Delete_Client_01").Result!;
        var response2 = (VoidOperationOutput)result.Value!;

        Assert.True(response2.Error == null);

        result = (ObjectResult)_clientsController.GetClientById("To_Delete_Client_01").Result!;
        response = (GetClientByIdOutput)result.Value!;

        Assert.True(response.Client == null);
    }


    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_clientsController.DeleteClient("Invalidid").Result!;

        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public void ShouldReturnError_CantCloseWithActiveAccounts()
    {
        Setup();

        var result = (ObjectResult)_clientsController.DeleteClient("Permanent_Client_01").Result!;
        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error?.Code == ClientsErrors.CantCloseWithActiveAccounts.Code);
    }
}

using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Controllers = BankingAppDataTier.Controllers;

namespace BankingAppDataTier.Tests.ClientsController;

public class DeleteClientTests
{
    private Controllers.ClientsController _clientsController;

    private void Setup()
    {
        _clientsController = ClientsControllerMock.Mock();
    }

    [Fact]
    public void ShouldBe_Success()
    {
        const string newName = "New Name";
        Setup();

        var result = (ObjectResult)_clientsController.GetClientById("JS0000000").Result!;
        var response = (GetClientByIdOutput)result.Value!;

        Assert.True(response.Client != null);

        result = (ObjectResult)_clientsController.DeleteClient("JS0000000").Result!;
        var response2 = (VoidOutput)result.Value!;

        Assert.True(response2.Error == null);

        result = (ObjectResult)_clientsController.GetClientById("JS0000000").Result!;
        response = (GetClientByIdOutput)result.Value!;

        Assert.True(response.Client == null);
    }


    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_clientsController.DeleteClient("Invalidid").Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public void ShouldReturnError_CantCloseWithActiveAccounts()
    {
        Setup();

        var result = (ObjectResult)_clientsController.DeleteClient("JW0000000").Result!;
        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == ClientsErrors.CantCloseWithActiveAccounts.Code);
    }
}

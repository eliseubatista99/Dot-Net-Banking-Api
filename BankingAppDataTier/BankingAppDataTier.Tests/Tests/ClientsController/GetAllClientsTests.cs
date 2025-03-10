using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using Controllers = BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;

namespace BankingAppDataTier.Tests.ClientsController;

public class GetAllClientsTests
{
    private Controllers.ClientsController _clientsController;

    private void Setup()
    {
        _clientsController = ClientsControllerMock.Mock();
    }

    [Fact]
    public void GetAllClients_Success()
    {
        Setup();

        var result = (ObjectResult)_clientsController.GetClients().Result!;
        var response = (GetClientsOutput)result.Value!;

        Assert.True(response.Clients.Count > 0);
    }
}

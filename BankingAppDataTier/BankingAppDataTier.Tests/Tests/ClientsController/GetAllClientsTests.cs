using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests;

public class GetAllClientsTests
{
    private ClientsController _clientsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _clientsController = TestMocksBuilder._ClientsControllerMock;
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

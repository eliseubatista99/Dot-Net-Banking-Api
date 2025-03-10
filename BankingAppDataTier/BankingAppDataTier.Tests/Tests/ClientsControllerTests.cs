using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests;

public class ClientsControllerTests
{
    private ClientsController _clientsController { get; set; }

    private void Setup()
    {
        TestingMocksBuilder testingMocksProvider = new TestingMocksBuilder();

        var logger = testingMocksProvider.MockLogger<ClientsController>();
        var databaseClientsProvider = testingMocksProvider.MockDatabaseClientsProvider();
        var databaseAccountsProvider = testingMocksProvider.MockDatabaseAccountsProvider();

        _clientsController = new ClientsController(logger, databaseClientsProvider, databaseAccountsProvider);
    }

    [Fact]
    public void ShouldGetAllClients()
    {
        Setup();

        var result = (ObjectResult) _clientsController.GetClients().Result!;
        var response = (GetClientsOutput) result.Value!;

        Assert.True(response.Clients.Count > 0);
    }

    [Theory]
    [InlineData("JW0000000")]
    [InlineData("JS0000000")]
    public void ShouldGetClientsById(string id)
    {
        Setup();

        var result = (ObjectResult)_clientsController.GetClientById(id).Result!;
        var response = (GetClientByIdOutput) result.Value!;

        Assert.True(response.Client != null);
    }
}

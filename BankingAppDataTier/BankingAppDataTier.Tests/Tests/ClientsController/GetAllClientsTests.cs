using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using Controllers = BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;

namespace BankingAppDataTier.Tests.ClientsController;

public class GetAllClientsTests
{
    private Controllers.ClientsController _clientsController { get; set; }
    private IDatabaseClientsProvider databaseClientsProvider;
    private IDatabaseAccountsProvider databaseAccountsProvider;

    private void Setup()
    {
        var logger = LoggerMock<Controllers.ClientsController>.Mock();
        databaseClientsProvider = DatabaseClientsProviderMock.Mock();
        databaseAccountsProvider = DatabaseAccountsProviderMock.Mock();

        _clientsController = new Controllers.ClientsController(logger, databaseClientsProvider, databaseAccountsProvider);
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

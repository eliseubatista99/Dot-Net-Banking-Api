
using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Controllers = BankingAppDataTier.Controllers;

namespace BankingAppDataTier.Tests.ClientsController;

public class GetClientByIdTests
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

    [Theory]
    [InlineData("JW0000000")]
    [InlineData("JS0000000")]
    public void ShouldBe_Success(string id)
    {
        Setup();

        var result = (ObjectResult)_clientsController.GetClientById(id).Result!;
        var response = (GetClientByIdOutput)result.Value!;

        Assert.True(response.Client != null);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_clientsController.GetClientById("invalid").Result!;
        var response = (GetClientByIdOutput)result.Value!;

        Assert.True(response.Client == null);
        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Controllers = BankingAppDataTier.Controllers;

namespace BankingAppDataTier.Tests.ClientsController;

public class HasValidPasswordTests
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
    public void ShouldBe_Success_ValidPassword()
    {
        Setup();

        var result = (ObjectResult)_clientsController.HasValidPassword(new HasValidPasswordInput
        {
            Id = "JW0000000",
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
            Id = "JW0000000",
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

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

public class AddClientTests
{
    private Controllers.ClientsController _clientsController;

    private void Setup()
    {
        _clientsController = ClientsControllerMock.Mock();
    }

    [Fact]
    public void ShouldBe_Success()
    {
        Setup();

        var result = (ObjectResult)_clientsController.AddClient(new AddClientInput
        {
            Client = new ClientDto
            {
                Id = "T0001",
                Name = "Test",
                Surname = "Client",
                BirthDate = new DateTime(1990, 02, 13),
                VATNumber = "123123123",
                PhoneNumber = "911111111",
                Email = "test.client@dotnetbanking.com"
            },
            PassWord = "password"
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);
    }


    [Fact]
    public void ShouldReturnError_IdAlreadyInUse()
    {
        Setup();

        var result = (ObjectResult)_clientsController.AddClient(new AddClientInput
        {
            Client = new ClientDto
            {
                Id = "JW0000000",
                Name = "John",
                Surname = "Wick",
                BirthDate = new DateTime(1990, 02, 13),
                VATNumber = "123123123",
                PhoneNumber = "911111111",
                Email = "jonh.wick@dotnetbanking.com"
            },
            PassWord = "password"
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
    }
}


using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using BankingAppDataTier.Controllers;

namespace BankingAppDataTier.Tests;
public class GetClientByIdTests
{
    private ClientsController _clientsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _clientsController = TestMocksBuilder._ClientsControllerMock;
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

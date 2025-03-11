using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Accounts;

public class GetAccountsByIdTests
{
    private AccountsController _accountsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _accountsController = TestMocksBuilder._AccountsControllerMock;
    }

    [Theory]
    [InlineData("ACJW000000")]
    [InlineData("ACJW000001")]
    [InlineData("ACJW000002")]
    public void ShouldBe_Success(string id)
    {
        Setup();

        var result = (ObjectResult)_accountsController.GetAccountById(id).Result!;
        var response = (GetAccountByIdOutput)result.Value!;

        Assert.True(response.Account != null);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_accountsController.GetAccountById("invalid").Result!;
        var response = (GetAccountByIdOutput)result.Value!;

        Assert.True(response.Account == null);
        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

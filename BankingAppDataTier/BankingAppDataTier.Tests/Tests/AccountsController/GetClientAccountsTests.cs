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

public class GetClientAccountsTests
{
    private AccountsController _accountsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _accountsController = TestMocksBuilder._AccountsControllerMock;
    }

    [Fact]
    public void ShouldBe_Success()
    {
        Setup();

        var result = (ObjectResult)_accountsController.GetClientAccounts("JW0000000").Result!;
        var response = (GetClientAccountsOutput)result.Value!;

        Assert.True(response.Accounts.Count > 0);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_accountsController.GetClientAccounts("invalid client").Result!;
        var response = (GetClientAccountsOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

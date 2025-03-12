using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
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

        var result = (ObjectResult)_accountsController.GetClientAccounts("Permanent_Client_01").Result!;
        var response = (GetClientAccountsOutput)result.Value!;

        Assert.True(response.Accounts.Count > 0);
    }
}

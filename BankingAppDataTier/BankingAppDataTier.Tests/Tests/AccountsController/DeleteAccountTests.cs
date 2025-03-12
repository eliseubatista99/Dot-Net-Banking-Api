using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Accounts;

public class DeleteAccountTests
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

        var result = (ObjectResult)_accountsController.DeleteAccount("ACJW000004").Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);
    }

    [Fact]
    public void ShouldReturnError_InvalidAccountId()
    {
        Setup();

        var result = (ObjectResult)_accountsController.DeleteAccount("invalidId").Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public void ShouldReturnError_CantCloseWithRelatedCards()
    {
        Setup();

        var result = (ObjectResult)_accountsController.DeleteAccount("ACJW000000").Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == AccountsErrors.CantCloseWithRelatedCards.Code);
    }


    [Fact]
    public void ShouldReturnError_CantCloseWithActiveLoans()
    {
        Setup();

        var result = (ObjectResult)_accountsController.DeleteAccount("ACJW000003").Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == AccountsErrors.CantCloseWithActiveLoans.Code);
    }
}

using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Accounts;

public class EditAccountTests
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
        const string newName = "NewName";
        Setup();

        var result = (ObjectResult)_accountsController.EditAccount(new EditAccountInput 
        {
            AccountId = "ACJW000003",
            Name = newName,
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_accountsController.GetAccountById("ACJW000003").Result!;

        var response2 = (GetAccountByIdOutput)result.Value!;

        Assert.True(response2.Account?.Name == newName);
    }

    [Fact]
    public void ShouldBe_Success_SavingsAccount()
    {
        const string newSourceAccount = "ACJW000003";
        const decimal newInterest = 20;
        const int newDuration = 15;
        Setup();

        var result = (ObjectResult)_accountsController.EditAccount(new EditAccountInput
        {
            AccountId = "ACJW000005",
            SourceAccountId = newSourceAccount,
            Interest = newInterest,
            Duration = newDuration,
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_accountsController.GetAccountById("ACJW000005").Result!;

        var response2 = (GetAccountByIdOutput)result.Value!;

        Assert.True(response2.Account?.SourceAccountId == newSourceAccount);
        Assert.True(response2.Account?.Interest == newInterest);
        Assert.True(response2.Account?.Duration == newDuration);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_accountsController.EditAccount(new EditAccountInput
        {
            AccountId = "invalid id",
            Name = "invalid name"
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public void ShouldReturnError_InvalidSourceAccount()
    {
        Setup();

        var result = (ObjectResult)_accountsController.EditAccount(new EditAccountInput
        {
            AccountId = "ACJW000005",
            SourceAccountId = "invalid_source",
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == AccountsErrors.InvalidSourceAccount.Code);
    }
}

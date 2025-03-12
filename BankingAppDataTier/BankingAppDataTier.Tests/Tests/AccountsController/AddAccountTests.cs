using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Accounts;

public class AddAccountTests
{
    private AccountsController _accountsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _accountsController = TestMocksBuilder._AccountsControllerMock;
    }

    [Fact]
    public void ShouldBe_Success_CurrentAccount()
    {
        Setup();

        var result = (ObjectResult)_accountsController.AddAccount(new AddAccountInput
        {
            Account = new AccountDto
            {
                Id = "TEST0001",
                OwnerCliendId = "JW0000000",
                AccountType = Contracts.Enums.AccountType.Current,
                Balance = 1000,
                Name = "Test Current Account",
                Image = "image",
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_accountsController.GetAccountById("TEST0001").Result!;

        var response2 = (GetAccountByIdOutput)result.Value!;

        Assert.True(response2.Account != null);
    }

    [Fact]
    public void ShouldBe_Success_SavingsAccount()
    {
        Setup();

        var result = (ObjectResult)_accountsController.AddAccount(new AddAccountInput
        {
            Account = new AccountDto
            {
                Id = "TEST0002",
                OwnerCliendId = "JW0000000",
                AccountType = Contracts.Enums.AccountType.Savings,
                Balance = 1000,
                Name = "Test Savings Account",
                Image = "image",
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_accountsController.GetAccountById("TEST0002").Result!;

        var response2 = (GetAccountByIdOutput)result.Value!;

        Assert.True(response2.Account != null);
    }

    [Fact]
    public void ShouldBe_Success_InvestementsAccount()
    {
        Setup();

        var result = (ObjectResult)_accountsController.AddAccount(new AddAccountInput
        {
            Account = new AccountDto
            {
                Id = "TEST0003",
                OwnerCliendId = "JW0000000",
                AccountType = Contracts.Enums.AccountType.Investments,
                Balance = 1000,
                Name = "Test Investments Account",
                Image = "image",
                Interest = 0.3M,
                Duration = 10,
                SourceAccountId = "ACJW000000",
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_accountsController.GetAccountById("TEST0003").Result!;

        var response2 = (GetAccountByIdOutput)result.Value!;

        Assert.True(response2.Account != null);
    }


    [Theory]
    [InlineData(null, 10, 0.3)]
    [InlineData("JW0000000", null, 0.3)]
    [InlineData("JW0000000", 10, null)]
    public void ShouldReturnError_MissingInvestmentAccountDetails(string? sourceAccountId, int? duration, double? interest)
    {
        Setup();

        var result = (ObjectResult)_accountsController.AddAccount(new AddAccountInput
        {
            Account = new AccountDto
            {
                Id = $"TEST{sourceAccountId}_{duration}_{interest}",
                OwnerCliendId = "JW0000000",
                AccountType = Contracts.Enums.AccountType.Investments,
                Balance = 1000,
                Name = "Test Investments Account",
                Image = "image",
                Interest = interest != null ? (decimal) interest : null,
                Duration = duration,
                SourceAccountId = sourceAccountId,
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == AccountsErrors.MissingInvestementsAccountDetails.Code);
    }
}

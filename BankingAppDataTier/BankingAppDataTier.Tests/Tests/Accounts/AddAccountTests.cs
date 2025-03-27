using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations.Accounts;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Accounts;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Accounts;

public class AddAccountTests: OperationTest<AddAccountOperation, AddAccountInput, VoidOperationOutput>
{
    private IDatabaseAccountsProvider databaseAccountsProvider { get; set; }

    public AddAccountTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new AddAccountOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseAccountsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseAccountsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success_CurrentAccount()
    {
        var addResponse = await SimulateOperationToTestCall(new AddAccountInput
        {
            Account = new AccountDto
            {
                Id = "TEST0001",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = Contracts.Enums.AccountType.Current,
                Balance = 1000,
                Name = "Test Current Account",
                Image = "image",
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);

        var getByIdResponse = databaseAccountsProvider.GetById("TEST0001");

        Assert.True(getByIdResponse != null);
    }

    [Fact]
    public async Task ShouldBe_Success_SavingsAccount()
    {
        var addResponse = await SimulateOperationToTestCall(new AddAccountInput
        {
            Account = new AccountDto
            {
                Id = "TEST0002",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = Contracts.Enums.AccountType.Savings,
                Balance = 1000,
                Name = "Test Savings Account",
                Image = "image",
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);

        var getByIdResponse = databaseAccountsProvider.GetById("TEST0002");

        Assert.True(getByIdResponse != null);
    }

    [Fact]
    public async Task ShouldBe_Success_InvestementsAccount()
    {
        var addResponse = await SimulateOperationToTestCall(new AddAccountInput
        {
            Account = new AccountDto
            {
                Id = "TEST0003",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = Contracts.Enums.AccountType.Investments,
                Balance = 1000,
                Name = "Test Investments Account",
                Image = "image",
                Interest = 0.3M,
                Duration = 10,
                SourceAccountId = "ACJW000000",
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);

        var getByIdResponse = databaseAccountsProvider.GetById("TEST0003");

        Assert.True(getByIdResponse != null);
    }


    [Theory]
    [InlineData(null, 10, 0.3)]
    [InlineData("JW0000000", null, 0.3)]
    [InlineData("JW0000000", 10, null)]
    public async Task ShouldReturnError_MissingInvestmentAccountDetails(string? sourceAccountId, int? duration, double? interest)
    {
         var response = await SimulateOperationToTestCall(new AddAccountInput
        {
            Account = new AccountDto
            {
                Id = $"TEST{sourceAccountId}_{duration}_{interest}",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = Contracts.Enums.AccountType.Investments,
                Balance = 1000,
                Name = "Test Investments Account",
                Image = "image",
                Interest = interest != null ? (decimal)interest : null,
                Duration = duration,
                SourceAccountId = sourceAccountId,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == AccountsErrors.MissingInvestementsAccountDetails.Code);
    }

    [Fact]
    public async Task ShouldReturnError_IdAlreadyInUse()
    {
        var response = await SimulateOperationToTestCall(new AddAccountInput
        {
            Account = new AccountDto
            {
                Id = "Permanent_Current_01",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = Contracts.Enums.AccountType.Current,
                Balance = 1000,
                Name = "Test Current Account",
                Image = "image",
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
    }
}

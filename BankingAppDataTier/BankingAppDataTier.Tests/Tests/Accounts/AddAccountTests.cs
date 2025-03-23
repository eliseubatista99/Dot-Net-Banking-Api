using Azure;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers.Accounts;
using BankingAppDataTier.Tests.Constants;
using BankingAppDataTier.Tests.Mocks;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests;
using ElideusDotNetFramework.Tests.Helpers;

namespace BankingAppDataTier.Tests.Accounts;

public class AddAccountTests: OperationTest<AddAccountOperation, AddAccountInput, VoidOperationOutput>
{
    protected override ElideusDotNetFrameworkTestsBuilder TestsBuilder { get; set; } = new BankingAppDataTierTestsBuilder();
   
    private GetAccountByIdOperation getAccountByIdOperation;

    protected override void Setup()
    {
        base.Setup();

        OperationToTest = new AddAccountOperation(TestsBuilder.ApplicationContextMock!, string.Empty);
        getAccountByIdOperation = new GetAccountByIdOperation(TestsBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success_CurrentAccountAsync()
    {
        Setup();

        var addResponse = await TestsHelper.SimulateCall<AddAccountOperation, AddAccountInput, VoidOperationOutput>(OperationToTest!, new AddAccountInput
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

        var getByIdResponse = await TestsHelper.SimulateCall<GetAccountByIdOperation, GetAccountByIdInput, GetAccountByIdOutput>(getAccountByIdOperation!, new GetAccountByIdInput
        {
            Id = "TEST0001",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(getByIdResponse.Account != null);
    }

    [Fact]
    public async Task ShouldBe_Success_SavingsAccountAsync()
    {
        Setup();

        var addResponse = await TestsHelper.SimulateCall<AddAccountOperation, AddAccountInput, VoidOperationOutput>(OperationToTest!, new AddAccountInput
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

        var getByIdResponse = await TestsHelper.SimulateCall<GetAccountByIdOperation, GetAccountByIdInput, GetAccountByIdOutput>(getAccountByIdOperation!, new GetAccountByIdInput
        {
            Id = "TEST0002",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(getByIdResponse.Account != null);
    }

    [Fact]
    public async Task ShouldBe_Success_InvestementsAccountAsync()
    {
        Setup();

        var addResponse = await TestsHelper.SimulateCall<AddAccountOperation, AddAccountInput, VoidOperationOutput>(OperationToTest!, new AddAccountInput
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

        var getByIdResponse = await TestsHelper.SimulateCall<GetAccountByIdOperation, GetAccountByIdInput, GetAccountByIdOutput>(getAccountByIdOperation!, new GetAccountByIdInput
        {
            Id = "TEST0003",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(getByIdResponse.Account != null);
    }


    [Theory]
    [InlineData(null, 10, 0.3)]
    [InlineData("JW0000000", null, 0.3)]
    [InlineData("JW0000000", 10, null)]
    public async Task ShouldReturnError_MissingInvestmentAccountDetailsAsync(string? sourceAccountId, int? duration, double? interest)
    {
        Setup();

        var response = await TestsHelper.SimulateCall<AddAccountOperation, AddAccountInput, VoidOperationOutput>(OperationToTest!, new AddAccountInput
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
    public async Task ShouldReturnError_IdAlreadyInUseAsync()
    {
        Setup();

        var response = await TestsHelper.SimulateCall<AddAccountOperation, AddAccountInput, VoidOperationOutput>(OperationToTest!, new AddAccountInput
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

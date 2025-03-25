using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Operations.Accounts;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Tests.Accounts;

public class EditAccountTests : OperationTest<EditAccountOperation, EditAccountInput, VoidOperationOutput>
{
    private IDatabaseAccountsProvider databaseAccountsProvider { get; set; }

    public EditAccountTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new EditAccountOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseAccountsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseAccountsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        const string newName = "NewName";

        var editResponse = await SimulateOperationToTestCall(new EditAccountInput
        {
            AccountId = "To_Edit_Current_01",
            Name = newName,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(editResponse.Error == null);

        var getByIdResponse = databaseAccountsProvider.GetById("To_Edit_Current_01");

        Assert.True(getByIdResponse?.Name == newName);
    }
    [Fact]
    public async Task ShouldBe_Success_InvestementsAccount()
    {
        const string newSourceAccount = "Permanent_Current_01";
        const decimal newInterest = 20;
        const int newDuration = 15;

        var editResponse = await SimulateOperationToTestCall(new EditAccountInput
        {
            AccountId = "To_Edit_Investements_01",
            SourceAccountId = newSourceAccount,
            Interest = newInterest,
            Duration = newDuration,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(editResponse.Error == null);

        var getByIdResponse = databaseAccountsProvider.GetById("To_Edit_Investements_01");

        Assert.True(getByIdResponse?.SourceAccountId == newSourceAccount);
        Assert.True(getByIdResponse?.Interest == newInterest);
        Assert.True(getByIdResponse?.Duration == newDuration);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new EditAccountInput
        {
            AccountId = "invalid id",
            Name = "invalid name",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidSourceAccount()
    {
        var response = await SimulateOperationToTestCall(new EditAccountInput
        {
            AccountId = "To_Edit_Investements_01",
            SourceAccountId = "invalid_source",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == AccountsErrors.InvalidSourceAccount.Code);
    }
}

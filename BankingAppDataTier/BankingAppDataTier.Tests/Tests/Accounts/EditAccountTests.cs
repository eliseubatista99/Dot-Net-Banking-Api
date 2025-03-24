using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Operations.Accounts;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests.Helpers;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Accounts;

public class EditAccountTests : OperationTest<EditAccountOperation, EditAccountInput, VoidOperationOutput>
{
    private GetAccountByIdOperation getAccountByIdOperation;

    public EditAccountTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new EditAccountOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        getAccountByIdOperation = new GetAccountByIdOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        const string newName = "NewName";

        var editResponse = await TestsHelper.SimulateCall<EditAccountOperation, EditAccountInput, VoidOperationOutput>(OperationToTest!, new EditAccountInput
        {
            AccountId = "To_Edit_Current_01",
            Name = newName,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(editResponse.Error == null);

        var getByIdResponse = await TestsHelper.SimulateCall<GetAccountByIdOperation, GetAccountByIdInput, GetAccountByIdOutput>(getAccountByIdOperation!, new GetAccountByIdInput
        {
            Id = "To_Edit_Current_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(getByIdResponse.Account?.Name == newName);
    }
    [Fact]
    public async Task ShouldBe_Success_InvestementsAccount()
    {
        const string newSourceAccount = "Permanent_Current_01";
        const decimal newInterest = 20;
        const int newDuration = 15;

        var editResponse = await TestsHelper.SimulateCall<EditAccountOperation, EditAccountInput, VoidOperationOutput>(OperationToTest!, new EditAccountInput
        {
            AccountId = "To_Edit_Investements_01",
            SourceAccountId = newSourceAccount,
            Interest = newInterest,
            Duration = newDuration,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(editResponse.Error == null);

        var getByIdResponse = await TestsHelper.SimulateCall<GetAccountByIdOperation, GetAccountByIdInput, GetAccountByIdOutput>(getAccountByIdOperation!, new GetAccountByIdInput
        {
            Id = "To_Edit_Investements_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(getByIdResponse.Account?.SourceAccountId == newSourceAccount);
        Assert.True(getByIdResponse.Account?.Interest == newInterest);
        Assert.True(getByIdResponse.Account?.Duration == newDuration);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await TestsHelper.SimulateCall<EditAccountOperation, EditAccountInput, VoidOperationOutput>(OperationToTest!, new EditAccountInput
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
        var response = await TestsHelper.SimulateCall<EditAccountOperation, EditAccountInput, VoidOperationOutput>(OperationToTest!, new EditAccountInput
        {
            AccountId = "To_Edit_Investements_01",
            SourceAccountId = "invalid_source",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == AccountsErrors.InvalidSourceAccount.Code);
    }
}

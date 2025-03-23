//using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
//using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers.Accounts;
//using BankingAppDataTier.Tests.Constants;
//using BankingAppDataTier.Tests.Mocks;
//using ElideusDotNetFramework.Operations.Contracts;

//namespace BankingAppDataTier.Tests.Accounts;

//public class EditAccountTests
//{
//    private EditAccountOperation editAccountOperation;
//    private GetAccountByIdOperation getAccountByIdOperation;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();

//        editAccountOperation = new EditAccountOperation(TestMocksBuilder._ExecutionContextMock, string.Empty);
//        getAccountByIdOperation = new GetAccountByIdOperation(TestMocksBuilder._ExecutionContextMock, string.Empty);
//    }

//    [Fact]
//    public void ShouldBe_Success()
//    {
//        const string newName = "NewName";
//        Setup();

//        var result = (OperationHttpResult)editAccountOperation.Call(new EditAccountInput 
//        {
//            AccountId = "To_Edit_Current_01",
//            Name = newName,
//            Metadata = TestsConstants.TestsMetadata,
//        }).Result!;

//        var response = (VoidOperationOutput)result.Output!;

//        Assert.True(response.Error == null);

//        result = (OperationHttpResult)getAccountByIdOperation.Call(new GetAccountByIdInput
//        {
//            Id = "To_Edit_Current_01",
//            Metadata = TestsConstants.TestsMetadata,
//        }).Result!;

//        var response2 = (GetAccountByIdOutput)result.Output!;

//        Assert.True(response2.Account?.Name == newName);
//    }

//    [Fact]
//    public void ShouldBe_Success_InvestementsAccount()
//    {
//        const string newSourceAccount = "Permanent_Current_01";
//        const decimal newInterest = 20;
//        const int newDuration = 15;
//        Setup();

//        var result = (OperationHttpResult)editAccountOperation.Call(new EditAccountInput
//        {
//            AccountId = "To_Edit_Investements_01",
//            SourceAccountId = newSourceAccount,
//            Interest = newInterest,
//            Duration = newDuration,
//            Metadata = TestsConstants.TestsMetadata,
//        }).Result!;

//        var response = (VoidOperationOutput)result.Output!;

//        Assert.True(response.Error == null);

//        result = (OperationHttpResult)getAccountByIdOperation.Call(new GetAccountByIdInput
//        {
//            Id = "To_Edit_Investements_01",
//            Metadata = TestsConstants.TestsMetadata,
//        }).Result!;

//        var response2 = (GetAccountByIdOutput)result.Output!;

//        Assert.True(response2.Account?.SourceAccountId == newSourceAccount);
//        Assert.True(response2.Account?.Interest == newInterest);
//        Assert.True(response2.Account?.Duration == newDuration);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidId()
//    {
//        Setup();

//        var result = (OperationHttpResult)editAccountOperation.Call(new EditAccountInput
//        {
//            AccountId = "invalid id",
//            Name = "invalid name",
//            Metadata = TestsConstants.TestsMetadata,
//        }).Result!;

//        var response = (VoidOperationOutput)result.Output!;

//        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidSourceAccount()
//    {
//        Setup();

//        var result = (OperationHttpResult)editAccountOperation.Call(new EditAccountInput
//        {
//            AccountId = "To_Edit_Investements_01",
//            SourceAccountId = "invalid_source",
//            Metadata = TestsConstants.TestsMetadata,
//        }).Result!;

//        var response = (VoidOperationOutput)result.Output!;

//        Assert.True(response.Error?.Code == AccountsErrors.InvalidSourceAccount.Code);
//    }
//}

using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers.Accounts;
using BankingAppDataTier.Tests.Constants;
using BankingAppDataTier.Tests.Mocks;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Tests.Accounts;

public class DeleteAccountTests
{
    private DeleteAccountOperation deleteAccountOperation;

    private void Setup()
    {
        TestMocksBuilder.Mock();

        deleteAccountOperation = new DeleteAccountOperation(TestMocksBuilder._ExecutionContextMock, string.Empty);
    }

    [Fact]
    public void ShouldBe_Success()
    {
        Setup();

        var result = (OperationHttpResult)deleteAccountOperation.Call(new DeleteAccountInput
        {
            Id = "To_Delete_Current_01",
            Metadata = TestsConstants.TestsMetadata,
        }).Result!;

        var response = (VoidOperationOutput)result.Output!;

        Assert.True(response.Error == null);
    }

    [Fact]
    public void ShouldReturnError_InvalidAccountId()
    {
        Setup();

        var result = (OperationHttpResult)deleteAccountOperation.Call(new DeleteAccountInput
        {
            Id = "invalidId",
            Metadata = TestsConstants.TestsMetadata,
        }).Result!;
        var response = (VoidOperationOutput)result.Output!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public void ShouldReturnError_CantCloseWithRelatedCards()
    {
        Setup();

        var result = (OperationHttpResult)deleteAccountOperation.Call(new DeleteAccountInput
        {
            Id = "Permanent_Current_01",
            Metadata = TestsConstants.TestsMetadata,
        }).Result!;

        var response = (VoidOperationOutput)result.Output!;

        Assert.True(response.Error?.Code == AccountsErrors.CantCloseWithRelatedCards.Code);
    }


    [Fact]
    public void ShouldReturnError_CantCloseWithActiveLoans()
    {
        Setup();

        var result = (OperationHttpResult)deleteAccountOperation.Call(new DeleteAccountInput
        {
            Id = "Permanent_Current_02",
            Metadata = TestsConstants.TestsMetadata,
        }).Result!;

        var response = (VoidOperationOutput)result.Output!;

        Assert.True(response.Error?.Code == AccountsErrors.CantCloseWithActiveLoans.Code);
    }
}

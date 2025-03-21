using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Controllers.Accounts;
using BankingAppDataTier.Tests.Constants;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Accounts;

public class GetAccountsByIdTests
{
    private GetAccountByIdOperation getAccountByIdOperation;

    private void Setup()
    {
        TestMocksBuilder.Mock();

        getAccountByIdOperation = new GetAccountByIdOperation(TestMocksBuilder._ExecutionContextMock);
    }

    [Theory]
    [InlineData("Permanent_Current_01")]
    [InlineData("Permanent_Savings_01")]
    [InlineData("Permanent_Investements_01")]
    public void ShouldBe_Success(string id)
    {
        Setup();

        var result = (OperationResultDto)getAccountByIdOperation.Call(new GetAccountByIdInput
        {
            Id = id,
            Metadata = TestsConstants.TestsMetadata,
        }).Result!; 
        
        var response = (GetAccountByIdOutput)result.Output!;

        Assert.True(response.Account != null);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (OperationResultDto)getAccountByIdOperation.Call(new GetAccountByIdInput
        {
            Id = "invalid",
            Metadata = TestsConstants.TestsMetadata,
        }).Result!; 
        
        var response = (GetAccountByIdOutput)result.Output!;

        Assert.True(response.Account == null);
        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

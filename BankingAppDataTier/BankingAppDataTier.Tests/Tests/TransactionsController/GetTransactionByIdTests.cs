//using BankingAppDataTier.Contracts.Dtos.Outputs.Transactions;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;

//namespace BankingAppDataTier.Tests.Transactions;

//public class GetTransactionByIdTests
//{
//    private TransactionsController _transactionsController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _transactionsController = TestMocksBuilder._TransactionsControllerMock;
//    }

//    [Theory]
//    [InlineData("Permanent_Transaction_01")]
//    [InlineData("Permanent_Transaction_02")]
//    [InlineData("Permanent_Transaction_03")]
//    public void ShouldBe_Success(string id)
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.GetTransactionById(id).Result!;

//        var response = (GetTransactionByIdOutput)result.Value!;

//        Assert.True(response.Transaction != null);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidId()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.GetTransactionById("invalid_id").Result!;

//        var response = (GetTransactionByIdOutput)result.Value!;

//        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
//    }
//}

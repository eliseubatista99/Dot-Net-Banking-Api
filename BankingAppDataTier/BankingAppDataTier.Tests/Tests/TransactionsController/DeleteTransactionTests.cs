//using BankingAppDataTier.Contracts.Dtos.Outputs.Transactions;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using ElideusDotNetFramework.Operations.Contracts;

//namespace BankingAppDataTier.Tests.Transactions;

//public class DeleteTransactionTests
//{
//    private TransactionsController _transactionsController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _transactionsController = TestMocksBuilder._TransactionsControllerMock;
//    }

//    [Fact]
//    public void ShouldBe_Success()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.DeleteTransaction("To_Delete_Transaction_01").Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error == null);

//        result = (ObjectResult)_transactionsController.GetTransactionById("To_Delete_Transaction_01").Result!;

//        var response2 = (GetTransactionByIdOutput)result.Value!;

//        Assert.True(response2.Transaction == null);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidId()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.DeleteTransaction("invalid_id").Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
//    }
//}

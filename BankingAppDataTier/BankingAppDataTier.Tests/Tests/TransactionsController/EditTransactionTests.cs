//using BankingAppDataTier.Contracts.Dtos.Inputs.Transactions;
//using BankingAppDataTier.Contracts.Dtos.Outputs.Transactions;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using ElideusDotNetFramework.Operations.Contracts;

//namespace BankingAppDataTier.Tests.Transactions;

//public class EditTransactionTests
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
//        const string newDescription = "desc new";
//        Setup();

//        var result = (ObjectResult)_transactionsController.EditTransaction(new EditTransactionInput
//        {
//            Id = "To_Edit_Transaction_01",
//            Description = newDescription,
//            DestinationName = "new destination"
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error == null);

//        result = (ObjectResult)_transactionsController.GetTransactionById("To_Edit_Transaction_01").Result!;

//        var response2 = (GetTransactionByIdOutput)result.Value!;

//        Assert.True(response2.Transaction?.Description == newDescription);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidId()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.EditTransaction(new EditTransactionInput
//        {
//            Id = "invalid_id",
//            Description = "new desc",
//            DestinationName = "new destination"
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
//    }
//}

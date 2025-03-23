//using BankingAppDataTier.Contracts.Dtos.Entitites;
//using BankingAppDataTier.Contracts.Dtos.Inputs.Transactions;
//using BankingAppDataTier.Contracts.Dtos.Outputs.Transactions;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using ElideusDotNetFramework.Operations.Contracts;

//namespace BankingAppDataTier.Tests.Transactions;

//public class AddTransactionTests
//{
//    private TransactionsController _transactionsController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _transactionsController = TestMocksBuilder._TransactionsControllerMock;
//    }

//    [Fact]
//    public void ShouldBe_Success_ForAccount()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.AddTransaction(new AddTransactionInput
//        {
//            Transaction = new TransactionDto
//            {
//                Id = "Test01",
//                TransactionDate = new DateTime(2025, 02, 10),
//                Description = "Test transfer",
//                Amount = 100.35M,
//                Fees = 1.25M,
//                Urgent = true,
//                SourceAccount = "Permanent_Current_01",
//                DestinationName = "Eletricity Company",
//                Role = Contracts.Enums.TransactionRole.Receiver,
//            },
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error == null);

//        result = (ObjectResult)_transactionsController.GetTransactionById("Test01").Result!;

//        var response2 = (GetTransactionByIdOutput)result.Value!;

//        Assert.True(response2.Transaction != null);
//    }

//    [Fact]
//    public void ShouldBe_Success_ForCard()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.AddTransaction(new AddTransactionInput
//        {
//            Transaction = new TransactionDto
//            {
//                Id = "Test02",
//                TransactionDate = new DateTime(2025, 02, 10),
//                Description = "Test transfer",
//                Amount = 100.35M,
//                Fees = 1.25M,
//                Urgent = true,
//                SourceCard = "Permanent_Debit_01",
//                DestinationName = "Eletricity Company",
//                Role = Contracts.Enums.TransactionRole.Sender,
//            },
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error == null);

//        result = (ObjectResult)_transactionsController.GetTransactionById("Test02").Result!;

//        var response2 = (GetTransactionByIdOutput)result.Value!;

//        Assert.True(response2.Transaction != null);
//    }

//    [Fact]
//    public void ShouldReturnError_IdAlreadyInUse()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.AddTransaction(new AddTransactionInput
//        {
//            Transaction = new TransactionDto
//            {
//                Id = "Permanent_Transaction_01",
//                TransactionDate = new DateTime(2025, 02, 10),
//                Description = "Test transfer",
//                Amount = 100.35M,
//                Fees = 1.25M,
//                Urgent = true,
//                SourceCard = "Permanent_Debit_01",
//                DestinationName = "Eletricity Company",
//                Role = Contracts.Enums.TransactionRole.Sender,
//            },
//        }).Result!;


//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidSourceAccount()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.AddTransaction(new AddTransactionInput
//        {
//            Transaction = new TransactionDto
//            {
//                Id = "Test03",
//                TransactionDate = new DateTime(2025, 02, 10),
//                Description = "Test transfer",
//                Amount = 100.35M,
//                Fees = 1.25M,
//                Urgent = true,
//                SourceAccount = "Invalid_Account",
//                DestinationName = "Eletricity Company",
//                Role = Contracts.Enums.TransactionRole.Sender,
//            },
//        }).Result!;


//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error?.Code == TransactionsErrors.InvalidSourceAccount.Code);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidSourceCard()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.AddTransaction(new AddTransactionInput
//        {
//            Transaction = new TransactionDto
//            {
//                Id = "Test03",
//                TransactionDate = new DateTime(2025, 02, 10),
//                Description = "Test transfer",
//                Amount = 100.35M,
//                Fees = 1.25M,
//                Urgent = true,
//                SourceCard = "Invalid_Card",
//                DestinationName = "Eletricity Company",
//                Role = Contracts.Enums.TransactionRole.Sender,
//            },
//        }).Result!;


//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error?.Code == TransactionsErrors.InvalidSourceCard.Code);
//    }
//}

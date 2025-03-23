//using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
//using BankingAppDataTier.Contracts.Dtos.Outputs.Loans;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using ElideusDotNetFramework.Operations.Contracts;

//namespace BankingAppDataTier.Tests.Loans;

//public class AmortizeLoanTests
//{
//    private LoansController _loansController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _loansController = TestMocksBuilder._LoansControllerMock;
//    }

//    [Fact]
//    public void ShouldBe_Success()
//    {
//        Setup();

//        var result = (ObjectResult)_loansController.AmortizeLoan(new AmortizeLoanInput
//        {
//            Id = "Permanent_AU_01",
//            Amount = 100,
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error == null);

//        result = (ObjectResult)_loansController.GetLoanById("Permanent_AU_01").Result!;

//        var response2 = (GetLoanByIdOutput)result.Value!;

//        Assert.True(response2.Loan?.PaidAmount == 100);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidId()
//    {
//        Setup();

//        var result = (ObjectResult)_loansController.AmortizeLoan(new AmortizeLoanInput
//        {
//            Id = "invalid_id",
//            Amount = 100,
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;


//        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
//    }

//    [Fact]
//    public void ShouldReturnError_InsufficientFunds()
//    {
//        Setup();

//        var result = (ObjectResult)_loansController.AmortizeLoan(new AmortizeLoanInput
//        {
//            Id = "Permanent_AU_01",
//            Amount = 999999,
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;


//        Assert.True(response.Error?.Code == LoansErrors.InsufficientFunds.Code);
//    }
//}

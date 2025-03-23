//using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
//using BankingAppDataTier.Contracts.Dtos.Outputs.Loans;
//using BankingAppDataTier.Contracts.Enums;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;

//namespace BankingAppDataTier.Tests.Loans;

//public class GetLoansOfAccountTests
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

//        var result = (ObjectResult)_loansController.GetLoansOfAccount(new GetLoansOfAccountInput
//        {
//            AccountId = "Permanent_Current_01",
//        }).Result!;

//        var response = (GetLoansOfAccountOutput)result.Value!;

//        Assert.True(response.Loans.Count > 0);
//    }

//    [Theory]
//    [InlineData(LoanType.Personal)]
//    [InlineData(LoanType.Auto)]
//    [InlineData(LoanType.Mortgage)]
//    public void ShouldBe_Success_For_LoanType(LoanType loanType)
//    {
//        Setup();

//        var result = (ObjectResult)_loansController.GetLoansOfAccount(new GetLoansOfAccountInput
//        {
//            AccountId = "Permanent_Current_01",
//            LoanType = loanType,
//        }).Result!;

//        var response = (GetLoansOfAccountOutput)result.Value!;

//        Assert.True(response.Loans.Count > 0);
//        Assert.True(!response.Loans.Exists(l => l.LoanType != loanType));
//    }
//}

using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Outputs.Loans;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Tests.Loans;

public class AddLoanTests
{
    private LoansController _loansController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _loansController = TestMocksBuilder._LoansControllerMock;
    }

    [Fact]
    public void ShouldBe_Success()
    {
        Setup();

        var result = (ObjectResult)_loansController.AddLoan(new AddLoanInput
        {
            Loan = new LoanDto
            {
                Id = "Test01",
                Name = "Loan Name",
                LoanType = Contracts.Enums.LoanType.Auto,
                Interest = 7.0M,
                StartDate = new DateTime(2025, 01, 01),
                RelatedOffer = "Permanent_AU_01",
                RelatedAccount = "Permanent_Current_01",
                Duration = 10,
                ContractedAmount = 10,
                PaidAmount = 0,
            }
        }).Result!;

        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_loansController.GetLoanById("Test01").Result!;

        var response2 = (GetLoanByIdOutput)result.Value!;

        Assert.True(response2.Loan != null);
    }

    [Fact]
    public void ShouldReturnError_IdAlreadyInUse()
    {
        Setup();

        var result = (ObjectResult)_loansController.AddLoan(new AddLoanInput
        {
            Loan = new LoanDto
            {
                Id = "Permanent_AU_01",
                Name = "Loan Name",
                LoanType = Contracts.Enums.LoanType.Auto,
                Interest = 7.0M,
                StartDate = new DateTime(2025, 01, 01),
                RelatedOffer = "Permanent_AU_01",
                RelatedAccount = "Permanent_Current_01",
                Duration = 10,
                ContractedAmount = 10,
                PaidAmount = 0,
            }
        }).Result!;

        var response = (VoidOperationOutput)result.Value!;


        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
    }
}

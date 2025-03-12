using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Loans;

public class EditLoanTests
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
        const string newName = "newName";
        Setup();

        var result = (ObjectResult)_loansController.EditLoan(new EditLoanInput
        {
            Id = "Permanent_AU_01",
            Name = newName,
            RelatedOffer = "Permanent_AU_01",
            RelatedAccount = "Permanent_Current_01",
            Duration = 10,
            PaidAmount = 0,
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_loansController.GetLoanById("Permanent_AU_01").Result!;

        var response2 = (GetLoanByIdOutput)result.Value!;

        Assert.True(response2.Loan?.Name == newName);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_loansController.DeleteLoan("invalid_id").Result!;

        var response = (VoidOutput)result.Value!;


        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public void ShouldReturnError_CantChangeLoanType()
    {
        Setup();

        var result = (ObjectResult)_loansController.EditLoan(new EditLoanInput
        {
            Id = "Permanent_AU_01",
            RelatedOffer = "Permanent_MO_01",
        }).Result!;
        var response = (VoidOutput)result.Value!;


        Assert.True(response.Error?.Code == LoansErrors.CantChangeLoanType.Code);
    }


    [Fact]
    public void ShouldReturnError_InvalidRelatedOffer()
    {
        Setup();

        var result = (ObjectResult)_loansController.EditLoan(new EditLoanInput
        {
            Id = "Permanent_AU_01",
            RelatedOffer = "invalid",
        }).Result!;
        var response = (VoidOutput)result.Value!;


        Assert.True(response.Error?.Code == LoansErrors.InvalidRelatedOffer.Code);
    }


    [Fact]
    public void ShouldReturnError_InvalidRelatedAccount()
    {
        Setup();

        var result = (ObjectResult)_loansController.EditLoan(new EditLoanInput
        {
            Id = "Permanent_AU_01",
            RelatedAccount = "invalid",
        }).Result!;
        var response = (VoidOutput)result.Value!;


        Assert.True(response.Error?.Code == LoansErrors.InvalidRelatedAccount.Code);
    }
}

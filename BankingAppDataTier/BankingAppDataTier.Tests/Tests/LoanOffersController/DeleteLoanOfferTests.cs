using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Tests.LoanOffers;

public class DeleteLoanOfferTests
{
    private LoanOffersController _loanOffersController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _loanOffersController = TestMocksBuilder._LoanOffersControllerMock;
    }

    [Fact]
    public void ShouldBe_Success()
    {
        Setup();

        var result = (ObjectResult)_loanOffersController.DeleteLoanOffer("To_Delete_AU_01").Result!;

        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_loanOffersController.GetLoanOfferById("To_Delete_AU_01").Result!;

        var response2 = (GetLoanOfferByIdOutput)result.Value!;

        Assert.True(response2.LoanOffer == null);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_loanOffersController.DeleteLoanOffer("Invalid_id").Result!;


        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public void ShouldReturnError_CantDeleteWithRelatedLoans()
    {
        Setup();

        var result = (ObjectResult)_loanOffersController.DeleteLoanOffer("Permanent_AU_01").Result!;


        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error?.Code == LoanOffersErrors.CantDeleteWithRelatedLoans.Code);
    }
}

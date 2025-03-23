using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Tests.LoanOffers;

public class ActivateOrDeactivateLoanOfferTests
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

        var result = (ObjectResult)_loanOffersController.ActivateOrDeactivateLoanOffer(new ActivateOrDeactivateLoanOfferInput
        {
            Id = "To_Edit_AU_02",
            Active = false,
        }).Result!;

        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_loanOffersController.GetLoanOfferByType(Contracts.Enums.LoanType.Auto).Result!;

        var response2 = (GetLoanOffersByTypeOutput)result.Value!;

        Assert.True(response2.LoanOffers.Find(l => l.Id == "To_Edit_AU_02") == null);

        result = (ObjectResult)_loanOffersController.ActivateOrDeactivateLoanOffer(new ActivateOrDeactivateLoanOfferInput
        {
            Id = "To_Edit_AU_02",
            Active = true,
        }).Result!;

        response = (VoidOperationOutput)result.Value!;
        Assert.True(response.Error == null);

        result = (ObjectResult)_loanOffersController.GetLoanOfferByType(Contracts.Enums.LoanType.Auto).Result!;

        response2 = (GetLoanOffersByTypeOutput)result.Value!;

        Assert.True(response2.LoanOffers.Find(l => l.Id == "To_Edit_AU_02") != null);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_loanOffersController.ActivateOrDeactivateLoanOffer(new ActivateOrDeactivateLoanOfferInput
        {
            Id = "invalidId",
            Active = false,
        }).Result!;

        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

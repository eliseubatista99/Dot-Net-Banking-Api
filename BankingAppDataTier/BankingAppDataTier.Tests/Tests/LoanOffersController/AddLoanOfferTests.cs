using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.LoanOffers;

public class AddLoanOfferTests
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

        var result = (ObjectResult)_loanOffersController.AddLoanOffer(new AddLoanOfferInput
        {
            LoanOffer = new LoanOfferDto
            {
                Id = "Test01",
                Name = "Loan Name",
                Description = "Desc",
                LoanType = Contracts.Enums.LoanType.Auto,
                MaxEffort = 30,
                Interest = 7.0M,
                IsActive = true,
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_loanOffersController.GetLoanOfferById("Test01").Result!;

        var response2 = (GetLoanOfferByIdOutput)result.Value!;

        Assert.True(response2.LoanOffer != null);
    }

    [Fact]
    public void ShouldReturnError_IdAlreadyInUse()
    {
        Setup();

        var result = (ObjectResult)_loanOffersController.AddLoanOffer(new AddLoanOfferInput
        {
            LoanOffer = new LoanOfferDto
            {
                Id = "Permanent_AU_01",
                Description = "Desc",
                Name = "Loan Name",
                LoanType = Contracts.Enums.LoanType.Auto,
                MaxEffort = 30,
                Interest = 7.0M,
                IsActive = true,
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
    }
}

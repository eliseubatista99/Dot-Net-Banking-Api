using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.LoanOffers;

public class GetLoanOfferByTypeTests
{
    private LoanOffersController _loanOffersController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _loanOffersController = TestMocksBuilder._LoanOffersControllerMock;
    }

    [Theory]
    [InlineData(LoanType.Personal)]
    [InlineData(LoanType.Auto)]
    [InlineData(LoanType.Mortgage)]
    public void ShouldBe_Success(LoanType loanType)
    {
        Setup();

        var result = (ObjectResult)_loanOffersController.GetLoanOfferByType(loanType).Result!;

        var response = (GetLoanOffersByTypeOutput)result.Value!;

        Assert.True(response.LoanOffers.Count > 0);
    }
}

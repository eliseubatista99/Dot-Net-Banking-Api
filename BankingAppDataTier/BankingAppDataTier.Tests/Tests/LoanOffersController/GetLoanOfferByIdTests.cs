//using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;

//namespace BankingAppDataTier.Tests.LoanOffers;

//public class GetLoanOfferByIdTests
//{
//    private LoanOffersController _loanOffersController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _loanOffersController = TestMocksBuilder._LoanOffersControllerMock;
//    }

//    [Theory]
//    [InlineData("Permanent_AU_01")]
//    [InlineData("Permanent_MO_01")]
//    [InlineData("Permanent_PE_01")]
//    public void ShouldBe_Success(string id)
//    {
//        Setup();

//        var result = (ObjectResult)_loanOffersController.GetLoanOfferById(id).Result!;

//        var response = (GetLoanOfferByIdOutput)result.Value!;

//        Assert.True(response.LoanOffer != null);   
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidId()
//    {
//        Setup();

//        var result = (ObjectResult)_loanOffersController.GetLoanOfferById("invalid_id").Result!;

//        var response = (GetLoanOfferByIdOutput)result.Value!;

//        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
//    }
//}

//using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
//using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using ElideusDotNetFramework.Operations.Contracts;

//namespace BankingAppDataTier.Tests.LoanOffers;

//public class EditLoanOfferTests
//{
//    private LoanOffersController _loanOffersController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _loanOffersController = TestMocksBuilder._LoanOffersControllerMock;
//    }

//    [Fact]
//    public void ShouldBe_Success()
//    {
//        const string newName = "NewName";
//        Setup();

//        var result = (ObjectResult)_loanOffersController.EditLoanOffer(new EditLoanOfferInput
//        {
//            Id = "To_Edit_AU_01",
//            Name = newName,
//            Description = "new Desc",
//            MaxEffort = 50,
//            Interest = 12.0M,
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error == null);

//        result = (ObjectResult)_loanOffersController.GetLoanOfferById("To_Edit_AU_01").Result!;

//        var response2 = (GetLoanOfferByIdOutput)result.Value!;

//        Assert.True(response2.LoanOffer?.Name == newName);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidId()
//    {
//        Setup();

//        var result = (ObjectResult)_loanOffersController.EditLoanOffer(new EditLoanOfferInput
//        {
//            Id = "invalid_id",
//            Description = "new Desc",
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
//    }
//}

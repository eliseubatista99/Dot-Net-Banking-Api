//using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;

//namespace BankingAppDataTier.Tests.Plastics;

//public class GetPlasticByIdTests
//{
//    private PlasticsController _plasticsController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _plasticsController = TestMocksBuilder._PlasticsControllerMock;
//    }

//    [Theory]
//    [InlineData("Permanent_Debit_01")]
//    [InlineData("Permanent_PrePaid_01")]
//    [InlineData("Permanent_Credit_01")]
//    public void ShouldBe_Success(string id)
//    {
//        Setup();

//        var result = (ObjectResult)_plasticsController.GetPlasticById(id).Result!;

//        var response = (GetPlasticByIdOutput)result.Value!;

//        Assert.True(response.Plastic != null);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidId()
//    {
//        Setup();

//        var result = (ObjectResult)_plasticsController.GetPlasticById("invalid_id").Result!;

//        var response = (GetPlasticByIdOutput)result.Value!;

//        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
//    }
//}

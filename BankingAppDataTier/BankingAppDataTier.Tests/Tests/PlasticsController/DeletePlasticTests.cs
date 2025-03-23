//using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using ElideusDotNetFramework.Operations.Contracts;


//namespace BankingAppDataTier.Tests.Plastics;

//public class DeletePlasticTests
//{
//    private PlasticsController _plasticsController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _plasticsController = TestMocksBuilder._PlasticsControllerMock;
//    }

//    [Fact]
//    public void ShouldBe_Success()
//    {
//        Setup();

//        var result = (ObjectResult)_plasticsController.DeletePlastic("To_Delete_Debit_01").Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error == null);

//        result = (ObjectResult)_plasticsController.GetPlasticById("To_Delete_Debit_01").Result!;

//        var response2 = (GetPlasticByIdOutput)result.Value!;

//        Assert.True(response2.Plastic == null);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidId()
//    {
//        Setup();

//        var result = (ObjectResult)_plasticsController.DeletePlastic("Invalid_id").Result!;


//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
//    }

//    [Fact]
//    public void ShouldReturnError_CantDeleteWithRelatedCards()
//    {
//        Setup();

//        var result = (ObjectResult)_plasticsController.DeletePlastic("Permanent_Debit_01").Result!;


//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error?.Code == PlasticsErrors.CantDeleteWithRelatedCards.Code);
//    }
//}

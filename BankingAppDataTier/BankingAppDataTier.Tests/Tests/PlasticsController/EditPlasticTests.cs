//using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
//using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using ElideusDotNetFramework.Operations.Contracts;

//namespace BankingAppDataTier.Tests.Plastics;

//public class EditPlasticTests
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
//        const string newName = "NewName";
//        Setup();

//        var result = (ObjectResult)_plasticsController.EditPlastic(new EditPlasticInput
//        {
//            Id = "Permanent_Debit_01",
//            Name = newName,
//            Image = string.Empty,
//            Cashback = 10,
//            Commission = 20,
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error == null);

//        result = (ObjectResult)_plasticsController.GetPlasticById("Permanent_Debit_01").Result!;

//        var response2 = (GetPlasticByIdOutput)result.Value!;

//        Assert.True(response2.Plastic?.Name == newName);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidId()
//    {
//        Setup();

//        var result = (ObjectResult)_plasticsController.EditPlastic(new EditPlasticInput
//        {
//            Id = "invalid_id",
//            Name = "new name",
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
//    }
//}

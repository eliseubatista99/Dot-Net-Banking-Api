//using BankingAppDataTier.Contracts.Dtos.Entitites;
//using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
//using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using ElideusDotNetFramework.Operations.Contracts;

//namespace BankingAppDataTier.Tests.Plastics;

//public class AddPlasticTests
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

//        var result = (ObjectResult)_plasticsController.AddPlastic(new AddPlasticInput
//        {
//            Plastic = new PlasticDto
//            {
//                Id = "Test01",
//                CardType = Contracts.Enums.CardType.Debit,
//                Name = "DotNet Basic",
//                Cashback = 3,
//                Commission = 10,
//                IsActive = true,
//                Image = string.Empty,
//            },
//        }).Result!;

//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error == null);

//        result = (ObjectResult)_plasticsController.GetPlasticById("Test01").Result!;

//        var response2 = (GetPlasticByIdOutput)result.Value!;

//        Assert.True(response2.Plastic != null);
//    }

//    [Fact]
//    public void ShouldReturnError_IdAlreadyInUse()
//    {
//        Setup();

//        var result = (ObjectResult)_plasticsController.AddPlastic(new AddPlasticInput
//        {
//            Plastic = new PlasticDto
//            {
//                Id = "Permanent_Debit_01",
//                CardType = Contracts.Enums.CardType.Debit,
//                Name = "DotNet Basic",
//                IsActive = true,
//                Image = string.Empty,
//            },
//        }).Result!;


//        var response = (VoidOperationOutput)result.Value!;

//        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
//    }
//}

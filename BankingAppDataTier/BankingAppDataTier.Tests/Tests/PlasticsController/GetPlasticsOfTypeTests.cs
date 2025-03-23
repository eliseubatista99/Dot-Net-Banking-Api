//using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
//using BankingAppDataTier.Contracts.Enums;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;

//namespace BankingAppDataTier.Tests.Plastics;

//public class GetPlasticsOfTypeTests
//{
//    private PlasticsController _plasticsController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _plasticsController = TestMocksBuilder._PlasticsControllerMock;
//    }

//    [Theory]
//    [InlineData(CardType.Debit)]
//    [InlineData(CardType.Credit)]
//    [InlineData(CardType.PrePaid)]
//    [InlineData(CardType.Meal)]
//    public void ShouldBe_Success(CardType cardType)
//    {
//        Setup();

//        var result = (ObjectResult)_plasticsController.GetPlasticsOfType(cardType).Result!;

//        var response = (GetPlasticsOfTypeOutput)result.Value!;

//        Assert.True(response.Plastics.Count > 0);
//    }
//}

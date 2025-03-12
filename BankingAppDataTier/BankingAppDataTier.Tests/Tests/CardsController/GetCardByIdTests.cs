using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Cards;

public class GetCardByIdTests
{
    private CardsController _cardsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _cardsController = TestMocksBuilder._CardsControllerMock;
    }

    [Theory]
    [InlineData("Permanent_Debit_01")]
    [InlineData("Permanent_PrePaid_01")]
    [InlineData("Permanent_Credit_01")]
    public void ShouldBe_Success(string id)
    {
        Setup();

        var result = (ObjectResult)_cardsController.GetCardById(id).Result!;
        var response = (GetCardByIdOutput)result.Value!;

        Assert.True(response.Card != null);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_cardsController.GetCardById("invalid").Result!;
        var response = (GetCardByIdOutput)result.Value!;

        Assert.True(response.Card == null);
        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

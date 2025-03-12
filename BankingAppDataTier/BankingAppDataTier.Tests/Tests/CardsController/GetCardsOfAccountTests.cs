using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Cards;

public class GetCardsOfAccountTests
{
    private CardsController _cardsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _cardsController = TestMocksBuilder._CardsControllerMock;
    }

    [Fact]
    public void ShouldBe_Success()
    {
        Setup();

        var result = (ObjectResult)_cardsController.GetCardsOfAccount("Permanent_Current_01").Result!;
        var response = (GetCardsOfAccountOutput)result.Value!;

        Assert.True(response.Cards.Count > 0);
    }

    [Fact]
    public void ShouldReturnError_InvalidAccountId()
    {
        Setup();

        var result = (ObjectResult)_cardsController.GetCardsOfAccount("invalid_id").Result!;
        var response = (GetCardsOfAccountOutput)result.Value!;

        Assert.True(response.Error?.Code == CardsErrors.InvalidAccount.Code);
    }
}

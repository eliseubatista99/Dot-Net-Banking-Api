using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Cards;

public class DeleteCardTests
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

        var result = (ObjectResult)_cardsController.DeleteCard("To_Delete_Debit_01").Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_cardsController.GetCardById("To_Delete_Debit_01").Result!;

        var response2 = (GetCardByIdOutput)result.Value!;

        Assert.True(response2.Card == null);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_cardsController.DeleteCard("InvalidId").Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

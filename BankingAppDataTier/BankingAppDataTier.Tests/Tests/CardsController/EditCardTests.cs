using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Tests.Cards;

public class EditCardTests
{
    private CardsController _cardsController;

    private void Setup()
    {
        TestMocksBuilder.Mock();
        _cardsController = TestMocksBuilder._CardsControllerMock;
    }

    [Fact]
    public void ShouldBe_Success_DebitCard()
    {
        const string newName = "MyCardNameTest";
        Setup();

        var result = (ObjectResult)_cardsController.EditCard(new EditCardInput
        {
            Id = "To_Edit_Debit_01",
            Name = newName,
            RequestDate = new DateTime(2025, 02, 01),
            ActivationDate = new DateTime(2025, 02, 15),
            ExpirationDate = new DateTime(2028, 02, 15),
        }).Result!;

        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_cardsController.GetCardById("To_Edit_Debit_01").Result!;

        var response2 = (GetCardByIdOutput)result.Value!;

        Assert.True(response2.Card?.Name == newName);
    }

    [Fact]
    public void ShouldBe_Success_CreditCard()
    {
        const string newName = "MyCardNameTest";
        Setup();

        var result = (ObjectResult)_cardsController.EditCard(new EditCardInput
        {
            Id = "To_Edit_Credit_01",
            Name = newName,
            RequestDate = new DateTime(2025, 02, 01),
            ActivationDate = new DateTime(2025, 02, 15),
            ExpirationDate = new DateTime(2028, 02, 15),
            PaymentDay = 11,
            Balance = 50,
        }).Result!;

        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_cardsController.GetCardById("To_Edit_Credit_01").Result!;

        var response2 = (GetCardByIdOutput)result.Value!;

        Assert.True(response2.Card?.Name == newName);
    }

    [Fact]
    public void ShouldBe_Success_PrePaidCard()
    {
        const string newName = "MyCardNameTest";
        Setup();

        var result = (ObjectResult)_cardsController.EditCard(new EditCardInput
        {
            Id = "To_Edit_PrePaid_01",
            Name = newName,
            RequestDate = new DateTime(2025, 02, 01),
            ActivationDate = new DateTime(2025, 02, 15),
            ExpirationDate = new DateTime(2028, 02, 15),
            Balance = 50,
        }).Result!;

        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_cardsController.GetCardById("To_Edit_PrePaid_01").Result!;

        var response2 = (GetCardByIdOutput)result.Value!;

        Assert.True(response2.Card?.Name == newName);
    }

    [Fact]
    public void ShouldReturnError_InvalidId()
    {
        Setup();

        var result = (ObjectResult)_cardsController.EditCard(new EditCardInput
        {
            Id = "invalid_id",
            Name = "test",
        }).Result!;

        var response = (VoidOperationOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

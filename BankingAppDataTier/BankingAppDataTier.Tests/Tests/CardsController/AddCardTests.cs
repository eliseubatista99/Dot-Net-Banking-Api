using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Controllers;
using BankingAppDataTier.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Cards;

public class AddCardTests
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
        Setup();

        var result = (ObjectResult)_cardsController.AddCard(new AddCardInput
        {
            Card = new CardDto
            {
                Id = "TestCard01",
                Name = "DB_Basic",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "DB_Basic",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
                CardType = Contracts.Enums.CardType.Debit,
                Image = "img",
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_cardsController.GetCardById("TestCard01").Result!;

        var response2 = (GetCardByIdOutput)result.Value!;

        Assert.True(response2.Card != null);
    }

    [Fact]
    public void ShouldBe_Success_CreditCard()
    {
        Setup();

        var result = (ObjectResult)_cardsController.AddCard(new AddCardInput
        {
            Card = new CardDto
            {
                Id = "TestCard02",
                Name = "CR_Prestige",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "CR_Prestige",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
                CardType = Contracts.Enums.CardType.Credit,
                Image = "img",
                PaymentDay = 21,
                Balance = 500,
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_cardsController.GetCardById("TestCard02").Result!;

        var response2 = (GetCardByIdOutput)result.Value!;

        Assert.True(response2.Card != null);
    }

    [Fact]
    public void ShouldBe_Success_PrePaidCard()
    {
        Setup();

        var result = (ObjectResult)_cardsController.AddCard(new AddCardInput
        {
            Card = new CardDto
            {
                Id = "TestCard03",
                Name = "PP_Agile",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "PP_Agile",
                Balance = 100,
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
                CardType= Contracts.Enums.CardType.PrePaid,
                Image = "img",
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error == null);

        result = (ObjectResult)_cardsController.GetCardById("TestCard03").Result!;

        var response2 = (GetCardByIdOutput)result.Value!;

        Assert.True(response2.Card != null);
    }

    [Fact]
    public void ShouldReturnError_IdAlreadyInUse()
    {
        Setup();

        var result = (ObjectResult)_cardsController.AddCard(new AddCardInput
        {
            Card = new CardDto
            {
                Id = "Permanent_Debit_01",
                Name = "DB_Basic",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "DB_Basic",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
                CardType = Contracts.Enums.CardType.Debit,
                Image = "img",
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
    }

    [Theory]
    [InlineData(null, 500.0)]
    [InlineData(15, null)]
    public void ShouldReturnError_MissingCreditCardDetails(int? paymentDay, double? balance)
    {
        Setup();

        var result = (ObjectResult)_cardsController.AddCard(new AddCardInput
        {
            Card = new CardDto
            {
                Id = "TestCard05",
                Name = "CR_Prestige",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "CR_Prestige",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
                CardType = Contracts.Enums.CardType.Credit,
                Image = "img",
                PaymentDay = paymentDay,
                Balance = balance != null? (decimal) balance : null,
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == CardsErrors.MissingCreditCardDetails.Code);
    }

    [Fact]
    public void ShouldReturnError_MissingPrePaidCardDetails()
    {
        Setup();

        var result = (ObjectResult)_cardsController.AddCard(new AddCardInput
        {
            Card = new CardDto
            {
                Id = "TestCard06",
                Name = "PP_Agile",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "PP_Agile",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
                CardType = Contracts.Enums.CardType.PrePaid,
                Image = "img",
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == CardsErrors.MissingPrePaidCardDetails.Code);
    }

    [Fact]
    public void ShouldReturnError_InvalidPlastic()
    {
        Setup();

        var result = (ObjectResult)_cardsController.AddCard(new AddCardInput
        {
            Card = new CardDto
            {
                Id = "TestCard07",
                Name = "DB_Basic",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "invalid plastic",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
                CardType = Contracts.Enums.CardType.Debit,
                Image = "img",
            },
        }).Result!;

        var response = (VoidOutput)result.Value!;

        Assert.True(response.Error?.Code == CardsErrors.InvalidPlastic.Code);
    }

}

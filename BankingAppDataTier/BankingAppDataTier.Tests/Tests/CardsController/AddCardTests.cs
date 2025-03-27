using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations.Cards;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Cards;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Cards;

public class AddCardTests : OperationTest<AddCardOperation, AddCardInput, VoidOperationOutput>
{
    private IDatabaseCardsProvider databaseCardsProvider { get; set; }

    public AddCardTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new AddCardOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databaseCardsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseCardsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success_DebitCard()
    {
        var addResponse = await SimulateOperationToTestCall(new AddCardInput
        {
            Card = new CardDto
            {
                Id = "TestCard01",
                Name = "DB_Basic",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_Debit_01",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
                CardType = Contracts.Enums.CardType.Debit,
                Image = "img",
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);

        var getByIdResponse = databaseCardsProvider.GetById("TestCard01");

        Assert.True(getByIdResponse != null);
    }

    [Fact]
    public async Task ShouldBe_Success_CreditCard()
    {
        var addResponse = await SimulateOperationToTestCall(new AddCardInput
        {
            Card = new CardDto
            {
                Id = "TestCard02",
                Name = "CR_Prestige",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_Credit_01",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
                CardType = Contracts.Enums.CardType.Credit,
                Image = "img",
                PaymentDay = 21,
                Balance = 500,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);

        var getByIdResponse = databaseCardsProvider.GetById("TestCard02");

        Assert.True(getByIdResponse != null);
    }

    [Fact]
    public async Task ShouldBe_Success_PrePaidCard()
    {
        var addResponse = await SimulateOperationToTestCall(new AddCardInput
        {
            Card = new CardDto
            {
                Id = "TestCard03",
                Name = "PP_Agile",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_PrePaid_01",
                Balance = 100,
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
                CardType = Contracts.Enums.CardType.PrePaid,
                Image = "img",
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);

        var getByIdResponse = databaseCardsProvider.GetById("TestCard03");

        Assert.True(getByIdResponse != null);
    }

    [Fact]
    public async Task ShouldReturnError_IdAlreadyInUse()
    {
        var response = await SimulateOperationToTestCall(new AddCardInput
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
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
    }

    [Theory]
    [InlineData(null, 500.0)]
    [InlineData(15, null)]
    public async Task ShouldReturnError_MissingCreditCardDetails(int? paymentDay, double? balance)
    {
        var response = await SimulateOperationToTestCall(new AddCardInput
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
                Balance = balance != null ? (decimal)balance : null,
            },
            Metadata = TestsConstants.TestsMetadata,
        });
        Assert.True(response.Error?.Code == CardsErrors.MissingCreditCardDetails.Code);
    }

    [Fact]
    public async Task ShouldReturnError_MissingPrePaidCardDetails()
    {
        var response = await SimulateOperationToTestCall(new AddCardInput
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
            Metadata = TestsConstants.TestsMetadata,
        });
        Assert.True(response.Error?.Code == CardsErrors.MissingPrePaidCardDetails.Code);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidPlastic()
    {
        var response = await SimulateOperationToTestCall(new AddCardInput
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
            Metadata = TestsConstants.TestsMetadata,
        });
        Assert.True(response.Error?.Code == CardsErrors.InvalidPlastic.Code);
    }
}

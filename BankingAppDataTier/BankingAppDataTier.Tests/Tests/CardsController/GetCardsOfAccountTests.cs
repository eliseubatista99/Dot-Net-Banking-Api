//using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;

//namespace BankingAppDataTier.Tests.Cards;

//public class GetCardsOfAccountTests
//{
//    private CardsController _cardsController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _cardsController = TestMocksBuilder._CardsControllerMock;
//    }

//    [Fact]
//    public void ShouldBe_Success()
//    {
//        Setup();

//        var result = (ObjectResult)_cardsController.GetCardsOfAccount("Permanent_Current_01").Result!;
//        var response = (GetCardsOfAccountOutput)result.Value!;

//        Assert.True(response.Cards.Count > 0);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidAccountId()
//    {
//        Setup();

//        var result = (ObjectResult)_cardsController.GetCardsOfAccount("invalid_id").Result!;
//        var response = (GetCardsOfAccountOutput)result.Value!;

//        Assert.True(response.Error?.Code == CardsErrors.InvalidAccount.Code);
//    }
//}

using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Operations.Cards;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Tests;
using ElideusDotNetFramework.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppDataTier.Tests.Cards;

public class GetCardsOfAccountTests : OperationTest<GetCardsOfAccountOperation, GetCardsOfAccountInput, GetCardsOfAccountOutput>
{
    public GetCardsOfAccountTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetCardsOfAccountOperation(_testBuilder.ApplicationContextMock!, string.Empty);
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var response = await TestsHelper.SimulateCall<GetCardsOfAccountOperation, GetCardsOfAccountInput, GetCardsOfAccountOutput>(OperationToTest!, new GetCardsOfAccountInput
        {
            AccountId = "Permanent_Current_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Cards.Count > 0);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidAccountId()
    {
        var response = await TestsHelper.SimulateCall<GetCardsOfAccountOperation, GetCardsOfAccountInput, GetCardsOfAccountOutput>(OperationToTest!, new GetCardsOfAccountInput
        {
            AccountId = "invalid_id",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == CardsErrors.InvalidAccount.Code);
    }
}

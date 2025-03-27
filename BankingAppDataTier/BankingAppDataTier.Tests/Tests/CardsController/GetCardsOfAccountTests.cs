using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations.Cards;
using BankingAppDataTier.Operations.Cards;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

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
        var response = await SimulateOperationToTestCall(new GetCardsOfAccountInput
        {
            AccountId = "Permanent_Current_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Cards.Count > 0);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidAccountId()
    {
        var response = await SimulateOperationToTestCall(new GetCardsOfAccountInput
        {
            AccountId = "invalid_id",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == CardsErrors.InvalidAccount.Code);
    }
}

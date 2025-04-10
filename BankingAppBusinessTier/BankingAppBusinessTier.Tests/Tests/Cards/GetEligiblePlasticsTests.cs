using BankingAppBusinessTier.Contracts.Operations.Cards;
using BankingAppBusinessTier.Operations.Cards;
using BankingAppBusinessTier.Tests.Constants;
using BankingAppDataTier.Tests;
using ElideusDotNetFramework.Tests;

namespace BankingAppBusinessTier.Tests.Accounts;

public class GetEligiblePlasticsTests : OperationTest<GetEligiblePlasticsOperation, GetEligiblePlasticsInput, GetEligiblePlasticsOutput>
{
    //private IDatabaseAccountsProvider databaseAccountsProvider { get; set; }

    public GetEligiblePlasticsTests(BankingAppBusinessTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetEligiblePlasticsOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        //databaseAccountsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseAccountsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var addResponse = await SimulateOperationToTestCall(new GetEligiblePlasticsInput
        {
            ClientId = "JW0000000",
            PlasticType = Contracts.Enums.CardType.Credit,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);
    }
}

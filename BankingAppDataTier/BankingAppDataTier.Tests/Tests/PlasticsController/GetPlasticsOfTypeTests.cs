using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Operations.Inputs.Plastics;
using BankingAppDataTier.Contracts.Operations.Outputs.Plastics;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Plastics;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Plastics;

public class GetPlasticsOfTypeTests : OperationTest<GetPlasticsOfTypeOperation, GetPlasticOfTypeInput, GetPlasticsOfTypeOutput>
{
    private IDatabasePlasticsProvider databasePlasticsProvider { get; set; }

    public GetPlasticsOfTypeTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetPlasticsOfTypeOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databasePlasticsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabasePlasticsProvider>()!;
    }

    [Theory]
    [InlineData(CardType.Debit)]
    [InlineData(CardType.Credit)]
    [InlineData(CardType.PrePaid)]
    [InlineData(CardType.Meal)]
    public async Task ShouldBe_Success(CardType cardType)
    {
        var response = await SimulateOperationToTestCall(new GetPlasticOfTypeInput
        {
            PlasticType = cardType,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Plastics.Count > 0);
    }
}

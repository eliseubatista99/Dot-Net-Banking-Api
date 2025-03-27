using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Plastics;

public class AddPlasticTests : OperationTest<AddPlasticOperation, AddPlasticInput, VoidOperationOutput>
{
    private IDatabasePlasticsProvider databasePlasticsProvider { get; set; }

    public AddPlasticTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new AddPlasticOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databasePlasticsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabasePlasticsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var addResponse = await SimulateOperationToTestCall(new AddPlasticInput
        {
            Plastic = new PlasticDto
            {
                Id = "Test01",
                CardType = Contracts.Enums.CardType.Debit,
                Name = "DotNet Basic",
                Cashback = 3,
                Commission = 10,
                IsActive = true,
                Image = string.Empty,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);

        var getByIdResult = databasePlasticsProvider.GetById("Test01");

        Assert.True(getByIdResult != null);
    }

    [Fact]
    public async Task ShouldReturnError_IdAlreadyInUse()
    {
        var response = await SimulateOperationToTestCall(new AddPlasticInput
        {
            Plastic = new PlasticDto
            {
                Id = "Permanent_Debit_01",
                CardType = Contracts.Enums.CardType.Debit,
                Name = "DotNet Basic",
                IsActive = true,
                Image = string.Empty,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
    }
}

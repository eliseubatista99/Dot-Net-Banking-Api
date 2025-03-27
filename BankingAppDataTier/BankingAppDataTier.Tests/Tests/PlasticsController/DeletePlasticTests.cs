using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations.Plastics;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Plastics;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Plastics;

public class DeletePlasticTests : OperationTest<DeletePlasticOperation, DeletePlasticInput, VoidOperationOutput>
{
    private IDatabasePlasticsProvider databasePlasticsProvider { get; set; }

    public DeletePlasticTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new DeletePlasticOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databasePlasticsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabasePlasticsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var delResponse = await SimulateOperationToTestCall(new DeletePlasticInput
        {
            Id = "To_Delete_Debit_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(delResponse.Error == null);

        var getByIdResult = databasePlasticsProvider.GetById("To_Delete_Debit_01");

        Assert.True(getByIdResult == null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new DeletePlasticInput
        {
            Id = "Invalid_id",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }

    [Fact]
    public async Task ShouldReturnError_CantDeleteWithRelatedCards()
    {
        var response = await SimulateOperationToTestCall(new DeletePlasticInput
        {
            Id = "Permanent_Debit_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == PlasticsErrors.CantDeleteWithRelatedCards.Code);
    }
}

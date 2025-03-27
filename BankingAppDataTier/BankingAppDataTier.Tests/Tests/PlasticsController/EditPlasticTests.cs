using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Plastics;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Plastics;

public class EditPlasticTests : OperationTest<EditPlasticOperation, EditPlasticInput, VoidOperationOutput>
{
    private IDatabasePlasticsProvider databasePlasticsProvider { get; set; }

    public EditPlasticTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new EditPlasticOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databasePlasticsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabasePlasticsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        const string newName = "NewName";

        var editResponse = await SimulateOperationToTestCall(new EditPlasticInput
        {
            Id = "Permanent_Debit_01",
            Name = newName,
            Image = string.Empty,
            Cashback = 10,
            Commission = 20,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(editResponse.Error == null);

        var getByIdResult = databasePlasticsProvider.GetById("Permanent_Debit_01");

        Assert.True(getByIdResult?.Name == newName);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new EditPlasticInput
        {
            Id = "invalid_id",
            Name = "new name",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

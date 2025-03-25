
using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Plastics;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Plastics;

public class GetPlasticByIdTests : OperationTest<GetPlasticByIdOperation, GetPlasticByIdInput, GetPlasticByIdOutput>
{
    private IDatabasePlasticsProvider databasePlasticsProvider { get; set; }

    public GetPlasticByIdTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetPlasticByIdOperation(_testBuilder.ApplicationContextMock!, string.Empty);
        databasePlasticsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabasePlasticsProvider>()!;
    }

    [Theory]
    [InlineData("Permanent_Debit_01")]
    [InlineData("Permanent_PrePaid_01")]
    [InlineData("Permanent_Credit_01")]
    public async Task ShouldBe_Success(string id)
    {
        var response = await SimulateOperationToTestCall(new GetPlasticByIdInput
        {
            Id = id,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Plastic != null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new GetPlasticByIdInput
        {
            Id = "invalid_id",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Transactions;

public class GetTransactionByIdTests : OperationTest<GetTransactionByIdOperation, GetTransactionByIdInput, GetTransactionByIdOutput>
{
    private IDatabasePlasticsProvider databasePlasticsProvider { get; set; }
    private IDatabaseTransactionsProvider databaseTransactionsProvider { get; set; }

    public GetTransactionByIdTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetTransactionByIdOperation(_testBuilder.ApplicationContextMock!, string.Empty);

        databaseTransactionsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseTransactionsProvider>()!;
    }

    [Theory]
    [InlineData("Permanent_Transaction_01")]
    [InlineData("Permanent_Transaction_02")]
    [InlineData("Permanent_Transaction_03")]
    public async Task ShouldBe_Success(string id)
    {
        var response = await SimulateOperationToTestCall(new GetTransactionByIdInput
        {
            Id = id,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Transaction != null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new GetTransactionByIdInput
        {
            Id = "invalid_id",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

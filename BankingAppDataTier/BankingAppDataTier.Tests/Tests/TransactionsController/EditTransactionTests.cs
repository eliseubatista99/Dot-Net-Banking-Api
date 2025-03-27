using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations.Transactions;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Transactions;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Transactions;

public class EditTransactionTests : OperationTest<EditTransactionOperation, EditTransactionInput, VoidOperationOutput>
{
    private IDatabasePlasticsProvider databasePlasticsProvider { get; set; }
    private IDatabaseTransactionsProvider databaseTransactionsProvider { get; set; }

    public EditTransactionTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new EditTransactionOperation(_testBuilder.ApplicationContextMock!, string.Empty);

        databaseTransactionsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseTransactionsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        const string newDescription = "desc new";

        var editResponse = await SimulateOperationToTestCall(new EditTransactionInput
        {
            Id = "To_Edit_Transaction_01",
            Description = newDescription,
            DestinationName = "new destination",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(editResponse.Error == null);

        var getByIdResult = databaseTransactionsProvider.GetById("To_Edit_Transaction_01");

        Assert.True(getByIdResult?.Description == newDescription);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new EditTransactionInput
        {
            Id = "invalid_id",
            Description = "new desc",
            DestinationName = "new destination",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

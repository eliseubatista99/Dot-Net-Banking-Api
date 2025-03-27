using BankingAppDataTier.Contracts.Dtos.Inputs.Transactions;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations.Transactions;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Transactions;

public class DeleteTransactionTests : OperationTest<DeleteTransactionOperation, DeleteTransactionInput, VoidOperationOutput>
{
    private IDatabasePlasticsProvider databasePlasticsProvider { get; set; }
    private IDatabaseTransactionsProvider databaseTransactionsProvider { get; set; }

    public DeleteTransactionTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new DeleteTransactionOperation(_testBuilder.ApplicationContextMock!, string.Empty);

        databaseTransactionsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseTransactionsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var delResponse = await SimulateOperationToTestCall(new DeleteTransactionInput
        {
            Id = "To_Delete_Transaction_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(delResponse.Error == null);

        var getByIdResult = databaseTransactionsProvider.GetById("To_Delete_Transaction_01");

        Assert.True(getByIdResult == null);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidId()
    {
        var response = await SimulateOperationToTestCall(new DeleteTransactionInput
        {
            Id = "invalid_id",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.InvalidId.Code);
    }
}

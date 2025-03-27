using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Transactions;

public class AddTransactionTests : OperationTest<AddTransactionOperation, AddTransactionInput, VoidOperationOutput>
{
    private IDatabasePlasticsProvider databasePlasticsProvider { get; set; }
    private IDatabaseTransactionsProvider databaseTransactionsProvider { get; set; }

    public AddTransactionTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new AddTransactionOperation(_testBuilder.ApplicationContextMock!, string.Empty);

        databaseTransactionsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseTransactionsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success_ForAccount()
    {
        var addResponse = await SimulateOperationToTestCall(new AddTransactionInput
        {
            Transaction = new TransactionDto
            {
                Id = "Test01",
                TransactionDate = new DateTime(2025, 02, 10),
                Description = "Test transfer",
                Amount = 100.35M,
                Fees = 1.25M,
                Urgent = true,
                SourceAccount = "Permanent_Current_01",
                DestinationName = "Eletricity Company",
                Role = Contracts.Enums.TransactionRole.Receiver,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);

        var getByIdResult = databaseTransactionsProvider.GetById("Test01");

        Assert.True(getByIdResult != null);
    }

    [Fact]
    public async Task ShouldBe_Success_ForCard()
    {
        var addResponse = await SimulateOperationToTestCall(new AddTransactionInput
        {
            Transaction = new TransactionDto
            {
                Id = "Test02",
                TransactionDate = new DateTime(2025, 02, 10),
                Description = "Test transfer",
                Amount = 100.35M,
                Fees = 1.25M,
                Urgent = true,
                SourceCard = "Permanent_Debit_01",
                DestinationName = "Eletricity Company",
                Role = Contracts.Enums.TransactionRole.Sender,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(addResponse.Error == null);

        var getByIdResult = databaseTransactionsProvider.GetById("Test02");

        Assert.True(getByIdResult != null);
    }

    [Fact]
    public async Task ShouldReturnError_IdAlreadyInUse()
    {
        var response = await SimulateOperationToTestCall(new AddTransactionInput
        {
            Transaction = new TransactionDto
            {
                Id = "Permanent_Transaction_01",
                TransactionDate = new DateTime(2025, 02, 10),
                Description = "Test transfer",
                Amount = 100.35M,
                Fees = 1.25M,
                Urgent = true,
                SourceCard = "Permanent_Debit_01",
                DestinationName = "Eletricity Company",
                Role = Contracts.Enums.TransactionRole.Sender,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == GenericErrors.IdAlreadyInUse.Code);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidSourceAccount()
    { 
        var response = await SimulateOperationToTestCall(new AddTransactionInput
        {
            Transaction = new TransactionDto
            {
                Id = "Test03",
                TransactionDate = new DateTime(2025, 02, 10),
                Description = "Test transfer",
                Amount = 100.35M,
                Fees = 1.25M,
                Urgent = true,
                SourceAccount = "Invalid_Account",
                DestinationName = "Eletricity Company",
                Role = Contracts.Enums.TransactionRole.Sender,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == TransactionsErrors.InvalidSourceAccount.Code);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidSourceCard()
    {
        var response = await SimulateOperationToTestCall(new AddTransactionInput
        {
            Transaction = new TransactionDto
            {
                Id = "Test03",
                TransactionDate = new DateTime(2025, 02, 10),
                Description = "Test transfer",
                Amount = 100.35M,
                Fees = 1.25M,
                Urgent = true,
                SourceCard = "Invalid_Card",
                DestinationName = "Eletricity Company",
                Role = Contracts.Enums.TransactionRole.Sender,
            },
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == TransactionsErrors.InvalidSourceCard.Code);
    }
}

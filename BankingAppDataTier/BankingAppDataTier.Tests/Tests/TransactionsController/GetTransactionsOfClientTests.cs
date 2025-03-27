using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Operations;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Transactions;

public class GetTransactionsOfClientTests : OperationTest<GetTransactionsOfClientOperation, GetTransactionsOfClientInput, GetTransactionsOfClientOutput>
{
    private IDatabasePlasticsProvider databasePlasticsProvider { get; set; }
    private IDatabaseTransactionsProvider databaseTransactionsProvider { get; set; }

    public GetTransactionsOfClientTests(BankingAppDataTierTestsBuilder _testBuilder) : base(_testBuilder)
    {
        OperationToTest = new GetTransactionsOfClientOperation(_testBuilder.ApplicationContextMock!, string.Empty);

        databaseTransactionsProvider = TestsBuilder.ApplicationContextMock!.GetDependency<IDatabaseTransactionsProvider>()!;
    }

    [Fact]
    public async Task ShouldBe_Success()
    {
        var response = await SimulateOperationToTestCall(new GetTransactionsOfClientInput
        {
            Client = "Permanent_Client_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Transactions.Count > 0);
    }

    [Theory]
    [InlineData("Permanent_Current_01", null)]
    [InlineData("Permanent_Current_01", "Permanent_Current_02")]
    [InlineData("Permanent_Current_01", "Permanent_Current_03")]
    public async Task ShouldFilter_ByAccounts(string? account1, string? account2)
    {
        var accountsList = new List<string>
        {
            account1,
            account2,
        };

        accountsList = accountsList.Where(a => a != null).ToList();

        var response = await SimulateOperationToTestCall(new GetTransactionsOfClientInput
        {
            Client = "Permanent_Client_01",
            Accounts = accountsList,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Transactions.Count > 0);
        Assert.True(response.Transactions.Find(t =>
        {
            var hasSourceAccount = accountsList.Contains(t.SourceAccount);
            var hasDestionationAccount = accountsList.Contains(t.DestinationAccount);

            return !hasSourceAccount && !hasDestionationAccount;
        }) == null);
    }

    [Theory]
    [InlineData("Permanent_Debit_01", null)]
    [InlineData("Permanent_Debit_01", "Permanent_Debit_02")]
    [InlineData("Permanent_Debit_01", "Permanent_Debit_03")]
    public async Task ShouldFilter_ByCards(string? card1, string? card2)
    {
        var cardsList = new List<string>
        {
            card1,
            card2,
        };

        cardsList = cardsList.Where(a => a != null).ToList();

        var response = await SimulateOperationToTestCall(new GetTransactionsOfClientInput
        {
            Client = "Permanent_Client_01",
            Cards = cardsList,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Transactions.Count > 0);
        Assert.True(response.Transactions.Find(t => !cardsList.Contains(t.SourceCard)) == null);
    }

    [Theory]
    [InlineData("Permanent_Current_01", "Permanent_Debit_01")]
    [InlineData("Permanent_Current_01", "Permanent_Debit_02")]
    public async Task ShouldFilter_ByCardsAndAccounts(string account, string card)
    {
        var cardsList = new List<string> { card };
        var accountsList = new List<string> { account };

        var response = await SimulateOperationToTestCall(new GetTransactionsOfClientInput
        {
            Client = "Permanent_Client_01",
            Accounts = accountsList,
            Cards = cardsList,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Transactions.Count > 0);
        Assert.True(response.Transactions.Find(t =>
        {
            var isFromCard = cardsList.Contains(t.SourceCard);
            var isFromAccount = accountsList.Contains(t.SourceAccount) || accountsList.Contains(t.DestinationAccount);

            return !isFromCard && !isFromAccount;
        }) == null);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(TransactionRole.None)]
    [InlineData(TransactionRole.Sender)]
    [InlineData(TransactionRole.Receiver)]
    public async Task ShouldFilter_ByRoles(TransactionRole? role)
    {
        var response = await SimulateOperationToTestCall(new GetTransactionsOfClientInput
        {
            Client = "Permanent_Client_01",
            Role = role,
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Transactions.Count > 0);
    }

    [Fact]
    public async Task ShouldReturnError_InvalidClient()
    {
        var response = await SimulateOperationToTestCall(new GetTransactionsOfClientInput
        {
            Client = "invalid_client",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == TransactionsErrors.InvalidClientId.Code);
    }

    [Fact]
    public async Task ShouldReturnError_NoAccountsFound()
    {
        var response = await SimulateOperationToTestCall(new GetTransactionsOfClientInput
        {
            Client = "To_Edit_Client_01",
            Metadata = TestsConstants.TestsMetadata,
        });

        Assert.True(response.Error?.Code == TransactionsErrors.NoAccountsFound.Code);
    }
}

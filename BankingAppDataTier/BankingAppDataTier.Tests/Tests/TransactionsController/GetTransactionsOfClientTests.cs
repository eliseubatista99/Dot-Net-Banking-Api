//using BankingAppDataTier.Contracts.Dtos.Outputs.Transactions;
//using BankingAppDataTier.Contracts.Dtos.Inputs.Transactions;
//using BankingAppDataTier.Contracts.Errors;
//using BankingAppDataTier.Controllers;
//using BankingAppDataTier.Tests.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using BankingAppDataTier.Contracts.Enums;

//namespace BankingAppDataTier.Tests.Transactions;

//public class GetTransactionsOfClientTests
//{
//    private TransactionsController _transactionsController;

//    private void Setup()
//    {
//        TestMocksBuilder.Mock();
//        _transactionsController = TestMocksBuilder._TransactionsControllerMock;
//    }

//    [Fact]
//    public void ShouldBe_Success()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.GetTransactionsOfClient(new GetTransactionsOfClientInput
//        {
//            Client = "Permanent_Client_01",
//        }).Result!;

//        var response = (GetTransactionsOfClientOutput)result.Value!;

//        Assert.True(response.Transactions.Count > 0);
//    }

//    [Theory]
//    [InlineData("Permanent_Current_01", null)]
//    [InlineData("Permanent_Current_01", "Permanent_Current_02")]
//    [InlineData("Permanent_Current_01", "Permanent_Current_03")]
//    public void ShouldFilter_ByAccounts(string? account1, string? account2)
//    {
//        Setup();

//        var accountsList = new List<string>
//        {
//            account1,
//            account2,
//        };

//        accountsList = accountsList.Where(a => a != null).ToList();

//        var result = (ObjectResult)_transactionsController.GetTransactionsOfClient(new GetTransactionsOfClientInput
//        {
//            Client = "Permanent_Client_01",
//            Accounts = accountsList,
//        }).Result!;

//        var response = (GetTransactionsOfClientOutput)result.Value!;

//        Assert.True(response.Transactions.Count > 0);
//        Assert.True(response.Transactions.Find(t =>
//        {
//            var hasSourceAccount = accountsList.Contains(t.SourceAccount);
//            var hasDestionationAccount = accountsList.Contains(t.DestinationAccount);

//            return !hasSourceAccount && !hasDestionationAccount;
//        }) == null);
//    }

//    [Theory]
//    [InlineData("Permanent_Debit_01", null)]
//    [InlineData("Permanent_Debit_01", "Permanent_Debit_02")]
//    [InlineData("Permanent_Debit_01", "Permanent_Debit_03")]
//    public void ShouldFilter_ByCards(string? card1, string? card2)
//    {
//        Setup();

//        var cardsList = new List<string>
//        {
//            card1,
//            card2,
//        };

//        cardsList = cardsList.Where(a => a != null).ToList();

//        var result = (ObjectResult)_transactionsController.GetTransactionsOfClient(new GetTransactionsOfClientInput
//        {
//            Client = "Permanent_Client_01",
//            Cards = cardsList,
//        }).Result!;

//        var response = (GetTransactionsOfClientOutput)result.Value!;

//        Assert.True(response.Transactions.Count > 0);
//        Assert.True(response.Transactions.Find(t => !cardsList.Contains(t.SourceCard)) == null);
//    }

//    [Theory]
//    [InlineData("Permanent_Current_01", "Permanent_Debit_01")]
//    [InlineData("Permanent_Current_01", "Permanent_Debit_02")]
//    public void ShouldFilter_ByCardsAndAccounts(string account, string card)
//    {
//        Setup();
//        var cardsList = new List<string> { card };
//        var accountsList = new List<string> { account };

//        var result = (ObjectResult)_transactionsController.GetTransactionsOfClient(new GetTransactionsOfClientInput
//        {
//            Client = "Permanent_Client_01",
//            Accounts = accountsList,
//            Cards = cardsList,
//        }).Result!;

//        var response = (GetTransactionsOfClientOutput)result.Value!;

//        Assert.True(response.Transactions.Count > 0);
//        Assert.True(response.Transactions.Find(t =>
//        {
//            var isFromCard = cardsList.Contains(t.SourceCard);
//            var isFromAccount = accountsList.Contains(t.SourceAccount) || accountsList.Contains(t.DestinationAccount);

//            return !isFromCard && !isFromAccount;
//        }) == null);
//    }

//    [Theory]
//    [InlineData(null)]
//    [InlineData(TransactionRole.None)]
//    [InlineData(TransactionRole.Sender)]
//    [InlineData(TransactionRole.Receiver)]
//    public void ShouldFilter_ByRoles(TransactionRole? role)
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.GetTransactionsOfClient(new GetTransactionsOfClientInput
//        {
//            Client = "Permanent_Client_01",
//            Role = role,
//        }).Result!;

//        var response = (GetTransactionsOfClientOutput)result.Value!;

//        Assert.True(response.Transactions.Count > 0);
//    }

//    [Fact]
//    public void ShouldReturnError_InvalidClient()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.GetTransactionsOfClient(new GetTransactionsOfClientInput
//        {
//            Client = "invalid_client",
//        }).Result!;

//        var response = (GetTransactionsOfClientOutput)result.Value!;

//        Assert.True(response.Error?.Code == TransactionsErrors.InvalidClientId.Code);
//    }

//    [Fact]
//    public void ShouldReturnError_NoAccountsFound()
//    {
//        Setup();

//        var result = (ObjectResult)_transactionsController.GetTransactionsOfClient(new GetTransactionsOfClientInput
//        {
//            Client = "To_Edit_Client_01",
//        }).Result!;

//        var response = (GetTransactionsOfClientOutput)result.Value!;

//        Assert.True(response.Error?.Code == TransactionsErrors.NoAccountsFound.Code);
//    }
//}

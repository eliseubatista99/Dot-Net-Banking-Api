using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Transactions;
using BankingAppDataTier.Contracts.Dtos.Outputs.Transactions;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations.Transactions
{
    public class GetTransactionsOfClientOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<GetTransactionsOfClientInput, GetTransactionsOfClientOutput>(context, endpoint)
    {
        private IDatabaseTransactionsProvider databaseTransactionsProvider;
        private IDatabaseAccountsProvider databaseAccountsProvider;
        private IDatabaseCardsProvider databaseCardsProvider;
        private IDatabaseClientsProvider databaseClientsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseTransactionsProvider = executionContext.GetDependency<IDatabaseTransactionsProvider>()!;
            databaseAccountsProvider = executionContext.GetDependency<IDatabaseAccountsProvider>()!;
            databaseCardsProvider = executionContext.GetDependency<IDatabaseCardsProvider>()!;
            databaseClientsProvider = executionContext.GetDependency<IDatabaseClientsProvider>()!;
        }

        protected override async Task<GetTransactionsOfClientOutput> ExecuteAsync(GetTransactionsOfClientInput input)
        {
            var clientInDb = databaseClientsProvider.GetById(input.Client);

            // Filter by cards if cards were specified in input, or if nothing was specified on input
            var filterByCards = input.Cards != null || (input.Cards == null && input.Accounts == null);

            // Filter by accounts if accounts were specified in input, or if nothing was specified on input
            var filterByAccounts = input.Accounts != null || (input.Cards == null && input.Accounts == null);

            if (clientInDb == null)
            {
                return new GetTransactionsOfClientOutput()
                {
                    Transactions = new List<TransactionDto>(),
                    Error = TransactionsErrors.InvalidClientId,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var result = new List<TransactionDto>();

            var accountsInDb = this.BuildAccountListFromInput(input.Client, input.Accounts);

            if (filterByAccounts)
            {
                // If the client has no accounts, return 
                if (accountsInDb == null || accountsInDb?.Count == 0)
                {
                    return new GetTransactionsOfClientOutput()
                    {
                        Transactions = new List<TransactionDto>(),
                        Error = TransactionsErrors.NoAccountsFound,
                        StatusCode = HttpStatusCode.NoContent,
                    };
                }

                foreach (var account in accountsInDb)
                {
                    var transactions = this.GetTransactionsForAccount(account.AccountId);

                    if (input.Role != null && input.Role != TransactionRole.None)
                    {
                        transactions = transactions.Where(t => t.Role == input.Role).ToList();
                    }

                    result.AddRange(transactions);
                }
            }

            if (filterByCards)
            {
                var cardsInDb = this.BuildCardsListFromInput(accountsInDb, input.Cards);

                foreach (var card in cardsInDb)
                {
                    var transactions = this.GetTransactionsForCard(card.Id);

                    if (input.Role != null && input.Role != TransactionRole.None)
                    {
                        transactions = transactions.Where(t => t.Role == input.Role).ToList();
                    }

                    result.AddRange(transactions);
                }

            }

            return new GetTransactionsOfClientOutput()
            {
                Transactions = result,
            };
        }

        private List<TransactionDto> GetTransactionsForAccount(string account)
        {
            var result = new List<TransactionDto>();

            var receivedTransactionsInDb = databaseTransactionsProvider.GetByDestinationAccount(account);
            var receivedTransactions = receivedTransactionsInDb.Select(t =>
            {
                var trans = mapperProvider.Map<TransactionTableEntry, TransactionDto>(t);
                trans.Role = TransactionRole.Receiver;

                return trans;
            });

            var sentTransactionsInDb = databaseTransactionsProvider.GetBySourceAccount(account);
            var sentTransactions = sentTransactionsInDb.Select(t =>
            {
                var trans = mapperProvider.Map<TransactionTableEntry, TransactionDto>(t);
                trans.Role = TransactionRole.Sender;

                return trans;
            });

            result.AddRange(receivedTransactions);
            result.AddRange(sentTransactions);

            return result;
        }

        private List<AccountsTableEntry> BuildAccountListFromInput(string clientId, List<string>? inputAccounts)
        {
            var accountsInDb = databaseAccountsProvider.GetAccountsOfClient(clientId);

            // If the client has no accounts, return 
            if (accountsInDb == null || accountsInDb?.Count == 0)
            {
                return new List<AccountsTableEntry>();
            }

            // If no accounts were specified in the input, return accounts with no filter
            if (inputAccounts == null || inputAccounts?.Count == 0)
            {
                return accountsInDb;
            }

            return accountsInDb.Where(a => inputAccounts.Contains(a.AccountId)).ToList();
        }

        private List<TransactionDto> GetTransactionsForCard(string card)
        {
            var sentTransactionsInDb = databaseTransactionsProvider.GetBySourceCard(card);
            return sentTransactionsInDb.Select(t =>
            {
                var trans = mapperProvider.Map<TransactionTableEntry, TransactionDto>(t);
                trans.Role = TransactionRole.Sender;

                return trans;
            }).ToList();
        }

        private List<CardsTableEntry> BuildCardsListFromInput(List<AccountsTableEntry> accounts, List<string>? inputCards)
        {
            var result = new List<CardsTableEntry>();

            foreach (var account in accounts)
            {
                var cardsInDb = databaseCardsProvider.GetCardsOfAccount(account.AccountId);

                if (cardsInDb == null || cardsInDb?.Count == 0)
                {
                    continue;
                }

                result.AddRange(cardsInDb);
            }

            // If no cards were specified in the input, return cards with no filter
            if (inputCards == null || inputCards?.Count == 0)
            {
                return result;
            }


            return result.Where(c => inputCards.Contains(c.Id)).ToList();
        }
    }
}

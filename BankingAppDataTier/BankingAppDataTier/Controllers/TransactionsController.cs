﻿using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Transactions;
using BankingAppDataTier.Contracts.Dtos.Outputs.Transactions;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace BankingAppDataTier.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ILogger logger;
        private readonly IMapperProvider mapperProvider;
        private readonly IDatabaseTransactionsProvider databaseTransactionsProvider;
        private readonly IDatabaseClientsProvider databaseClientsProvider;
        private readonly IDatabaseAccountsProvider databaseAccountsProvider;
        private readonly IDatabaseCardsProvider databaseCardsProvider;

        public TransactionsController(IApplicationContext _executionContext)
        {
            logger = _executionContext.GetDependency<ILogger>()!;
            mapperProvider = _executionContext.GetDependency<IMapperProvider>()!;
            databaseTransactionsProvider = _executionContext.GetDependency<IDatabaseTransactionsProvider>()!;
            databaseClientsProvider = _executionContext.GetDependency<IDatabaseClientsProvider>()!;
            databaseAccountsProvider = _executionContext.GetDependency<IDatabaseAccountsProvider>()!;
            databaseCardsProvider = _executionContext.GetDependency<IDatabaseCardsProvider>()!;
        }

        [HttpGet("GetTransactionById/{id}")]
        public ActionResult<GetTransactionByIdOutput> GetTransactionById([FromBody] GetTransactionByIdInput input)
        {
            var itemInDb = databaseTransactionsProvider.GetById(input.Id);

            if (itemInDb == null)
            {
                return NotFound(new GetTransactionByIdOutput()
                {
                    Transaction = null,
                    Error = GenericErrors.InvalidId,
                });
            }

            return Ok(new GetTransactionByIdOutput()
            {
                Transaction = mapperProvider.Map<TransactionTableEntry, TransactionDto>(itemInDb),
            });
        }


        [HttpGet("GetTransactionsOfClient/{client}")]
        public ActionResult<GetTransactionsOfClientOutput> GetTransactionsOfClient([FromBody] GetTransactionsOfClientInput input)
        {
            var clientInDb = databaseClientsProvider.GetById(input.Client);

            // Filter by cards if cards were specified in input, or if nothing was specified on input
            var filterByCards = input.Cards != null || (input.Cards == null && input.Accounts == null);

            // Filter by accounts if accounts were specified in input, or if nothing was specified on input
            var filterByAccounts = input.Accounts != null || (input.Cards == null && input.Accounts == null);

            if (clientInDb == null)
            {
                return NotFound(new GetTransactionsOfClientOutput()
                {
                    Transactions = new List<TransactionDto>(),
                    Error = TransactionsErrors.InvalidClientId,
                });
            }

            var result = new List<TransactionDto>();

            var accountsInDb = this.BuildAccountListFromInput(input.Client, input.Accounts);

            if (filterByAccounts)
            {
                // If the client has no accounts, return 
                if (accountsInDb == null || accountsInDb?.Count == 0)
                {
                    return NotFound(new GetTransactionsOfClientOutput()
                    {
                        Transactions = new List<TransactionDto>(),
                        Error = TransactionsErrors.NoAccountsFound,
                    });
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

            return Ok(new GetTransactionsOfClientOutput()
            {
                Transactions = result,
            });
        }

        [HttpPost("AddTransaction")]
        public ActionResult<VoidOperationOutput> AddTransaction([FromBody] AddTransactionInput input)
        {
            var transactionInDb = databaseTransactionsProvider.GetById(input.Transaction.Id);

            if (transactionInDb != null)
            {
                return BadRequest(new VoidOperationOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                });
            }

            if(input.Transaction.SourceAccount != null)
            {
                var accountInDb = databaseAccountsProvider.GetById(input.Transaction.SourceAccount);

                if (accountInDb == null)
                {
                    return BadRequest(new VoidOperationOutput()
                    {
                        Error = TransactionsErrors.InvalidSourceAccount,
                    });
                }
            }

            if (input.Transaction.SourceCard != null)
            {
                var cardInDb = databaseCardsProvider.GetById(input.Transaction.SourceCard);

                if (cardInDb == null)
                {
                    return BadRequest(new VoidOperationOutput()
                    {
                        Error = TransactionsErrors.InvalidSourceCard,
                    });
                }
            }

            var entry = mapperProvider.Map<TransactionDto, TransactionTableEntry>(input.Transaction);

            var result = databaseTransactionsProvider.Add(entry);

            if (!result)
            {
                return new InternalServerError(new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOperationOutput());
        }

        [HttpPatch("EditTransaction")]
        public ActionResult<VoidOperationOutput> EditTransaction([FromBody] EditTransactionInput input)
        {
            var entryInDb = databaseTransactionsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            entryInDb.Description = input.Description != null ? input.Description : entryInDb.Description;
            entryInDb.DestinationName = input.DestinationName != null ? input.DestinationName : entryInDb.DestinationName;

            var result = databaseTransactionsProvider.Edit(entryInDb);

            if (!result)
            {
                return new InternalServerError(new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOperationOutput());
        }

        [HttpDelete("DeleteTransaction/{id}")]
        public ActionResult<VoidOperationOutput> DeleteTransaction([FromBody] DeleteTransactionInput input)
        {
            var result = false;
            var entryInDb = databaseTransactionsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                });
            }


            result = databaseTransactionsProvider.Delete(input.Id);

            if (!result)
            {
                return new InternalServerError(new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOperationOutput());
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

            foreach(var account in accounts)
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

using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Transactions;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Transactions;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.MapperProfiles;
using Microsoft.AspNetCore.Mvc;


namespace BankingAppDataTier.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IDatabaseTransactionsProvider databaseTransactionsProvider;
        private readonly IDatabaseClientsProvider databaseClientsProvider;
        private readonly IDatabaseAccountsProvider databaseAccountsProvider;
        private readonly IDatabaseCardsProvider databaseCardsProvider;

        public TransactionsController(
            ILogger<ClientsController> _logger,
            IDatabaseTransactionsProvider _dbTransactionsProvider,
            IDatabaseClientsProvider _dbClientsProvider,
            IDatabaseAccountsProvider _dbAccountsProvider,
            IDatabaseCardsProvider _dbCardsProvider)
        {
            logger = _logger;
            databaseTransactionsProvider = _dbTransactionsProvider;
            databaseClientsProvider = _dbClientsProvider;
            databaseAccountsProvider = _dbAccountsProvider;
            databaseCardsProvider = _dbCardsProvider;
        }

        [HttpGet("GetTransactionById/{id}")]
        public ActionResult<GetTransactionByIdOutput> GetTransactionById(string id)
        {
            var itemInDb = databaseTransactionsProvider.GetById(id);

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
                Transaction = TransactionsMapperProfile.MapTableEntryToDto(itemInDb),
            });
        }


        [HttpGet("GetTransactionsOfClient/{client}")]
        public ActionResult<GetTransactionsOfClientOutput> GetTransactionsOfClient([FromBody] GetTransactionsOfClientInput input)
        {
            var clientInDb = databaseClientsProvider.GetById(input.Client);

            if (clientInDb == null)
            {
                return NotFound(new GetTransactionsOfClientOutput()
                {
                    Transactions = new List<TransactionDto>(),
                    Error = TransactionsErrors.InvalidClientId,
                });
            }

            var accountsInDb = this.BuildAccountListFromInput(input.Client, input.Accounts);

            // If the client has no accounts, return 
            if(accountsInDb == null || accountsInDb?.Count == 0)
            {
                return NotFound(new GetTransactionsOfClientOutput()
                {
                    Transactions = new List<TransactionDto>(),
                    Error = TransactionsErrors.NoAccountsFound,
                });
            }

            var result = this.GetTransactionsForAccounts(accountsInDb, input.Role);

            return Ok(new GetTransactionsOfClientOutput()
            {
                Transactions = result,
            });
        }

        [HttpGet("GetTransactionsByDate/{date}")]
        public ActionResult<GetTransactionsByDateOutput> GetTransactionsByDate([FromBody] GetTransactionsByDateInput input)
        {
            var startDate = new DateTime(input.StartDate.Year, input.StartDate.Month, input.StartDate.Day, 0,0,0);
            var endDate = new DateTime(input.EndDate.Year, input.EndDate.Month, input.EndDate.Day, 0,0,0);

            var accountsInDb = this.BuildAccountListFromInput(input.Client, input.Accounts);

            // If the client has no accounts, return 
            if (accountsInDb == null || accountsInDb?.Count == 0)
            {
                return NotFound(new GetTransactionsOfClientOutput()
                {
                    Transactions = new List<TransactionDto>(),
                    Error = TransactionsErrors.NoAccountsFound,
                });
            }

            var result = this.GetTransactionsForAccounts(accountsInDb, input.Role);

            return Ok(new GetTransactionsByDateOutput()
            {
                Transactions = result.Where(t => t.TransactionDate.Ticks >= startDate.Ticks && t.TransactionDate.Ticks <= endDate.Ticks).ToList(),
            });
        }

        [HttpGet("GetTransactionsOfSourceCard/{card}")]
        public ActionResult<GetTransactionsOfSourceCardOutput> GetTransactionsOfSourceCard(string card)
        {
            var cardInDb = databaseCardsProvider.GetById(card);

            if (cardInDb == null)
            {
                return NotFound(new GetTransactionsOfSourceCardOutput()
                {
                    Transactions = new List<TransactionDto>(),
                    Error = TransactionsErrors.NoCardsFound,
                });
            }

            List<TransactionTableEntry> transactionsInDb = databaseTransactionsProvider.GetBySourceCard(cardInDb.Id);

            var transactions = transactionsInDb.Select(t =>
            {
                var trans = TransactionsMapperProfile.MapTableEntryToDto(t);
                trans.Role = TransactionRole.Sender;

                return trans;
            }).ToList();

            return Ok(new GetTransactionsOfSourceCardOutput()
            {
                Transactions = transactions,
            });
        }

        [HttpGet("GetUrgentTransactions")]
        public ActionResult<GetUrgentTransactionsOutput> GetUrgentTransactions([FromBody] GetUrgentTransactionsInput input)
        {
            var accountsInDb = this.BuildAccountListFromInput(input.Client, input.Accounts);

            // If the client has no accounts, return 
            if (accountsInDb == null || accountsInDb?.Count == 0)
            {
                return NotFound(new GetTransactionsOfClientOutput()
                {
                    Transactions = new List<TransactionDto>(),
                    Error = TransactionsErrors.NoAccountsFound,
                });
            }

            var result = this.GetTransactionsForAccounts(accountsInDb, input.Role);

            return Ok(new GetUrgentTransactionsOutput()
            {
                Transactions = result.Where(t => t.Urgent == input.Urgent).ToList(),
            });
        }


        [HttpPost("AddTransaction")]
        public ActionResult<VoidOutput> AddTransaction([FromBody] AddTransactionInput input)
        {
            var transactionInDb = databaseTransactionsProvider.GetById(input.Transaction.Id);

            if (transactionInDb != null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                });
            }

            var entry = TransactionsMapperProfile.MapDtoToTableEntry(input.Transaction);

            var result = databaseTransactionsProvider.Add(entry);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpPatch("EditTransaction")]
        public ActionResult<VoidOutput> EditTransaction([FromBody] EditTransactionInput input)
        {
            var entryInDb = databaseTransactionsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            entryInDb.Description = input.Description != null ? input.Description : entryInDb.Description;
            entryInDb.DestinationName = input.DestinationName != null ? input.DestinationName : entryInDb.DestinationName;

            var result = databaseTransactionsProvider.Edit(entryInDb);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpDelete("DeleteTransaction/{id}")]
        public ActionResult<VoidOutput> DeleteTransaction(string id)
        {
            var result = false;
            var entryInDb = databaseTransactionsProvider.GetById(id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId,
                });
            }


            result = databaseTransactionsProvider.Delete(id);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        private List<TransactionDto> GetTransactionsForAccounts(List<AccountsTableEntry>? accounts, TransactionRole? role = TransactionRole.None)
        {
            if(accounts == null || accounts.Count == 0)
            {
                return new List<TransactionDto>();
            }

            var result = new List<TransactionDto>();

            foreach (var account in accounts)
            {
                var transactions = this.GetTransactionsForAccount(account.AccountId);

                if (role != TransactionRole.None)
                {
                    transactions = transactions.Where(t => t.Role == role).ToList();
                }

                result.AddRange(transactions);
            }

            return result;
        }

        private List<TransactionDto> GetTransactionsForAccount(string account)
        {
            var result = new List<TransactionDto>();

            var receivedTransactionsInDb = databaseTransactionsProvider.GetByDestinationAccount(account);
            var receivedTransactions = receivedTransactionsInDb.Select(t =>
            {
                var trans = TransactionsMapperProfile.MapTableEntryToDto(t);
                trans.Role = TransactionRole.Receiver;

                return trans;
            });

            var sentTransactionsInDb = databaseTransactionsProvider.GetBySourceAccount(account);
            var sentTransactions = sentTransactionsInDb.Select(t =>
            {
                var trans = TransactionsMapperProfile.MapTableEntryToDto(t);
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
    }
}

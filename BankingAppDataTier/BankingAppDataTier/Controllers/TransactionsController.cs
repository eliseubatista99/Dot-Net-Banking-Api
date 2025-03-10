using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
using BankingAppDataTier.Contracts.Dtos.Inputs.Transactions;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
using BankingAppDataTier.Contracts.Dtos.Outputs.Transactions;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.MapperProfiles;
using BankingAppDataTier.Providers;
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

        public TransactionsController(
            ILogger<ClientsController> _logger,
            IDatabaseTransactionsProvider _dbTransactionsProvider,
            IDatabaseClientsProvider _dbClientsProvider,
            IDatabaseAccountsProvider _dbAccountsProvider)
        {
            logger = _logger;
            databaseTransactionsProvider = _dbTransactionsProvider;
            databaseClientsProvider = _dbClientsProvider;
            databaseAccountsProvider = _dbAccountsProvider;
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
        public ActionResult<GetTransactionsOfClientOutput> GetTransactionsOfClient(string client)
        {
            return Ok(new GetTransactionsOfClientOutput()
            {
                Transactions = new List<TransactionDto>(),
            });
        }

        [HttpGet("GetTransactionsOfSourceAccount/{account}")]
        public ActionResult<GetTransactionsOfSourceAccountOutput> GetTransactionsOfSourceAccount(string account)
        {
            return Ok(new GetTransactionsOfSourceAccountOutput()
            {
                Transactions = new List<TransactionDto>(),
            });
        }

        [HttpGet("GetTransactionsForDestinationAccount/{account}")]
        public ActionResult<GetTransactionsForDestinationAccountOutput> GetTransactionsForDestinationAccount(string account)
        {
            return Ok(new GetTransactionsForDestinationAccountOutput()
            {
                Transactions = new List<TransactionDto>(),
            });
        }

        [HttpGet("GetTransactionsByDate/{account}")]
        public ActionResult<GetTransactionsByDateOutput> GetTransactionsByDate(string account)
        {
            return Ok(new GetTransactionsByDateOutput()
            {
                Transactions = new List<TransactionDto>(),
            });
        }

        [HttpGet("GetTransactionsOfSourceCard/{card}")]
        public ActionResult<GetTransactionsOfSourceCardOutput> GetTransactionsOfSourceCard(string card)
        {
            return Ok(new GetTransactionsOfSourceCardOutput()
            {
                Transactions = new List<TransactionDto>(),
            });
        }

        [HttpGet("GetUrgentTransactions")]
        public ActionResult<GetUrgentTransactionsOutput> GetUrgentTransactions()
        {
            return Ok(new GetUrgentTransactionsOutput()
            {
                Transactions = new List<TransactionDto>(),
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

    }
}

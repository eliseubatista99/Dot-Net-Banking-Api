using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
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
    public class LoansController : Controller
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IDatabaseLoansProvider databaseLoansProvider;
        private readonly IDatabaseAccountsProvider databaseAccountsProvider;

        public LoansController(ILogger<ClientsController> _logger, IDatabaseLoansProvider _dbLoansProvider, IDatabaseAccountsProvider _dbAccountsProvider)
        {
            logger = _logger;
            databaseLoansProvider = _dbLoansProvider;
            databaseAccountsProvider = _dbAccountsProvider;
        }

        [HttpGet("GetLoansOfAccount/{account}")]
        public ActionResult<GetLoansOfAccountOutput> GetLoansOfAccount(string account)
        {
            var result = new List<LoanDto>();

            var itemsInDb = databaseLoansProvider.GetByAccount(account);

            if (itemsInDb == null || itemsInDb.Count == 0)
            {
                return Ok(new GetLoansOfAccountOutput()
                {
                    Loans = new List<LoanDto>(),
                });
            }

            result = itemsInDb.Select(i => LoansMapperProfile.MapTableEntryToDto(i)).ToList();

            return Ok(new GetLoansOfAccountOutput()
            {
                Loans = result,
            });
        }

        [HttpGet("GetLoansOfClient/{client}")]
        public ActionResult<GetLoansOfClientOutput> GetLoansOfClient(string client)
        {
            var clientAccounts = databaseAccountsProvider.GetAccountsOfClient(client);

            if (clientAccounts == null || clientAccounts.Count == 0)
            {
                return Ok(new GetLoansOfClientOutput()
                {
                    Loans = new List<LoanDto>(),
                });
            }

            var result = new List<LoanDto>();

            foreach(var account in clientAccounts)
            {
                var itemsInDb = databaseLoansProvider.GetByAccount(account.AccountId);

                var loansOfAccount = itemsInDb.Select(i => LoansMapperProfile.MapTableEntryToDto(i)).ToList();

                result.AddRange(loansOfAccount);
            }

            return Ok(new GetLoansOfClientOutput()
            {
                Loans = result,
            });
        }

        [HttpGet("GetLoanById/{id}")]
        public ActionResult<GetLoanByIdOutput> GetLoanById(string id)
        {
            var itemInDb = databaseLoansProvider.GetById(id);

            if (itemInDb == null)
            {
                return NotFound(new GetLoanByIdOutput()
                {
                    Loan = null,
                    Error = GenericErrors.InvalidId,
                });
            }

            return Ok(new GetLoanByIdOutput()
            {
                Loan = LoansMapperProfile.MapTableEntryToDto(itemInDb),
            });
        }

        [HttpPost("AddLoan")]
        public ActionResult<VoidOutput> AddLoan([FromBody] AddLoanInput input)
        {
            var itemInDb = databaseLoansProvider.GetById(input.Loan.Id);

            if (itemInDb != null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                });
            }

            var entry = LoansMapperProfile.MapDtoToTableEntry(input.Loan);

            var result = databaseLoansProvider.Add(entry);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpPatch("EditLoan")]
        public ActionResult<VoidOutput> EditLoan([FromBody] EditLoanInput input)
        {
            var entryInDb = databaseLoansProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            entryInDb.StartDate = input.StartDate != null ? input.StartDate.GetValueOrDefault() : entryInDb.StartDate;
            entryInDb.RelatedAccount = input.RelatedAccount != null ? input.RelatedAccount : entryInDb.RelatedAccount;
            entryInDb.RelatedOffer = input.RelatedOffer != null ? input.RelatedOffer : entryInDb.RelatedOffer;
            entryInDb.Duration = input.Duration != null ? input.Duration.GetValueOrDefault() : entryInDb.Duration;
            entryInDb.Amount = input.Amount != null ? input.Amount.GetValueOrDefault() : entryInDb.Amount;

            var result = databaseLoansProvider.Edit(entryInDb);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpDelete("DeleteLoan/{id}")]
        public ActionResult<VoidOutput> DeleteLoan(string id)
        {
            var result = false;
            var entryInDb = databaseLoansProvider.GetById(id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId,
                });
            }

            result = databaseLoansProvider.Delete(id);

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

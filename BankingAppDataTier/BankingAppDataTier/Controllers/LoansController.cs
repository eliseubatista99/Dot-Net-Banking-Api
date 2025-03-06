using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Loans;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Loans;
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
        private readonly IDatabaseLoanOfferProvider databaseLoanOffersProvider;
        private readonly IDatabaseAccountsProvider databaseAccountsProvider;

        public LoansController(ILogger<ClientsController> _logger, IDatabaseLoansProvider _dbLoansProvider, 
            IDatabaseAccountsProvider _dbAccountsProvider, IDatabaseLoanOfferProvider _dbLoanOffersProvider)
        {
            logger = _logger;
            databaseLoansProvider = _dbLoansProvider;
            databaseAccountsProvider = _dbAccountsProvider;
            databaseLoanOffersProvider = _dbLoanOffersProvider;
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

            result = itemsInDb.Select(i => this.BuildLoanDto(i)).ToList();

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

                var loansOfAccount = itemsInDb.Select(i => this.BuildLoanDto(i)).ToList();

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
                Loan = this.BuildLoanDto(itemInDb),
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

            var relatedOffer = databaseLoanOffersProvider.GetById(input.Loan.RelatedOffer);

            if (relatedOffer == null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = LoansErrors.InvalidRelatedOffer,
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

            if(input.RelatedOffer != null)
            {
                var relatedOffer = databaseLoanOffersProvider.GetById(input.RelatedOffer);

                if (relatedOffer == null)
                {
                    return BadRequest(new VoidOutput()
                    {
                        Error = LoansErrors.InvalidRelatedOffer,
                    });
                }

                if (relatedOffer.LoanType != entryInDb.RelatedOffer)
                {
                    return BadRequest(new VoidOutput()
                    {
                        Error = LoansErrors.CantChangeLoanType,
                    });
                }
            }

            if (input.RelatedAccount != null)
            {
                var relatedAccount = databaseAccountsProvider.GetById(input.RelatedAccount);

                if (relatedAccount == null)
                {
                    return BadRequest(new VoidOutput()
                    {
                        Error = LoansErrors.InvalidRelatedAccount,
                    });
                }
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

        private LoanDto BuildLoanDto(LoanTableEntry entry)
        {
            var loan = LoansMapperProfile.MapTableEntryToDto(entry);
            var offerData = databaseLoanOffersProvider.GetById(loan.RelatedOffer);

            if (offerData == null)
            {
                return loan;
            }

            loan.LoanType = EnumsMapperProfile.MapLoanTypeFromString(offerData.LoanType);
            loan.Interest = offerData.Interest;

            return loan;
        }

    }
}

using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BankingAppDataTier.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoanOffersController : Controller
    {
        private readonly ILogger logger;
        private readonly IMapperProvider mapperProvider;
        private readonly IDatabaseLoansProvider databaseLoansProvider;
        private readonly IDatabaseLoanOfferProvider databaseLoanOffersProvider;

        public LoanOffersController(IExecutionContext _executionContext)
        {
            logger = _executionContext.GetDependency<ILogger>()!;
            mapperProvider = _executionContext.GetDependency<IMapperProvider>()!;
            databaseLoansProvider = _executionContext.GetDependency<IDatabaseLoansProvider>()!;
            databaseLoanOffersProvider = _executionContext.GetDependency<IDatabaseLoanOfferProvider>()!;
        }

        [HttpGet("GetLoanOfferByType/{loanType}&{includeInactive}")]
        public ActionResult<GetLoanOffersByTypeOutput> GetLoanOfferByType(LoanType loanType, bool includeInactive = false)
        {
            var result = new List<LoanOfferDto>();

            var typeAsString = mapperProvider.Map<LoanType, string>(loanType);

            var loanOffersInDb = databaseLoanOffersProvider.GetByType(typeAsString, includeInactive != true);

            if (loanOffersInDb == null || loanOffersInDb.Count == 0)
            {
                return Ok(new GetLoanOffersByTypeOutput()
                {
                    LoanOffers = new List<LoanOfferDto>(),
                });
            }

            result = loanOffersInDb.Select(i => mapperProvider.Map<LoanOfferTableEntry, LoanOfferDto>(i)).ToList();

            return Ok(new GetLoanOffersByTypeOutput()
            {
                LoanOffers = result,
            });
        }

        [HttpGet("GetLoanOfferById/{id}")]
        public ActionResult<GetLoanOfferByIdOutput> GetLoanOfferById(string id)
        {
            var itemInDb = databaseLoanOffersProvider.GetById(id);

            if (itemInDb == null)
            {
                return NotFound(new GetLoanOfferByIdOutput()
                {
                    LoanOffer = null,
                    Error = GenericErrors.InvalidId,
                });
            }

            return Ok(new GetLoanOfferByIdOutput()
            {
                LoanOffer = mapperProvider.Map<LoanOfferTableEntry, LoanOfferDto>(itemInDb)
            });
        }

        [HttpPost("AddLoanOffer")]
        public ActionResult<VoidOutput> AddLoanOffer([FromBody] AddLoanOfferInput input)
        {
            var itemInDb = databaseLoanOffersProvider.GetById(input.LoanOffer.Id);

            if (itemInDb != null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                });
            }

            var entry = mapperProvider.Map<LoanOfferDto, LoanOfferTableEntry>(input.LoanOffer);


            var result = databaseLoanOffersProvider.Add(entry);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpPatch("EditLoanOffer")]
        public ActionResult<VoidOutput> EditLoanOffer([FromBody] EditLoanOfferInput input)
        {
            var entryInDb = databaseLoanOffersProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            entryInDb.Name = input.Name != null ? input.Name : entryInDb.Name;
            entryInDb.Description = input.Description != null ? input.Description : entryInDb.Description;
            entryInDb.MaxEffort = input.MaxEffort != null ? input.MaxEffort.GetValueOrDefault() : entryInDb.MaxEffort;
            entryInDb.Interest = input.Interest != null ? input.Interest.GetValueOrDefault() : entryInDb.Interest;

            var result = databaseLoanOffersProvider.Edit(entryInDb);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpPatch("ActivateOrDeactivatePlastic")]
        public ActionResult<VoidOutput> ActivateOrDeactivateLoanOffer([FromBody] ActivateOrDeactivateLoanOfferInput input)
        {
            var entryInDb = databaseLoanOffersProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            if (input.Active == entryInDb.IsActive)
            {
                return Ok(new VoidOutput());
            }

            entryInDb.IsActive = input.Active;

            var result = databaseLoanOffersProvider.Edit(entryInDb);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }


        [HttpDelete("DeleteLoanOffer/{id}")]
        public ActionResult<VoidOutput> DeleteLoanOffer(string id)
        {
            var result = false;
            var entryInDb = databaseLoanOffersProvider.GetById(id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId,
                });
            }

            var loansWithThisOffer = databaseLoansProvider.GetByOffer(entryInDb.Id);

            if (loansWithThisOffer?.Count > 0)
            {
                return BadRequest(new VoidOutput
                {
                    Error = LoanOffersErrors.CantDeleteWithRelatedLoans,
                });
            }

            result = databaseLoanOffersProvider.Delete(id);

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

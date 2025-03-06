using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer;
using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs.LoansOffers;
using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
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
    public class LoanOffersController : Controller
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IDatabaseLoanOfferProvider databaseLoanOffersProvider;

        public LoanOffersController(ILogger<ClientsController> _logger, IDatabaseLoanOfferProvider _dbLoanOffersProvider)
        {
            logger = _logger;
            databaseLoanOffersProvider = _dbLoanOffersProvider;
        }

        [HttpGet("GetLoanOfferByType/{loanType}&{includeInactive}")]
        public ActionResult<GetLoanOffersByTypeOutput> GetLoanOfferByType(LoanType loanType, bool includeInactive = false)
        {
            var result = new List<LoanOfferDto>();

            var typeAsString = EnumsMapperProfile.MapLoanTypeToString(loanType);

            var loanOffersInDb = databaseLoanOffersProvider.GetByType(typeAsString, includeInactive != true);

            if (loanOffersInDb == null || loanOffersInDb.Count == 0)
            {
                return Ok(new GetLoanOffersByTypeOutput()
                {
                    LoanOffers = new List<LoanOfferDto>(),
                });
            }

            result = loanOffersInDb.Select(i => LoanOffersMapperProfile.MapTableEntryToDto(i)).ToList();

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
                LoanOffer = LoanOffersMapperProfile.MapTableEntryToDto(itemInDb),
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

            var entry = LoanOffersMapperProfile.MapDtoToTableEntry(input.LoanOffer);

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

            entryInDb.LoanType = input.LoanType != null ? EnumsMapperProfile.MapLoanTypeToString(input.LoanType.GetValueOrDefault()) : entryInDb.LoanType;
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

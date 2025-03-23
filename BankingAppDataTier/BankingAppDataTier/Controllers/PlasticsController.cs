using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Plastics;
using BankingAppDataTier.Contracts.Dtos.Outputs.Plastics;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BankingAppDataTier.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PlasticsController : Controller
    {
        private readonly ILogger logger;
        private readonly IMapperProvider mapperProvider;
        private readonly IDatabasePlasticsProvider databasePlasticsProvider;
        private readonly IDatabaseCardsProvider databaseCardsProvider;
        public PlasticsController(IApplicationContext _executionContext)
        {
            logger = _executionContext.GetDependency<ILogger>()!;
            mapperProvider = _executionContext.GetDependency<IMapperProvider>()!;
            databasePlasticsProvider = _executionContext.GetDependency<IDatabasePlasticsProvider>()!;
            databaseCardsProvider = _executionContext.GetDependency<IDatabaseCardsProvider>()!;
        }

        [HttpGet("GetPlasticsOfType/{cardType}&{includeInactive}")]
        public ActionResult<GetPlasticsOfTypeOutput> GetPlasticsOfType(CardType cardType, bool includeInactive = false)
        {
            var result = new List<PlasticDto>();

            var typeAsString = mapperProvider.Map<CardType, string>(cardType);

            var plasticsInDb = databasePlasticsProvider.GetPlasticsOfCardType(typeAsString, includeInactive != true);

            if (plasticsInDb == null || plasticsInDb.Count == 0)
            {
                return Ok(new GetPlasticsOfTypeOutput()
                {
                    Plastics = new List<PlasticDto>(),
                });
            }

            result = plasticsInDb.Select(acc => mapperProvider.Map<PlasticTableEntry, PlasticDto>(acc)).ToList();

            return Ok(new GetPlasticsOfTypeOutput()
            {
                Plastics = result,
            });
        }

        [HttpGet("GetPlasticById/{id}")]
        public ActionResult<GetPlasticByIdOutput> GetPlasticById(string id)
        {
            var itemInDb = databasePlasticsProvider.GetById(id);

            if (itemInDb == null)
            {
                return NotFound(new GetPlasticByIdOutput()
                {
                    Plastic = null,
                    Error = GenericErrors.InvalidId,
                });
            }

            return Ok(new GetPlasticByIdOutput()
            {
                Plastic = mapperProvider.Map<PlasticTableEntry, PlasticDto>(itemInDb),
            });
        }

        [HttpPost("AddPlastic")]
        public ActionResult<VoidOperationOutput> AddPlastic([FromBody] AddPlasticInput input)
        {
            var plasticInDb = databasePlasticsProvider.GetById(input.Plastic.Id);

            if (plasticInDb != null)
            {
                return BadRequest(new VoidOperationOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                });
            }

            var entry = mapperProvider.Map<PlasticDto, PlasticTableEntry>(input.Plastic);

            var result = databasePlasticsProvider.Add(entry);

            if (!result)
            {
                return new InternalServerError(new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOperationOutput());
        }

        [HttpPatch("EditPlastic")]
        public ActionResult<VoidOperationOutput> EditPlastic([FromBody] EditPlasticInput input)
        {
            var entryInDb = databasePlasticsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            entryInDb.Name = input.Name != null ? input.Name : entryInDb.Name;
            entryInDb.Cashback = input.Cashback != null ? input.Cashback : entryInDb.Cashback;
            entryInDb.Commission = input.Commission != null ? input.Commission : entryInDb.Commission;
            entryInDb.Image = input.Image != null ? input.Image : entryInDb.Image;


            var result = databasePlasticsProvider.Edit(entryInDb);

            if (!result)
            {
                return new InternalServerError(new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOperationOutput());
        }

        [HttpPatch("ActivateOrDeactivatePlastic")]
        public ActionResult<VoidOperationOutput> ActivateOrDeactivatePlastic([FromBody] ActivateOrDeactivatePlasticInput input)
        {
            var entryInDb = databasePlasticsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            if (input.Active == entryInDb.IsActive)
            {
                return Ok(new VoidOperationOutput());
            }

            entryInDb.IsActive = input.Active;

            var result = databasePlasticsProvider.Edit(entryInDb);

            if (!result)
            {
                return new InternalServerError(new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOperationOutput());
        }


        [HttpDelete("DeletePlastic/{id}")]
        public ActionResult<VoidOperationOutput> DeletePlastic(string id)
        {
            var result = false;
            var entryInDb = databasePlasticsProvider.GetById(id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                });
            }

            var cardsWithThisPlastic = databaseCardsProvider.GetCardsWithPlastic(id);

            if (cardsWithThisPlastic != null && cardsWithThisPlastic.Count > 0)
            {
                return BadRequest(new VoidOperationOutput
                {
                    Error = PlasticsErrors.CantDeleteWithRelatedCards,
                });
            }

            result = databasePlasticsProvider.Delete(id);

            if (!result)
            {
                return new InternalServerError(new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOperationOutput());
        }

    }
}

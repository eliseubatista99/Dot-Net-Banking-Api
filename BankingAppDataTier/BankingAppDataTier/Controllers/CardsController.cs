using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Cards;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Cards;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.MapperProfiles;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Xml;


namespace BankingAppDataTier.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CardsController : Controller
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IMapperProvider mapperProvider;
        private readonly IDatabaseCardsProvider databaseCardsProvider;
        private readonly IDatabasePlasticsProvider databasePlasticsProvider;
        private readonly IDatabaseAccountsProvider databaseAccountsProvider;

        public CardsController(
            ILogger<ClientsController> _logger,
            IMapperProvider _mapper,
            IDatabaseCardsProvider _dbCardsProvider,
            IDatabasePlasticsProvider _dbPlasticsProvider,
            IDatabaseAccountsProvider _dbAccountsProvider)
        {
            logger = _logger;
            mapperProvider = _mapper;
            databaseCardsProvider = _dbCardsProvider;
            databasePlasticsProvider = _dbPlasticsProvider;
            databaseAccountsProvider = _dbAccountsProvider;
        }

        [HttpGet("GetCardsOfAccount/{account}")]
        public ActionResult<GetCardsOfAccountOutput> GetCardsOfAccount(string account)
        {
            var result = new List<CardDto>();

            var accountInDb = databaseAccountsProvider.GetById(account);

            if (accountInDb == null)
            {
                return Ok(new GetCardsOfAccountOutput()
                {
                    Cards = null,
                    Error = CardsErrors.InvalidAccount,
                });
            }
            var cardsInDb = databaseCardsProvider.GetCardsOfAccount(account);

            if (cardsInDb == null || cardsInDb.Count == 0)
            {
                return Ok(new GetCardsOfAccountOutput()
                {
                    Cards = new List<CardDto>(),
                });
            }

            result = cardsInDb.Select(acc => this.BuildCardDto(acc)).ToList();

            return Ok(new GetCardsOfAccountOutput()
            {
                Cards = result,
            });
        }

        [HttpGet("GetCardById/{id}")]
        public ActionResult<GetCardByIdOutput> GetCardById(string id)
        {
            var itemInDb = databaseCardsProvider.GetById(id);

            if (itemInDb == null)
            {
                return NotFound(new GetCardByIdOutput()
                {
                    Card = null,
                    Error = GenericErrors.InvalidId,
                });
            }

            return Ok(new GetCardByIdOutput()
            {
                Card = this.BuildCardDto(itemInDb),
            });
        }

        [HttpPost("AddCard")]
        public ActionResult<VoidOutput> AddCard([FromBody] AddCardInput input)
        {
            var cardInDb = databaseCardsProvider.GetById(input.Card.Id);

            if (cardInDb != null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                });
            }

            if(input.Card.CardType == CardType.Credit && (input.Card.PaymentDay == null || input.Card.Balance == null))
            {
                return BadRequest(new VoidOutput()
                {
                    Error = CardsErrors.MissingCreditCardDetails,
                });
            }
            else if (input.Card.CardType == CardType.PrePaid && input.Card.Balance == null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = CardsErrors.MissingPrePaidCardDetails,
                });
            }

            var relatedPlastic = databasePlasticsProvider.GetById(input.Card.PlasticId);

            if (relatedPlastic == null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = CardsErrors.InvalidPlastic,
                });
            }

            var entry = mapperProvider.Map<CardDto, CardsTableEntry>(input.Card);

            var result = databaseCardsProvider.Add(entry);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpPatch("EditCard")]
        public ActionResult<VoidOutput> EditCard([FromBody] EditCardInput input)
        {
            var entryInDb = databaseCardsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            var card = this.BuildCardDto(entryInDb);

            if (card.CardType == CardType.Credit)
            {
                entryInDb.PaymentDay = input.PaymentDay != null ? input.PaymentDay : entryInDb.PaymentDay;
                entryInDb.Balance = input.Balance != null ? input.Balance : entryInDb.Balance;
            } 
            else if(card.CardType == CardType.PrePaid)
            {
                entryInDb.Balance = input.Balance != null ? input.Balance : entryInDb.Balance;
            }



            entryInDb.Name = input.Name != null ? input.Name : entryInDb.Name;
            entryInDb.RequestDate = input.RequestDate != null ? input.RequestDate.GetValueOrDefault() : entryInDb.RequestDate;
            entryInDb.ActivationDate = input.ActivationDate != null ? input.ActivationDate : entryInDb.ActivationDate;
            entryInDb.ExpirationDate = input.ExpirationDate != null ? input.ExpirationDate.GetValueOrDefault() : entryInDb.ExpirationDate;

            var result = databaseCardsProvider.Edit(entryInDb);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpDelete("DeleteCard/{id}")]
        public ActionResult<VoidOutput> DeleteCard(string id)
        {
            var result = false;
            var entryInDb = databaseCardsProvider.GetById(id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId,
                });
            }

            result = databaseCardsProvider.Delete(id);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        private CardDto BuildCardDto(CardsTableEntry entry)
        {
            var card = mapperProvider.Map<CardsTableEntry, CardDto>(entry);

            var plasticData = databasePlasticsProvider.GetById(card.PlasticId);

            if (plasticData == null)
            {
                return card;
            }

            card.CardType = mapperProvider.Map<string, CardType>(plasticData.CardType);
            card.Image = plasticData.Image;

            return card;
        }
    }
}

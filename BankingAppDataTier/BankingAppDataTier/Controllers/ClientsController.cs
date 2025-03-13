using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.MapperProfiles;
using Microsoft.AspNetCore.Mvc;


namespace BankingAppDataTier.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IMapperProvider mapperProvider;
        private readonly IDatabaseClientsProvider databaseClientsProvider;
        private readonly IDatabaseAccountsProvider databaseAccountsProvider;

        public ClientsController(
            ILogger<ClientsController> _logger,
            IMapperProvider _mapper,
            IDatabaseClientsProvider _dbClientsProvider,
            IDatabaseAccountsProvider _dbAccountsProvider)
        {
            logger = _logger;
            mapperProvider = _mapper;
            databaseClientsProvider = _dbClientsProvider;
            databaseAccountsProvider = _dbAccountsProvider;
        }

        [HttpGet("GetClients")]
        public ActionResult<GetClientsOutput> GetClients()
        {
            //Request.Headers.TryGetValue("", out StringValues headerValue);
            List<ClientDto> result = new List<ClientDto>();

            var itemsInDb = databaseClientsProvider.GetAll();

            return Ok(new GetClientsOutput
            {
                Clients = itemsInDb.Select(client => ClientsMapperProfile.MapTableEntryToDto(client)).ToList()
            });
        }

        [HttpGet("GetClientById/{id}")]
        public ActionResult<GetClientByIdOutput> GetClientById(string id)
        {
            List<ClientDto> result = new List<ClientDto>();

            var itemInDb = databaseClientsProvider.GetById(id);

            if (itemInDb == null)
            {
                return NotFound(new GetClientByIdOutput()
                {
                    Client = null,
                    Error = GenericErrors.InvalidId,
                });
            }

            return Ok(new GetClientByIdOutput
            {
                Client = ClientsMapperProfile.MapTableEntryToDto(itemInDb),
            });
        }

        [HttpPost("HasValidPassword")]
        public ActionResult<HasValidPasswordOutput> HasValidPassword([FromBody] HasValidPasswordInput input)
        {
            List<ClientDto> result = new List<ClientDto>();

            var itemInDb = databaseClientsProvider.GetById(input.Id);

            if (itemInDb == null)
            {
                return NotFound(new HasValidPasswordOutput()
                {
                    Result = false,
                    Error = GenericErrors.InvalidId,
                });
            }

            var validPassword = input.PassWord.Equals(itemInDb.Password);

            return Ok(new HasValidPasswordOutput
            {
                Result = validPassword,
            });
        }

        [HttpPost("AddClient")]
        public ActionResult<VoidOutput> AddClient([FromBody] AddClientInput input)
        {
            var clientInDb = databaseClientsProvider.GetById(input.Client.Id);

            if (clientInDb != null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                });
            }

            var entry = ClientsMapperProfile.MapDtoToTableEntry(input.Client);
            entry.Password = input.PassWord;

            var result = databaseClientsProvider.Add(entry);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpPatch("EditClient")]
        public ActionResult<VoidOutput> EditClient([FromBody] EditClientInput input)
        {
            var entryInDb = databaseClientsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            entryInDb.Name = input.Name != null ? input.Name : entryInDb.Name;
            entryInDb.Surname = input.Surname != null ? input.Surname : entryInDb.Surname;
            entryInDb.BirthDate = input.BirthDate != null ? input.BirthDate.GetValueOrDefault() : entryInDb.BirthDate;
            entryInDb.VATNumber = input.VATNumber != null ? input.VATNumber : entryInDb.VATNumber;
            entryInDb.PhoneNumber = input.PhoneNumber != null ? input.PhoneNumber : entryInDb.PhoneNumber;
            entryInDb.Email = input.Email != null ? input.Email : entryInDb.Email;

            var result = databaseClientsProvider.Edit(entryInDb);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpPost("ChangePassword")]
        public ActionResult<VoidOutput> ChangePassword([FromBody] ChangeClientPasswordInput input)
        {
            var entryInDb = databaseClientsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return NotFound(new VoidOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            var result = databaseClientsProvider.ChangePassword(input.Id, input.PassWord);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }


        [HttpDelete("DeleteClient/{id}")]
        public ActionResult<VoidOutput> DeleteClient(string id)
        {
            var result = false;
            var entryInDb = databaseClientsProvider.GetById(id);

            if (entryInDb == null)
            {
                return NotFound(new VoidOutput
                {
                    Error = GenericErrors.InvalidId,
                });
            }

            var accountsOfClient = databaseAccountsProvider.GetAccountsOfClient(entryInDb.Id);

            if (accountsOfClient != null && accountsOfClient.Count > 0)
            {
                return NotFound(new VoidOutput
                {
                    Error = ClientsErrors.CantCloseWithActiveAccounts,
                });
            }

            result = databaseClientsProvider.Delete(id);

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

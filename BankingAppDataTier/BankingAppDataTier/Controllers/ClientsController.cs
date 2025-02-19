using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.MapperProfiles;
using BankingAppDataTier.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;


namespace BankingAppDataTier.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IDatabaseClientsProvider databaseClientsProvider;

        public ClientsController(ILogger<ClientsController> _logger, IDatabaseClientsProvider _dbClientsProvider)
        {
            logger = _logger;
            databaseClientsProvider = _dbClientsProvider;
        }

        [HttpGet("GetClients")]
        public ActionResult<GetClientsOutput> GetClients()
        {
            Request.Headers.TryGetValue("", out StringValues headerValue);
            List<ClientDto> result = new List<ClientDto>();

            var itemsInDb = databaseClientsProvider.GetAll();

            return Ok(new GetClientsOutput
            {
                Clients = itemsInDb.Select(client => ClientsMapperProfile.MapClientTableEntryToClientDto(client)).ToList()
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
                    Error = ClientsErrors.InvalidClientId,
                });
            }

            return Ok(new GetClientByIdOutput
            {
                Client = ClientsMapperProfile.MapClientTableEntryToClientDto(itemInDb),
            });
        }

        [HttpPost("AddClient")]
        public ActionResult<VoidOutput> AddClient([FromBody] AddClientInput input)
        {
            var clientInDb = databaseClientsProvider.GetById(input.Client.Id);

            if (clientInDb != null)
            {
                return BadRequest(new GetClientAccountsOutput()
                {
                    Accounts = new List<AccountDto>(),
                    Error = ClientsErrors.IdAlreadyInUse,
                });
            }

            var entry = ClientsMapperProfile.MapClientDtoToClientTableEntry(input.Client);
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
                    Error = ClientsErrors.InvalidClientId
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

        [HttpPatch("ChangePassword")]
        public ActionResult<VoidOutput> ChangePassword([FromBody] ChangeClientPasswordInput input)
        {
            var entryInDb = databaseClientsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return NotFound(new VoidOutput
                {
                    Error = ClientsErrors.InvalidClientId
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
                    Error = ClientsErrors.InvalidClientId,
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

using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Clients;
using BankingAppDataTier.Contracts.Dtos.Outputs.Clients;
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
    public class ClientsController : Controller
    {
        private readonly ILogger logger;
        private readonly IMapperProvider mapperProvider;
        private readonly IDatabaseClientsProvider databaseClientsProvider;
        private readonly IDatabaseAccountsProvider databaseAccountsProvider;

        public ClientsController(IApplicationContext _executionContext)
        {
            logger = _executionContext.GetDependency<ILogger>()!;
            mapperProvider = _executionContext.GetDependency<IMapperProvider>()!;
            databaseClientsProvider = _executionContext.GetDependency<IDatabaseClientsProvider>()!;
            databaseAccountsProvider = _executionContext.GetDependency<IDatabaseAccountsProvider>()!;
        }

        [HttpGet("GetClients")]
        public ActionResult<GetClientsOutput> GetClients()
        {
            //Request.Headers.TryGetValue("", out StringValues headerValue);
            List<ClientDto> result = new List<ClientDto>();

            var itemsInDb = databaseClientsProvider.GetAll();

            return Ok(new GetClientsOutput
            {
                Clients = itemsInDb.Select(client => mapperProvider.Map<ClientsTableEntry, ClientDto>(client)).ToList()
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
                Client = mapperProvider.Map<ClientsTableEntry, ClientDto>(itemInDb)
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
        public ActionResult<VoidOperationOutput> AddClient([FromBody] AddClientInput input)
        {
            var clientInDb = databaseClientsProvider.GetById(input.Client.Id);

            if (clientInDb != null)
            {
                return BadRequest(new VoidOperationOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                });
            }

            var entry = mapperProvider.Map<ClientDto, ClientsTableEntry>(input.Client);
            entry.Password = input.PassWord;

            var result = databaseClientsProvider.Add(entry);

            if (!result)
            {
                return new InternalServerError(new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOperationOutput());
        }

        [HttpPatch("EditClient")]
        public ActionResult<VoidOperationOutput> EditClient([FromBody] EditClientInput input)
        {
            var entryInDb = databaseClientsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOperationOutput
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
                return new InternalServerError(new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOperationOutput());
        }

        [HttpPost("ChangePassword")]
        public ActionResult<VoidOperationOutput> ChangePassword([FromBody] ChangeClientPasswordInput input)
        {
            var entryInDb = databaseClientsProvider.GetById(input.Id);

            if (entryInDb == null)
            {
                return NotFound(new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            var result = databaseClientsProvider.ChangePassword(input.Id, input.PassWord);

            if (!result)
            {
                return new InternalServerError(new VoidOperationOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOperationOutput());
        }


        [HttpDelete("DeleteClient/{id}")]
        public ActionResult<VoidOperationOutput> DeleteClient(string id)
        {
            var result = false;
            var entryInDb = databaseClientsProvider.GetById(id);

            if (entryInDb == null)
            {
                return NotFound(new VoidOperationOutput
                {
                    Error = GenericErrors.InvalidId,
                });
            }

            var accountsOfClient = databaseAccountsProvider.GetAccountsOfClient(entryInDb.Id);

            if (accountsOfClient != null && accountsOfClient.Count > 0)
            {
                return NotFound(new VoidOperationOutput
                {
                    Error = ClientsErrors.CantCloseWithActiveAccounts,
                });
            }

            result = databaseClientsProvider.Delete(id);

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

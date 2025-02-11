using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.MapperProfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;


namespace BankingAppDataTier.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : Controller
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IDatabaseProvider databaseProvider;

        public ClientsController(ILogger<ClientsController> _logger, IDatabaseProvider _dbProvider)
        {
            logger = _logger;
            databaseProvider = _dbProvider;
        }

        [HttpGet()]
        public List<ClientDto> GetClients()
        {
            Request.Headers.TryGetValue("", out StringValues headerValue);
            List<ClientDto> result = new List<ClientDto>();

            var clientsInDb = databaseProvider.GetAllClients();

            return clientsInDb.Select(client => ClientsMapperProfile.MapClientTableEntryToClientDto(client)).ToList();
        }

        [HttpGet("{id}")]
        public ClientDto GetClient(string id)
        {
            List<ClientDto> result = new List<ClientDto>();

            var clientInDb = databaseProvider.GetClientById(id);

            if(clientInDb != null)
            {
                return ClientsMapperProfile.MapClientTableEntryToClientDto(clientInDb);
            }

            return null;
        }

        [HttpPost()]
        public bool AddClient([FromBody] ClientDto client, string password)
        {
            var clientEntry = ClientsMapperProfile.MapClientDtoToClientTableEntry(client);
            clientEntry.Password = password;

            return databaseProvider.AddClient(clientEntry);
        }
    }
}

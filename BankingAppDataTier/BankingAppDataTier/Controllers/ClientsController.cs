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
        private readonly IDatabaseClientsProvider databaseClientsProvider;

        public ClientsController(ILogger<ClientsController> _logger, IDatabaseClientsProvider _dbClientsProvider)
        {
            logger = _logger;
            databaseClientsProvider = _dbClientsProvider;
        }

        [HttpGet()]
        public List<ClientDto> GetClients()
        {
            Request.Headers.TryGetValue("", out StringValues headerValue);
            List<ClientDto> result = new List<ClientDto>();

            var itemsInDb = databaseClientsProvider.GetAll();

            return itemsInDb.Select(client => ClientsMapperProfile.MapClientTableEntryToClientDto(client)).ToList();
        }

        [HttpGet("{id}")]
        public ClientDto GetClient(string id)
        {
            List<ClientDto> result = new List<ClientDto>();

            var itemInDb = databaseClientsProvider.GetById(id);

            if(itemInDb != null)
            {
                return ClientsMapperProfile.MapClientTableEntryToClientDto(itemInDb);
            }

            return null;
        }

        [HttpPost()]
        public bool AddClient([FromBody] ClientDto item, string password)
        {
            var entry = ClientsMapperProfile.MapClientDtoToClientTableEntry(item);
            entry.Password = password;

            return databaseClientsProvider.Add(entry);
        }
    }
}

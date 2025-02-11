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
    public class ClientsAccountsController : Controller
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IDatabaseClientsProvider databaseProvider;

        public ClientsAccountsController(ILogger<ClientsController> _logger, IDatabaseClientsProvider _dbProvider)
        {
            logger = _logger;
            databaseProvider = _dbProvider;
        }

        [HttpGet("accountsOfClient/{clientId}")]
        public List<ClientDto> GetAccountsOfClient(string clientId)
        {
            Request.Headers.TryGetValue("", out StringValues headerValue);
            List<ClientDto> result = new List<ClientDto>();

            var clientsInDb = databaseProvider.GetAll();

            return clientsInDb.Select(client => ClientsMapperProfile.MapClientTableEntryToClientDto(client)).ToList();
        }

        [HttpGet("clientOfAccount/{accountId}")]
        public ClientDto GetClientOfAccount(string accountId)
        {
            List<ClientDto> result = new List<ClientDto>();

            var clientInDb = databaseProvider.GetById(accountId);

            if (clientInDb != null)
            {
                return ClientsMapperProfile.MapClientTableEntryToClientDto(clientInDb);
            }

            return null;
        }
    }
}

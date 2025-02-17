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
    public class AccountsController : Controller
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IDatabaseAccountsProvider databaseAccountsProvider;
        private readonly IDatabaseClientAccountBridgeProvider databaseClientAccountBridgeProvider;
        private readonly IDatabaseInvestmentsAccountBridgeProvider databaseInvestmentsAccountBridgeProvider;

        public AccountsController(ILogger<ClientsController> _logger, IDatabaseAccountsProvider _dbAccountsProvider,
            IDatabaseClientAccountBridgeProvider _dbClientAccountsBridgeProvider, IDatabaseInvestmentsAccountBridgeProvider _dbInvestmentsAccountBridgeProvider)
        {
            logger = _logger;
            databaseAccountsProvider = _dbAccountsProvider;
            databaseClientAccountBridgeProvider = _dbClientAccountsBridgeProvider;
            databaseInvestmentsAccountBridgeProvider = _dbInvestmentsAccountBridgeProvider;
        }

        [HttpGet()]
        public List<AccountDto> GetAccounts()
        {
            Request.Headers.TryGetValue("", out StringValues headerValue);
            List<AccountDto> result = new List<AccountDto>();

            var itemsInDb = databaseAccountsProvider.GetAll();

            return itemsInDb.Select(item => AccountsMapperProfile.MapAccountsTableEntryToAccountDto(item)).ToList();
        }

        [HttpGet("{id}")]
        public AccountDto GetAccount(string id)
        {
            List<AccountDto> result = new List<AccountDto>();

            var itemInDb = databaseAccountsProvider.GetById(id);

            if (itemInDb != null)
            {
                return AccountsMapperProfile.MapAccountsTableEntryToAccountDto(itemInDb);
            }

            return null;
        }

        [HttpPost("AddAccount")]
        public bool AddAccount([FromBody] AccountDto item, string clientId)
        {
            var entry = AccountsMapperProfile.MapAccountDtoToAccountsTableEntry(item);
            var clientAccountBridgeEntry = new ClientAccountBridgeTableEntry() { Id = $"{clientId}_{entry.AccountId}", AccountId = entry.AccountId, ClientId = clientId };

            var result = databaseAccountsProvider.Add(entry);

            if (!result)
            {
                return false;
            }

            result = databaseClientAccountBridgeProvider.Add(clientAccountBridgeEntry);

            if (!result)
            {
                return false;
            }

            return result;
        }

        [HttpPost("AddInvestmentsAccount")]
        public bool AddInvestementsAccount([FromBody] AccountDto item, string clientId, string sourceAccountId, int duration, decimal interest)
        {
            var entry = AccountsMapperProfile.MapAccountDtoToAccountsTableEntry(item);
            var clientAccountBridgeEntry = new ClientAccountBridgeTableEntry() { Id = $"{clientId}_{entry.AccountId}", AccountId = entry.AccountId, ClientId = clientId };

            var result = databaseAccountsProvider.Add(entry);

            if (!result)
            {
                return false;
            }

            result = databaseClientAccountBridgeProvider.Add(clientAccountBridgeEntry);

            if (!result)
            {
                return false;
            }

            if (item.AccountType == AccountType.Savings)
            {
                var investmentAccountBridgeEntry = new InvestmentsAccountBridgeTableEntry() { Id = $"{entry.AccountId}_{sourceAccountId}", 
                    SourceAccountId = sourceAccountId, InvestmentsAccountId = entry.AccountId, Duration = duration, Interest = interest };

                result = databaseInvestmentsAccountBridgeProvider.Add(investmentAccountBridgeEntry);
            }

            if (!result)
            {
                return false;
            }

            return result;
        }

        [HttpPatch("EditAccount")]
        public bool EditAccount([FromBody] AccountDto item, string clientId)
        {
            var entry = AccountsMapperProfile.MapAccountDtoToAccountsTableEntry(item);

            var entryInDb = databaseAccountsProvider.GetById(entry.AccountId);

            if (entryInDb == null)
            {
                return false;
            }

            var result = databaseAccountsProvider.Edit(entry);

            if (!result)
            {
                return false;
            }

            return result;
        }

        [HttpDelete("DeleteAccount/{accountId}")]
        public bool DeleteAccount(string accountId)
        {
            var result = false;
            var entryInDb = databaseAccountsProvider.GetById(accountId);

            if (entryInDb == null)
            {
                return false;
            }

            var accountType = AccountsMapperProfile.MapStringAccountTypeToAccountTypeEnum(entryInDb.AccountType);

            if (accountType == AccountType.Savings)
            {
                var investmentsAccountBridgeEntryInDb = databaseInvestmentsAccountBridgeProvider.GetByInvestmentsAccountId(entryInDb.AccountId);

                if (investmentsAccountBridgeEntryInDb == null)
                {
                    return false;
                }
            }
            else
            {
                var investmentsAccountBridgeEntriesInDb = databaseInvestmentsAccountBridgeProvider.GetInvestmentsAccountsOfAccount(entryInDb.AccountId);

                if (investmentsAccountBridgeEntriesInDb == null)
                {
                    return false;
                }

                investmentsAccountBridgeEntriesInDb = investmentsAccountBridgeEntriesInDb.Select(acc =>
                {
                    acc.SourceAccountId = entryInDb.AccountId;
                    return acc;
                }).ToList();

                foreach(var acc in investmentsAccountBridgeEntriesInDb)
                {
                    result = databaseInvestmentsAccountBridgeProvider.Delete(acc.Id);

                    if (!result)
                    {
                        return false;
                    }
                }
            }


            var clientAccountBridgeEntryInDb = databaseClientAccountBridgeProvider.GetByAccountId(entryInDb.AccountId);

            if (clientAccountBridgeEntryInDb == null)
            {
                return false;
            }

            result = databaseClientAccountBridgeProvider.Delete(entryInDb.AccountId);

            if (!result)
            {
                return false;
            }

            result = databaseAccountsProvider.Delete(entryInDb.AccountId);

            if (!result)
            {
                return false;
            }

            return result;
        }

    }
}

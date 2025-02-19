using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.MapperProfiles;
using Microsoft.AspNetCore.Mvc;


namespace BankingAppDataTier.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : Controller
    {
        private readonly ILogger<ClientsController> logger;
        private readonly IDatabaseClientsProvider databaseClientsProvider;
        private readonly IDatabaseAccountsProvider databaseAccountsProvider;

        public AccountsController(ILogger<ClientsController> _logger, IDatabaseClientsProvider _dbClientsProvider,
            IDatabaseAccountsProvider _dbAccountsProvider)
        {
            logger = _logger;
            databaseClientsProvider = _dbClientsProvider;
            databaseAccountsProvider = _dbAccountsProvider;
        }

        [HttpGet("GetClientAccounts/{clientId}")]
        public ActionResult<GetClientAccountsOutput> GetClientAccounts(string clientId)
        {
            var result = new List<AccountDto>();

            var clientInDb = databaseClientsProvider.GetById(clientId);

            if (clientInDb == null)
            {
                return BadRequest(new GetClientAccountsOutput()
                {
                    Accounts = new List<AccountDto>(),
                    Error = ClientsErrors.InvalidClientId,
                });
            }

            var clientAccountsInDb = databaseAccountsProvider.GetAccountsOfClient(clientId);

            if (clientAccountsInDb == null || clientAccountsInDb.Count == 0)
            {
                return Ok(new GetClientAccountsOutput()
                {
                    Accounts = new List<AccountDto>(),
                });
            }

            result = clientAccountsInDb.Select(acc => AccountsMapperProfile.MapAccountsTableEntryToAccountDto(acc)).ToList();

            return Ok(new GetClientAccountsOutput()
            {
                Accounts = result,
            });
        }

        [HttpGet("GetAccountById/{id}")]
        public ActionResult<GetAccountByIdOutput> GetAccountById(string id)
        {
            var itemInDb = databaseAccountsProvider.GetAccountOfId(id);

            if (itemInDb == null)
            {
                return NotFound(new GetAccountByIdOutput()
                {
                    Account = null,
                    Error = AccountsErrors.InvalidAccountId,
                });
            }

            return Ok(new GetAccountByIdOutput()
            {
                Account = AccountsMapperProfile.MapAccountsTableEntryToAccountDto(itemInDb),
            });
        }

        [HttpPost("AddAccount")]
        public ActionResult<VoidOutput> AddAccount([FromBody] AddAccountInput input)
        {
            if (input.Account.AccountType == AccountType.Investments)
            {
                if (input.Account.SourceAccountId == null || input.Account.Duration == null || input.Account.Interest == null)
                {
                    return BadRequest(new VoidOutput
                    {
                        Error = AccountsErrors.MissingInvestementsAccountDetails,
                    });
                }
            }

            var clientInDb = databaseClientsProvider.GetById(input.ClientId);

            if (clientInDb == null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = ClientsErrors.InvalidClientId,
                });
            }

            var accountInDb = databaseAccountsProvider.GetAccountOfId(input.Account.Id);

            if (accountInDb == null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = AccountsErrors.IdAlreadyInUse,
                });
            }

            var entry = AccountsMapperProfile.MapAccountDtoToAccountsTableEntry(input.Account);

            var result = databaseAccountsProvider.Add(entry, input.ClientId);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpPatch("EditAccount")]
        public ActionResult<VoidOutput> EditAccount([FromBody] EditAccountInput input)
        {
            var entryInDb = databaseAccountsProvider.GetAccountOfId(input.AccountId);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = AccountsErrors.InvalidAccountId
                });
            }

            entryInDb.Balance = input.Balance != null ? input.Balance.GetValueOrDefault() : entryInDb.Balance;
            entryInDb.AccountType = input.AccountType != null ? 
                AccountsMapperProfile.MaAccountTypeEnumToStringAccountType(input.AccountType.GetValueOrDefault()) : entryInDb.AccountType;
            entryInDb.Image = input.Image != null ? input.Image : entryInDb.Image;
            entryInDb.Name = input.Name != null ? input.Name : entryInDb.Name;
            entryInDb.SourceAccountId = input.SourceAccountId != null ? input.SourceAccountId : entryInDb.SourceAccountId;
            entryInDb.Duration = input.Duration != null ? input.Duration : entryInDb.Duration;
            entryInDb.Interest = input.Interest != null ? input.Interest : entryInDb.Interest;


            var result = databaseAccountsProvider.Edit(entryInDb);

            if (!result)
            {
                return new InternalServerError(new VoidOutput
                {
                    Error = GenericErrors.FailedToPerformDatabaseOperation,
                });
            }

            return Ok(new VoidOutput());
        }

        [HttpDelete("DeleteAccount/{id}")]
        public ActionResult<VoidOutput> DeleteAccount(string id)
        {
            var result = false;
            var entryInDb = databaseAccountsProvider.GetAccountOfId(id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = AccountsErrors.InvalidAccountId,
                });
            }

            result = databaseAccountsProvider.Delete(id);

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

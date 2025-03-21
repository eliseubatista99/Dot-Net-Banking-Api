﻿using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using Microsoft.AspNetCore.Mvc;


namespace BankingAppDataTier.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : Controller
    {
        private readonly ILogger logger;
        private readonly IMapperProvider mapperProvider;
        private readonly IDatabaseClientsProvider databaseClientsProvider;
        private readonly IDatabaseAccountsProvider databaseAccountsProvider;
        private readonly IDatabaseCardsProvider databaseCardsProvider;
        private readonly IDatabaseLoansProvider databaseLoansProvider;

        public AccountsController(IExecutionContext _executionContext)
        {
            logger = _executionContext.GetDependency<ILogger>()!;
            mapperProvider = _executionContext.GetDependency <IMapperProvider>()!;
            databaseClientsProvider = _executionContext.GetDependency<IDatabaseClientsProvider>()!;
            databaseAccountsProvider = _executionContext.GetDependency<IDatabaseAccountsProvider>()!;
            databaseCardsProvider = _executionContext.GetDependency<IDatabaseCardsProvider>()!;
            databaseLoansProvider = _executionContext.GetDependency<IDatabaseLoansProvider>()!;
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
                    Error = GenericErrors.InvalidId,
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

            result = clientAccountsInDb.Select(acc => mapperProvider.Map<AccountsTableEntry, AccountDto>(acc)).ToList();

            return Ok(new GetClientAccountsOutput()
            {
                Accounts = result,
            });
        }

        [HttpGet("GetAccountById/{id}")]
        public ActionResult<GetAccountByIdOutput> GetAccountById(string id)
        {
            var itemInDb = databaseAccountsProvider.GetById(id);

            if (itemInDb == null)
            {
                return NotFound(new GetAccountByIdOutput()
                {
                    Account = null,
                    Error = GenericErrors.InvalidId,
                });
            }

            return Ok(new GetAccountByIdOutput()
            {
                Account = mapperProvider.Map<AccountsTableEntry, AccountDto>(itemInDb),
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

            var clientInDb = databaseClientsProvider.GetById(input.Account.OwnerCliendId);

            if (clientInDb == null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = AccountsErrors.InvalidOwnerId,
                });
            }

            var accountInDb = databaseAccountsProvider.GetById(input.Account.Id);

            if (accountInDb != null)
            {
                return BadRequest(new VoidOutput()
                {
                    Error = GenericErrors.IdAlreadyInUse,
                });
            }

            var entry = mapperProvider.Map<AccountDto, AccountsTableEntry>(input.Account);

            var result = databaseAccountsProvider.Add(entry);

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
            var entryInDb = databaseAccountsProvider.GetById(input.AccountId);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId
                });
            }

            if(entryInDb.AccountType == BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS)
            {
                if (input.SourceAccountId != null)
                {
                    var sourceAccountInDb = databaseAccountsProvider.GetById(input.SourceAccountId);

                    if (sourceAccountInDb == null)
                    {
                        return BadRequest(new VoidOutput
                        {
                            Error = AccountsErrors.InvalidSourceAccount,
                        });
                    }
                }

                entryInDb.SourceAccountId = input.SourceAccountId != null ? input.SourceAccountId : entryInDb.SourceAccountId;
                entryInDb.Duration = input.Duration != null ? input.Duration : entryInDb.Duration;
                entryInDb.Interest = input.Interest != null ? input.Interest : entryInDb.Interest;
            }

            entryInDb.Balance = input.Balance != null ? input.Balance.GetValueOrDefault() : entryInDb.Balance;
            entryInDb.Image = input.Image != null ? input.Image : entryInDb.Image;
            entryInDb.Name = input.Name != null ? input.Name : entryInDb.Name;

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
            var entryInDb = databaseAccountsProvider.GetById(id);

            if (entryInDb == null)
            {
                return BadRequest(new VoidOutput
                {
                    Error = GenericErrors.InvalidId,
                });
            }

            var cardsOfAccount = databaseCardsProvider.GetCardsOfAccount(entryInDb.AccountId);

            if (cardsOfAccount != null && cardsOfAccount.Count > 0)
            {
                return BadRequest(new VoidOutput
                {
                    Error = AccountsErrors.CantCloseWithRelatedCards,
                });
            }

            var loansOfAccount = databaseLoansProvider.GetByAccount(entryInDb.AccountId);

            if (loansOfAccount != null && loansOfAccount.Count > 0)
            {
                return BadRequest(new VoidOutput
                {
                    Error = AccountsErrors.CantCloseWithActiveLoans,
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

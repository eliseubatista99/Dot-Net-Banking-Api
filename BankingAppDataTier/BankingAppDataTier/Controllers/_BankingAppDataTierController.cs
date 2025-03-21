using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entities;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs;
using BankingAppDataTier.Contracts.Dtos.Inputs.Authentication;
using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.DatabaseInitializers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace BankingAppDataTier.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class _BankingAppDataTierController : Controller
    {
        protected IAuthenticationProvider authenticationProvider;

        public _BankingAppDataTierController(IAuthenticationProvider _authProvider)
        {
            authenticationProvider = _authProvider;
        }

        protected (bool authorized, BankingAppDataTierError? error) IsAuthorized(_BaseInput input)
        {
            if (input.Metadata?.Token == null)
            {
                return (false, AuthenticationErrors.InvalidToken);
            }

            var tokenValidationResult = authenticationProvider.IsValidToken(input.Metadata.Token);

            return (tokenValidationResult.isValid, null);
        }
    }
}

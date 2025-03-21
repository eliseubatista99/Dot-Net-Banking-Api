using BankingAppDataTier.Contracts.Dtos.Entities;
using BankingAppDataTier.Contracts.Dtos.Inputs;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using Microsoft.AspNetCore.Mvc;


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

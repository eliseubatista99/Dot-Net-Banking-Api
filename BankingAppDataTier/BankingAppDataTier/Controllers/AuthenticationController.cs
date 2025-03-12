using BankingAppDataTier.Contracts.Dtos.Inputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
using BankingAppDataTier.Contracts.Providers;
using Microsoft.AspNetCore.Mvc;


namespace BankingAppDataTier.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<ClientsController> logger;

        private readonly IConfiguration configuration;

        private readonly IAuthenticationProvider authenticationProvider;

        public AuthenticationController(ILogger<ClientsController> _logger, IConfiguration _config, IAuthenticationProvider authProvider)
        {
            logger = _logger;
            configuration = _config;
            authenticationProvider = authProvider;
        }

        [HttpPost("token")]
        public ActionResult<GenerateTokenOutput> GenerateToken([FromBody] AuthenticationInputDto input)
        {
            var token = authenticationProvider.GenerateToken(input.AppId);
            
            return Ok(new GenerateTokenOutput()
            {
                Token = token,
            });
        }        
    }
}

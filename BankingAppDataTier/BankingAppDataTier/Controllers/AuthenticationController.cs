using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.MapperProfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


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
        public IActionResult GenerateToken([FromBody] AuthenticationInputDto input)
        {
            var token = authenticationProvider.GenerateToken(input.AppId);
            
            return Ok(token);
        }        
    }
}

using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
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

        public AuthenticationController(ILogger<ClientsController> _logger, IConfiguration _config)
        {
            logger = _logger;
            configuration = _config;
        }

        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] AuthenticationInputDto input)
        {
            var authConfigs = configuration.GetSection(AuthenticationConfigs.AuthenticationSection);

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfigs[AuthenticationConfigs.Key]!));
            var lifetime = AuthenticationConfigs.TokenLifetime;
            var issuer = authConfigs.GetSection(AuthenticationConfigs.Issuer).Value!;
            var audience = authConfigs.GetSection(AuthenticationConfigs.Audience).Value!;
            DateTime expireDateTime = DateTime.UtcNow.Add(lifetime);

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("appId", input.AppId),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                           issuer: issuer,
                           audience: input.AppId,
                           claims: claims,
                           expires: expireDateTime,
                           signingCredentials: creds
                       );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string writtenToken = handler.WriteToken(token);

            return Ok(writtenToken);
        }        
    }
}

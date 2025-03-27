using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingAppDataTier.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private IConfiguration Configuration;

        public AuthenticationProvider(IApplicationContext applicationContext)
        {
            this.Configuration = applicationContext.GetDependency<IConfiguration>()!;
        }

        public void AddAuthenticationToApplicationBuilder(ref WebApplicationBuilder builder)
        {
            var authConfigs = builder.Configuration.GetSection(AuthenticationConfigs.AuthenticationSection);

            // Add the process of verifying who they are
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authConfigs.GetSection(AuthenticationConfigs.Issuer).Value!,
                    ValidAudience = authConfigs.GetSection(AuthenticationConfigs.Audience).Value!,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfigs[AuthenticationConfigs.Key]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });
        }

        public void AddAuthorizationToSwaggerGen(ref WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        public (string token, DateTime expirationTime) GenerateToken(string clientId)
        {
            var configs = GetTokenConfigs();

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(JwtRegisteredClaimNames.Name, clientId),
                }),
                Expires = configs.expireDateTime,
                Issuer = configs.issuer,
                Audience = configs.audience,
                SigningCredentials = new SigningCredentials(configs.key, SecurityAlgorithms.HmacSha256),

            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(tokenDescriptor);

            return (handler.WriteToken(securityToken), configs.expireDateTime);
        }

        public (bool isValid, DateTime expirationTime) IsValidToken(string token)
        {
            try
            {
                var configs = GetTokenConfigs();

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = configs.key,
                    ValidateLifetime = false,
                    ValidIssuer = configs.issuer,
                    ValidAudience = configs.audience,
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

                var jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return (false, DateTime.Now);
                }

                return (true, jwtSecurityToken.ValidTo);
            }
            catch (Exception ex)
            {
                return (false, DateTime.Now);
            }

        }

        public int GetTokenLifeTime()
        {
            var configs = GetTokenConfigs();

            return configs.lifeTime;
        }

        private (SymmetricSecurityKey key, string issuer, string audience, DateTime expireDateTime, int lifeTime) GetTokenConfigs()
        {
            var authConfigs = this.Configuration.GetSection(AuthenticationConfigs.AuthenticationSection);

            var tokenHandler = new JwtSecurityTokenHandler();

            var issuer = authConfigs.GetSection(AuthenticationConfigs.Issuer).Value!;
            var audience = authConfigs.GetSection(AuthenticationConfigs.Audience).Value!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfigs[AuthenticationConfigs.Key]!));
            var lifetime = AuthenticationConfigs.TokenLifetime;
            DateTime expireDateTime = DateTime.UtcNow.AddMinutes(lifetime);

            return (key, issuer, audience, expireDateTime, lifetime);
        }
    }
}

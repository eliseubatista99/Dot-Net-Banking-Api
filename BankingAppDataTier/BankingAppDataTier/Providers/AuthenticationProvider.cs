using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Providers;
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

        public AuthenticationProvider(IConfiguration configuration)
        {
            this.Configuration = configuration;
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

        public string GenerateToken(string audience)
        {
            var authConfigs = this.Configuration.GetSection(AuthenticationConfigs.AuthenticationSection);

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfigs[AuthenticationConfigs.Key]!));
            var lifetime = AuthenticationConfigs.TokenLifetime;
            var issuer = authConfigs.GetSection(AuthenticationConfigs.Issuer).Value!;
            DateTime expireDateTime = DateTime.UtcNow.Add(lifetime);

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("audience", audience),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                           issuer: issuer,
                           audience: audience,
                           claims: claims,
                           expires: expireDateTime,
                           signingCredentials: creds
                       );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string writtenToken = handler.WriteToken(token);

            return writtenToken;
        }
    }
}

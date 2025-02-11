using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BankingAppDataTier
{
    class Program
    {
        static void InjectDependencies(ref WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IDatabaseProvider, DatabaseProvider>();
        }

        static void AddAuthorization(ref WebApplicationBuilder builder)
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

            // Add the process of verifying what access they have
            builder.Services.AddAuthorization();
        }

        static void ConfigureSwaggerGen(ref WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "BankingAppData", Version = "v1" });

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


        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Cors
            builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            InjectDependencies(ref builder);
            AddAuthorization(ref builder);

            var databaseInitializer = new DatabaseInitializer(builder.Services.BuildServiceProvider().GetService<IDatabaseProvider>());

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            ConfigureSwaggerGen(ref builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

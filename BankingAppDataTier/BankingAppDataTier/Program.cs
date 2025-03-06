using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.Providers;

namespace BankingAppDataTier
{
    class Program
    {
        static (IAuthenticationProvider authProvider, IServiceCollection serviceCollection) InjectDependencies(ref WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IAuthenticationProvider, AuthenticationProvider>();
            builder.Services.AddSingleton<IDatabaseClientsProvider, DatabaseClientsProvider>();
            builder.Services.AddSingleton<IDatabaseAccountsProvider, DatabaseAccountsProvider>();
            builder.Services.AddSingleton<IDatabasePlasticsProvider, DatabasePlasticsProvider>();
            builder.Services.AddSingleton<IDatabaseCardsProvider, DatabaseCardsProvider>();
            builder.Services.AddSingleton<IDatabaseTransactionsProvider, DatabaseTransactionsProvider>();
            builder.Services.AddSingleton<IDatabaseLoanOfferProvider, DatabaseLoanOffersProvider>();
            builder.Services.AddSingleton<IDatabaseLoansProvider, DatabaseLoanProvider>();

            var authProvider = builder.Services.BuildServiceProvider().GetService<IAuthenticationProvider>()!;

            return (authProvider, builder.Services);
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

            var (authProvider, serviceCollection) = InjectDependencies(ref builder);

            // Add process of verifying who they are
            authProvider!.AddAuthenticationToApplicationBuilder(ref builder);

            // Add the process of verifying what access they have
            builder.Services.AddAuthorization();

            var databaseInitializer = new DatabaseInitializer(serviceCollection);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // Add authorization to swagger gen
            authProvider!.AddAuthorizationToSwaggerGen(ref builder);

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

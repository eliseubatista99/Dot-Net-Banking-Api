using BankingAppAuthenticationTier.DatabaseInitializers;
using BankingAppAuthenticationTier.MapperProfiles;
using BankingAppAuthenticationTier.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using Swashbuckle.AspNetCore.SwaggerGen;
using BankingAppAuthenticationTier.Library.Providers;
using ElideusDotNetFramework.Authentication;

namespace BankingAppAuthenticationTier
{
    public class BankingAppAuthenticationTierApplication : ElideusDotNetFrameworkApplication
    {
        protected override OperationsBuilder OperationsBuilder { get; set; } = new BankingAppAuthenticationTierOperationsBuilder();
        protected override bool UseAuthentication { get; set; } = false;

        protected override void AddAuthorizationToSwagger(ref WebApplicationBuilder builder, ref SwaggerGenOptions options)
        {
            base.AddAuthorizationToSwagger(ref builder, ref options);

            //var authProvider = builder.Services.BuildServiceProvider().GetService<IAuthenticationProvider>()!;

            //authProvider!.AddAuthorizationToSwaggerGen(ref builder);
        }

        protected override void ConfigureAuthentication(ref WebApplicationBuilder builder)
        {
            base.ConfigureAuthentication(ref builder);

            //var authProvider = builder.Services.BuildServiceProvider().GetService<IAuthenticationProvider>()!;

            //authProvider!.AddAuthenticationToApplicationBuilder(ref builder);
        }

        protected override void InitializeDatabase(ref WebApplicationBuilder builder)
        {
            base.InitializeDatabase(ref builder);

            ClientsDatabaseInitializer.DefaultMock(ApplicationContext!.GetDependency<IDatabaseClientsProvider>()!);
            TokenDatabaseInitializer.InitializeDatabase(ApplicationContext!.GetDependency<IDatabaseTokenProvider>()!);
        }

        protected override void InjectDependencies(ref WebApplicationBuilder builder)
        {
            base.InjectDependencies(ref builder);

            ApplicationContext?.AddDependency<IAuthenticationProvider, BankingAppAuthenticationProvider>(ref builder);
            ApplicationContext?.AddDependency<IDatabaseClientsProvider, DatabaseClientsProvider>(ref builder);
            ApplicationContext?.AddDependency<IDatabaseTokenProvider, DatabaseTokenProvider>(ref builder);
        }

        protected override void InitializeAutoMapper()
        {
            base.InitializeAutoMapper();

            var mapper = ApplicationContext.GetDependency<IMapperProvider>();

            mapper.CreateMapper(
                new List<AutoMapper.Profile>
                {
                    new TokensMapperProfile(),
                    new ClientsMapperProfile(),
                });
        }
    }
}

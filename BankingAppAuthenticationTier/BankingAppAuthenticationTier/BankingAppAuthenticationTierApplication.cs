using BankingAppAuthenticationTier.DatabaseInitializers;
using BankingAppAuthenticationTier.MapperProfiles;
using BankingAppAuthenticationTier.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using BankingAppAuthenticationTier.Library.Providers;

namespace BankingAppAuthenticationTier
{
    public class BankingAppAuthenticationTierApplication : ElideusDotNetFrameworkApplication
    {
        protected override OperationsBuilder OperationsBuilder { get; set; } = new BankingAppAuthenticationTierOperationsBuilder();

        protected override void InitializeDatabase(ref WebApplicationBuilder builder)
        {
            base.InitializeDatabase(ref builder);

            ClientsDatabaseInitializer.DefaultMock(ApplicationContext!.GetDependency<IDatabaseClientsProvider>()!);
        }

        protected override void InjectDependencies(ref WebApplicationBuilder builder)
        {
            base.InjectDependencies(ref builder);

            ApplicationContext?.AddDependency<IAuthenticationProvider, BankingAppAuthenticationProvider>(ref builder);
            ApplicationContext?.AddDependency<IDatabaseClientsProvider, DatabaseClientsProvider>(ref builder);
        }

        protected override void InitializeAutoMapper()
        {
            base.InitializeAutoMapper();

            var mapper = ApplicationContext.GetDependency<IMapperProvider>();

            mapper.CreateMapper(
                new List<AutoMapper.Profile>
                {
                    new ClientsMapperProfile(),
                });
        }
    }
}

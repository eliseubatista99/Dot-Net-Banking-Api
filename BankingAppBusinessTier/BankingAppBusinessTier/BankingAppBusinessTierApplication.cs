using BankingAppBusinessTier.Library.Providers;
using BankingAppBusinessTier.MapperProfiles;
using BankingAppBusinessTier.Providers;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppBusinessTier
{
    public class BankingAppBusinessTierApplication : ElideusDotNetFrameworkApplication
    {
        protected override OperationsBuilder OperationsBuilder { get; set; } = new BankingAppBusinessTierOperationsBuilder();

        protected override void InjectDependencies(ref WebApplicationBuilder builder)
        {
            base.InjectDependencies(ref builder);

            ApplicationContext?.AddDependency<IAuthenticationTierProvider, AuthenticationTierProvider>(ref builder);
            ApplicationContext?.AddDependency<IDataTierProvider, DataTierProvider>(ref builder);

        }

        protected override void InitializeAutoMapper()
        {
            base.InitializeAutoMapper();

            var mapper = ApplicationContext.GetDependency<IMapperProvider>();

            mapper.CreateMapper(
                new List<AutoMapper.Profile>
                {
                    new DataTierMapperProfile(),
                    //new ClientsMapperProfile(),
                    //new AccountsMapperProfile(),
                    //new PlasticsMapperProfile(),
                    //new CardsMapperProfile(),
                    //new LoanOffersMapperProfile(),
                    //new LoansMapperProfile(),
                    //new TransactionsMapperProfile(),
                });
        }
    }
}

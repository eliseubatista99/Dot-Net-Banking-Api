using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Core.Operations;
using ExternalApplications.DataTier.MapperProfiles;
using ExternalApplications.DataTier.Providers;

namespace BankingAppBusinessTier
{
    public class BankingAppBusinessTierApplication : ElideusDotNetFrameworkApplication
    {
        protected override OperationsBuilder OperationsBuilder { get; set; } = new BankingAppBusinessTierOperationsBuilder();

        protected override void InjectDependencies(ref WebApplicationBuilder builder)
        {
            base.InjectDependencies(ref builder);

            ApplicationContext?.AddDependency<IDataTierProvider, DataTierProvider>(ref builder);
            //ApplicationContext?.AddDependency<IDatabaseClientsProvider, DatabaseClientsProvider>(ref builder);
            //ApplicationContext?.AddDependency<IDatabaseTokenProvider, DatabaseTokenProvider>(ref builder);
            //ApplicationContext?.AddDependency<IDatabaseAccountsProvider, DatabaseAccountsProvider>(ref builder);
            //ApplicationContext?.AddDependency<IDatabasePlasticsProvider, DatabasePlasticsProvider>(ref builder);
            //ApplicationContext?.AddDependency<IDatabaseCardsProvider, DatabaseCardsProvider>(ref builder);
            //ApplicationContext?.AddDependency<IDatabaseTransactionsProvider, DatabaseTransactionsProvider>(ref builder);
            //ApplicationContext?.AddDependency<IDatabaseLoanOfferProvider, DatabaseLoanOffersProvider>(ref builder);
            //ApplicationContext?.AddDependency<IDatabaseLoansProvider, DatabaseLoanProvider>(ref builder);
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

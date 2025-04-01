using BankingAppDataTier.Library.Providers;
using BankingAppDataTier.DatabaseInitializers;
using BankingAppDataTier.MapperProfiles;
using BankingAppDataTier.Providers;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;

namespace BankingAppDataTier
{
    public class BankingAppDataTierApplication : ElideusDotNetFrameworkApplication
    {
        protected override OperationsBuilder OperationsBuilder { get; set; } = new BankingAppDataTierOperationsBuilder();

        protected override void InitializeDatabase(ref WebApplicationBuilder builder)
        {
            base.InitializeDatabase(ref builder);

            ClientsDatabaseInitializer.DefaultMock(ApplicationContext!.GetDependency<IDatabaseClientsProvider>()!);
            AccountsDatabaseInitializer.DefaultMock(ApplicationContext!.GetDependency<IDatabaseAccountsProvider>()!);
            PlasticsDatabaseInitializer.DefaultMock(ApplicationContext!.GetDependency<IDatabasePlasticsProvider>()!);
            CardsDatabaseInitializer.DefaultMock(ApplicationContext!.GetDependency<IDatabaseCardsProvider>()!);
            TransactionsDatabaseInitializer.DefaultMock(ApplicationContext!.GetDependency<IDatabaseTransactionsProvider>()!);
            LoanOffersDatabaseInitializer.DefaultMock(ApplicationContext!.GetDependency<IDatabaseLoanOfferProvider>()!);
            LoansDatabaseInitializer.DefaultMock(ApplicationContext!.GetDependency<IDatabaseLoansProvider>()!);

        }

        protected override void InjectDependencies(ref WebApplicationBuilder builder)
        {
            base.InjectDependencies(ref builder);

            ApplicationContext?.AddDependency<IAuthenticationTierProvider, AuthenticationTierProvider>(ref builder);
            ApplicationContext?.AddDependency<IDatabaseClientsProvider, DatabaseClientsProvider>(ref builder);
            ApplicationContext?.AddDependency<IDatabaseAccountsProvider, DatabaseAccountsProvider>(ref builder);
            ApplicationContext?.AddDependency<IDatabasePlasticsProvider, DatabasePlasticsProvider>(ref builder);
            ApplicationContext?.AddDependency<IDatabaseCardsProvider, DatabaseCardsProvider>(ref builder);
            ApplicationContext?.AddDependency<IDatabaseTransactionsProvider, DatabaseTransactionsProvider>(ref builder);
            ApplicationContext?.AddDependency<IDatabaseLoanOfferProvider, DatabaseLoanOffersProvider>(ref builder);
            ApplicationContext?.AddDependency<IDatabaseLoansProvider, DatabaseLoanProvider>(ref builder);
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
                    new AccountsMapperProfile(),
                    new PlasticsMapperProfile(),
                    new CardsMapperProfile(),
                    new LoanOffersMapperProfile(),
                    new LoansMapperProfile(),
                    new TransactionsMapperProfile(),
                });
        }
    }
}

using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Core.Operations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BankingAppPresentationTier
{
    public class BankingAppPresentationTierApplication : ElideusDotNetFrameworkApplication
    {
        protected override OperationsBuilder OperationsBuilder { get; set; } = new BankingAppPresentationTierOperationsBuilder();

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

        protected override void InjectDependencies(ref WebApplicationBuilder builder)
        {
            base.InjectDependencies(ref builder);

            //ApplicationContext?.AddDependency<IAuthenticationProvider, AuthenticationProvider>(ref builder);
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
                    //new TokensMapperProfile(),
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

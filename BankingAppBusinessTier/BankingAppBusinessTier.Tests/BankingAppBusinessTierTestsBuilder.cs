using BankingAppBusinessTier.Tests.Mocks;
using BankingAppDataTier.Tests;
using ElideusDotNetFramework.Tests;
using TechTalk.SpecFlow.xUnit.SpecFlowPlugin;

[assembly: AssemblyFixture(typeof(BankingAppBusinessTierTestsBuilder))]
namespace BankingAppDataTier.Tests
{
    public class BankingAppBusinessTierTestsBuilder : ElideusDotNetFrameworkTestsBuilder
    {
        public BankingAppBusinessTierTestsBuilder() : base()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            ApplicationContextMock = new BankingAppBusinessTierApplicationContextMock().Mock();

            MockDependencies();
        }

        protected void MockDependencies()
        {
            //ApplicationContextMock!.AddTestDependency(new AuthenticationProvider(ApplicationContextMock));
            //ApplicationContextMock!.AddTestDependency(new DatabaseAccountsProvider(ApplicationContextMock));
            //ApplicationContextMock!.AddTestDependency(new DatabaseCardsProvider(ApplicationContextMock));
            //ApplicationContextMock!.AddTestDependency(new DatabaseClientsProvider(ApplicationContextMock));
            //ApplicationContextMock!.AddTestDependency(new DatabaseLoanOffersProvider(ApplicationContextMock));
            //ApplicationContextMock!.AddTestDependency(new DatabaseLoanProvider(ApplicationContextMock));
            //ApplicationContextMock!.AddTestDependency(new DatabasePlasticsProvider(ApplicationContextMock));
            //ApplicationContextMock!.AddTestDependency(new DatabaseTokenProvider(ApplicationContextMock));
            //ApplicationContextMock!.AddTestDependency(new DatabaseTransactionsProvider(ApplicationContextMock));
        }
    }
}

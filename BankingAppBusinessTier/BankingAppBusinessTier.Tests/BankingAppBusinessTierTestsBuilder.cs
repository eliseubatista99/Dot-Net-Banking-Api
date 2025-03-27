using BankingAppBusinessTier.Tests.Mocks;
using BankingAppBusinessTier.Tests.Mocks.Providers;
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
            ApplicationContextMock!.AddTestDependency(new AuthenticationTierProviderMock());
            ApplicationContextMock!.AddTestDependency(new DataTierProviderMock());
        }
    }
}

using BankingAppAuthenticationTier.Library.Providers;
using BankingAppAuthenticationTier.Providers;
using BankingAppAuthenticationTier.Tests;
using BankingAppAuthenticationTier.Tests.Mocks;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Tests;
using TechTalk.SpecFlow.xUnit.SpecFlowPlugin;

[assembly: AssemblyFixture(typeof(BankingAppAuthenticationTierTestsBuilder))]
namespace BankingAppAuthenticationTier.Tests
{
    public class BankingAppAuthenticationTierTestsBuilder : ElideusDotNetFrameworkTestsBuilder
    {
        public BankingAppAuthenticationTierTestsBuilder() : base()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            ApplicationContextMock = new BakingAppDataTierApplicationContextMock().Mock();

            MockDependencies();
            MockDatabase();
        }

        protected void MockDependencies()
        {
            ApplicationContextMock!.AddTestDependency(new BankingAppAuthenticationProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseClientsProviderMock());
        }

        private void MockDatabase()
        {
            var _AuthenticationProviderMock = ApplicationContextMock!.GetDependency<IAuthenticationProvider>();
            var _DatabaseClientsProviderMock = ApplicationContextMock!.GetDependency<IDatabaseClientsProvider>();
        }
    }
}

using BankingAppAuthenticationTier.Library.Providers;
using BankingAppAuthenticationTier.Providers;
using BankingAppAuthenticationTier.Tests;
using BankingAppAuthenticationTier.Tests.Mocks;
using BankingAppAuthenticationTier.Tests.Mocks.Database;
using ElideusDotNetFramework.Authentication;
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
            ApplicationContextMock!.AddTestDependency(new DatabaseTokenProvider(ApplicationContextMock));
        }

        private void MockDatabase()
        {
            var _AuthenticationProviderMock = ApplicationContextMock!.GetDependency<IAuthenticationProvider>();
            var _DatabaseClientsProviderMock = ApplicationContextMock!.GetDependency<IDatabaseClientsProvider>();
            var _DatabaseTokensProviderMock = ApplicationContextMock!.GetDependency<IDatabaseTokenProvider>();

            // Create databases if not exists
            _DatabaseTokensProviderMock!.CreateTableIfNotExists();

            // Clean database values
            _DatabaseTokensProviderMock.DeleteAll();

            // Mock database values
            TokensEntriesMock.Mock(_DatabaseTokensProviderMock);
        }
    }
}

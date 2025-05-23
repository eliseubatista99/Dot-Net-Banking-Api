﻿using BankingAppDataTier.Library.Providers;
using BankingAppDataTier.Providers;
using BankingAppDataTier.Tests;
using BankingAppDataTier.Tests.Mocks;
using BankingAppDataTier.Tests.Mocks.Database;
using BankingAppDataTier.Tests.Mocks.Providers;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Tests;
using TechTalk.SpecFlow.xUnit.SpecFlowPlugin;

[assembly: AssemblyFixture(typeof(BankingAppDataTierTestsBuilder))]
namespace BankingAppDataTier.Tests
{
    public class BankingAppDataTierTestsBuilder : ElideusDotNetFrameworkTestsBuilder
    {
        public BankingAppDataTierTestsBuilder() : base()
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
            ApplicationContextMock!.AddTestDependency(new AuthenticationTierProviderMock());
            ApplicationContextMock!.AddTestDependency(new DatabaseAccountsProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseCardsProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseClientsProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseLoanOffersProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseLoanProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabasePlasticsProvider(ApplicationContextMock));
            ApplicationContextMock!.AddTestDependency(new DatabaseTransactionsProvider(ApplicationContextMock));
        }

        private void MockDatabase()
        {
            var _AuthenticationProviderMock = ApplicationContextMock!.GetDependency<IAuthenticationTierProvider>();
            var _DatabaseClientsProviderMock = ApplicationContextMock!.GetDependency<IDatabaseClientsProvider>();
            var _DatabaseAccountsProviderMock = ApplicationContextMock!.GetDependency<IDatabaseAccountsProvider>();
            var _DatabasePlasticsProviderMock = ApplicationContextMock!.GetDependency<IDatabasePlasticsProvider>();
            var _DatabaseCardsProviderMock = ApplicationContextMock!.GetDependency<IDatabaseCardsProvider>();
            var _DatabaseLoanOffersProviderMock = ApplicationContextMock!.GetDependency<IDatabaseLoanOfferProvider>();
            var _DatabaseLoanProviderMock = ApplicationContextMock!.GetDependency<IDatabaseLoansProvider>();
            var _DatabaseTransactionsProviderMock = ApplicationContextMock!.GetDependency<IDatabaseTransactionsProvider>();

            // Create databases if not exists
            _DatabaseClientsProviderMock!.CreateTableIfNotExists();
            _DatabaseAccountsProviderMock!.CreateTableIfNotExists();
            _DatabasePlasticsProviderMock!.CreateTableIfNotExists();
            _DatabaseCardsProviderMock!.CreateTableIfNotExists();
            _DatabaseLoanOffersProviderMock!.CreateTableIfNotExists();
            _DatabaseLoanProviderMock!.CreateTableIfNotExists();
            _DatabaseTransactionsProviderMock!.CreateTableIfNotExists();

            // Clean database values
            _DatabaseTransactionsProviderMock.DeleteAll();
            _DatabaseLoanProviderMock.DeleteAll();
            _DatabaseLoanOffersProviderMock.DeleteAll();
            _DatabaseCardsProviderMock.DeleteAll();
            _DatabasePlasticsProviderMock.DeleteAll();
            _DatabaseAccountsProviderMock.DeleteAll();
            _DatabaseClientsProviderMock.DeleteAll();

            // Mock database values
            ClientsEntriesMock.Mock(_DatabaseClientsProviderMock);
            AccountsEntriesMock.Mock(_DatabaseAccountsProviderMock);
            PlasticsEntriesMock.Mock(_DatabasePlasticsProviderMock);
            CardsEntriesMock.Mock(_DatabaseCardsProviderMock);
            LoanOffersEntriesMock.Mock(_DatabaseLoanOffersProviderMock);
            LoansEntriesMock.Mock(_DatabaseLoanProviderMock);
            TransactionsEntriesMock.Mock(_DatabaseTransactionsProviderMock);
        }
    }
}

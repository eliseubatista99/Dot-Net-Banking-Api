using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.Providers;
using BankingAppDataTier.Tests.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Tests.Mocks
{
    public class TestingMocksBuilder
    {
        public ILogger<T> MockLogger<T>()
        {
            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
                .SetMinimumLevel(LogLevel.Trace)
                .AddConsole());

            ILogger<T> logger = loggerFactory.CreateLogger<T>();

            return logger;
        }

        public IDatabaseClientsProvider MockDatabaseClientsProvider()
        {
            IConfiguration configuration = MockConfiguration();
            var provider =  new DatabaseClientsProvider(configuration);

            ClientsDatabaseMock.DefaultMock(provider);
            return provider;
        }

        public IDatabaseAccountsProvider MockDatabaseAccountsProvider()
        {
            IConfiguration configuration = MockConfiguration();
            var provider = new DatabaseAccountsProvider(configuration);

            AccountsDatabaseMock.DefaultMock(provider);
            return provider;
        }


        private IConfiguration MockConfiguration()
        {
            var inMemorySettings = new Dictionary<string, string?> {
                {$"{DatabaseConfigs.DatabaseSection}:{DatabaseConfigs.DatabaseConnection}", TestsConstants.ConnectionString},
                {"SectionName:SomeKey", "SectionValue"},
                //...populate as needed for the test
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            return configuration;
        }
    }
}

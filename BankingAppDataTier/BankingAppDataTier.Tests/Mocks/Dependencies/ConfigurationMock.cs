using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Tests.Constants;
using Microsoft.Extensions.Configuration;

namespace BankingAppDataTier.Tests.Mocks
{
    public static class ConfigurationMock
    {
        private static IConfiguration _configuration;

        public static IConfiguration Mock()
        {
            var inMemorySettings = new Dictionary<string, string?> {
                {$"{DatabaseConfigs.DatabaseSection}:{DatabaseConfigs.DatabaseConnection}", TestsConstants.ConnectionString},
                {"SectionName:SomeKey", "SectionValue"},
                //...populate as needed for the test
            };

            return Mock(inMemorySettings);
        }

        public static IConfiguration Mock(Dictionary<string, string?> mock)
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(mock)
                .Build();

            return _configuration;
        }
    }
}

using AutoMapper;
using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.MapperProfiles;
using BankingAppDataTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppDataTier.Tests.Mocks
{
    public class BakingAppDataTierApplicationContextMock : ApplicationContextMock
    {
        protected override Dictionary<string, string?> Configurations { get; set; } = new Dictionary<string, string?>
        {
            {$"{DatabaseConfigs.DatabaseSection}:{DatabaseConfigs.DatabaseConnection}", TestsConstants.ConnectionString},
            {$"{AuthenticationConfigs.AuthenticationSection}:{AuthenticationConfigs.Issuer}", TestsConstants.AuthenticationIssuer},
            {$"{AuthenticationConfigs.AuthenticationSection}:{AuthenticationConfigs.Audience}", TestsConstants.AuthenticationAudience},
            {$"{AuthenticationConfigs.AuthenticationSection}:{AuthenticationConfigs.Key}", TestsConstants.AuthenticationKey},
        };

        protected override List<Profile> MapperProfiles { get; set; } = new List<Profile>
        {
            new TokensMapperProfile(),
            new ClientsMapperProfile(),
            new AccountsMapperProfile(),
            new PlasticsMapperProfile(),
            new CardsMapperProfile(),
            new LoanOffersMapperProfile(),
            new LoansMapperProfile(),
            new TransactionsMapperProfile(),
        };
    }
}

using AutoMapper;
using BankingAppAuthenticationTier.Library.Configs;
using BankingAppAuthenticationTier.MapperProfiles;
using BankingAppAuthenticationTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppAuthenticationTier.Tests.Mocks
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
        };
    }
}

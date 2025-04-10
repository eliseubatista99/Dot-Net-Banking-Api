using AutoMapper;
using BankingAppBusinessTier.Library.Configs;
using BankingAppBusinessTier.Tests.Constants;
using ElideusDotNetFramework.Tests;

namespace BankingAppBusinessTier.Tests.Mocks
{
    public class BankingAppBusinessTierApplicationContextMock : ApplicationContextMock
    {
        protected override Dictionary<string, string?> Configurations { get; set; } = new Dictionary<string, string?>
        {
            {$"{AuthenticationTierConfigs.Section}:{AuthenticationTierConfigs.Url}", TestsConstants.AuthenticationTierUrl},
            {$"{DataTierConfigs.Section}:{DataTierConfigs.Url}", TestsConstants.AuthenticationTierUrl},  
        };

        protected override List<Profile> MapperProfiles { get; set; } = new List<Profile>
        {
            //new TokensMapperProfile(),
            //new ClientsMapperProfile(),
            //new AccountsMapperProfile(),
            //new PlasticsMapperProfile(),
            //new CardsMapperProfile(),
            //new LoanOffersMapperProfile(),
            //new LoansMapperProfile(),
            //new TransactionsMapperProfile(),
        };
    }
}

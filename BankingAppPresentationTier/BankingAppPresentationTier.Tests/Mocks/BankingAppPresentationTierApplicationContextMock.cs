using AutoMapper;
using BankingAppBusinessTier.Tests.Constants;
using BankingAppPresentationTier.Contracts.Configs;
using ElideusDotNetFramework.Tests.Mocks;

namespace BankingAppBusinessTier.Tests.Mocks
{
    public class BankingAppPresentationTierApplicationContextMock : ApplicationContextMock
    {
        protected override Dictionary<string, string?> Configurations { get; set; } = new Dictionary<string, string?>
        {
            {$"{BusinessTierConfigs.Section}:{BusinessTierConfigs.Url}", TestsConstants.BusinessTierUrl},
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

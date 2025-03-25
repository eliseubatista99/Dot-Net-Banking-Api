using AutoMapper;
using BankingAppBusinessTier.Contracts.Configs;
using BankingAppBusinessTier.Tests.Constants;
using ElideusDotNetFramework.Tests.Mocks;

namespace BankingAppBusinessTier.Tests.Mocks
{
    public class BankingAppBusinessTierApplicationContextMock : ApplicationContextMock
    {
        protected override Dictionary<string, string?> Configurations { get; set; } = new Dictionary<string, string?>
        {
            {$"{DataTierConfigs.Section}:{DataTierConfigs.Url}", TestsConstants.DataTierUrl},
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

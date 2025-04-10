using BankingAppBusinessTier.Library.Configs;
using BankingAppBusinessTier.Library.Providers;
using BankingAppDataTier.Contracts.Operations;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.ExternalServices;

namespace BankingAppBusinessTier.Providers
{
    public class DataTierProvider : ExternalServiceProvider, IDataTierProvider
    {
        protected IConfiguration configuration;

        public DataTierProvider(IApplicationContext _applicationContext) : base(_applicationContext)
        {
            this.configuration = applicationContext.GetDependency<IConfiguration>()!;
            httpClient = new HttpClient();
        }

        protected override string GetServiceUrl()
        {
            return configuration.GetSection(DataTierConfigs.Section).GetValue<string>(DataTierConfigs.Url)!;
        }

        public Task<GetPlasticsOfTypeOutput> GetPlasticsOfType(GetPlasticOfTypeInput input)
        {
            return CallExternalPostOperation<GetPlasticOfTypeInput, GetPlasticsOfTypeOutput>("GetPlasticsOfType", input);
        }
    }
}

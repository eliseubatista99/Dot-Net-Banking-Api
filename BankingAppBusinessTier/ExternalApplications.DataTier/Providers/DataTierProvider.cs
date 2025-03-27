using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Operations;
using ElideusDotNetFramework.Core;
using ExternalApplications.DataTier.Configs;
using ExternalApplications.DataTier.Modules;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApplications.DataTier.Providers
{
    public class DataTierProvider : ExternalApplicationProvider, IDataTierProvider
    {
        public DataTierProvider(IApplicationContext _applicationContext) : base(_applicationContext)
        {
            var configuration = this.applicationContext.GetDependency<IConfiguration>()!;
            externalServiceUrl = configuration.GetSection(DataTierConfigs.Section).GetValue<string>(DataTierConfigs.Url)!;

            httpClient = new HttpClient
            {
                BaseAddress = new Uri(externalServiceUrl),
            };
        }

        public Task<GetPlasticsOfTypeOutput> Authenticate(GetPlasticOfTypeInput input)
        {
            throw new NotImplementedException();
        }

        public Task<GetPlasticsOfTypeOutput> GetPlasticOfType(GetPlasticOfTypeInput input)
        {
            return CallExternalPostOperation<GetPlasticOfTypeInput, GetPlasticsOfTypeOutput>("GetPlasticsOfType", input);
        }
    }
}

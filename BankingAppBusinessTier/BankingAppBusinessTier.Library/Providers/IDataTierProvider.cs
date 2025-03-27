using BankingAppDataTier.Contracts.Operations;
using ElideusDotNetFramework.ExternalServices;

namespace BankingAppBusinessTier.Library.Providers
{
    public interface IDataTierProvider : IExternalServiceProvider
    {
        public Task<GetPlasticsOfTypeOutput> GetPlasticsOfType(GetPlasticOfTypeInput input);
    }
}

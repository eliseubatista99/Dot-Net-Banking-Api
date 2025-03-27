using BankingAppBusinessTier.Library.Providers;
using BankingAppDataTier.Contracts.Operations;

namespace BankingAppBusinessTier.Tests.Mocks.Providers
{
    public class DataTierProviderMock : IDataTierProvider
    {
        public async Task<GetPlasticsOfTypeOutput> GetPlasticsOfType(GetPlasticOfTypeInput input)
        {
            await Task.Delay(1);

            return new GetPlasticsOfTypeOutput
            {
                Plastics = new List<BankingAppDataTier.Contracts.Dtos.PlasticDto>()
            };
        }
    }
}

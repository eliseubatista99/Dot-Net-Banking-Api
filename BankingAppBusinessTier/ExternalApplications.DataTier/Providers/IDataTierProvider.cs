using BankingAppDataTier.Contracts.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApplications.DataTier.Providers
{
    public interface IDataTierProvider
    {
        public Task<GetPlasticsOfTypeOutput> Authenticate(GetPlasticOfTypeInput input);

        public Task<GetPlasticsOfTypeOutput> GetPlasticOfType(GetPlasticOfTypeInput input);
    }
}

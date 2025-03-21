using BankingAppDataTier.Contracts.Dtos.Entities;
using System.Net;

namespace BankingAppDataTier.Contracts.Dtos.Outputs
{
    public class _BaseOutput
    {
        public HttpStatusCode? StatusCode { get; set; }

        public BankingAppDataTierError? Error { get; set; }
    }
}

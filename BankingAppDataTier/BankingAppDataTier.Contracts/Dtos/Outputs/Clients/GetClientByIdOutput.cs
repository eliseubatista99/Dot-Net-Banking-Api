using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Clients
{
    public class GetClientByIdOutput : OperationOutput
    {
        public required ClientDto? Client { get; set; }
    }
}

using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Clients
{
    public class GetClientByIdOutput : BaseOutput
    {
        public required ClientDto? Client { get; set; }
    }
}

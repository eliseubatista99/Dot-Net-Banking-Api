using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Clients
{
    public class GetClientsOutput : _BaseOutput
    {
        public required List<ClientDto> Clients { get; set; }
    }
}

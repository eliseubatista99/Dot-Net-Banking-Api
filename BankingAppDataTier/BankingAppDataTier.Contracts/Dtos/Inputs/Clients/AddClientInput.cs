using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Clients
{
    public class AddClientInput : _BaseInput
    {
        public required ClientDto Client { get; set; }

        public required string PassWord { get; set; }
    }
}

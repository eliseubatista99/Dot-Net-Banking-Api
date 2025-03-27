using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Clients
{
    public class AddClientInput : OperationInput
    {
        public required ClientDto Client { get; set; }

        public required string PassWord { get; set; }
    }
}

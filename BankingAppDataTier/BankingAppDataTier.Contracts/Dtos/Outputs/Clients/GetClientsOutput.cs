using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Clients
{
    [ExcludeFromCodeCoverage]

    public class GetClientsOutput : OperationOutput
    {
        public required List<ClientDto> Clients { get; set; }
    }
}

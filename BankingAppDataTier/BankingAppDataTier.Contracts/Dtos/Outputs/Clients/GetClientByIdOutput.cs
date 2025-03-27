using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Clients
{
    [ExcludeFromCodeCoverage]

    public class GetClientByIdOutput : OperationOutput
    {
        public required ClientDto? Client { get; set; }
    }
}

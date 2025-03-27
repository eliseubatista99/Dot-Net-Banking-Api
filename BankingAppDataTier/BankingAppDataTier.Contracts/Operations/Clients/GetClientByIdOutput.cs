using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Clients
{
    [ExcludeFromCodeCoverage]

    public class GetClientByIdOutput : OperationOutput
    {
        public required ClientDto? Client { get; set; }
    }
}

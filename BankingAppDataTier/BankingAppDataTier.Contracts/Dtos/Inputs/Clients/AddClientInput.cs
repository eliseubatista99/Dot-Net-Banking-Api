using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Clients
{
    [ExcludeFromCodeCoverage]

    public class AddClientInput : OperationInput
    {
        public required ClientDto Client { get; set; }

        public required string PassWord { get; set; }
    }
}

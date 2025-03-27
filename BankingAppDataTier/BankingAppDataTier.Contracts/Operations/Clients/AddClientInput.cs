using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class AddClientInput : OperationInput
    {
        public required ClientDto Client { get; set; }

        public required string PassWord { get; set; }
    }
}

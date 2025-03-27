using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    [ExcludeFromCodeCoverage]

    public class GetAuthenticationPositionsInput : OperationInput
    {
        public required string ClientId { get; set; }

        public int? NumberOfPositions { get; set; }

    }
}

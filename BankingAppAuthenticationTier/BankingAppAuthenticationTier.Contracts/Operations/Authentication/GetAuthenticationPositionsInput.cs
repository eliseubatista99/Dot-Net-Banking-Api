using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class GetAuthenticationPositionsInput : OperationInput
    {
        public required string ClientId { get; set; }

        public int? NumberOfPositions { get; set; }

    }
}

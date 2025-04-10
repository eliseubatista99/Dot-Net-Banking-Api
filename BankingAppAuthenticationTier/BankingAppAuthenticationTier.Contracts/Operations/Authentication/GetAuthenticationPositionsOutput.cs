using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class GetAuthenticationPositionsOutput : OperationOutput
    {
        public required List<int> Positions { get; set; }
    }
}

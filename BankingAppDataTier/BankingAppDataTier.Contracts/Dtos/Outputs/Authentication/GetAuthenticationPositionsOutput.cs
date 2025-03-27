using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    [ExcludeFromCodeCoverage]

    public class GetAuthenticationPositionsOutput : OperationOutput
    {
        public required List<int> Positions { get; set; }
    }
}

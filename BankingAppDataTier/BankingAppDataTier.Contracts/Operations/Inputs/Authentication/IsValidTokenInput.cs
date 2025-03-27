using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Inputs.Authentication
{
    [ExcludeFromCodeCoverage]

    public class IsValidTokenInput : OperationInput
    {
        public required string Token { get; set; }

    }
}

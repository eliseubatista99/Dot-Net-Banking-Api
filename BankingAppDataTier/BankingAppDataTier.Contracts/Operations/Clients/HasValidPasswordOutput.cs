using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Clients
{
    [ExcludeFromCodeCoverage]

    public class HasValidPasswordOutput : OperationOutput
    {
        public required bool Result { get; set; }
    }
}

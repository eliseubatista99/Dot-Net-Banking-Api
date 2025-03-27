using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Clients
{
    public class HasValidPasswordOutput : OperationOutput
    {
        public required bool Result { get; set; }
    }
}
